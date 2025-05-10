using NftCatcherApi.Enums;
using NftCatcherApi.Infrastructure;
using NftCatcherApi.StateMachines;

namespace NftCatcherApi.Services;

public class StateService(RedisService redis)
{
    public async ValueTask<BotStateMachine> GetUserStateMachine(long userId)
    {
        var stateStr = await redis.GetValueAsync<string>($"user_state:{userId}");
        var lastState = stateStr == null ? MainState.MainMenu : Enum.Parse<MainState>(stateStr);
        return new BotStateMachine(lastState); 
    }
    
    public async ValueTask<MainState> GetUserState(long userId)
    {
        var stateStr = await redis.GetValueAsync<string>($"user_state:{userId}");
        return stateStr == null ? MainState.MainMenu : Enum.Parse<MainState>(stateStr);
    }

    public async ValueTask SetUserState(long userId, BotStateMachine stateMachine)
    {
        await redis.SetValueAsync($"user_state:{userId}", stateMachine.GetCurrentState().ToString());
    } 
}