using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.BingoTasPlayer
{
    public class TASFileInfo
    {
        public TASFileInfo(string path, string name, string startLabel, string endLabel="")
        {
            this.name = name;
            this.startlabel = startLabel;
            this.endlabel = endLabel;
            this.path = path;
        }
        public TASFileInfo(string path) : this(path, Path.GetFileName(path), "","") {}

        public string path;
        public string name;
        public string startlabel;
        public string endlabel;
        public bool hasJump => File.ReadLines(Path.Combine(Everest.PathEverest,"GMBingoPlayer",path)).Any((s) => s.ToLower().StartsWith("play"));
        public override string ToString()
        {
            return "name: " + name + " | " + "start: " + startlabel + (endlabel == "" ? "" : " | end: " + endlabel + " ["+(hasJump?"J":" ")+"]");
        }

        public static void Validate(TASFileInfo info) {

            string path = Path.Combine(Everest.PathEverest, "GMBingoPlayer", info.path);
            if (!File.Exists(path)) {
                throw new Exception("File "+info.name+" does not exist");
            }
            if (info.startlabel != "" && !File.ReadLines(path).Any(s => s.StartsWith("#" + info.startlabel))) {
                throw new Exception("Start Label " + info.startlabel + " does not exist in " + info.name);
            }
            if (info.endlabel != "" && !File.ReadLines(path).Any(s => s == "#" + info.endlabel)) {
                throw new Exception("End Label " + info.endlabel+ " does not exist in " + info.name);
            }
        }
    }
    public class TASObjectiveInfo : TASFileInfo
    {
        public TASObjectiveInfo(string name, string startlabel = "Start", string endlabel = "") : base("../GMBingoPlayer/objective repository/" + name + ".tas", name, startlabel, endlabel)
        {

        }

    }
}
