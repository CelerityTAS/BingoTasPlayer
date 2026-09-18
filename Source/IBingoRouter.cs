using System.Collections.Immutable;
using System.Linq;

namespace Celeste.Mod.BingoTasPlayer;

public struct RouteChange(TASFileInfo fileinfo, TickAttempt[]? tickAttempts)
{
    public TASFileInfo FilePath = fileinfo;
    public TickAttempt[]? TickAttempts = tickAttempts;
    public override string ToString() {
        return "Path: "+FilePath + " | " + "TickAttempts ("+TickAttempts?.Count()+"): " + ((TickAttempts?.Count() > 0)?TickAttempts[0].ToString():"");
    }
}
public struct TickAttempt(int delay, Objective objective)
{
    public int Delay = delay;
    public Objective Objective = objective;
    public override string ToString() {
        return "after " + Delay + ": " + Objective.ToString();
    }
}
public struct Objective(string name, int index)
{
    public string name = name;
    public int index = index;
    public override string ToString() {
        return "Objective "+name+" : "+index;
    }
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
    public TickAttempt[] GetTickAttempts();
    public void SetBoard(Objective[] objectives);
    public Objective[] getBoard();
}