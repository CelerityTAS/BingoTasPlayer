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
            route = [
                new (enter,null),
                new(new TASObjectiveInfo("1A-start"),new TickAttempt[] {new TickAttempt(0,new Objective("wg",0))}),
                ];

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
            foreach (var item in route) Logger.Warn("bingotasAi", item.ToString());
        }
        private static List<RouteChange> route = new();
        private static RouteChange previous;
        private static bool changedRoute = false;
        public RouteChange? OnTasCompleted() {
            //Testing
            if (changedRoute) return null;


            if (route.Count == 0) return null;
            previous = route[0];
            route.RemoveAt(0);
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
            return null;
            changedRoute = true;
            previous.FilePath.endlabel = NextComment();
            return previous;
        }
    }
}
