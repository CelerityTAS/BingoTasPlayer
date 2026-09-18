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
            /*
             * These files are good to use as shortcuts for menuing
             * */
            /*
             * These enter files go from the chapter select screen to gameplay inside the checkpoints.
             * - enter just enters the A-Side for the first time (so no checkpoints or B-Sides have been unlocked yet)
             * - entercp1 enters the first checkpoint of a level (N means no Postcard, so the A-Side has already been beaten before, P for Postcard, if you have not completed the A-Side yet)
             * - entercp2 etc. just enter the secondm, third and so on checkpoint.
             * - entersummit enters summit for the first time
             * - entersummitcp1 enters 0m (so the level has been played before)
             * - enterfarewell enters farewell for the first time                                   (importantly, this gets DTS)
             * - enterfarewellCP1 enters start of farewell if the chapter has already been played   (importantly, this gets DTS)
             * - entercore
             * - entercorecp1
             * - enterB enters the B-side if it has not yet been played
             * - enterBcp1 enters the first checkpoint of the B-Side (you cannot enter other checkpoints, gotta commit to B-Sides)
             * */
            TASFileInfo enter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterWithPostcard.tas", "EnterWithPostcard", "", "");
            TASFileInfo entercp1N = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1NoPostCard.tas", "EnterCP1N", "", "");
            TASFileInfo entercp1P = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1WithPostCard.tas", "EnterCP1P", "", "");
            TASFileInfo entercp2 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP2.tas", "EnterCP2", "", "");
            TASFileInfo entercp3 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP3.tas", "EnterCP3", "", "");
            TASFileInfo entercp4 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP4.tas", "EnterCP4", "", "");
            TASFileInfo entercp5 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP5.tas", "EnterCP5", "", "");
            TASFileInfo entercp6 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP6.tas", "EnterCP6", "", "");
            TASFileInfo entersummit = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummit.tas", "EnterSummit", "", "");
            TASFileInfo entersummitcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummitCP1.tas", "EnterSummitCP1", "", "");
            TASFileInfo enterfarewell = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewell.tas", "EnterFarewell", "", "");
            TASFileInfo enterfarewellcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewellCP1.tas", "EnterFarewellCP1", "", "");
            TASFileInfo entercore = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCore.tas", "EnterCore", "", "");
            TASFileInfo entercorecp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCoreCP1.tas", "EnterCoreCP1", "", "");
            TASFileInfo enterB = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSide.tas", "EnterBSide", "", "");
            TASFileInfo enterBcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSideCP1.tas", "EnterBSideCP1", "", "");

            /*
             * load a from b is played after a B-side is completed and enters the next A-side automatically. There is currently no leaveB, but it has also not yet been needed.
             * If you really wnat a leaveB, it would probably be rtmmenu, although you would need to test that.
             * */
            TASFileInfo loadafromb = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "CelesteTAS/LoadAFromB.tas", "LoadAFromB", "SkipExit", "");

            /*
             * These files leave a chapter and place the game on the Chapter Select screen
             * - leave is used when a chapter was completed for the first time. (There might be bugs, if no deaths occur and no collectibles are gotten. But surely this will never happen in Bingo :D)
             *   importantly it places the game on the next chapter, not the one that was just completed.
             * - leaveagain is used when finishing a chapter, that was already completed before, it will place the game on the chapter that was just completed, not the next.
             * - leavepico leaves pico and places the game in the pico 8 room, you will need to play rtm after this to get to chapter-select.
             * - rtm returns to map after a frame of gameplay (this is important for unlocking checkpoints)
             * - rtmwakeup returns to map after a wakeup animation, like 2A (does not unlock awake), 5A and 5B
             * - rtmCassette returns to map after the proper time has passed to collect a cassette.
             * - rtmmenu is used when the file already has an RTM build in, like all fromX files and 8A-heart files (as well as all B-sides, but we have loadafromb)
             * */
            TASFileInfo leave = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveChapter.tas", "LeaveChapter", "", "");
            TASFileInfo leaveagain = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveAgain.tas", "LeaveAgain", "", "");
            TASFileInfo leavepico = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeavePico.tas", "LeavePico", "0", "Exit");
            TASFileInfo rtm = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "WalkinCheckpoint", "");
            TASFileInfo rtmwakeup = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Wakeup", "");
            TASFileInfo rtmCassette = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Cassette", "");
            TASFileInfo rtmmenu = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Menu", "");
            TASFileInfo rtmsummitnocollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitNoCollect", "");
            TASFileInfo rtmsummitcollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitCollect", "");

            /*
             * These are for navigating the Chapter select Screen
             * - Left and Right go left a chapter and right a chapter
             * - skipchapter skips the current chapter, so if the game is on 4A, 5A will be unlocked using the assist skip. It will then end on that chapter, not entering it.
             *   Use one of the enter-files for that
             */
            TASFileInfo Left = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Left.tas", "Left", "", "");
            TASFileInfo Right = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Right.tas", "Right", "", "");
            TASFileInfo skipchapter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "SkipChapter.tas", "SkipChapter", "", "");

            /*
             * restarts the chapter. beware to do this before the game rtms, 
             * new (new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null), // tick 13
               new (restartchapter,null)
             * is the correct way to restart after getting 2A-blue, not "new (new TASObjectiveInfo("2A-fromstart-heart"),null)"
             * */
            TASFileInfo restartchapter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RestartChapter.tas", "RestartChapter", "", "");


            /*
             * The elements of the list contain two things, info about the file and info about what to tick.
             * First the file:
             * 
             * The easiest way to add objectives is using 
             * new TASObjectiveInfo({name of file in objective repository})
             * or 
             * new TASObjectiveInfo({name of file in objective repository}, {startlabel}, {endlabel})
             * 
             * startlabel is usually:
             * Start, when entering from the previous checkpoint, which is default in TASObjectiveInfo's constructor
             * RTM, when entering not the first checkpoint from the chapter select screen (needs to be added to the constructor (don't forget, lol))
             * DTS for all farewell files that have begun at the start checkpoint (and therefor got DTS)
             * FromGrabless is used in 2A-awake files when intervention was played grabless, since otherwise the objective is not valid.
             * 
             * endlabels: always play till the last thing you want to get in a checkpoint. you might have to check what that is.
             * Heart, you can RTM immediately
             * Cassette, you can rtmcassette
             * Winged, Seeded you can RTM immediately if you want nothing more
             * some files have ARB or Collect labels, but some might be buggy.
             * 
             * ------
             * Farewell powersource and remembered are currently kinda bugged with ending labels.
             * I will need to fix them in the future. just let them play till the end of ps or remembered.
             * ---------
             * 
             * 0mARBCollect, 5000mARBCollect, etc. are labels in 7A files that are used to collect the last berry of a previous checkpoint (usually as end-labels)
             * 
             * 4A cliffface also sometimes has the skipfirst as an option which allows skipping the first berry.
             * 
             * The second thing is info about ticks.
             * This is an array of so called tickattempts, which have a constructor that needs:
             * - the time in frames the objective is in the file
             * - info about the objective, which needs the name of the objective (irrelevant) and the index of the objective (0-indexed)
             * */
            previous = new RouteChange(new(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "start.tas", "start", "0"), null);


            route = [];
            // to see old routes (They will need to be fixed probably. ADD A FRAME OF RIGHT BEFORE SKIPCHAPTER)
            //Add(TASRoutes.routethriteen);


            /*
             * Weekly Blackout t16
                159745 
                Gen: Solo Blackout
                Progression: Tournament Standard
                https://celestebingo.rhelmot.io/room/VkgWE7reQ8e6S5-V3Xia0g?password=gg

                1B or Farewell binos?
             * */https://celestebingo.rhelmot.io/room/wJt_qo8ZRTO51BS4WkQivA?password=s
                       // Seed: 306187 | Varient: Lockout | Optimized: Lockout
            Add(enterChapter(Chapter.C1A, 0));
            Add("1A-start-progress4-winged");
            Add("1A-crossing-progress2");
            Add("1A-chasm-collect", GetAfterLabel(new Dictionary<int, string> { { 14, "1UP" } }));
            Add(enterChapter(Chapter.C2A, 0));
            Add("2A-fromstart-heart", "Start", "Heart", GetAfterCompletion(new int[] { 23 }));
            Add(restartchapter);
            Add("2A-start-cassette-seeded", "Start", "Seeded");
            Add(enterChapter(Chapter.C1A, 3));
            Add("1A-chasm-cassette", "RTM", "Cassette", GetAfterCompletion(new int[] { 0 }));
            Add(enterChapter(Chapter.C2B, 0));
            Add("2B");
            Add(enterChapter(Chapter.C4A, 0));
            Add("4A-start-cassette-seeded");
            Add("4A-shrine-heart", GetAfterLabel(new Dictionary<int, string> { { 13, "Heart" } }));
            Add("4A-oldtrail");
            Add("4A-cliffface");
            Add(enterChapter(Chapter.C5A, 0));
            Add("5A-start-1up");
            Add("5A-depths-cassette-arb-winged-seeded", "Start", "ARB", () => GetAfterCompletion(new int[] { 5 })().Concat(GetAfterLabel(new Dictionary<int, string> { { 4, "Seeded" } })()).ToArray());
            Add(enterChapter(Chapter.C5B, 0));
            Add("5B-seekerkill", () => GetAfterCompletion(new int[] { 19 })().Concat(GetAfterLabel(new Dictionary<int, string> { { 21, "Seeker" } })()).ToArray());
            Add(enterChapter(Chapter.C7A, 0));
            Add("7A-0m-gem-fast3");
            Add("7A-500m-gem-arb-winged");
            Add("7A-1000m-gem-arb", GetAfterLabel(new Dictionary<int, string> { { 24, "Gem" } }));
            Add("7A-1500m", "Start", "1000mARBCollect", GetAfterCompletion(new int[] { 20 }));
            Add(enterChapter(Chapter.C3A, 0));
            Add("3A-start-arb-winged", () => GetAfterCompletion(new int[] { 10 })().Concat(GetAfterLabel(new Dictionary<int, string> { { 12, "ARB" } })()).ToArray());
            Add("3A-hugemess-btc-heart-fast5-winged", () => GetAfterCompletion(new int[] { 22, 8 })().Concat(GetAfterLabel(new Dictionary<int, string> { { 6, "Heart" } })()).ToArray());
            Add("3A-shaft-cassette", "Start", "Cassette");
            Add(enterChapter(Chapter.C3B, 0));
            Add("3B", GetAfterCompletion(new int[] { 9 }));
            Add(enterChapter(Chapter.C5A, 2));
            Add("5A-depths-grabless", "RTM", GetAfterCompletion(new int[] { 3 }));
            Add("5A-unraveling");
            Add("5A-search-keys", "Start", "Key3", GetAfterCompletion(new int[] { 17 }));
            Add(enterChapter(Chapter.C8A, 0));
            Add("8A-start-intothecore");
            Add("8A-hotandcold");
            Add("8A-heartofthemountain-heart", GetAfterCompletion(new int[] { 18, 15 }));
            Add(enterChapter(Chapter.C4B, 0));
            Add("4B");
            Add(enterChapter(Chapter.C1B, 0));
            Add("1B", GetAfterCompletion(new int[] { 2 }));
            Add(enterChapter(Chapter.C9, 0));
            Add("9A-start-singular");
            Add("9A-powersource-keys", "DTS", "Key3DTS", GetAfterCompletion(new int[] { 1 }));
            Add(enterChapter(Chapter.C7A, 4));
            Add("7A-1500m", "RTM");
            Add("7A-2000m-gem");
            Add("7A-2500m-gem", "Start", "Gem", GetAfterCompletion(new int[] { 11 }));
            Add(enterChapter(Chapter.C1A, 2));
            Add("1A-crossing-dashless", "RTM", GetAfterCompletion(new int[] { 7 }));
            Add(enterChapter(Chapter.C3A, 2));
            Add("3A-fromhugemess-enterpico", "RTM");
            Add("pico-berries", "Start", "5Berries");
            Add(leavepico, new TickAttempt[] { new TickAttempt(0,new Objective("Obj 21",21)) });
            Add(rtm);


        }
        private static Func<TickAttempt[]> GetAfterCompletion(int[] indices) {
            return () => {
                int delay = Manager.Controller.Inputs.Count - Manager.Controller.CurrentFrameInTas - 1;
                TickAttempt[] ticks = indices.Select((i) => new TickAttempt(delay, getObjective(BingoTasPlayerModule.router, i))).ToArray();
                return ticks;
            };
        }
        private static Func<TickAttempt[]> GetAfterLabel(Dictionary<int, string> indexafterlabel) {
            return () => {
                List<Tuple<int, int>> delays = indexafterlabel.Select((v) => {
                    try {
                        KeyValuePair<int, List<Comment>> p = Manager.Controller.Comments.First(p => {
                            return p.Key > Manager.Controller.CurrentFrameInTas && p.Value.Any(com => com.Text == v.Value && Path.GetFullPath(com.FilePath).SequenceEqual(Path.GetFullPath("./GMBingoPlayer/" + previous.FilePath.path)));
                        });
                        return new Tuple<int, int>(v.Key, p.Value.Last().Frame - BingoTasPlayerModule.lastTASOffset);
                    } catch {
                        return new Tuple<int, int>(v.Key, Manager.Controller.Inputs.Count - Manager.Controller.CurrentFrameInTas - 1);
                    }
                }).ToList();
                TickAttempt[] ticks = delays.Select(v => new TickAttempt(v.Item2, getObjective(BingoTasPlayerModule.router, v.Item1))).ToArray();
                return ticks;
            };
        }

        private static void Add(TASFileInfo info, TickAttempt[] ticks = null) {
            route.Add(new(info, ticks));
        }
        private static void Add(TASFileInfo info, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { info }, ticks));
        }
        private static void Add(string name, TickAttempt[] ticks = null) {
            route.Add(new(new TASObjectiveInfo(name), ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name));
        }
        private static void Add(string name, string start, TickAttempt[] ticks = null) {
            route.Add(new(new TASObjectiveInfo(name, start), ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start));
        }
        private static void Add(string name, string start, string end, TickAttempt[] ticks = null) {
            route.Add(new(new TASObjectiveInfo(name, start, end), ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start, end));
        }
        private static void Add(string name, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name) }, ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name));
        }
        private static void Add(string name, string start, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start) }, ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start));
        }
        private static void Add(string name, string start, string end, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start, end) }, ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start, end));
        }
        private static void Add(Func<TASFileInfo[]> file, TickAttempt[] ticks = null) {
            route.Add(new(file, () => ticks));
            //Does not need Validation
        }
        private static void Add(Func<TASFileInfo[]> file, Func<TickAttempt[]> ticks) {
            route.Add(new(file, ticks));
            //Does not need Validation
        }
        private static void Add(RouteChange[] oldroute) {
            foreach (RouteChange change in oldroute) {
                Add(change.FilePath, change.TickAttempts);
                TASFileInfo.Validate(change.FilePath);
            }
        }

        private static List<RouteAction> route = new();
        private static RouteChange previous;
        private static RouteAction previosAction;
        private static bool changedRoute = false;
        public RouteChange? OnTasCompleted() {
            //Testing
            if (changedRoute) {
                changedRoute = false;
            }


            if (route.Count == 0) return null;
            RouteChange[] actions = route[0].Get();
            if (actions.Length > 1) {
                route.InsertRange(1, actions.Select(v => new RouteAction(v.FilePath, v.TickAttempts)));
                route.RemoveAt(0);
            }
            previous = route[0].Get()[0];
            previosAction = route[0];
            route.RemoveAt(0);
            Logger.Warn("bingoai", "Playing " + previous.ToString());
            return previous;
        }
        public TickAttempt[] GetTickAttempts() {
            return previosAction.InitTickAttempts();
        }

        public RouteChange? OnTickevent(Objective[] objectives) {
            return null;
            changedRoute = true;
            previous.FilePath.endlabel = "" + 0;
            return previous;
        }
        public Objective[] board;
        public Objective[] getBoard() { return board; }

        public static Objective getObjective(IBingoRouter router, int i) {
            Objective[] board = router.getBoard();
            if (board == null) {
                return new Objective("Objective " + i, i);
            } else {
                return board[i];
            }

        }
        public void SetBoard(Objective[] objectives) {
            board = objectives;
        }

        public RouteChange? TickAttemptResult(Objective[] objectives, bool[] results) {
            return null;
            changedRoute = true;
            previous.FilePath.endlabel = NextComment();
            return previous;
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
        public enum Chapter { P, C1A, C1B, C2A, C2B, C3A, C3B, C4A, C4B, C5A, C5B, C6A, C6B, C7A, C7B, C8A, E, C9 }
        private Func<TASFileInfo[]> enterChapter(Chapter chapter, int Cp = 0) {
            return new Func<TASFileInfo[]>(() => {
                #region Files
                TASFileInfo leave = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveChapter.tas", "LeaveChapter", "", "");
                TASFileInfo leaveagain = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveAgain.tas", "LeaveAgain", "", "");
                TASFileInfo leavepico = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeavePico.tas", "LeavePico", "0", "Exit");
                TASFileInfo rtm = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "WalkinCheckpoint", "");
                TASFileInfo rtmwakeup = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Wakeup", "");
                TASFileInfo rtmCassette = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Cassette", "");
                TASFileInfo rtmmenu = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Menu", "");
                TASFileInfo rtmsummitnocollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitNoCollect", "");
                TASFileInfo rtmsummitcollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitCollect", "");
                TASFileInfo Left = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Left.tas", "Left", "", "");
                TASFileInfo Right = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Right.tas", "Right", "", "");
                TASFileInfo enter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterWithPostcard.tas", "EnterWithPostcard", "", "");
                TASFileInfo entercp1N = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1NoPostCard.tas", "EnterCP1N", "", "");
                TASFileInfo entercp1P = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1WithPostCard.tas", "EnterCP1P", "", "");
                TASFileInfo entercp2 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP2.tas", "EnterCP2", "", "");
                TASFileInfo entercp3 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP3.tas", "EnterCP3", "", "");
                TASFileInfo entercp4 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP4.tas", "EnterCP4", "", "");
                TASFileInfo entercp5 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP5.tas", "EnterCP5", "", "");
                TASFileInfo entercp6 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP6.tas", "EnterCP6", "", "");
                TASFileInfo entersummit = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummit.tas", "EnterSummit", "", "");
                TASFileInfo entersummitcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummitCP1.tas", "EnterSummitCP1", "", "");
                TASFileInfo enterfarewell = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewell.tas", "EnterFarewell", "", "");
                TASFileInfo enterfarewellcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewellCP1.tas", "EnterFarewellCP1", "", "");
                TASFileInfo entercore = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCore.tas", "EnterCore", "", "");
                TASFileInfo entercorecp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCoreCP1.tas", "EnterCoreCP1", "", "");
                TASFileInfo enterB = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSide.tas", "EnterBSide", "", "");
                TASFileInfo enterBcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSideCP1.tas", "EnterBSideCP1", "", "");
                TASFileInfo skipchapter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "SkipChapter.tas", "SkipChapter", "", "");
                #endregion
                List<TASFileInfo> ret = [];
                // Check validity
                int currentchapter = 0;
                // 2B exit thinks it's on 3A
                RouteChange prev = previous;
                switch (prev.FilePath.name.Substring(0, 2)) {
                    case "st":
                    case "1A":
                    case "1B":
                        currentchapter = 1;
                        break;
                    case "2A":
                    case "2B":
                        currentchapter = 2;
                        break;
                    case "3A":
                    case "3B":
                        currentchapter = 3;
                        break;
                    case "4A":
                    case "4B":
                        currentchapter = 4;
                        break;
                    case "5A":
                    case "5B":
                        currentchapter = 5;
                        break;
                    case "6A":
                    case "6B":
                        currentchapter = 6;
                        break;
                    case "7A":
                    case "7B":
                        currentchapter = 7;
                        break;
                    case "8A":
                    case "8B":
                        currentchapter = 9;
                        break;
                    case "Ep":
                        currentchapter = 8;
                        break;
                    case "9A":
                        currentchapter = 10;
                        break;
                    default:
                        Logger.Error("BingoAI", "How did you get to Chapter " + prev.FilePath.name);
                        break;
                }
                int goalchapter = 0;
                switch (chapter) {
                    case Chapter.P:
                        goalchapter = 0;
                        break;
                    case Chapter.C1A:
                    case Chapter.C1B:
                        goalchapter = 1;
                        break;
                    case Chapter.C2A:
                    case Chapter.C2B:
                        goalchapter = 2;
                        break;
                    case Chapter.C3A:
                    case Chapter.C3B:
                        goalchapter = 3;
                        break;
                    case Chapter.C4A:
                    case Chapter.C4B:
                        goalchapter = 4;
                        break;
                    case Chapter.C5A:
                    case Chapter.C5B:
                        goalchapter = 5;
                        break;
                    case Chapter.C6A:
                    case Chapter.C6B:
                        goalchapter = 6;
                        break;
                    case Chapter.C7A:
                    case Chapter.C7B:
                        goalchapter = 7;
                        break;
                    case Chapter.C8A:
                        goalchapter = 9;
                        break;
                    case Chapter.C9:
                        goalchapter = 10;
                        break;
                    case Chapter.E:
                        goalchapter = 8;
                        break;
                    default:
                        goalchapter = 8;
                        break;
                }

                bool needsskip = false;
                List<int> unlocked = [];
                if (SaveData.Instance != null) {

                    unlocked = unlockedChapters().ToList();
                    unlocked.AddRange(skippableChapters());
                    unlocked.Sort();
                    if (skippableChapters().Contains(goalchapter)) {
                        needsskip = true;
                    }
                    if (!unlocked.Contains(goalchapter)) {
                        Logger.Warn("bingoai", "Selected Chapter " + goalchapter + " was not in List of gettable Chapters");
                        return null;
                    }
                }

                // Check if Checkpoint is in Range TBA:

                // 1. Step: get to menu
                //Check how the last played file ended:
                string lastEndLabel = prev.FilePath.endlabel;
                string[] immediatertm = { "Seeded", "Winged", "Heart", "Cutscene", "TheoEnd", "TheoEndStart", "ARB", "Collect", "Key", "Bottom", "Top", "Library", "2000M", "2K", "Bino1", "Bino", "Bino2", "Bino3", "Bino4", "Key1", "Key2", "Key3", "Key4", "Key5", "Key1DTS", "Key2DTS", "Key3DTS", "Key4DTS", "Key5DTS" };
                if (lastEndLabel == "Cassette") {
                    ret.Add(rtmCassette);
                } else if (lastEndLabel == "") {
                    string[] rtmchapters = { "1B", "2B", "3B", "4B", "5B", "6B", "7B", "8A-heartofthemountain" };
                    string[] leavecheckpoints = { "1A-chasm", "1A-wingedgolden", "2A-awake", "3A-presidentialsuite", "4A-cliffface", "5A-rescue", "resolution", "7A-3000m", "8A-heartofthemountain" };
                    if (rtmchapters.Any((c) => prev.FilePath.name.Contains(c))) ret.Add(rtmmenu);
                    else if (prev.FilePath.name == "start") { } else if (prev.FilePath.name.Contains("2A-fromstart")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("1A-fromstart")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("leavepico")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("6A-fromhollows")) { ret.Add(rtmmenu); } else if (leavecheckpoints.Any(cp => prev.FilePath.name.Contains(cp))) { ret.Add(leave); currentchapter++; } else if (prev.FilePath.name.Contains("7A")) { ret.Add(rtmsummitcollect); } else {
                        ret.Add(rtm);
                    }
                } else {
                    ret.Add(rtm);
                }

                // 2. Step: move to right chapter



                int currentchapterindex = unlocked.IndexOf(currentchapter);
                int goalchapterindex = unlocked.IndexOf(goalchapter);
                Logger.Warn("bingoAi", "Moving from Chapter " + currentchapterindex + " to " + goalchapterindex);
                int tempchapter = currentchapterindex;
                while (tempchapter - goalchapterindex != 0) {
                    int chapterdiff = tempchapter - goalchapterindex;
                    if (chapterdiff > 0) {
                        ret.Add(Left);
                        tempchapter--;
                    }
                    if (chapterdiff < 0) {
                        ret.Add(Right);
                        tempchapter++;
                    }
                }

                if (needsskip) {
                    // Maybe this doesn't work with Summit and Reflection, but I'll fix that once it becomes a problem :)
                    ret.Add(skipchapter);
                }

                // 3. Step: enter
                switch (chapter) {
                    case Chapter.C1A:
                    case Chapter.C2A:
                    case Chapter.C3A:
                    case Chapter.C4A:
                    case Chapter.C5A:
                    case Chapter.C6A:
                    case Chapter.C7A:
                    case Chapter.C8A:
                    case Chapter.C9:
                    case Chapter.P:
                    case Chapter.E:
                        switch (Cp) {
                            case 0:
                                if (goalchapter == 7) ret.Add(entersummit);
                                else if (goalchapter == 9) ret.Add(entercore);
                                else if (goalchapter == 10) ret.Add(enterfarewell);
                                else ret.Add(enter);
                                break;
                            case 1:
                                // TODO: Figure out Postcards ret.Add(new(entercp1P, null));
                                if (goalchapter == 7) ret.Add(entersummitcp1);
                                else if (goalchapter == 9) ret.Add(entercorecp1);
                                else if (goalchapter == 10) ret.Add(enterfarewellcp1);
                                else {
                                    if (BingoUI.BingoModule.SaveData.ClearedAreas.Contains(goalchapter)) {
                                        ret.Add(entercp1N);
                                    } else ret.Add(entercp1P);
                                }
                                break;
                            case 2:
                                ret.Add(entercp2);
                                break;
                            case 3:
                                ret.Add(entercp3);
                                break;
                            case 4:
                                ret.Add(entercp4);
                                break;
                            case 5:
                                ret.Add(entercp5);
                                break;
                            case 6:
                                ret.Add(entercp6);
                                break;
                        }
                        break;
                    case Chapter.C1B:
                    case Chapter.C2B:
                    case Chapter.C3B:
                    case Chapter.C4B:
                    case Chapter.C5B:
                    case Chapter.C6B:
                    case Chapter.C7B:
                        switch (Cp) {
                            case 0:
                                ret.Add(enterB);
                                break;
                            case 1:
                                ret.Add(enterBcp1);
                                break;
                        }
                        break;
                }

                // 4. Step: change active route to this one.
                return ret.ToArray();
            });
        }

        public static int[] unlockedChapters() {
            List<int> chapters = [];
            List<BingoUI.ChapterStatus> status = BingoUI.CustomProgression.ChapterStatuses();
            for (int i = 0; i < status.Count; i++) {
                BingoUI.ChapterStatus a = status[i];
                if (a.Icon == BingoUI.ChapterIconStatus.Excited || a.Icon == BingoUI.ChapterIconStatus.Shown) chapters.Add(i);
            }
            return chapters.ToArray();
        }
        public static int[] skippableChapters() {
            List<int> chapters = [];
            List<BingoUI.ChapterStatus> status = BingoUI.CustomProgression.ChapterStatuses();
            for (int i = 0; i < status.Count; i++) {
                BingoUI.ChapterStatus a = status[i];
                if (a.Icon == BingoUI.ChapterIconStatus.Skippable) {
                    chapters.Add(i);
                }
            }
            return chapters.ToArray();
        }
    }
}
