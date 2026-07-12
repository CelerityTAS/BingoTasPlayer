using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS;
using TAS.Input;

namespace Celeste.Mod.BingoTasPlayer {
    internal class TASRouter : IBingoRouter {
        public TASRouter() {
            //TASFileInfo start = new TASFileInfo("../GMBingoPlayer/start.tas", "start", "", "");
            TASFileInfo enter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterWithPostcard.tas", "EnterWithPostcard", "", "");
            TASFileInfo leave = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveChapter.tas", "LeaveChapter", "", "");
            route = [enter, new TASObjectiveInfo("1A-start")];
            foreach (var item in route) Logger.Warn("bingotasAi", item.ToString());
        }
        private static List<TASFileInfo> route = new();
        private static RouteChange previous;
        private static bool changedRoute = false;
        public RouteChange? OnTasCompleted() {
            //Testing
            if (changedRoute) return null;


            if (route.Count == 0) return null;
            previous = new(route[0], null);
            route.RemoveAt(0);

            TickAttempt[] arr = new TickAttempt[2];
            arr[0] = new TickAttempt(600, new Objective("name", 2));
            arr[1] = new TickAttempt(300, new Objective("name2", 4));
            if (route.Count == 0) return new RouteChange(previous.FilePath, arr);
            return previous;
        }

        public RouteChange? OnTickevent(Objective[] objectives) {
            return null;
            previous.FilePath.endlabel = NextComment();
            return previous;
        }

        public void SetBoard(Objective[] objectives) {

        }

        private string NextComment() {
            try {
                KeyValuePair<int, List<Comment>> p = Manager.Controller.Comments.First(p => {
                    return p.Key > Manager.Controller.CurrentFrameInTas && p.Value.Any(com => com.Text.StartsWith("lvl_") && Path.GetFullPath(com.FilePath).SequenceEqual(Path.GetFullPath("./GMBingoPlayer/" + previous.FilePath.path)));
                });
                return p.Value.Last().Text;
            } catch {
                Logger.Info("bingoAi", "No label was found to nicely end the tas on, playing to end");
                return previous.FilePath.endlabel;
            }

        }

        public RouteChange? TickAttemptResult(Objective[] objectives, bool[] results) {
            changedRoute = true;
            previous.FilePath.endlabel = NextComment();
            return previous;
        }
    }
}
