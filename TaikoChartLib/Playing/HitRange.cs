using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    [Serializable]
    public struct HitRange
    {
        public static HitRange FromMs(float perfect, float ok, float miss)
        {
            return new HitRange()
            {
                Perfect = perfect * 0.001f,
                Ok = ok * 0.001f,
                Miss = miss * 0.001f
            };
        }

        public static readonly HitRange Hard = FromMs(25.025f, 75.075f, 108.442f);
        public static readonly HitRange Easy = FromMs(41.708f, 108.442f, 125.125f);

        public float Perfect { get; set; }
        public float Ok { get; set; }
        public float Miss { get; set; }

        public JudgeType GetJudge(double time)
        {
            double absTime = Math.Abs(time);
            if (absTime < Perfect) return JudgeType.Perfect;
            else if (absTime < Ok) return JudgeType.Ok;
            else return JudgeType.Miss;
        }
    }
}
