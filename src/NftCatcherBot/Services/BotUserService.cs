using AutoMapper;
using NftCatcherApi.Dtos;
using NftCatcherApi.Entities;
using NftCatcherApi.Repositories;

namespace NftCatcherApi.Services;

public class BotUserService(IRepository<BotUser> repository, IMapper mapper)
{
    public async ValueTask<BotUserResultDto> GetBotUserByUserId(long userId)
    {
        var botUser = await repository.SelectAsync(bu => bu.UserId == userId);
        return mapper.Map<BotUserResultDto>(botUser);
    }

    /*
    public async ValueTask<PagedResultDto<BotUserResultDto>> GetBotUsers(PaginationParams @params)
    {
        var query = repository.SelectAll()
            .Where(bt => !bt.IsDeleted);

        var totalCount = await query.CountAsync();

        var botUsers = await query
            .ToPagedList(@params)
            .ToListAsync();

        return new PagedResultDto<BotUserResultDto>()
        {
            TotalCount = totalCount,
            Items = mapper.Map<List<BotUserResultDto>>(botUsers)
        };
    }
    */
    
    public async ValueTask AddBotUser(BotUserCreationDto dto)
    {
        var exists = await repository
            .SelectAsync(bu => bu.UserId == dto.UserId);

        if (exists is not null)
            return;
        
        var mappedBotUser = mapper.Map<BotUser>(dto);
        await repository.InsertAsync(mappedBotUser);
        await repository.SaveAsync();
    }

}