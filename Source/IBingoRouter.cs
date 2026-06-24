namespace Celeste.Mod.BingoTasPlayer;

public struct RouteChange(string? filePath, TickAttempt[]? tickAttempts)
{
    public string? FilePath = filePath;
    public TickAttempt[]? TickAttempts = tickAttempts;
    
}
public struct TickAttempt(int delay, Objective objective)
{
    public int Delay = delay;
    public Objective Objective = objective;
}
public struct Objective(string name)
{
    public string name = name;
}


public interface IBingoRouter
{
    public RouteChange? OnTickevent(Objective[] objectives);
    public RouteChange? TickAttemptResult(Objective[] objectives, bool[] results);
    public RouteChange OnTasCompleted();
    public void SetBoard(Objective[] objectives);
}