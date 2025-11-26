using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.Playing;

namespace TaikoChartLib
{
    [Serializable]
    public class Chip
    {
        public static bool IsNote(ChipType chipType) => chipType >= ChipType.None && chipType < ChipType.NoteMax;
        public static bool IsHittable(ChipType chipType)
        {
            switch (chipType)
            {
                case ChipType.Don:
                case ChipType.Ka:
                case ChipType.DonBig:
                case ChipType.KaBig:
                    return true;
            }
            return false;
        }

        public static bool IsHittable(ChipType chipType, HitType hitType)
        {
            switch (chipType)
            {
                case ChipType.Don:
                case ChipType.DonBig:
                    return hitType == HitType.DonLeft || hitType == HitType.DonRight;
                case ChipType.Ka:
                case ChipType.KaBig:
                    return hitType == HitType.KaLeft || hitType == HitType.KaRight;
            }
            return false;
        }

        public static bool IsBig(ChipType chipType) => chipType == ChipType.DonBig || chipType == ChipType.KaBig || chipType == ChipType.RollBig;
        public static bool IsRoll(ChipType chipType) => 
            chipType == ChipType.Roll
            || chipType == ChipType.RollBig
            || chipType == ChipType.Balloon
            || chipType == ChipType.Kusudama;

        public ChipType ChipType { get; set; } = ChipType.None;
        public ChipParams Params { get; set; } = new ChipParams();
    }
}
