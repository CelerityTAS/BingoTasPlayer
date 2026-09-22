using Celeste.Mod.BingoUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.BingoTasPlayer {
    internal class GameState {
        public GameState() { }

        //All of These are ripped straight out of BingoUI
        private List<string> WingedBerryIDList = new List<string> { "9c:2", "3b:2", "end_3c:13", "06-a:7", "13-b:31", "c-01:26", "b-21:99", "b-04:67", "d-10b:682", "e-09:398", "end:4" };
        private List<string> SeedBerryIDList = new List<string> { "d1:67", "a-10:13", "b-17:10", "e-12:504" };

        public int WingedBerries {
            get {
                List<AreaStats> areas = SaveData.Instance.Areas_Safe;
                int wingedBerryCount = 0;
                foreach (AreaStats myArea in areas) {
                    foreach (EntityID id in myArea.Modes[(int) AreaMode.Normal].Strawberries)
                        if (WingedBerryIDList.Contains(id.ToString()))
                            wingedBerryCount++;
                }
                return wingedBerryCount;
            }
        }

        public int SeededBerries {
            get {
                List<AreaStats> areas = SaveData.Instance.Areas_Safe;
                int seedBerryCount = 0;
                foreach (AreaStats myArea in areas) {
                    foreach (EntityID id in myArea.Modes[(int) AreaMode.Normal].Strawberries)
                        if (SeedBerryIDList.Contains(id.ToString()))
                            seedBerryCount++;
                }
                return seedBerryCount;
            }
        }

        public int Cassettes {
            get {
                List<AreaStats> areas = SaveData.Instance.Areas_Safe;
                int cassetteCount = 0;
                foreach (AreaStats myArea in areas)
                    if (myArea.Cassette)
                        cassetteCount++;
                return cassetteCount;
            }
        }

        public int BlueHearts {
            get {
                List<AreaStats> areas = SaveData.Instance.Areas_Safe;
                int blueHeartCount = 0;
                foreach (AreaStats myArea in areas)
                    if (myArea.Modes[(int) AreaMode.Normal].HeartGem)
                        blueHeartCount++;
                return blueHeartCount;
            }
        }

        public int RedHearts {
            get {
                List<AreaStats> areas = SaveData.Instance.Areas_Safe;
                int redHeartCount = 0;
                foreach (AreaStats myArea in areas)
                    if (myArea.Modes[(int) AreaMode.BSide].HeartGem)
                        redHeartCount++;
                return redHeartCount;
            }
        }

        public int Binoculars {
            get {
                return BingoModule.SaveData.BinocularsList.Count;
            }
        }

        public int SeekersHit {
            get {
                return BingoModule.SaveData.SeekersHit;
            }
        }

        public int OshiroHits {
            get {
                return BingoModule.SaveData.OshiroHits;
            }
        }

        public int SnowballHits {
            get {
                return BingoModule.SaveData.SnowballHits;
            }
        }

        public int Keys {
            get {
                return BingoModule.SaveData.KeysList.Count;
            }
        }
    }
}