using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA
{
    public class TJAReader
    {
        public static readonly Regex RemoveComment = new Regex("( *//.*|\r)", RegexOptions.Multiline);
        public static readonly Regex SplitLineRegex = new Regex("\n");
        public static readonly Regex CommandSplitRegex = new Regex(",");


        static TJAReader()
        {
        }


        public static Difficulty GetDifficulty(string text)
        {
            text = text.ToLower();
            if (text.Contains("easy") || text.Contains("0"))
            {
                return Difficulty.Easy;
            }
            else if (text.Contains("normal") || text.Contains("1"))
            {
                return Difficulty.Normal;
            }
            else if (text.Contains("hard") || text.Contains("2"))
            {
                return Difficulty.Hard;
            }
            else if (text.Contains("oni") || text.Contains("3"))
            {
                return Difficulty.Extreme;
            }
            else if (text.Contains("edit") || text.Contains("4"))
            {
                return Difficulty.Extra;
            }

            return Difficulty.Easy;
        }

        public static TJATaikoChart LoadFromText(string text)
        {
            ParseState state = new ParseState();
            ParseCourseState courseState = new ParseCourseState();
            TJATaikoChart taikoChart = new TJATaikoChart();
            ParseChipsState chipsState = new ParseChipsState();

            text = RemoveComment.Replace(text, "");

            string[] lines = SplitLineRegex.Split(text);
            foreach (string line in lines)
            {
                if (line.Length == 0) continue;

                string[] headerSplited = line.Split(':');
                if (headerSplited.Length >= 2)
                {
                    string key = headerSplited[0];
                    string value = string.Join(null, headerSplited, 1, headerSplited.Length - 1);

                    ParseHeader(taikoChart, ref state, ref courseState, key, value);
                }
                else if (line[0] == '#')
                {
                    ParseCommand(taikoChart, ref state, ref courseState, ref chipsState, line);
                }
                else if (state.Loading)
                {
                    ParseChips(taikoChart, ref state, ref courseState, ref chipsState, line);
                }
            }

            return taikoChart;
        }

        public static TCLVector2 ParseComplex(string text)
        {
            if (float.TryParse(text, out float value))
            {
                return new TCLVector2(value, 0.0f);
            }

            return new TCLVector2(1.0f, 0.0f);
        }

        public static ChipType CharToChipType(char ch)
        {
            switch (ch)
            {
                case '1':
                    return ChipType.Don;
                case '2':
                    return ChipType.Ka;
                case '3':
                    return ChipType.DonBig;
                case '4':
                    return ChipType.KaBig;
                case '5':
                    return ChipType.Roll;
                case '6':
                    return ChipType.RollBig;
                case '7':
                    return ChipType.Balloon;
                case '8':
                    return ChipType.RollEnd;
                case '9':
                    return ChipType.Kusudama;
            }
            return ChipType.None;
        }

        private static void ParseHeader(TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, string key, string value)
        {
            switch(key)
            {
                case "TITLE":
                    Lang.SetValue(taikoChart.Title, Lang.Default, value);
                    break;
                case "TITLEEN":
                    Lang.SetValue(taikoChart.Title, Lang.En, value);
                    break;
                case "TITLEJA":
                    Lang.SetValue(taikoChart.Title, Lang.Ja, value);
                    break;
                case "TITLEES":
                    Lang.SetValue(taikoChart.Title, Lang.Es, value);
                    break;
                case "TITLEFR":
                    Lang.SetValue(taikoChart.Title, Lang.Fr, value);
                    break;
                case "TITLEZH":
                    Lang.SetValue(taikoChart.Title, Lang.Zh, value);
                    break;
                case "SUBTITLE":
                    Lang.SetValue(taikoChart.SubTitle, Lang.Default, value);
                    break;
                case "SUBTITLEEN":
                    Lang.SetValue(taikoChart.SubTitle, Lang.En, value);
                    break;
                case "SUBTITLEJA":
                    Lang.SetValue(taikoChart.SubTitle, Lang.Ja, value);
                    break;
                case "SUBTITLEES":
                    Lang.SetValue(taikoChart.SubTitle, Lang.Es, value);
                    break;
                case "SUBTITLEFR":
                    Lang.SetValue(taikoChart.SubTitle, Lang.Fr, value);
                    break;
                case "SUBTITLEZH":
                    Lang.SetValue(taikoChart.SubTitle, Lang.Zh, value);
                    break;
                case "MAKER":
                    foreach (var childValue in CommandSplitRegex.Split(value))
                    {
                        taikoChart.Charter.Add(childValue);
                    }
                    break;
                case "ARTIST":
                    foreach (var childValue in CommandSplitRegex.Split(value))
                    {
                        taikoChart.Artist.Add(childValue);
                    }
                    break;
                case "GENRE":
                    foreach (var childValue in CommandSplitRegex.Split(value))
                    {
                        taikoChart.Genre.Add(childValue);
                    }
                    break;
                case "AUTHOR":
                    taikoChart.Author = value;
                    break;
                case "WAVE":
                    taikoChart.SongFileName = value;
                    break;
                case "PREIMAGE":
                case "COVER":
                    taikoChart.JacketFileName = value;
                    break;
                case "TAIKOWEBSKIN":
                    break;
                case "SCENEPRESET":
                    taikoChart.ScenePreset = value;
                    break;
                case "TOWERTYPE":
                    taikoChart.TowerType = value;
                    break;
                case "SELECTBG":
                    taikoChart.SongSelectionBGFileName = value;
                    break;
                case "BGIMAGE":
                    taikoChart.BGImageFileName = value;
                    break;
                case "BGOFFSET":
                    if (float.TryParse(value, out float bgoffset))
                    {
                        taikoChart.BGImageOffset = bgoffset;
                    }
                    break;
                case "BGMOVIE":
                    taikoChart.BGMovieFileName = value;
                    break;
                case "MOVIEOFFSET":
                    if (float.TryParse(value, out float movieOffset))
                    {
                        taikoChart.BGMovieOffset = movieOffset;
                    }
                    break;
                case "OFFSET":
                    if (float.TryParse(value, out float offset))
                    {
                        taikoChart.SongOffset = offset;
                    }
                    break;
                case "DEMOSTART":
                    if (float.TryParse(value, out float demoStart))
                    {
                        taikoChart.SongPreviewOffset = demoStart;
                    }
                    break;
                case "SONGVOL":
                    if (int.TryParse(value, out int songVol))
                    {
                        taikoChart.SongVolume = songVol;
                    }
                    break;
                case "SEVOL":
                    if (int.TryParse(value, out int seVol))
                    {
                        taikoChart.SoundEffectVolume = seVol;
                    }
                    break;
                case "NOTESDESIGNER":
                case "NOTESDESIGNER1":
                case "NOTESDESIGNER2":
                case "NOTESDESIGNER3":
                case "NOTESDESIGNER4":
                    foreach (var childValue in CommandSplitRegex.Split(value))
                    {
                        state.Course.Charter.Add(childValue);
                    }
                    break;
                case "HEADSCROLL":
                    state.HeadScroll = ParseComplex(value);
                    break;
                case "SIDE":
                    break;
                case "SIDEREV":
                    break;
                case "COURSE":
                    {
                        Difficulty difficulty = GetDifficulty(value);

                        state.Course = new Course();
                        courseState = new ParseCourseState()
                        {
                            ChipsData = new ChipsData()
                        };

                        if (!taikoChart.Courses.ContainsKey(difficulty))
                        {
                            taikoChart.Courses.Add(difficulty, state.Course);
                        }
                    }
                    break;
                case "LEVEL":
                    if (int.TryParse(value, out int level))
                    {
                        state.Course.Level = level;
                    }
                    break;
                case "BPM":
                    if (float.TryParse(value, out float bpm))
                    {
                        state.BPM = bpm;
                    }
                    break;
                case "SCOREINIT":
                    if (int.TryParse(value, out int scoreInit))
                    {
                        courseState.ScoreInit = scoreInit;
                    }
                    break;
                case "SCOREDIFF":
                    if (int.TryParse(value, out int scoreDiff))
                    {
                        courseState.ScoreDiff = scoreDiff;
                    }
                    break;
                case "HIDDENBRANCH":
                    if (int.TryParse(value, out int hiddenBranch))
                    {
                        courseState.HiddenBranch = hiddenBranch >= 1;
                    }
                    break;
                case "TJACOMPAT":
                    if (value.StartsWith("jiro1"))
                    {
                        taikoChart.TJACompat = ~TJACompat.Jiro1;
                    }
                    else if (value.StartsWith("jiro2"))
                    {
                        taikoChart.TJACompat = ~TJACompat.Jiro2;
                    }
                    else if (value.StartsWith("tmg"))
                    {
                        taikoChart.TJACompat = ~TJACompat.TMG;
                    }
                    else if (value.StartsWith("tjap3"))
                    {
                        taikoChart.TJACompat = ~TJACompat.TJAP3;
                    }
                    else if (value.StartsWith("oos"))
                    {
                        taikoChart.TJACompat = ~TJACompat.OOS;
                    }
                    break;
            }
        }

        private static void ParseCommand(TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState, string text)
        {
            QueueChip queueChip = TJACommand.ParseCommand(text, taikoChart, ref state, ref courseState, ref chipsState);
            if (queueChip != null)
            {
                chipsState.ChipQueues.Add(queueChip);
            }
        }

        private static void ParseChips(TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState, string text)
        {
            foreach (char ch in text)
            {
                if (ch == ' ') continue;
                else if (ch == ',')
                {
                    int noteCount = chipsState.ChipQueues.Where(x => Chip.IsNote(x.ChipType)).Sum(x => 1);
                    if (noteCount == 0)
                    {
                        QueueChip queueChip = new QueueChip()
                        {
                            ChipType = ChipType.None
                        };
                        chipsState.ChipQueues.Add(queueChip);
                        noteCount++;
                    }

                    float step = 240f / noteCount;


                    foreach (QueueChip queueChip in chipsState.ChipQueues)
                    {
                        TJACommand.PostCommand(queueChip, taikoChart, ref state, ref courseState, ref chipsState);

                        Chip chip = new Chip()
                        {
                            ChipType = queueChip.ChipType,
                            Params = chipsState.CurrentParams.Copy()
                        };

                        if (Chip.IsNote(chip.ChipType))
                        {
                            chipsState.CurrentParams.Time += step * (chip.Params.Measure.X / chip.Params.Measure.Y) / chip.Params.BPM;
                        }

                        courseState.ChipsData.Chips.Add(chip);

                    }
                    chipsState.ChipQueues.Clear();
                }
                else
                {
                    QueueChip queueChip = new QueueChip()
                    {
                        ChipType = CharToChipType(ch)
                    };

                    chipsState.ChipQueues.Add(queueChip);
                }
            }
        }
    }
}
