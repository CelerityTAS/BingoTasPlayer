using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.BingoTasPlayer {
    internal class TASRoutes {
        static TASFileInfo enter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterWithPostcard.tas", "EnterWithPostcard", "", "");
        static TASFileInfo entercp1N = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1NoPostCard.tas", "EnterCP1N", "", "");
        static TASFileInfo entercp1P = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP1WithPostCard.tas", "EnterCP1P", "", "");
        static TASFileInfo entercp2 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP2.tas", "EnterCP2", "", "");
        static TASFileInfo entercp3 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP3.tas", "EnterCP3", "", "");
        static TASFileInfo entercp4 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP4.tas", "EnterCP4", "", "");
        static TASFileInfo entercp5 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP5.tas", "EnterCP5", "", "");
        static TASFileInfo entercp6 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCP6.tas", "EnterCP6", "", "");
        static TASFileInfo entersummit = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummit.tas", "EnterSummit", "", "");
        static TASFileInfo entersummitcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterSummitCP1.tas", "EnterSummitCP1", "", "");
        static TASFileInfo enterfarewell = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewell.tas", "EnterFarewell", "", "");
        static TASFileInfo enterfarewellcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterFarewellCP1.tas", "EnterFarewellCP1", "", "");
        static TASFileInfo entercore = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCore.tas", "EnterCore", "", "");
        static TASFileInfo entercorecp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterCoreCP1.tas", "EnterCoreCP1", "", "");
        static TASFileInfo enterB = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSide.tas", "EnterBSide", "", "");
        static TASFileInfo enterBcp1 = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "EnterBSideCP1.tas", "EnterBSideCP1", "", "");
        static TASFileInfo loadafromb = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "CelesteTAS/LoadAFromB.tas", "LoadAFromB", "SkipExit", "");
        static TASFileInfo leave = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveChapter.tas", "LeaveChapter", "", "");
        static TASFileInfo leaveagain = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveAgain.tas", "LeaveAgain", "", "");
        static TASFileInfo leavepico = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeavePico.tas", "LeavePico", "0", "Exit");
        static TASFileInfo rtm = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "WalkinCheckpoint", "");
        static TASFileInfo rtmwakeup = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Wakeup", "");
        static TASFileInfo rtmCassette = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Cassette", "");
        static TASFileInfo rtmmenu = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "Menu", "");
        static TASFileInfo rtmsummitnocollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitNoCollect", "");
        static TASFileInfo rtmsummitcollect = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RTM.tas", "RTM", "SummitCollect", "");
        static TASFileInfo Left = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Left.tas", "Left", "", "");
        static TASFileInfo Right = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "Right.tas", "Right", "", "");
        static TASFileInfo skipchapter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "SkipChapter.tas", "SkipChapter", "", "");
        static TASFileInfo restartchapter = new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "RestartChapter.tas", "RestartChapter", "", "");
        /*
             * 2 bino in 4 and 10 binos: 5 in 7A, 2 in 4A, 2 in 7B, 2 bino in 2B or 2 bino in 1B
             * berries: 20 in 5A, 15 in 4A, 15 in 7A
             * 
             * seeded in: 4A and 5A
             * winged: 1A start chasm, 3A start+mess, 4A oltrail, 5A depths, 7A 0m (or 2A awake instead of mess)
             * 
             * B sides: 1B, 2B, 5B
             * blue hearts: 5A, 4A, 1A
             * theo cutscenes: 1A, 2A or 5A
             * 
             * 1A
             * start dashless winged
             * crossing theo
             * crossing heart
             * chasm cassette winged
             * 
             * 2A
             * start cassette
             * intervention jumpless
             * 
             * 3A skip
             * start
             * hugemess pico berries complete
             * 
             * 4A
             * start grabless cassette bino seeded
             * shrine
             * oldtrail arb
             * cliffface arb bino
             * 
             * 5A
             * start
             * depths heart cassette winged seeded
             * unraveling
             * search arb
             * 
             * 7A
             * 0m gem
             * 500m gem 3bino
             * 1000m 2bino
             * 1500m cassette
             * 
             * 7B
             * 2k
             * 
             * 8A
             * itc berry
             * 
             * */
        public static RouteChange[] blitzroute = [
                new (enter,null),
                new(new TASObjectiveInfo("1A-start-dashless-winged"),null),
                new(new TASObjectiveInfo("1A-crossing-theo"),new TickAttempt[] { new TickAttempt(0,new("dashless start",22))}),
                new(new TASObjectiveInfo("1A-chasm-cassette-winged","Start","Winged"),null),
                new (rtm,null),
                new (enterB,null),
                new(new TASObjectiveInfo("1B-bino"),null),
                new (loadafromb,null),
                new(new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null),
                new (restartchapter,null),
                new(new TASObjectiveInfo("2A-start-cassette"),null),
                new(new TASObjectiveInfo("2A-intervention-jumpless"),new TickAttempt[] { new TickAttempt(0,new("2A C",4))}),
                new(new TASObjectiveInfo("2A-awake-arb-winged","FromGrabless"),new TickAttempt[] { new TickAttempt(0,new("jumpless int",15))}),
                new (leave,null),
                new (Left,null),
                new (enterB,null),
                new(new TASObjectiveInfo("2B-bino"),null),
                new (loadafromb,null),
                new(new TASObjectiveInfo("3A-start-winged"),null),
                new(new TASObjectiveInfo("3A-fromhugemess-enterpico"),null),
                new(new TASObjectiveInfo("pico-berries","Start","Orb"),null),
                new(new TASObjectiveInfo("pico","Orb"),new TickAttempt[] { new TickAttempt(0,new("pico berry",8))}),
                new(rtmmenu,new TickAttempt[] { new TickAttempt(0,new("pico",24))}),
                new (skipchapter,null),
                new (enter,null),
                new(new TASObjectiveInfo("4A-start-grabless-cassette-bino-seeded"),null),
                new(new TASObjectiveInfo("4A-shrine-heart-fast1"),new TickAttempt[] { new TickAttempt(0,new("4A start grabless",13)),new TickAttempt(0,new("4AC",2))}),
                new(new TASObjectiveInfo("4A-oldtrail-progress7-winged"),null),
                new(new TASObjectiveInfo("4A-cliffface-bino-arb-collect"),null),
                new (leave,new TickAttempt[] { new TickAttempt(0,new("3up",6)),new TickAttempt(0,new("trail arb",9)),new TickAttempt(0,new("cliffface arb",20))}),
                new (enter,null),
                new(new TASObjectiveInfo("5A-start-fast2"),null),
                new(new TASObjectiveInfo("5A-depths-heart-cassette-arb-winged-seeded"),null),
                new(new TASObjectiveInfo("5A-unraveling-arb"),new TickAttempt[] { new TickAttempt(0,new("5AH",10)),new TickAttempt(0,new("2seeded",11))}),
                new(new TASObjectiveInfo("5A-search-theo-arb","Start","Collect"),null),
                new (rtm,new TickAttempt[] { new TickAttempt(0, new("20 in 5A", 19)), new TickAttempt(0,new("search arb",17)), new TickAttempt(0,new("2 theo cutscenes",18))}),
                new (enterB,null),
                new(new TASObjectiveInfo("5B"),null),
                new (rtmmenu,new TickAttempt[] { new TickAttempt(0,new("3b3r",12))}),
                new (Right,null),
                new (Right,null),
                new (entersummit,null),
                new(new TASObjectiveInfo("7A-0m-gem-fast3"),null),// fast2
                new(new TASObjectiveInfo("7A-500m-gem-bino-arb-winged"),null),// 6 berries
                new(new TASObjectiveInfo("7A-1000m-2bino-fast5"),new TickAttempt[] { new TickAttempt(0,new("gems",3))}), // 5 berries
                new(new TASObjectiveInfo("7A-1500m-cassette-fast4","Start","Cassette"),new TickAttempt[] { new TickAttempt(0, new("10 bino", 0)), new TickAttempt(0,new("50 berries",5)),new TickAttempt(0,new("5 bino in 7",7)), new TickAttempt(0,new("2 bino in 4",16)) }), // 2 berries
                new (rtmCassette,new TickAttempt[] { new TickAttempt(0,new("7 winged berries",21)),new TickAttempt(0,new("15 in 3",23)) }),
                new (enterB,null),
                new(new TASObjectiveInfo("7B","Start","2000M"),null),
                new (rtm,new TickAttempt[] { new TickAttempt(0,new("2k",14))}),
                new (Right,null),
                new (entercore,null),
                new(new TASObjectiveInfo("8A-start-intothecore-arb","Start","Collect"),null),
                new (rtm,new TickAttempt[] { new TickAttempt(0,new("itc berry",1))})
                ]; // 24:38.269

        RouteChange[] routethriteenalt = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-wingedgolden-2winged"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("1astartdashless",6)), new TickAttempt(0,new Objective("wingedgolden",21))}),
                new (enter,null),
                new(new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null),
                new (restartchapter,new TickAttempt[] {new TickAttempt(0,new Objective("2ablue",15))}),
                new(new TASObjectiveInfo("2A-start-grabless-cassette-arb-seeded-1up-2up-3up"),null),
                new(new TASObjectiveInfo("2A-intervention-grabless"),null),
                new(new TASObjectiveInfo("2A-awake-grabless-arb-winged", "FromGrabless"),null),
                new (leave,null),
                new (Left,null),
                new (enterB,null),
                new(new TASObjectiveInfo("2B-bino"),null),
                new (loadafromb,null),
                new(new TASObjectiveInfo("3A-start-grabless-fast5-winged"),null),
                new(new TASObjectiveInfo("3A-hugemess-tbc-grabless-heart-fast5-winged"),null),
                new(new TASObjectiveInfo("3A-shaft-grabless-theo"),new TickAttempt[] {new TickAttempt(0,new Objective("3hearts",14)), new TickAttempt(0,new Objective("messorder",20))}),
                new(new TASObjectiveInfo("3A-presidentialsuite-grabless"),new TickAttempt[] {new TickAttempt(0,new Objective("theo",22))}),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("2chaptergrabless",8))}),
                new (enter,null),
                new(new TASObjectiveInfo("4A-start-grabless-bino"),null),
                new(new TASObjectiveInfo("4A-shrine-heart-bino", "Start", "Heart"), new TickAttempt[] {new TickAttempt(0,new Objective("grablessstart",4))}),
                new (rtm,null),
                new (skipchapter,null),
                new (enter,null),
                new(new TASObjectiveInfo("5A-start-jumpless"),null),
                new(new TASObjectiveInfo("5A-depths-heart-cassette-winged"), new TickAttempt[] {new TickAttempt(0,new Objective("4astartjumpless",2))}),
                new(new TASObjectiveInfo("5A-unraveling"),new TickAttempt[] {new TickAttempt(0,new Objective("7winged",1)), new TickAttempt(0,new Objective("25berries",18))}),
                new(new TASObjectiveInfo("5A-search-arb-1up"),null),
                new(new TASObjectiveInfo("5A-rescue-arb", "Start", "Collect"),null),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("arbrescue",23))}),
                new (enterB,null),
                new(new TASObjectiveInfo("5B"),null),
                new (loadafromb, new TickAttempt[] {new TickAttempt(0,new Objective("6hearts",3)), new TickAttempt(0,new Objective("5b",17))}),
                new(new TASObjectiveInfo("6A-start"),null),
                new(new TASObjectiveInfo("6A-lake-kevin", "Start", "Collect"),null),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("kevin4sides",13))}),
                new (Right, null),
                new (entersummit,null),
                new(new TASObjectiveInfo("7A-0m-fast1"),null),
                new(new TASObjectiveInfo("7A-500m-bino-arb-winged"),null),
                new(new TASObjectiveInfo("7A-1000m-3bino"),null),
                new(new TASObjectiveInfo("7A-1500m-arb-winged-1up-2up-3up"),new TickAttempt[] {new TickAttempt(0,new Objective("5binosummit",12)), new TickAttempt(0,new Objective("40berries",7))}),
                new(new TASObjectiveInfo("7A-2000m-snowball15"),new TickAttempt[] {new TickAttempt(0,new Objective("1upin3",9)), new TickAttempt(0,new Objective("arb1500m",19)), new TickAttempt(0,new Objective("9winged",24))}),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("15snowballs",11))}),
                new (Right, null),
                new (entercore,null),
                new(new TASObjectiveInfo("8A-start-intothecore-arb", "Start", "Collect"),null),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("arbintothecore",0)), new TickAttempt(0,new Objective("50berries",10))}),
                new (Right, null),
                new (enterfarewell,null),
                new(new TASObjectiveInfo("9A-start-singular-bino"),null),
                new(new TASObjectiveInfo("9A-powersource-keys", "DTS", "Key2DTS"),new TickAttempt[] {new TickAttempt(0,new Objective("2binoin4",5))}),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("powersourcekeys",16))})
            ];

        public static RouteChange[] routethriteen = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-wingedgolden-2winged"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("wg",21)),new TickAttempt(0,new Objective("start dashless",6))}),
                new (Left,null),
                new (entercp2,null),
                new (new TASObjectiveInfo("1A-crossing-heart","RTM","Heart"),null), //needs to comment out Start-Movement?
                new (rtm,null),
                new (entercp3,null),
                new (new TASObjectiveInfo("1A-chasm-cassette","RTM","Cassette"),null),//needs to comment out Start-Movement?
                new (rtmCassette,null),
                new (enterB,null),
                new (new TASObjectiveInfo("1B-bino","Start","Bino2"),null),
                new (rtm,null),
                new (Right,null),
                new (enter,null),
                new (new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("2AB",15))}),
                new (Right,null),
            new (skipchapter,null),
                new (enter,null),
                new (new TASObjectiveInfo("3A-start-grabless-fast5-winged"),null),
                new (new TASObjectiveInfo("3A-hugemess-tbc-grabless-heart-winged"),null),
                new (new TASObjectiveInfo("3A-shaft-grabless-theo"),new TickAttempt[] {new TickAttempt(0,new Objective("order",20)),new TickAttempt(0,new Objective("3 hearts",14))}),
                new (new TASObjectiveInfo("3A-presidentialsuite-grabless-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("theo",22))}),
                new (leave,null),
                new (enter,null),
                new (new TASObjectiveInfo("4A-start-grabless-progress5"),null),
                new (new TASObjectiveInfo("4A-shrine-heart-bino-fast4-collect","Start","Heart"),new TickAttempt[] {new TickAttempt(0,new Objective("4A start grabless",4))}),
                new (rtm,null),
                new (entercp2,null),
                new (new TASObjectiveInfo("4A-shrine-grabless", "RTM"),null), // Start movement remove
                new (new TASObjectiveInfo("4A-oldtrail-grabless-winged"),null),
                new (new TASObjectiveInfo("4A-cliffface-grabless-snowball-bino"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("2c grabless",8)),new TickAttempt(0,new Objective("15 snowballs",11))}),
                new (enter,null),
                new (new TASObjectiveInfo("5A-start-jumpless"),null),
                new (new TASObjectiveInfo("5A-depths-heart-cassette-arb-winged-seeded-1up"),new TickAttempt[] {new TickAttempt(0,new Objective("jumpless",2))}),
                new (new TASObjectiveInfo("5A-unraveling-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("7 winged",1)),new TickAttempt(0,new Objective("25",18))}),
                new (new TASObjectiveInfo("5A-search"),null),
                new (new TASObjectiveInfo("5A-rescue-arb","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("rescue arb",23))}),
                new (enterB,null),
                new (new TASObjectiveInfo("5B"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("6 hearts",3)),new TickAttempt(0,new Objective("5b",17)) }),
                new (new TASObjectiveInfo("6A-start"),null),
                new (new TASObjectiveInfo("6A-lake-kevin","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("kevin",13))}),
                new (Right,null),
                new (entersummit,null),
                new (new TASObjectiveInfo("7A-0m-fast3"),null),
                new (new TASObjectiveInfo("7A-500m-bino-winged"),null),
                new (new TASObjectiveInfo("7A-1000m-2bino-fast1"),null),
                new (new TASObjectiveInfo("7A-1500m-arb-winged-1up-2up-3up","Start","Collect"),new TickAttempt[] {new TickAttempt(0,new Objective("5bino in summit",12))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("1up in 3",9)), new TickAttempt(0, new Objective("40", 7)),new TickAttempt(0,new Objective("1500m arb",19)),new TickAttempt(0,new Objective("9 winged",24))}),
                new (Right,null),
                new (entercore,null),
                new (new TASObjectiveInfo("8A-start-intothecore-arb","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("itc arb",0)),new TickAttempt(0,new Objective("50 berries",10))}),
                new (Right,null),
                new (enterfarewell,null),
                new (new TASObjectiveInfo("9A-start-singular"),null),
                new (new TASObjectiveInfo("9A-powersource-keys-bino","DTS","Bino2DTS"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("2bino in 4",5)),new TickAttempt(0,new Objective("2keys in farewell",16))}),
                ];

        /*
         * 
         * Weekly Blackout t13
            104349
            Gen: Co-op Blackout
            Progression: Tournament Standard
         * 
         * meme chapter 9 farewellskip grabless
         * 
         * 
         * 9 winged berries       (3x1A 2x3A 500m 1500m, 4A, 5A)
         * 6 heart                (1A, 2A, 3A, 4A, 5A, 5B)
         * 2 grabless chapters    (3A, 4A)
         * 4 2 bino chapters      (1B, 4A, summit, farewell) (1B 12+46, 7B 55 seconds)
         * 3 chapters with 1up    (4A start-shrine 5Adepths 1500m)
         * completed chapters: 1A, 3A, 4A, 5B
         * 50 berries (9winged, 7 1500m, 1 itc, 5 4start, 8shrine, 10depths, 1rescue, 1unraveling, 1 0m, 4 3start, 1 shaft, 1 1A-blue, 1 in 1000m bino)
         * 50 berries alt (9winged, 7 1500m, 1 itc, 5 4start, 4shrine, 10depths, 1rescue, 3 0m, 4 3start, 1 shaft, 3 suite, 1 1A-blue, 1 in 1000m bino)
         * 
         * 2:36.366(9198) 5A-heart
         * 2:21.474(8322) 5A noheart 15 seconds
         * 1B is 20 seconds
         * 
         * cliffface snowball route:
         * wg 2 winged fp
         * 
         * crossing heart
         * 
         * 2A blue 
         * skip
         * 
         * start grabless fast5 winged
         * any mess order heart grabless winged
         * shaft theo grabless arb
         * suite grabless arb 
         * 
         * 4A
         * start grabless progress5
         * shrine heart-bino-arb (canceled) 8 stop at heart
         * shrine grabless
         * oldtrail grabless winged
         * cliffface grabless snowball bino
         * 
         * 5A
         * start jumpless
         * depths cassette heart arb winged
         * unraveling arb1
         * search
         * rescue arb
         * 5B
         * 
         * 6A
         * start
         * lake kevin
         * 
         * 7A
         * 0m fast3
         * 500m 2bino
         * 1000m bino
         * 1500m arb 8 1up
         * 
         * 8A
         * start itc arb
         * 
         * 9
         * start bino
         * ps 2keys
         * 
         * 
         * summit snowball route
         * */

        #region T12
        RouteChange[] routetwelve = [
            new (skipchapter,null),
                new (enter,null),
                new(new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null),
                new (restartchapter,null),
                new(new TASObjectiveInfo("2A-start-cassette-arb-seeded-1up-2up-3up"),null),
                new(new TASObjectiveInfo("2A-intervention-arb-1up-2up-3up"),null),
                new (rtmwakeup,null),
                new (entercp1P,null),
                new(new TASObjectiveInfo("2A-start-grabless"),null),
                new(new TASObjectiveInfo("2A-intervention-grabless"),null),
                new(new TASObjectiveInfo("2A-awake-theo", "FromGrabless", "TheoEnd"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("theoawake",1))}),
                new (entercp3,null),
                new(new TASObjectiveInfo("2A-awake-grabless-arb-winged", "RTM"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("grabless2a",10)), new TickAttempt(0,new Objective("ac2a",7))}),
                new (enter,null),
                new(new TASObjectiveInfo("3A-start"),null),
                new(new TASObjectiveInfo("3A-fromhugemess-enterpico"),null),
                new(new TASObjectiveInfo("pico", "Start", "OldSite"),null),
                new (leavepico,new TickAttempt[] {new TickAttempt(0,new Objective("sitepico",8))}),
                new (rtm,null),
                new (entercp2,null),
                new(new TASObjectiveInfo("3A-hugemess-cbt-grabless-arb-winged", "RTM"),null),
                new(new TASObjectiveInfo("3A-shaft-cassette", "Start", "Cassette"),new TickAttempt[] {new TickAttempt(0,new Objective("arbhm",14)), new TickAttempt(0,new Objective("ghm",23))}),
                new (rtmCassette,null),
                new (enterB,null),
                new(new TASObjectiveInfo("3B-bino"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("3b",9))}),
                new(new TASObjectiveInfo("4A-start-cassette-fast6-seeded-1up", "Start", "Cassette"),null),
                new (rtmCassette,new TickAttempt[] {new TickAttempt(0,new Objective("1upin2",17)), new TickAttempt(0,new Objective("4a1up",24))}),
                new (enterB,null),
                new(new TASObjectiveInfo("4B-3bino"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("5bsidebinos",12)), new TickAttempt(0,new Objective("3c3h",0))}),
                new(new TASObjectiveInfo("5A-start-arb"),null),
                new(new TASObjectiveInfo("5A-depths-heart-cassette-arb-winged-seeded"),new TickAttempt[] {new TickAttempt(0,new Objective("arbstart",21))}),
                new(new TASObjectiveInfo("5A-unraveling-seekerstuns15-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("50berries",2)), new TickAttempt(0,new Objective("5ac",16))}),
                new(new TASObjectiveInfo("5A-search-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("15stuns",19))}),
                new(new TASObjectiveInfo("5A-rescue-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("1kins",5))}),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("ac5a",13))}),
                new (Right,null),
                new (entersummit,null),
                new(new TASObjectiveInfo("7A-0m-fast1"),null),
                new(new TASObjectiveInfo("7A-500m-bino"),null),
                new(new TASObjectiveInfo("7A-1000m-bino"),null),
                new(new TASObjectiveInfo("7A-1500m"),new TickAttempt[] {new TickAttempt(0,new Objective("5bino7a",4))}),
                new(new TASObjectiveInfo("7A-2000m-arb-winged-seeded"),null),
                new(new TASObjectiveInfo("7A-2500m-fast4"),new TickAttempt[] {new TickAttempt(0,new Objective("4seeded",6)), new TickAttempt(0,new Objective("10bin3",18))}),
                new(new TASObjectiveInfo("7A-3000m-arb"),null) ,
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("207A",15)),new TickAttempt(0,new Objective("3karb",22)),new TickAttempt(0,new Objective("4asides",20))}),
                new (Right,null),
                new (entercore,null),
                new(new TASObjectiveInfo("8A-start-intothecore-switch-arb"),null),
                new(new TASObjectiveInfo("8A-hotandcold-arb"), new TickAttempt[] {new TickAttempt(0,new Objective("switch",3))}),
                new(new TASObjectiveInfo("8A-heartofthemountain-heart-arb", "Start", "ARB"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("5b8a",11))})
        ];
        #endregion T12
        #region T11
        // needs Key1DTS in 9A-powersource-keys
        RouteChange[] routeeleven = [
            new (skipchapter, null),
                new (enter, null),
                new (new TASObjectiveInfo("2A-fromstart-heart", "Start", "Heart"), null),
                new (restartchapter, new TickAttempt[] {new TickAttempt(0, new Objective("Old Site Blue Heart", 17))}),
                new (new TASObjectiveInfo("2A-start-cassette-bino-arb-seeded-1up-2up-3up"), null),
                new (new TASObjectiveInfo("2A-intervention"), new TickAttempt[] {new TickAttempt(0, new Objective("Get a 1-UP in 2A", 24))}),
                new (new TASObjectiveInfo("2A-awake-poem", "Start", "Checkpoint"), null),
                new (rtm, new TickAttempt[] {new TickAttempt(0, new Objective("Read the poem in Awake", 10))}),
                new (entercp3, null),
                new (new TASObjectiveInfo("2A-awake-dashless", "RTM"), null),
                new (leave, new TickAttempt[] {new TickAttempt(0, new Objective("Dashless Awake", 1))}),
                new (Left, null),
                new (enterB, null),
                new (new TASObjectiveInfo("2B-bino"), null),
                new (loadafromb, new TickAttempt[] {new TickAttempt(0, new Objective("Blue and Red Heart in Old Site", 3))}),
                new (new TASObjectiveInfo("3A-start-grabless-winged"), null),
                new (new TASObjectiveInfo("3A-fromhugemess-enterpico"), null),
                new (new TASObjectiveInfo("pico-berries", "Start", "15"), null),
                new (leavepico, new TickAttempt[] {new TickAttempt(0, new Objective("10 Berries in PICO-8", 6)), new TickAttempt(0, new Objective("15 Berries in PICO-8", 20))}),
                new (rtm, null),
                new (entercp2, null),
                new (new TASObjectiveInfo("3A-hugemess-cbt-grabless-winged", "RTM"), null),
                new (new TASObjectiveInfo("3A-shaft-grabless-cassette"), null),
                new (new TASObjectiveInfo("3A-presidentialsuite-grabless"), null),
                new (leave, new TickAttempt[] {new TickAttempt(0, new Objective("Grabless Presidential Suite", 5)), new TickAttempt(0, new Objective("Grabless 3A", 11))}),
                new (Left, null),
                new (enterB, null),
                new (new TASObjectiveInfo("3B"), null),
                new (loadafromb, null),
                new (new TASObjectiveInfo("4A-start-cassette-arb-seeded"), null),
                new (new TASObjectiveInfo("4A-shrine-heart-bino-arb"), new TickAttempt[] {new TickAttempt(0, new Objective("Golden Ridge Cassette", 12))}),
                new (new TASObjectiveInfo("4A-oldtrail-arb"), new TickAttempt[] {new TickAttempt(0, new Objective("20 Berries", 8))}),
                new (new TASObjectiveInfo("4A-cliffface-bino-arb"), new TickAttempt[] {new TickAttempt(0, new Objective("All Berries in Old Trail", 21))}),
                new (leave, new TickAttempt[] {new TickAttempt(0, new Objective("All Collectibles in 4A", 2))}),
                new (enter, null),
                new (new TASObjectiveInfo("5A-start"), null),
                new (new TASObjectiveInfo("5A-depths-cassette-winged"), null),
                new (new TASObjectiveInfo("5A-unraveling"), null),
                new (new TASObjectiveInfo("5A-search-keys", "Start", "Key1"), null),
                new (rtm, new TickAttempt[] {new TickAttempt(0, new Objective("Get 1 Key in Search", 13))}),
                new (enterB, null),
                new (new TASObjectiveInfo("5B"), null),
                new (loadafromb, new TickAttempt[] {new TickAttempt(0, new Objective("3 B-Sides", 18)), new TickAttempt(0, new Objective("Mirror Temple B-Side", 22))}),
                new (new TASObjectiveInfo("6A-start"), null),
                new (new TASObjectiveInfo("6A-lake-kevin", "Start", "Collect"), null),
                new (rtm, new TickAttempt[] {new TickAttempt(0, new Objective("Hit a Kevin from all 4 sides", 15))}),
                new (Right, null),
                new (entersummit, null),
                new (new TASObjectiveInfo("7A-0m"), null),
                new (new TASObjectiveInfo("7A-500m-gem-bino-winged"), null),
                new (new TASObjectiveInfo("7A-1000m-gem-bino"), new TickAttempt[]{ new TickAttempt(0, new Objective("5 Winged Berries", 19)) }),
                new (new TASObjectiveInfo("7A-1500m-gem", "Start", "Gem"), new TickAttempt[]{ new TickAttempt(0, new Objective("5 Binos in Summit", 23))}),
                new (rtm, new TickAttempt[] {new TickAttempt(0, new Objective("1000m and 1500m Gems", 7))}),
                new (Right, null),
                new (entercore, null),
                new (new TASObjectiveInfo("8A-start-intothecore-arb"), null),
                new (new TASObjectiveInfo("8A-hotandcold-arb"), null),
                new (new TASObjectiveInfo("8A-heartofthemountain-heart-cassette-arb"), null),
                new (rtmmenu, new TickAttempt[] {new TickAttempt(0, new Objective("All Collectibles in 8A", 0)), new TickAttempt(0, new Objective("6 Hearts", 14))}),
                new (Right, null),
                new (enterfarewell, null),
                new (new TASObjectiveInfo("9A-start-singular-bino"), null),
                new (new TASObjectiveInfo("9A-powersource-keys", "DTS", "Key1DTS"), new TickAttempt[] {new TickAttempt(0, new Objective("Use 1 Binocular in 5 Chapters", 9)), new TickAttempt(0, new Objective("Use 2 Binoculars in 4 Chapters", 16))}),
                new (rtm, new TickAttempt[] {new TickAttempt(0, new Objective("Get 1 Key in Power Source", 4))})
            ];
        #endregion T11

        #region T10
        RouteChange[] routeten = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-start-grabless"),null),
                new(new TASObjectiveInfo("1A-crossing-heart-theo","Start","Heart"),null),
                new (rtm,null),
                new (entercp2,null),
                new(new TASObjectiveInfo("1A-crossing-grabless","RTM"),null),
                new(new TASObjectiveInfo("1A-chasm-cassette","Start","Cassette"),null),
                new (rtmCassette,new TickAttempt[] {new TickAttempt(0,new Objective("1ac",4))}),
                new (entercp3,null),
                new(new TASObjectiveInfo("1A-chasm-grabless", "RTM"),null),
                new (leave,null),
                new (Left,null),
                new (enterB,null),
                new(new TASObjectiveInfo("1B-Bino"),null),
                new (loadafromb,null),
                new(new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null),
                new (restartchapter,null),
                new(new TASObjectiveInfo("2A-start-cassette-bino","Start","Cassette"),null),
                new (rtmCassette,null),
                new (entercp1P,null),
                new(new TASObjectiveInfo("2A-start-grabless"),null),
                new(new TASObjectiveInfo("2A-intervention-grabless"),null),
                new(new TASObjectiveInfo("2A-awake-theo", "FromGrabless", "TheoEnd"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("theoawake",6))}),
                new (entercp3,null),
                new(new TASObjectiveInfo("2A-awake-grabless", "RTM"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("2cg",0))}),
                new (Left,null),
                new (enterB,null),
                new(new TASObjectiveInfo("2B-Bino"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("2B",13))}),
                new(new TASObjectiveInfo("3A-start-arb"),null),
                new(new TASObjectiveInfo("3A-fromhugemess-enterpico"),null),
                new(new TASObjectiveInfo("pico-berries", "Start", "Orb"),null),
                new (leavepico,new TickAttempt[] {new TickAttempt(0,new Objective("orbpico",8)),new TickAttempt(0,new Objective("10bpico",10))}),
                new (rtm,null),
                new (entercp2,null),
                new(new TASObjectiveInfo("3A-hugemess-cbt-heart-fast5-winged", "RTM"),null),
                new(new TASObjectiveInfo("3A-shaft-theo", "Start", "TheoEnd"),new TickAttempt[] {new TickAttempt(0,new Objective("order",21))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("3theos",1))}),
                new (skipchapter,null),
                new (enter,null),
                new(new TASObjectiveInfo("4A-start-arb"),null),
                new(new TASObjectiveInfo("4A-shrine-dashless"),new TickAttempt[] {new TickAttempt(0,new Objective("start4aarb",3))}),
                new (rtm, new TickAttempt[] {new TickAttempt(0,new Objective("dshrine",15))}),
                new (entercp2,null),
                new(new TASObjectiveInfo("4A-shrine-heart-bino", "RTM"),null),
                new(new TASObjectiveInfo("4A-oldtrail-arb-winged"),new TickAttempt[] {new TickAttempt(0,new Objective("4blue",12))}),
                new(new TASObjectiveInfo("4A-cliffface-bino"),new TickAttempt[] {new TickAttempt(0,new Objective("15b4a",5)), new TickAttempt(0,new Objective("3winged",19))}),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("7binos",7)), new TickAttempt(0,new Objective("2bionsin3",23))}),
                new (enter,null),
                new(new TASObjectiveInfo("5A-start-arb-1up"),null),
                new(new TASObjectiveInfo("5A-depths-cassette-fast2-seeded"),null),
                new(new TASObjectiveInfo("5A-unraveling-grabless-arb"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("gunraveling",17))}),
                new (enterB,null),
                new(new TASObjectiveInfo("5B-seekerstuns20"),null),
                new (rtmmenu,new TickAttempt[] {new TickAttempt(0,new Objective("15stuns",24)), new TickAttempt(0,new Objective("3A3Bsides",16))}),
                new (Right,null),
                new (Right,null),
                new (entersummit,null),
                new(new TASObjectiveInfo("7A-0m-gem-fast1"),null),
                new(new TASObjectiveInfo("7A-500m-gem"),null),
                new(new TASObjectiveInfo("7A-1000m-gem-arb"),null),
                new(new TASObjectiveInfo("7A-1500m-gem-cassette-arb-winged-1up", "Start", "ARB"),new TickAttempt[] {new TickAttempt(0,new Objective("arb1k",18))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("15in4",2)), new TickAttempt(0,new Objective("arb15k",9)), new TickAttempt(0,new Objective("4gems",11)), new TickAttempt(0,new Objective("7ac",20)), new TickAttempt(0,new Objective("1upin2",22))}),
                new (Right,null),
                new (entercore,null),
                new(new TASObjectiveInfo("8A-start-intothecore-switch", "Start", "Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0, new Objective("switch", 14))})
        ];
        #endregion T10

        #region T6
        RouteChange[] routesix = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-wingedgolden"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("wg",6))}),
                new (Left,null),
                new (entercp1N,null),
                new(new TASObjectiveInfo("1A-start-winged","Start","Winged"),null),
                new (rtm,null),
                new (entercp3,null),
                new(new TASObjectiveInfo("1A-chasm-cassette-fast4-nocollect-winged","RTM"),null),
                new (leaveagain,new TickAttempt[] {new TickAttempt(0,new Objective("1A c",23))}),
                new (enterB,null),
                new(new TASObjectiveInfo("1B-bino"),null),
                new (loadafromb,null),
                new (new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null), // tick 13
                new (restartchapter,null),
                new (new TASObjectiveInfo("2A-start-cassette-arb-seeded-1up-2up-3up","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("arb start",0)),new TickAttempt(0,new Objective("1up",14))}),
                new (enterB,null),
                new (new TASObjectiveInfo("2B-bino"),null),
                new (loadafromb,null),
                new (new TASObjectiveInfo("3A-start-fast5-winged"),null),
                new (new TASObjectiveInfo("3A-fromhugemess-letter"),null),
                new (rtmmenu,new TickAttempt[] {new TickAttempt(0,new Objective("letter",16))}),
                new (entercp3,null),
                new (new TASObjectiveInfo("3A-hugemess-btc-grabless-heart-winged", "RTM"),null),
                new (new TASObjectiveInfo("3A-shaft"),new TickAttempt[] { new TickAttempt(0, new Objective("3A blue", 12)), new TickAttempt(0, new Objective("grabless mess", 19)), new TickAttempt(0,new Objective("mess order",21)),new TickAttempt(0,new Objective("2b2r",24))}),
                new (new TASObjectiveInfo("3A-presidentialsuite-oshiro"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("oshiro",1))}),
                new (skipchapter,null),
                new (enter,null),
                new (new TASObjectiveInfo("5A-start-arb-1up"),null),
                new (new TASObjectiveInfo("5A-depths-cassette-winged","Start","Cassette"),new TickAttempt[] {new TickAttempt(0, new Objective("5A arb", 8)), new TickAttempt(0, new Objective("5A 1up", 7))}),
                new (rtmCassette,null),
                new (enterB,null),
                new (new TASObjectiveInfo("5B-bino"),null),
                new (loadafromb,null),
                new (new TASObjectiveInfo("6A-start"),null),
                new (new TASObjectiveInfo("6A-lake"),null),
                new (new TASObjectiveInfo("6A-fromhollows-heart-cassette"),null),
                new (rtmmenu,new TickAttempt[] {new TickAttempt(0, new Objective("3b3r", 2))}),
                new (entercp3,null),
                new (new TASObjectiveInfo("6A-hollows-bottom","RTM","Complete"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0, new Objective("bottom hollows", 9))}),
                new (Right,null),
                new (entersummit,null),
                new (new TASObjectiveInfo("7A-0m"),null),
                new (new TASObjectiveInfo("7A-500m-bino-arb-winged"),null),
                new (new TASObjectiveInfo("7A-1000m"),new TickAttempt[] {new TickAttempt(0, new Objective("5 in 5", 5)),new TickAttempt(0, new Objective("binos", 3)),new TickAttempt(0, new Objective("all 500m", 15)),new TickAttempt(0, new Objective("1 in 3", 17)),new TickAttempt(0, new Objective("2 in 3", 20))}),
                new (new TASObjectiveInfo("7A-1500m-cassette-fast7-winged"),null),
                new (new TASObjectiveInfo("7A-2000m-gem-fast6-winged"),new TickAttempt[] {new TickAttempt(0, new Objective("5c", 10)),new TickAttempt(0, new Objective("7c", 22))}),
                new (new TASObjectiveInfo("7A-2500m-gem","Start","Gem"),new TickAttempt[] {new TickAttempt(0, new Objective("20 in 7", 4)),new TickAttempt(0, new Objective("9 winged", 18))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0, new Objective("2k 2500 gem", 13))}),
                new (Right,null),
                new (entercore,null),
                new (new TASObjectiveInfo("8A-start-intothecore-switch","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0, new Objective("switch", 11))})
        ];
        #endregion T6
        #region T5
        RouteChange[] routefive = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-start-arb-winged-1up"),null),
                new(new TASObjectiveInfo("1A-crossing-heart-arb-1up"), new TickAttempt[] {new TickAttempt(0,new Objective("start arb",1))}),
                new(new TASObjectiveInfo("1A-chasm-grabless"),new TickAttempt[] {new TickAttempt(0,new Objective("crossing arb",17))}),
                new (leave, null),
                new (Left, null),
                new (entercp1N,null),
                new (new TASObjectiveInfo("1A-start-grabless"),null),
                new (new TASObjectiveInfo("1A-crossing-grabless"),null),
                new (rtm,null),
                new (Right,null),
                new (enter,null),
                new (new TASObjectiveInfo("2A-start-bino-arb-seeded-1up-2up-3up"),null),
                new (new TASObjectiveInfo("2A-intervention-fast5"),new TickAttempt[] {new TickAttempt(0,new Objective("start arb",10))}),
                new (new TASObjectiveInfo("2A-awake-grabless-arb-winged"),null),
                new (leave, null),
                new (Left, null),
                new (entercp1N,null),
                new (new TASObjectiveInfo("2A-start-grabless"),null),
                new (new TASObjectiveInfo("2A-intervention-grabless"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("2 grabless",6))}),
                new (Right,null),
                new (enter,null),
                new (new TASObjectiveInfo("3A-start-arb-winged"),null),
                new (new TASObjectiveInfo("3A-fromhugemess-enterpico"),null),
                new (new TASObjectiveInfo("pico","Start","OldSite"),null),
                new (leavepico,new TickAttempt[] {new TickAttempt(0,new Objective("pico site",8))}),
                new (rtm,null),
                new (entercp2,null),
                new (new TASObjectiveInfo("3A-hugemess-cbt-grabless", "RTM"),null),
                new (new TASObjectiveInfo("3A-shaft-cassette-arb","Start","ARB"),new TickAttempt[] {new TickAttempt(0,new Objective("grabless mess",3)),new TickAttempt(0,new Objective("order",12))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("15 in 3",14)),new TickAttempt(0,new Objective("arb elev",24))}),
                new (enterB,null),
                new (new TASObjectiveInfo("3B-bino","Start","Library"),null),
                new (new TASObjectiveInfo("3B-bino","Library"),new TickAttempt[] {new TickAttempt(0,new Objective("Library",20))}),
                new (loadafromb,null),
                new (new TASObjectiveInfo("4A-start-cassette-bino-fast6-seeded-1up"),null),
                new (new TASObjectiveInfo("4A-shrine-heart-bino-arb"),new TickAttempt[] {new TickAttempt(0,new Objective("1 up in 3",21))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("4A blue",16)),new TickAttempt(0,new Objective("15 in 4",19))}),
                new (enterB,null),
                new (new TASObjectiveInfo("4B-3bino"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("4B",9)),new TickAttempt(0,new Objective("8 bino",15)),new TickAttempt(0,new Objective("2 b sides",18)),new TickAttempt(0,new Objective("2 bino in 3",22))}),
                new (new TASObjectiveInfo("5A-start"),null),
                new (new TASObjectiveInfo("5A-depths-cassette-bino","Start","Bino"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("1 bino in 5",4))}),
                new (enterB,null),
                new (new TASObjectiveInfo("5B-2keys","Start","Key2"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("2 keys in 5B",5))}),
                new (skipchapter,null),
                new (enter,null),
                new (new TASObjectiveInfo("6A-start"),null),
                new (new TASObjectiveInfo("6A-lake"),null),
                new (new TASObjectiveInfo("6A-fromhollows-heart"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("6A blue",11))}),
                new (Right,null),
                new (entersummit,null),
                new (new TASObjectiveInfo("7A-0m-gem"),null),
                new (new TASObjectiveInfo("7A-500m-gem"),null),
                new (new TASObjectiveInfo("7A-1000m"),new TickAttempt[] {new TickAttempt(0,new Objective("2 gems",13))}),
                new (new TASObjectiveInfo("7A-1500m-cassette","Start","Cassette"),null),
                new (rtmCassette,new TickAttempt[] {new TickAttempt(0,new Objective("7 cassette",7))}),
                new (Right,null),
                new (entercore,null),
                new (new TASObjectiveInfo("8A-start-intothecore"),null),
                new (new TASObjectiveInfo("8A-hotandcold"),null),
                new (new TASObjectiveInfo("8A-heartofthemountain-heart-cassette","Start","Heart"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("8 blue",0)),new TickAttempt(0,new Objective("5 cassette",2)),new TickAttempt(0,new Objective("6 hearts",23))}),
                ];
        #endregion T5

        #region T4
        RouteChange[] routeone = [
            new (enter,null),
                new(new TASObjectiveInfo("1A-start-progress4-winged"),null),
                new(new TASObjectiveInfo("1A-crossing-theo"),null),
                new(new TASObjectiveInfo("1A-chasm-cassette-fast4-nocollect-winged"),null),
                new (leave,null),
                new (Left,null),
                new (enterB,null),
                new(new TASObjectiveInfo("1B"),null),
                new (loadafromb,null),
                new (new TASObjectiveInfo("2A-fromstart-heart","Start","Heart"),null), // tick 13
                new (restartchapter,new TickAttempt[] {new TickAttempt(0,new Objective("heart in 2A",13))}),
                new (new TASObjectiveInfo("2A-start-cassette-arb-seeded-1up-2up-3up"),null),
                new (new TASObjectiveInfo("2A-intervention-fast6"),new TickAttempt[] {new TickAttempt(0,new Objective("1up",22)),new TickAttempt(0,new Objective("arb",15))}),
                new (new TASObjectiveInfo("2A-awake-theo"),null),
                new (leave,null),
                new (Left,null),
                new (enterB,null),
                new (new TASObjectiveInfo("2B"),null),
                new (loadafromb,null),
                new (new TASObjectiveInfo("3A-start-fast5-winged"),null),
                new (new TASObjectiveInfo("3A-hugemess-cbt-fast5-winged"),new TickAttempt[] {new TickAttempt(0,new Objective("5 in 3",4))}),
                new (new TASObjectiveInfo("3A-shaft-theo","Start","Complete"),new TickAttempt[] {new TickAttempt(0,new Objective("2 winged in 2",6)),new TickAttempt(0,new Objective("mess order",16))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("3 theo",8))}),
                new (skipchapter,null),
                new (enter,null),
                new (new TASObjectiveInfo("4A-start-cassette"),null),
                new (new TASObjectiveInfo("4A-shrine-fast5"),new TickAttempt[] {new TickAttempt(0,new Objective("4a cassette",23))}),
                new (new TASObjectiveInfo("4A-oldtrail"),null),
                new (new TASObjectiveInfo("4A-cliffface-snowball-arb"),null),
                new (leave,new TickAttempt[] {new TickAttempt(0,new Objective("snowball",7)),new TickAttempt(0,new Objective("arb",14))}),
                new (enter,null),
                new (new TASObjectiveInfo("5A-start-theo"),null),
                new (new TASObjectiveInfo("5A-depths-grabless-heart-cassette-seeded"),new TickAttempt[] {new TickAttempt(0,new Objective("theo phone",0))}),
                new (new TASObjectiveInfo("5A-unraveling-grabless"),new TickAttempt[] {new TickAttempt(0,new Objective("depths grabless",24))}),
                new (new TASObjectiveInfo("5A-search-keys","Start","Key3"),new TickAttempt[] {new TickAttempt(0,new Objective("unraveling grabless",1))}),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("search keys",5))}),
                new (enterB,null),
                new (new TASObjectiveInfo("5B"),null),
                new (loadafromb,new TickAttempt[] {new TickAttempt(0,new Objective("5 AB heart",19)),new TickAttempt(0,new Objective("3B",21)),new TickAttempt(0,new Objective("3AB",3))}),
                new (new TASObjectiveInfo("6A-start"),null),
                new (new TASObjectiveInfo("6A-lake-grabless"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("grabless lake",10))}),
                new (Right,null),
                new (entersummit,null),
                new (new TASObjectiveInfo("7A-0m-gem-fast3"),null),
                new (new TASObjectiveInfo("7A-500m-gem-fast5-winged"),null),
                new (new TASObjectiveInfo("7A-1000m-gem-arb"),null),
                new (new TASObjectiveInfo("7A-1500m-gem-fast7-winged-1up"),new TickAttempt[] {new TickAttempt(100,new Objective("1k arb",2))}),
                new (new TASObjectiveInfo("7A-2000m-gem","Start","Gem"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("5 gems",11)), new TickAttempt(0, new Objective("20 berries", 9)), new TickAttempt(0, new Objective("65 berries", 18))}),
                new (Right,null),
                new (entercore,null),
                new (new TASObjectiveInfo("8A-start-intothecore-switch","Start","Collect"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("switch",12))}),
                new (Right,null),
                new (enterfarewell,null),
                new (new TASObjectiveInfo("9A-start-singular-bino"),null),
                new (new TASObjectiveInfo("9A-powersource-keys-3bino","DTS", "Key5DTS"),null),
                new (rtm,new TickAttempt[] {new TickAttempt(0,new Objective("keys",20)),new TickAttempt(0,new Objective("binos",17)),new TickAttempt(0,new Objective("6 hearts",23))})
        ];
        /*
         * Route Info for Test:
         * Berry total: 8+15+10+10+1+21=65
         * 1A                               A and B Side
         * start fast4 winged
         * crossing theo
         * chasm cassette fast4 winged
         * 1B
         * 
         * 2A 5 berries                     A and B Side
         * heart restart
         * start arb 1up cassette 9
         * int berry 6 berry
         * awake theo
         * 2B
         * 
         * 3A                     SKIP
         * start fast5 winged
         * cbt order fast5
         * theo elevator shaft
         * 
         * 4A 5 berries                     A Side
         * start cassette
         * shrine fast5
         * oldtrail
         * cliffface snowball arb
         * 
         * 5A:                              B Side
         * theo phone
         * grabless depths
         * grabless unraveling
         * search keys
         * 
         * depths heart cassette seeded
         * 5B
         * 
         * 6A grabless lake
         * 
         * 7A: (5 berries) 5 gem 2 winged [21 berries]
         * 0 gem fast3
         * 500 gem fast5 winged
         * 1000 gem arb
         * 1500 gem fast7 winged
         * 2000 gem
         * 
         * core
         * switch
         * 
         * farewell 5 binos 5 keys
         * */

        #endregion

    }
}
