using System;
using Microsoft.Xna.Framework;
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
        On.Celeste.Celeste.Update += On_Celeste_Update;
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        On.Celeste.Celeste.Update -= On_Celeste_Update;
        // TODO: unapply any hooks applied in Load()
    }


    private static string TASFilePath = Everest.PathEverest + "\\TmpBingoAIFiles\\" + "route.tas";

    //Path to get between TASFilePath and the GMBingoPlayer Repository as a relative path
    public static string GMBingoPlayerRepoRelativePath = "../GMBingoPlayer/";
    private static List<TASFileInfo> PlayedFiles = new();
    private static IBingoRouter router = new TASRouter();
    private static RouteChange route;
    public static void StartTas() {
        route = new RouteChange(new(GMBingoPlayerRepoRelativePath + "start.tas"), null);
        NextTas(route);
        Manager.DisableRun();
        Manager.Controller.FilePath = TASFilePath;
        Manager.EnableRun();
    }
    public static bool NextTas(RouteChange route) {
        TASFileInfo f = route.FilePath;
        PlayedFiles.Add(f);
        string texttowrite = "";
        foreach (TASFileInfo r in PlayedFiles) {
            texttowrite += "Read, " + r.path + ", " + r.startlabel + (r.endlabel == "" ? "" : (", " + r.endlabel)) + "\n";
        }
        File.WriteAllText(TASFilePath, texttowrite);
        Manager.Controller.ReadFile(TASFilePath);
        Manager.Controller.NeedsReload = true;
        return true;
    }
    public static bool ChangeTas(RouteChange route) {
        if (PlayedFiles.Count <= 0) return false;
        PlayedFiles[PlayedFiles.Count - 1] = route.FilePath;
        string texttowrite = "";
        foreach (TASFileInfo r in PlayedFiles) {
            texttowrite += "Read, " + r.path + ", " + r.startlabel + (r.endlabel == "" ? "" : (", " + r.endlabel)) + "\n";
        }
        File.WriteAllText(TASFilePath, texttowrite);
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
        if (recentBoard == null) return true;
        var board = BingoClient.BingoClient.Instance.GetBoard();
        string[] colors = new string[board.Count];
        for (int i = 0; i < board.Count; i++) {
            colors[i] = board[i].colors;
        }
        return !Enumerable.SequenceEqual(recentBoard, colors);
        //evallua return invokeMethod("Celeste.Mod.BingoTasPlayer.BingoTasPlayerModule","HasBoardChanged")
    }
    private static int lastTASOffset = 0;
    private static bool completedTicks = false;
    private static SortedDictionary<int, bool> results = new();

    private static void On_Celeste_Update(On.Celeste.Celeste.orig_Update orig, Celeste self, GameTime gameTime) {
        if (Settings.TestBind.Pressed) {
            if (!BingoClient.BingoClient.Instance.Connected) orig(self, gameTime);
            string[] objnamearr = GetBoard();
            Objective[] objarr = new Objective[objnamearr.Length];
            for (int i = 0; i < objnamearr.Length; i++) {

                objarr[i] = new Objective(objnamearr[i], i);
            }
            router.SetBoard(objarr);

            lastTASOffset = 0;
            completedTicks = false;
            results.Clear();
            PlayedFiles.Clear();
            StartTas();

        }
        if (route.TickAttempts != null) {
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
                    results.Clear();
                    completedTicks = false;
                }
            }
        }
        if (!Manager.Controller.CanPlayback && PlayedFiles.Count > 0) {
            RouteChange? newroute = null;
            // This is the completion logic, which only matters when the Tick logic didn't return anything.
            // This will be the final thing here, I just want to wait with implementing the Tick-Delays.
            if (newroute == null) {
                lastTASOffset = Manager.Controller.CurrentFrameInTas + 1;
                newroute = router.OnTasCompleted();
            }
            if (newroute != null) {
                Logger.Info("bingoai", newroute.Value.FilePath.ToString());
                NextTas(newroute.Value);
                results.Clear();
                completedTicks = false;
                route = newroute.Value;
            } else {
                Manager.DisableRun();
            }
        }
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
                    results.Clear();
                    completedTicks = false;
                }
            }

        }

        orig(self, gameTime);
    }

}