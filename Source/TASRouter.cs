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


            //https://celestebingo.rhelmot.io/room/wJt_qo8ZRTO51BS4WkQivA?password=s
            // Seed: 306566 | Varient: Lockout | Optimized: Lockout | https://celestebingo.rhelmot.io/room/BtWtPRz0TWGfxFqg0pepqA?password=sa
            Add(enterChapter(Chapter.C1A, 0));
            Add("1A-start-arb-winged-1up", GetAfterLabel(new Dictionary<int, string> { { 21, "1UP" } }));
            Add("1A-crossing-heart-fast4-collect", GetAfterLabel(new Dictionary<int, string> { { 5, "Heart" } }));
            Add("1A-chasm-progress2-winged");
            Add(enterChapter(Chapter.C2A, 0));
            Add("2A-start-seeded");
            Add("2A-intervention-arb-1up-2up-3up", GetAfterCompletion(new int[] { 19 }));
            Add("2A-awake-arb-winged");
            AddIf(NFree(new int[] { 3 }, 1), enterChapter(Chapter.C3A, 0));
            AddIf(NWasFree(), "3A-start-grabless-winged", GetAfterLabel(new Dictionary<int, string> { { 3, "Winged" } }));
            AddIf(NWasFree(), "3A-hugemess-tbc-grabless-heart-fast5-winged");
            AddIf(NWasFree(), "3A-shaft-grabless-cassette-diary-arb", GetAfterLabel(new Dictionary<int, string> { { 16, "Diary" }, { 24, "Diary" }, { 8, "Cassette" }, { 1, "Diary" } }));
            AddIf(NWasFree(), "3A-presidentialsuite-grabless", GetAfterCompletion(new int[] { 14 }));
            AddIf(NWasFree(), enterChapter(Chapter.C4A, 0));
            AddIf(NWasFree(), "4A-start-cassette-bino-seeded", GetAfterLabel(new Dictionary<int, string> { { 12, "Cassette" }, { 10, "Seeded" } }));
            AddIf(NWasFree(), "4A-shrine-bino");
            AddIf(NWasFree(), "4A-oldtrail");
            AddIf(NWasFree(), "4A-cliffface-bino");
            AddIf(NWasFree(), enterChapter(Chapter.C9, 0));
            AddIf(NWasFree(), "9A-start-singular-bino");
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("9A-powersource-keys-bino", "DTS", "Bino4DTS") }, () => GetAfterCompletion(new int[] { 18 })().Concat(GetAfterLabel(new Dictionary<int, string> { { 15, "Key3DTS" } })()).ToArray());
            AddIf(NWasFree(), enterChapter(Chapter.C3A, 2));
            AddIf(NWasFree(), "3A-fromhugemess-enterpico", "RTM");
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("pico-berries", "Start", "Orb") }, GetAfterCompletion(new int[] { 4 }));
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("pico", "Orb", "Exit") }, GetAfterCompletion(new int[] { 22 }));
            AddIf(NWasFree(), enterChapter(Chapter.C7A, 0));
            AddIf(NWasFree(), "7A-0m-fast1");
            AddIf(NWasFree(), "7A-500m-bino-fast3");
            AddIf(NWasFree(), "7A-1000m-fast3");
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("7A-1500m-arb-1up-2up-3up", "Start", "ARB") }, GetAfterCompletion(new int[] { 9 }));
            AddIf(NWasFree(), enterChapter(Chapter.C2A, 1));
            AddIf(NWasFree(), "2A-start-fast5", "Start", "lvl_3x");
            AddIf(NWasFree(), enterChapter(Chapter.C1A, 2));
            AddIf(NWasFree(), "1A-crossing-arb-1up", "RTM", "lvl_6z (1)");
            AddIf(NWasFree(), enterChapter(Chapter.C1A, 3));
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("1A-chasm-cassette-fast2-nocollect", "RTM", "Cassette") }, GetAfterCompletion(new int[] { 23 }));
            AddIf(NWasFree(), enterChapter(Chapter.C3B, 0));
            AddIf(NWasFree(), "3B-bino", GetAfterCompletion(new int[] { 2 }));
            AddIf(NWasFree(), enterChapter(Chapter.C1B, 0));
            AddIf(NWasFree(), "1B-bino", GetAfterLabel(new Dictionary<int, string> { { 11, "Bino2" } }));
            AddIf(NWasFree(), enterChapter(Chapter.C8A, 0));
            AddIf(NWasFree(), "8A-start-intothecore");
            AddIf(NWasFree(), "8A-hotandcold");
            AddIf(NWasFree(), () => new TASFileInfo[] { new TASObjectiveInfo("8A-heartofthemountain-heart-arb", "Start", "ARB") }, GetAfterCompletion(new int[] { 0 }));
            AddIf(NWasFree(), enterChapter(Chapter.C5A, 0));
            AddIf(NWasFree(), "5A-start-arb");
            AddIf(NWasFree(), "5A-depths-heart-arb-winged-seeded", GetAfterLabel(new Dictionary<int, string> { { 7, "Heart" }, { 13, "Heart" } }));
            AddIf(NWasFree(), "5A-unravelling-seekerstuns-15-arb", GetAfterCompletion(new int[] { 6 }));
            AddIf(NWasFree(), "5A-search-grabless-arb", GetAfterCompletion(new int[] { 17 }));

            gamestate = new GameState();
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

        #region AddFunctions
        private static void Add(TASFileInfo info, TickAttempt[] ticks = null) {
            route.Add(new(info, ticks));
        }
        private static void Add(TASFileInfo info, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { info }, ticks));
        }
        private static void Add(TASFileInfo info, Func<TickAttempt[]> ticks1, Func<TickAttempt[]> ticks2) {
            route.Add(new(() => new TASFileInfo[] { info }, () => { return ticks1.Invoke().Concat(ticks2.Invoke()).ToArray(); }));
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
        private static void Add(string name, Func<TickAttempt[]> ticks1, Func<TickAttempt[]> ticks2) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name) }, () => { return ticks1.Invoke().Concat(ticks2.Invoke()).ToArray(); }));
            TASFileInfo.Validate(new TASObjectiveInfo(name));
        }
        private static void Add(string name, string start, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start) }, ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start));
        }
        private static void Add(string name, string start, Func<TickAttempt[]> ticks1, Func<TickAttempt[]> ticks2) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start) }, () => { return ticks1.Invoke().Concat(ticks2.Invoke()).ToArray(); }));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start));
        }
        private static void Add(string name, string start, string end, Func<TickAttempt[]> ticks) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start, end) }, ticks));
            TASFileInfo.Validate(new TASObjectiveInfo(name, start, end));
        }
        private static void Add(string name, string start, string end, Func<TickAttempt[]> ticks1, Func<TickAttempt[]> ticks2) {
            route.Add(new(() => new TASFileInfo[] { new TASObjectiveInfo(name, start, end) }, () => { return ticks1.Invoke().Concat(ticks2.Invoke()).ToArray(); }));
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
        private static void Add(Func<TASFileInfo[]> file, Func<TickAttempt[]> ticks1, Func<TickAttempt[]> ticks2) {
            route.Add(new(file, () => { return ticks1.Invoke().Concat(ticks2.Invoke()).ToArray(); }));
            //Does not need Validation
        }
        private static void Add(RouteChange[] oldroute) {
            foreach (RouteChange change in oldroute) {
                Add(change.FilePath, change.TickAttempts);
                TASFileInfo.Validate(change.FilePath);
            }
        }
        private static void AddIf(Func<bool> Condition, TASFileInfo info, TickAttempt[] ticks = null) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return new TASFileInfo[] { info };
                } else return null;
            }, () => ticks));
        }
        private static void AddIf(Func<bool> Condition, Func<TASFileInfo[]> info, TickAttempt[] ticks = null) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return info.Invoke();
                } else return null;
            }, () => ticks));
        }
        private static void AddIf(Func<bool> Condition, Func<TASFileInfo[]> info, Func<TickAttempt[]> ticks) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return info.Invoke();
                } else return null;
            }, ticks));
        }
        private static void AddIf(Func<bool> Condition, string name, TickAttempt[] ticks = null) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return new TASFileInfo[] { new TASObjectiveInfo(name) };
                } else return null;
            }, () => ticks));
        }
        private static void AddIf(Func<bool> Condition, string name, string start, TickAttempt[] ticks = null) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return new TASFileInfo[] { new TASObjectiveInfo(name, start) };
                } else return null;
            }, () => ticks));
        }
        private static void AddIf(Func<bool> Condition, string name, string start, string end, TickAttempt[] ticks = null) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return new TASFileInfo[] { new TASObjectiveInfo(name, start, end) };
                } else return null;
            }, () => ticks));
        }
        private static void AddIf(Func<bool> Condition, string name, Func<TickAttempt[]> ticks) {
            route.Add(new RouteAction(() => {
                if (Condition.Invoke()) {
                    return new TASFileInfo[] { new TASObjectiveInfo(name) };
                } else return null;
            }, ticks));
        }


        private static Func<bool> NFree(int[] slots, int n) {
            return () => {
                WASFree = slots.Count((i) => !isTicked(i)) >= n;
                return WASFree;
            };
        }
        private static Func<bool> NWasFree() {
            return () => WASFree;
        }
        private static bool WASFree = true;

        #endregion

        private static List<RouteAction> route = new();
        private static RouteChange previous;
        private static RouteAction previosAction;
        private static bool changedRoute = false;
        private GameState gamestate;
        public RouteChange? OnTasCompleted() {
            //Testing
            if (changedRoute) {
                changedRoute = false;
            }


            if (route.Count == 0) return null;
            RouteChange[] actions = route[0].Get();
            while (actions == null) {
                route.RemoveAt(0);
                actions = route[0].Get();
            }
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
        public static bool isTicked(int i) {
            if (!BingoClient.BingoClient.Instance.Connected) return false;
            return BingoClient.BingoClient.Instance.GetObjectiveStatus(i) == BingoClient.ObjectiveStatus.Claimed;
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
        public class Chapter {
            public Chapter(int ChapterN, bool AS, bool BS, string endcpS, string wakeupcpS) {
                A = AS;
                B = BS;
                chapter = ChapterN;
                endcp = endcpS;
                wakeupcp = wakeupcpS;
            }
            public int chapter;
            public bool A;
            public bool B;
            public string endcp;
            public string wakeupcp;
            public static Chapter P = new Chapter(0, true, false, "start", null);
            public static Chapter C1A = new Chapter(1, true, false, "chasm", null);
            public static Chapter C1B = new Chapter(1, false, true, null, null);
            public static Chapter C2A = new Chapter(2, true, false, "awake", "intervention");
            public static Chapter C2B = new Chapter(2, false, true, "combination lock", null);
            public static Chapter C3A = new Chapter(3, true, false, "presidentialsuite", null);
            public static Chapter C3B = new Chapter(3, false, true, "rooftop", null);
            public static Chapter C4A = new Chapter(4, true, false, "cliffface", null);
            public static Chapter C4B = new Chapter(4, false, true, null, null);
            public static Chapter C5A = new Chapter(5, true, false, "rescue", "depths");
            public static Chapter C5B = new Chapter(5, false, true, "mixmaster", "centralchamber");
            public static Chapter C6A = new Chapter(6, true, false, "resolution", "start");
            public static Chapter C6B = new Chapter(6, false, true, "reprieve", null);
            public static Chapter C7A = new Chapter(7, true, false, "3000m", null);
            public static Chapter C7B = new Chapter(7, false, true, "3000m", null);
            public static Chapter E = new Chapter(8, true, false, null, null);
            public static Chapter C8A = new Chapter(9, true, false, "heartofthemountain", null);
            public static Chapter C9 = new Chapter(10, true, false, "farewell", null);
        }
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
                    case "pi":
                    case "le":
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
                int goalchapter = chapter.chapter;

                bool needsskip = false;
                List<int> unlocked = [];
                if (SaveData.Instance != null) {

                    unlocked = unlockedChapters().ToList();
                    unlocked.AddRange(skippableChapters());
                    if (skippableChapters().Contains(6) && goalchapter == 7) unlocked.Add(7);
                    unlocked.Sort();
                    if (skippableChapters().Contains(goalchapter) || (skippableChapters().Contains(6) && goalchapter == 7)) {
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
                if (lastEndLabel == "Cassette") {
                    ret.Add(rtmCassette);
                } else if (lastEndLabel == "") {
                    string[] rtmchapters = { "1B", "2B", "3B", "4B", "5B", "6B", "7B", "8A-heartofthemountain" };
                    string[] leavecheckpoints = { "1A-chasm", "1A-wingedgolden", "2A-awake", "3A-presidentialsuite", "4A-cliffface", "5A-rescue", "resolution", "7A-3000m", "8A-heartofthemountain" };
                    string[] wakeupcheckpoints = { "2A-intervention", "5A-depths" };
                    if (rtmchapters.Any((c) => prev.FilePath.name.Contains(c))) ret.Add(rtmmenu);
                    else if (prev.FilePath.name == "start") { } else if (prev.FilePath.name.Contains("2A-fromstart")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("1A-fromstart")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("leavepico")) { ret.Add(rtmmenu); } else if (prev.FilePath.name.Contains("6A-fromhollows")) { ret.Add(rtmmenu); } else if (wakeupcheckpoints.Any(v => prev.FilePath.name.Contains(v))) {
                        ret.Add(rtmwakeup);
                    } else if (leavecheckpoints.Any(cp => prev.FilePath.name.Contains(cp))) { ret.Add(leave); currentchapter++; } else if (prev.FilePath.name.Contains("7A")) { ret.Add(rtmsummitcollect); } else {
                        ret.Add(rtm);
                    }
                } else {
                    ret.Add(rtm);
                }

                // 2. Step: move to right chapter



                int currentchapterindex = unlocked.IndexOf(currentchapter);
                int goalchapterindex = unlocked.IndexOf(goalchapter);
                if ((skippableChapters().Contains(6) && goalchapter == 7)) goalchapterindex--;
                Logger.Warn("bingoAi", "Moving from Chapter " + currentchapterindex + " to " + goalchapterindex);
                if (currentchapterindex < goalchapterindex) {
                    ret.Add(new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "NRight.tas", "NRight", (goalchapterindex - currentchapterindex) + "Right", ""));
                }
                if (currentchapterindex > goalchapterindex) {
                    ret.Add(new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "NLeft.tas", "NLeft", (currentchapterindex - goalchapterindex) + "Left", ""));
                }

                if (needsskip) {
                    // Maybe this doesn't work with Summit and Reflection, but I'll fix that once it becomes a problem :)
                    ret.Add(skipchapter);
                }
                if ((skippableChapters().Contains(6) && goalchapter == 7)) {
                    ret.Add(Right);
                }

                    // 3. Step: enter
                    if (chapter.A) {
                    switch (Cp) {
                        case 0:
                            if (goalchapter == 7) ret.Add(entersummit);
                            else if (goalchapter == 9) ret.Add(entercore);
                            else if (goalchapter == 10) ret.Add(enterfarewell);
                            else ret.Add(enter);
                            break;
                        case 1:
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
                } else if (chapter.B) {
                    switch (Cp) {
                        case 0:
                            ret.Add(enterB);
                            break;
                        case 1:
                            ret.Add(enterBcp1);
                            break;
                    }
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
