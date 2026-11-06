using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using TAS.Input.Commands;

namespace Celeste.Mod.BingoTasPlayer {
    internal class DataCreator {

        public DataCreator() { }
        RouteChange? previous = null;
        List<JSONData> jsondata = new List<JSONData>();
        List<RouteChange> toTest;
        int count = 100;
        public RouteChange? nextTAS() {
            if (previous == null) {
                string text = File.ReadAllText(Path.Combine(Everest.PathEverest, "TmpBingoAIFiles", "BerryData.json"));
                jsondata = JsonSerializer.Deserialize<List<JSONData>>(text);

                List<string> objectives = Directory.EnumerateFiles(Path.Combine(Everest.PathEverest, "GMBingoPlayer", "objective repository")).ToList();
                objectives = objectives.Select(v => Path.GetFileName(Path.GetFullPath(v))).ToList();
                objectives = objectives.Select(v => v.Remove(v.Length - 4)).ToList();
                objectives = objectives.FindAll(v => v.Contains("fast") || v.Contains("progress"));
                objectives = objectives.FindAll(v => !jsondata.Any(c => c.filename == v));
                toTest = objectives.Select(v => new RouteChange(new TASObjectiveInfo(v, "0"), null)).ToList();
                previous = toTest[0];
                Logger.Warn("bingoai", previous.ToString());
                toTest.RemoveAt(0);
                count = 100;
                return previous;
            }
            string filename = previous.Value.FilePath.name;
            int chapterIndex = 0;
            // 2B exit thinks it's on 3A
            switch (previous.Value.FilePath.name.Substring(0, 2)) {
                case "st":
                case "1A":
                case "1B":
                    chapterIndex = 1;
                    break;
                case "2A":
                case "2B":
                    chapterIndex = 2;
                    break;
                case "3A":
                case "3B":
                case "pi":
                case "le":
                    chapterIndex = 3;
                    break;
                case "4A":
                case "4B":
                    chapterIndex = 4;
                    break;
                case "5A":
                case "5B":
                    chapterIndex = 5;
                    break;
                case "6A":
                case "6B":
                    chapterIndex = 6;
                    break;
                case "7A":
                case "7B":
                    chapterIndex = 7;
                    break;
                case "8A":
                case "8B":
                    chapterIndex = 9;
                    break;
                case "Ep":
                    chapterIndex = 8;
                    break;
                case "9A":
                    chapterIndex = 10;
                    break;
                default:
                    Logger.Error("BingoAI", "How did you get to Chapter " + previous.Value.FilePath.name);
                    break;
            }
            int checkpointIndex = 0;
            if (filename.Contains("start") || filename.Contains("0m")) checkpointIndex = 0;
            string[] seccheckpoints = new string[] { "crossing", "intervention", "hugemess", "shrine", "depths", "500m", "intothecore" };
            string[] thirdcheckpoints = new string[] { "chasm", "awake", "shaft", "oldtrail", "unravelling", "1000m", "hotandcold" };
            string[] fourthcheckpoints = new string[] { "presidentialsuite", "cliffface", "search", "1500m", "heartofthemountain" };
            string[] fithcheckpoints = new string[] { "rescue", "2000m" };
            string[] sixthcheckpoints = new string[] { "2500m" };
            string[] seventh = new string[] { "3000m" };

            if (seccheckpoints.Any(v => filename.Contains(v))) checkpointIndex = 1;
            if (thirdcheckpoints.Any(v => filename.Contains(v))) checkpointIndex = 2;
            if (fourthcheckpoints.Any(v => filename.Contains(v))) checkpointIndex = 3;
            if (fithcheckpoints.Any(v => filename.Contains(v))) checkpointIndex = 4;
            if (sixthcheckpoints.Any(v => filename.Contains(v))) checkpointIndex = 5;
            if (seventh.Any(v => filename.Contains(v))) checkpointIndex = 6;

            int num = 0;
            if (checkpointIndex == 0) {
                num = AreaData.Areas[chapterIndex].Mode[0].StartStrawberries;
            } else {
                num = AreaData.Areas[chapterIndex].Mode[0].Checkpoints[checkpointIndex - 1].Strawberries;
            }
            bool[] array = new bool[num];
            List<int> berriesgotten = new List<int>();
            List<EntityID> berryIDsgotten = new List<EntityID>();
            foreach (EntityID entityID in SaveData.Instance.Areas[chapterIndex].Modes[0].Strawberries) {
                for (int i = 0; i < num; i++) {
                    EntityData entityData = AreaData.Areas[chapterIndex].Mode[0].StrawberriesByCheckpoint[checkpointIndex, i];
                    if (entityData != null && entityData.Level.Name == entityID.Level && entityData.ID == entityID.ID) {
                        array[i] = true;
                        berriesgotten.Add(i + 1);
                        berryIDsgotten.Add(entityID);
                    }
                }
            }

            JSONData data = new JSONData(previous.Value.FilePath.name, chapterIndex, checkpointIndex, num, berryIDsgotten.Select(v => v.Key).ToArray(), berriesgotten.ToArray());
            jsondata.Add(data);
            SaveData.Instance.Assists.NoGrabbing = false;

            if (toTest.Count > 0 && count > 0) {
                //string[] leavecheckpoints = { "1A-chasm", "1A-wingedgolden", "2A-awake", "3A-presidentialsuite", "4A-cliffface", "5A-rescue", "resolution", "7A-3000m", "8A-heartofthemountain" };
                //if (!justleft && leavecheckpoints.Any(cp => previous.Value.FilePath.name.Contains(cp))) {
                //    justleft = true;
                //    return new RouteChange(new TASFileInfo(BingoTasPlayerModule.GMBingoPlayerRepoRelativePath + "LeaveChapter.tas", "LeaveChapter", "", ""), null);
                //}
                previous = toTest[0];
                toTest.RemoveAt(0);
                count--;

                File.WriteAllText(Path.Combine(Everest.PathEverest, "TmpBingoAIFiles", "BerryData.json"), JsonSerializer.Serialize(jsondata));
                return previous;
            } else {
                File.WriteAllText(Path.Combine(Everest.PathEverest, "TmpBingoAIFiles", "BerryData.json"), JsonSerializer.Serialize(jsondata));
                return null;
            }
        }
        private struct JSONData(string filename, int chapterIndex, int checkpointIndex, int maxBerries, string[] berrykeys, int[] berrynumbers) {
            [JsonInclude]
            public string filename = filename;
            [JsonInclude]
            public int chapterIndex = chapterIndex;
            [JsonInclude]
            public int checkpointIndex = checkpointIndex;
            [JsonInclude]
            public int maxBerries = maxBerries;
            [JsonInclude]
            public string[] berrykeys = berrykeys;
            [JsonInclude]
            public int[] berrynumbers = berrynumbers;
            public override string ToString() {
                string s = filename + " collects " + berrynumbers.Length + "/" + maxBerries + "Berries: ";
                berrynumbers.ToList().ForEach(f => s += " " + f);
                return s;
            }
        }
    }
}
