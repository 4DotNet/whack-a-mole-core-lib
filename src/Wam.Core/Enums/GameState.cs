using System.Diagnostics;

namespace Wam.Core.Enums;

public abstract class GameState
{

    public static readonly GameState Init = new GameStateInit();
    public static readonly GameState Current = new GameStateCurrent();
    public static readonly GameState Started = new GameStateStarted();
    public static readonly GameState Finished = new GameStateFinished();
    public static readonly GameState Cancelled = new GameStateCancelled();
    public static readonly GameState[] All = { Init, Current, Started, Finished, Cancelled };

    public static GameState FromCode(string code)
    {
        var state = Array.Find(All, s => s.Code == code);
        if (state == null)
        {
            throw new InvalidOperationException($"Invalid game state code {code}");
        }
        return state;
    }

    public abstract string Code { get; }
    public virtual string TranslationKey => $"Game.States.{Code}";

    public virtual bool CanChangeTo(GameState state) => false;

}

public class GameStateInit : GameState
{
    public override string Code => "Init";
    public override bool CanChangeTo(GameState state)
    {
        return state == Current || state == Cancelled;
    }
}

public class GameStateCurrent : GameState
{
    public override string Code => "Current";

    public override bool CanChangeTo(GameState state)
    {
        return state == Started || state == Cancelled;
    }
}

public class GameStateStarted : GameState
{
    public override string Code => "Started";

    public override bool CanChangeTo(GameState state)
    {
        return state == Finished || state == Cancelled;
    }
}

public class GameStateFinished : GameState
{
    public override string Code => "Finished";
}

public class GameStateCancelled : GameState
{
    public override string Code => "Cancelled";
}