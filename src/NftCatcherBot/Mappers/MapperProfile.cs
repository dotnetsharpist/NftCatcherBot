using AutoMapper;
using NftCatcherApi.Dtos;
using NftCatcherApi.Entities;
using Telegram.Bot.Types;

namespace NftCatcherApi.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Bot user
        CreateMap<BotUser, BotUserCreationDto>().ReverseMap();
        CreateMap<BotUser, BotUserResultDto>().ReverseMap();
        CreateMap<User, BotUserCreationDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
        
    }
}