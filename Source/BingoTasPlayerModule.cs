using System;
using Microsoft.Xna.Framework;
using TAS;
using TAS.Input;
using Celeste.Mod.BingoClient;

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

    public override void Load() {
        On.Celeste.Celeste.Update += On_Celeste_Update;
        // TODO: apply any hooks that should always be active
    }

    public override void Unload()
    {
        On.Celeste.Celeste.Update -= On_Celeste_Update;
        // TODO: unapply any hooks applied in Load()
    }

    private static int b;
    
    public static void RunTas(string filename)
    {
        Manager.DisableRun();
        Manager.Controller.FilePath = filename;
        Manager.EnableRun();
    }

    public static bool TryTick(int slot)
    {
        return true;
    }

    public static string[] GetBoard()
    {
        return null;
    }

    public static bool HasBoardChanged()
    {
        return false;
    }
    
    private static void On_Celeste_Update(On.Celeste.Celeste.orig_Update orig, Celeste self, GameTime gameTime)
    {
        if (Settings.TestBind.Pressed)
        {
            RunTas("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Celeste\\tas\\test.tas");
            b = 60;
        }

        if (b > 0)
        {
            if (Manager.CurrState == Manager.State.Disabled)
            {
                RunTas("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Celeste\\tas\\test2.tas");
                b = 0;
            }
        }
        
        orig(self, gameTime); 
    }
}