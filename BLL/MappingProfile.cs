// ...
using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserProfileDTO>();

        
        
        CreateMap<Recipe, RecipeDTO>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

        
        CreateMap<CreateRecipeDTO, Recipe>();
        CreateMap<UpdateRecipeDTO, Recipe>();
        
        CreateMap<Rating, RatingDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
        CreateMap<CreateRatingDTO, Rating>();
    }
}