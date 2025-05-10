using NftCatcherApi.Enums;
using Stateless;

namespace NftCatcherApi.StateMachines;

public class BotStateMachine 
{
    private readonly StateMachine<MainState, Trigger> _machine;

    public BotStateMachine(MainState state)
    {
        _machine = new StateMachine<MainState, Trigger>(state);

        _machine.Configure(MainState.MainMenu)
            .Permit(Trigger.GoToNigger, MainState.Nigger)
            .Ignore(Trigger.Cancel);

        _machine.Configure(MainState.Nigger)
            .Permit(Trigger.Cancel, MainState.Nigger)
            .Ignore(Trigger.GoToNigger);
    }

    public async ValueTask Fire(Trigger trigger) => await _machine.FireAsync(trigger);

    public MainState GetCurrentState() => _machine.State;
}
