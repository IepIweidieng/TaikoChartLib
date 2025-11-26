using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Command;
using TaikoChartLib.TJA.Status;
using static System.Net.Mime.MediaTypeNames;

namespace TaikoChartLib.TJA
{
    internal abstract class TJACommand
    {
        internal static Dictionary<string, TJACommand> Commands = new Dictionary<string, TJACommand>();

        public static QueueChip ParseCommand(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            foreach (var item in Commands)
            {
                if (!text.StartsWith(item.Key)) continue;

                QueueChip queueChip = item.Value.Process(text.Substring(item.Key.Length), taikoChart, ref state, ref courseState, ref chipsState);
                if (queueChip == null) return null;

                queueChip.CommandName = item.Key;

                return queueChip;
            }
            return null;
        }

        public static void PostCommand(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            foreach (var item in Commands)
            {
                if (queueChip.CommandName != item.Key) continue;

                item.Value.PostProcess(queueChip, taikoChart, ref state, ref courseState, ref chipsState);
                break;
            }
        }

        static TJACommand()
        {
            Commands.Add("#START", new CommandStart());
            Commands.Add("#END", new CommandEnd());
            Commands.Add("#BPMCHANGE", new CommandBPMChange());
            Commands.Add("#SCROLL", new CommandScroll());
            Commands.Add("#MEASURE", new CommandMeasure());
            Commands.Add("#DELAY", new CommandDelay());
            Commands.Add("#GOGOSTART", new CommandGoGoStart());
            Commands.Add("#GOGOEND", new CommandGoGoEnd());
            Commands.Add("#DUMMYSTART", new CommandNone());
            Commands.Add("#DUMMYEND", new CommandNone());
            Commands.Add("#BARLINESCROLL", new CommandNone());
            Commands.Add("#HISPEED", new CommandNone());
            Commands.Add("#SPEED", new CommandNone());
            Commands.Add("#DIRECTION", new CommandNone());
            Commands.Add("#BARLINEOFF", new CommandNone());
            Commands.Add("#BARLINEON", new CommandNone());
            Commands.Add("#BARLINE", new CommandNone());
            Commands.Add("#JPOSSCROLL", new CommandNone());
            Commands.Add("#SUDDEN", new CommandNone());
            Commands.Add("#HIDDEN", new CommandNone());
            Commands.Add("#NOTESPAWN", new CommandNone());
            Commands.Add("#ENABLEDORON", new CommandNone());
            Commands.Add("#DISABLEDORON", new CommandNone());
            Commands.Add("#LYRIC", new CommandNone());
            Commands.Add("#SENOTECHANGE", new CommandNone());
            Commands.Add("#NOTESCHANGE", new CommandNone());
            Commands.Add("#BALLOON", new CommandNone());
            Commands.Add("#PARTNERNOTE", new CommandNone());
            Commands.Add("#GIANTNOTE", new CommandNone());
            Commands.Add("#NOTEIF", new CommandNone());
            Commands.Add("#COMMANDIFF", new CommandNone());
            Commands.Add("#SECTION", new CommandNone());
            Commands.Add("#LEVELHOLD", new CommandNone());
            Commands.Add("#LEVELREDIR", new CommandNone());
            Commands.Add("#BRANCHSTART", new CommandNone());
            Commands.Add("#BRANCHEND", new CommandNone());
            Commands.Add("#FROMNOR", new CommandNone());
            Commands.Add("#FROMEXP", new CommandNone());
            Commands.Add("#FROMMAS", new CommandNone());
            Commands.Add("#N", new CommandNone());
            Commands.Add("#E", new CommandNone());
            Commands.Add("#M", new CommandNone());
            Commands.Add("#GROUP", new CommandNone());
            Commands.Add("#IFSPAWN", new CommandNone());
            Commands.Add("#UNLESSSPAWN", new CommandNone());
            Commands.Add("#ELSEIFSPAWN", new CommandNone());
            Commands.Add("#ELSESPAWN", new CommandNone());
            Commands.Add("#IFSPAWNEND", new CommandNone());
            Commands.Add("#NEXTSONG", new CommandNone());
            Commands.Add("#GAMETYPE", new CommandNone());
            Commands.Add("#RESETCOMMAND", new CommandNone());
            Commands.Add("#SIZE", new CommandNone());
            Commands.Add("#BARLINESIZE", new CommandNone());
            Commands.Add("#ANGLE", new CommandNone());
            Commands.Add("#COLOR", new CommandNone());
            Commands.Add("#ALPHA", new CommandNone());
            Commands.Add("#GRADATION", new CommandNone());
            Commands.Add("#INCLUDE", new CommandNone());
            Commands.Add("#SPLITLANE", new CommandNone());
            Commands.Add("#MERGELANE", new CommandNone());
            Commands.Add("#ADDOBJECT", new CommandNone());
            Commands.Add("#REMOVEOBJECT", new CommandNone());
            Commands.Add("#OBJX", new CommandNone());
            Commands.Add("#OBJHMOVESTART", new CommandNone());
            Commands.Add("#OBJHMOVEEND", new CommandNone());
            Commands.Add("#OBJY", new CommandNone());
            Commands.Add("#OBJVMOVESTART", new CommandNone());
            Commands.Add("#OBJVMOVEEND", new CommandNone());
        }




        public TJACommand()
        {
        }

        public abstract QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState);
        public abstract void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState);
    }
}
