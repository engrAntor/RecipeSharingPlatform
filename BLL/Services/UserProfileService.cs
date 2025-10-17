using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;

namespace BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IMapper _mapper;

        public UserProfileService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public UserProfileDTO GetProfile(int userId)
        {
            var user = DataAccessFactory.GetUserRepo().GetById(userId);
            return _mapper.Map<UserProfileDTO>(user);
        }

        
        public bool UpdateProfile(int userId, UserProfileUpdateDTO profileData)
        {
            
            var userRepo = DataAccessFactory.GetUserRepo();

            
            
            var user = userRepo.GetById(userId);
            if (user == null) return false;

            
            user.Name = profileData.Name;

            
            
            userRepo.Update(user);

            return true;
        }
    }
}