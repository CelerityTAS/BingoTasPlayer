using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.BingoTasPlayer {
    internal class RouteAction {
        public RouteAction(TASFileInfo fileinfo, TickAttempt[]? tickAttempts) {
            Action = () => new TASFileInfo[] { fileinfo };
            TickAttempts = () => tickAttempts;
        }
        public RouteAction(Func<TASFileInfo[]> action, Func<TickAttempt[]> ticks) {
            Action = action;
            TickAttempts = ticks ?? (() => null);
        }
        public RouteChange[] Get() {
            TASFileInfo[] fi = Action.Invoke();
            if (fi == null) { return null; };
            RouteChange[] ra = fi.Select((f) => new RouteChange(f, null)).ToArray();
            ra[0].TickAttempts = tickAttemts;
            return ra;
        }
        public TickAttempt[] InitTickAttempts() {
            tickAttemts = TickAttempts.Invoke();
            return tickAttemts;
        }
        public Func<TickAttempt[]> TickAttempts;
        private TickAttempt[] tickAttemts = null;
        public Func<TASFileInfo[]> Action;

    }
}
