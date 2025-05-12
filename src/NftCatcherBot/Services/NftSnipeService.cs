using AutoMapper;
using NftCatcherApi.Dtos;
using NftCatcherApi.Entities;
using NftCatcherApi.Repositories;

namespace NftCatcherApi.Services;

public class NftSnipeService(IRepository<NftSnipe> repository, IMapper mapper)
{
    public async ValueTask<bool> AddAsync(NftSnipeCreationDto dto)
    {
        var existingSniper = await repository.SelectAsync(ns => ns.BotUserId == dto.BotUserId);
        if (existingSniper is not null)
            return false;

        var mappedNftSnipe = mapper.Map<NftSnipe>(dto);
        await repository.InsertAsync(mappedNftSnipe);
        return await repository.SaveAsync();
    }
}