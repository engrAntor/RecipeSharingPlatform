using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class FeedService : IFeedService
    {
        private readonly IMapper _mapper;

        public FeedService()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        public List<RecipeDTO> GetUserFeed(int userId)
        {
            
            var followingIds = DataAccessFactory.GetUserFollowRepo().GetAll()
                                              .Where(f => f.FollowerId == userId)
                                              .Select(f => f.FollowingId)
                                              .ToList();

            if (!followingIds.Any())
            {
                
                return new List<RecipeDTO>();
            }

            
            var feedRecipes = DataAccessFactory.GetRecipeRepo().GetAll()
                                               .Where(r => followingIds.Contains(r.AuthorId))
                                               .OrderByDescending(r => r.CreatedAt) 
                                               .ToList();

            
            return _mapper.Map<List<RecipeDTO>>(feedRecipes);
        }
    }
}