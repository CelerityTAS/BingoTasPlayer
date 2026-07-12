namespace Celeste.Mod.BingoTasPlayer;

public struct RouteChange(TASFileInfo fileinfo, TickAttempt[]? tickAttempts)
{
    public TASFileInfo FilePath = fileinfo;
    public TickAttempt[]? TickAttempts = tickAttempts;
    
}
public struct TickAttempt(int delay, Objective objective)
{
    public int Delay = delay;
    public Objective Objective = objective;
}
public struct Objective(string name, int index)
{
    public string name = name;
    public int index = index;
}


public interface IBingoRouter
{
    ///<summary>
    ///Will return null if no route change is to take place, will return the previous route with a different endlabel.
    ///</summary>
    public RouteChange? OnTickevent(Objective[] objectives);
    public RouteChange? TickAttemptResult(Objective[] objectives, bool[] results);
    ///<summary>
    ///Will only return null when the tas ends
    ///</summary>
    public RouteChange? OnTasCompleted();
    public void SetBoard(Objective[] objectives);
}