using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;
using static System.Net.Mime.MediaTypeNames;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandStart : TJACommand
    {
        public CommandStart()
        {
        }

        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            StyleSide side = StyleSide.Single;
            if (text.EndsWith("P1"))
            {
                side = StyleSide.DoubleP1;
            }
            else if (text.EndsWith("P2"))
            {
                side = StyleSide.DoubleP2;
            }

            chipsState = new ParseChipsState()
            {
                CurrentParams = new ChipParams()
                {
                    BPM = state.BPM,
                    Scroll = state.HeadScroll
                }
            };

            courseState.ChipsData = new ChipsData()
            {
                InitBPM = state.BPM,
                InitScroll = state.HeadScroll
            };

            if (!state.Course.ChipsDatas.ContainsKey(side))
            {
                state.Course.ChipsDatas.Add(side, courseState.ChipsData);
            }

            state.Loading = true;

            return null;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
        }
    }
}
