using Celeste.Mod.BingoClient;
using IL.Monocle;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using TAS;
using TAS.Input;
using Celeste.Mod.BingoClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Monocle;

namespace Celeste.Mod.BingoTasPlayer;

public class BingoTasPlayerModule : EverestModule {
    public static BingoTasPlayerModule Instance { get; private set; }

    public override Type SettingsType => typeof(BingoTasPlayerModuleSettings);
    public static BingoTasPlayerModuleSettings Settings => (BingoTasPlayerModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(BingoTasPlayerModuleSession);
    public static BingoTasPlayerModuleSession Session => (BingoTasPlayerModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(BingoTasPlayerModuleSaveData);
    public static BingoTasPlayerModuleSaveData SaveData => (BingoTasPlayerModuleSaveData) Instance._SaveData;

    public BingoTasPlayerModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(BingoTasPlayerModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(BingoTasPlayerModule), LogLevel.Info);
#endif
    }
    private static string[] recentBoard;
    public override void Load() {
        if (!Directory.Exists(Everest.PathEverest + "\\TmpBingoAIFiles\\")) {
            Directory.CreateDirectory(Everest.PathEverest + "\\TmpBingoAIFiles\\");
        }
        BingoClient.BingoClient bc = BingoClient.BingoClient.Instance;
        Type BC = bc.GetType();
        rendermenu = BC.GetMethod("RenderMenu", BindingFlags.Instance | BindingFlags.NonPublic);
        On.Celeste.Celeste.Update += On_Celeste_Update;
        On.Monocle.Engine.RenderCore += this.Render;
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        On.Celeste.Celeste.Update -= On_Celeste_Update;
        On.Monocle.Engine.RenderCore -= this.Render;
        // TODO: unapply any hooks applied in Load()
    }


    private static string TASFilePath = Everest.PathEverest + "\\TmpBingoAIFiles\\" + "route.tas";

    //Path to get between TASFilePath and the GMBingoPlayer Repository as a relative path
    public static string GMBingoPlayerRepoRelativePath = "../GMBingoPlayer/";
    private static List<TASFileInfo> PlayedFiles = new();
    private static IBingoRouter router = new TASRouter();
    private static RouteChange route;
    public static void StartTas() {
        route = new RouteChange(new(GMBingoPlayerRepoRelativePath + "start.tas", "start", "0"), null);
        NextTas(route);
        Manager.DisableRun();
        Manager.Controller.FilePath = TASFilePath;
        Manager.EnableRun();
    }
    public static bool NextTas(RouteChange route) {
        TASFileInfo f = route.FilePath;
        PlayedFiles.Add(f);
        string texttowrite = "";
        int numberstart = cummplayfixnumber;
        while (numberstart > 0) {
            if (numberstart >= 10000) {
                numberstart -= 9999;
                texttowrite += "9999\n";
            } else {
                texttowrite += numberstart + "\n";
                numberstart = 0;
            }
        }
        foreach (TASFileInfo r in PlayedFiles) {
            texttowrite += "Read, " + r.path + (r.startlabel == "" ? "" : ", ") + r.startlabel + ((r.startlabel == "" || r.endlabel == "") ? "" : (", " + r.endlabel)) + "\n";
        }
        File.WriteAllText(TASFilePath, texttowrite);
        filehasplayfix = route.FilePath.hasJump && route.FilePath.startlabel!="Start"&&route.FilePath.endlabel!="";
        Manager.Controller.ReadFile(TASFilePath);
        Manager.Controller.NeedsReload = true;
        return true;
    }
    public static bool ChangeTas(RouteChange route) {
        if (PlayedFiles.Count <= 0) return false;
        PlayedFiles[PlayedFiles.Count - 1] = route.FilePath;
        string texttowrite = "";
        int numberstart = cummplayfixnumber;
        while (numberstart > 0) {
            if (numberstart >= 10000) {
                numberstart -= 9999;
                texttowrite += "9999\n";
            } else {
                texttowrite += numberstart + "\n";
                numberstart = 0;
            }
        }
        foreach (TASFileInfo r in PlayedFiles) {
            texttowrite += "Read, " + r.path + ", " + r.startlabel + (r.endlabel == "" ? "" : (", " + r.endlabel)) + "\n";
        }
        File.WriteAllText(TASFilePath, texttowrite);
        filehasplayfix = route.FilePath.hasJump;
        Manager.Controller.ReadFile(TASFilePath);
        Manager.Controller.NeedsReload = true;
        return true;
    }
    public static bool TryTick(int slot) {
        if (!BingoClient.BingoClient.Instance.Connected) return false;
        if (BingoClient.BingoClient.Instance.GetObjectiveStatus(slot) == ObjectiveStatus.Claimed) {
            return false;
        }
        BingoClient.BingoClient.Instance.SendClaim(slot);
        //BingoClient.BingoClient.Instance.SendClear(slot); we will not clear
        return true;
        //evallua return invokeMethod("Celeste.Mod.BingoTasPlayer.BingoTasPlayerModule","TryTick",2)
    }

    public static string[] GetBoard() {
        List<BingoClient.BingoClient.SquareMsg> boardlist = BingoClient.BingoClient.Instance.GetBoard();
        string[] board = new string[boardlist.Count];
        if (recentBoard == null) recentBoard = new string[boardlist.Count];
        for (int i = 0; i < boardlist.Count; i++) {
            board[i] = boardlist[i].name;
            recentBoard[i] = boardlist[i].colors;
        }
        return board;
        //evallua return invokeMethod("Celeste.Mod.BingoTasPlayer.BingoTasPlayerModule","GetBoard")
    }

    public static bool HasBoardChanged() {
        if (!BingoClient.BingoClient.Instance.Connected) { return false; }
        if (recentBoard == null) return true;
        for (int i = 0; i < 25; i++) {
            // if our recentboard and the board bingoclient have differ in claimed objectives
            if ((BingoClient.BingoClient.Instance.GetObjectiveStatus(i) == ObjectiveStatus.Claimed && recentBoard[i] == "blank") || (BingoClient.BingoClient.Instance.GetObjectiveStatus(i) != ObjectiveStatus.Claimed && (recentBoard[i] != "blank" && recentBoard[i] != "" && recentBoard[i] != null))) return true;
        }
        return false;
        //evallua return invokeMethod("Celeste.Mod.BingoTasPlayer.BingoTasPlayerModule","HasBoardChanged")
    }
    public static int lastTASOffset = 0;
    public static int frameInTAS => Manager.Controller.CurrentFrameInTas - lastTASOffset;
    private static bool completedTicks = false;
    private static bool filehasplayfix = false;
    private static int playfixnumber = 0;
    private static int cummplayfixnumber = 0;
    private static SortedDictionary<int, bool> results = new();
    private static MethodInfo rendermenu;
    private static List<Tuple<int, int>> menushoudopen = new();
    private static int changedtas = 0;

    private void Render(On.Monocle.Engine.orig_RenderCore orig, Monocle.Engine self) {
        orig(self);
        //menushoudopen.Count > 0 && menushoudopen.Any((t) => t.Item1 < Manager.Controller.CurrentFrameInTas - lastTASOffset && t.Item2 > Manager.Controller.CurrentFrameInTas - lastTASOffset)
        if (menushoudopen.Count > 0 && menushoudopen.Any((t) => t.Item1 < Manager.Controller.CurrentFrameInTas - lastTASOffset && t.Item2 > Manager.Controller.CurrentFrameInTas - lastTASOffset)) {
            BingoClient.BingoClient.Instance.MenuTriggered = true;
            rendermenu.Invoke(BingoClient.BingoClient.Instance, []);
        }
    }
    private static void On_Celeste_Update(On.Celeste.Celeste.orig_Update orig, Celeste self, GameTime gameTime) {
        // Logic for Starting / resetting
        if (Settings.StartBind.Pressed && !Manager.Running) {
            router = new TASRouter();
            if (!BingoClient.BingoClient.Instance.Connected) {
                orig(self, gameTime);
            } else {
                string[] objnamearr = GetBoard();
                Objective[] objarr = new Objective[objnamearr.Length];
                for (int i = 0; i < objnamearr.Length; i++) {

                    objarr[i] = new Objective(objnamearr[i], i);
                }
                router.SetBoard(objarr);
            }

            lastTASOffset = 0;
            completedTicks = false;
            results.Clear();
            PlayedFiles.Clear();
            StartTas();
            filehasplayfix = false;
        }
        // Logic for "Route has Ticks it needs to do"
        if (route.TickAttempts != null && !completedTicks) {
            Objective[] objectives = new Objective[route.TickAttempts.Length];

            for (int i = 0; i < route.TickAttempts.Length; i++) {
                TickAttempt v = route.TickAttempts[i];
                if (Manager.Controller.CurrentFrameInTas > lastTASOffset + v.Delay && !results.ContainsKey(i)) {
                    results[i] = TryTick(v.Objective.index);
                }
            }
            if (results.Count == route.TickAttempts.Length && !completedTicks) {
                Logger.Info("bingoai", "All ticks returned");
                completedTicks = true;
                RouteChange? r = router.TickAttemptResult(objectives, results.Values.ToArray());
                if (r != null) {
                    route = r.Value;
                    ChangeTas(r.Value);
                }
            }
        }



        // Logic for the board having changed
        if (BingoClient.BingoClient.Instance.Connected && HasBoardChanged() && Manager.Running) {
            Logger.Info("bingoai", "Board Changed");
            var board = BingoClient.BingoClient.Instance.GetBoard();
            if (recentBoard == null) recentBoard = new string[board.Count];
            string[] colors = new string[board.Count];
            List<Objective> changes = new();
            for (int i = 0; i < board.Count; i++) {
                if (board[i].colors != recentBoard[i]) {
                    changes.Add(new(board[i].name, i));
                }
                recentBoard[i] = board[i].colors;
            }
            if (changes.Count < board.Count) {
                RouteChange? r = router.OnTickevent(changes.ToArray());
                if (r != null) {
                    route = r.Value;
                    ChangeTas(r.Value);
                }
            }
        }
        if (changedtas > 1) {
            route.TickAttempts = router.GetTickAttempts();
            if (route.TickAttempts != null) {
                foreach (var t in route.TickAttempts) {
                    menushoudopen.Add(new(t.Delay - 30, t.Delay + 60));
                }
            }
            changedtas = 0;

            // implement the getting of relevant time here, since the TAS tools will have updated to be in the right file
            if (filehasplayfix) {
                KeyValuePair<int, List<Comment>> p = Manager.Controller.Comments.First(p => {
                    return p.Key > Manager.Controller.CurrentFrameInTas && p.Value.Any(com => com.Text == route.FilePath.endlabel && Path.GetFullPath(com.FilePath).SequenceEqual(Path.GetFullPath("./GMBingoPlayer/" + route.FilePath.path)));
                });
                playfixnumber = p.Value.Last().Frame - BingoTasPlayerModule.lastTASOffset;
            } else {
                playfixnumber = 0;
            }
        }
        if (changedtas == 1) changedtas++;

        if (playfixnumber != 0 && playfixnumber <= Manager.Controller.CurrentFrameInTas - lastTASOffset) {
            // This is all the frames that are not supposed to happen:
            // we want to reset the tas file to no longer have the RTM problem
            PlayedFiles.Clear();
            cummplayfixnumber = Manager.Controller.CurrentFrameInTas;
            Manager.Controller.NeedsReload = true;
        }

        // Logic for completed Files
        // Might want to restart the tas to deal with in checkpoint stops
        if ((!Manager.Controller.CanPlayback && PlayedFiles.Count > 0 )|| (playfixnumber != 0 && playfixnumber <= Manager.Controller.CurrentFrameInTas - lastTASOffset)) {
            lastTASOffset = Manager.Controller.CurrentFrameInTas + 1;
            RouteChange? newroute = router.OnTasCompleted();
            TASRouter.unlockedChapters();

            if (newroute != null) {
                changedtas = 1;
                List<Tuple<int, int>> shouldremain = menushoudopen.FindAll(t => t.Item2 >= Manager.Controller.Inputs.Count - 60).Select(v => new Tuple<int, int>(0, 60)).ToList();
                menushoudopen.Clear();
                menushoudopen.AddRange(shouldremain);

                Logger.Info("bingoai", newroute.Value.FilePath.ToString());
                NextTas(newroute.Value);
                results.Clear();
                completedTicks = false;
                route = newroute.Value;
            } else {
                Manager.DisableRun();
            }
        }


        orig(self, gameTime);
    }

}