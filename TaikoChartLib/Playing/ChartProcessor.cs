using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public delegate void AddedChip(PlayingChip playingChip);
    public delegate void TickedChip(PlayingChip playingChip);
    public delegate void OveredChip(PlayingChip playingChip);
    public delegate void HittedChip(PlayingChip playingChip);
    public delegate void JudgeChip(PlayingChip playingChip, JudgeType judge);

    public class ChartProcessor
    {
        public AddedChip AddedChip { get; set; }
        public TickedChip TickedChip { get; set; }
        public OveredChip OveredChip { get; set; }
        public HittedChip HitUpdatedChip { get; set; }
        public Action GoGoStarted { get; set; }
        public Action GoGoEnded { get; set; }
        public JudgeChip JudgeChip { get; set; }

        public HitRange HitRange { get; set; } = HitRange.Hard;
        public TimeSpan BigNoteQueueInterval { get; set; } = TimeSpan.FromSeconds(0.05);

        private ChipsData _chipsData;
        public ChipsData ChipsData
        {
            protected get => _chipsData;
            set
            {
                if (_chipsData != value)
                {
                    BPM = value.InitBPM;
                    Scroll = value.InitScroll;
                    Measure = new TCLVector2(4, 4);
                    Branch = BranchType.Normal;

                    for (int i = 0; i < value.Chips.Count; i++)
                    {
                        AddChip(i, value.Chips[i]);
                    }
                }
                _chipsData = value;
            }
        }

        public bool IsPlaying { get; protected set; }

        public float BPM { get; protected set; }
        public TCLVector2 Scroll { get; protected set; }
        public TCLVector2 Measure { get; set; }
        public bool IsGoGoTime { get; protected set; }
        public BranchType Branch { get; protected set; }

        private List<PlayingChip> playingChips = new List<PlayingChip>();
        private List<BigNoteQueue> bigNoteQueues = new List<BigNoteQueue>();

        public ChartProcessor()
        {

        }

        public virtual void Play()
        {
            IsPlaying = true;
        }

        public virtual void Pause()
        {
            IsPlaying = false;
        }

        public virtual void Reset()
        {
            playingChips.ForEach(x =>
            {
                x.IsOver = false;
                x.IsHit = false;
                x.IsMiss = false;
            });
        }

        public void AddChip(int index, Chip chip)
        {
            PlayingChip playingChip;
            if (Chip.IsRoll(chip.ChipType))
            {
                playingChip = new PlayingChipRoll(chip, index);
            }
            else
            {
                playingChip = new PlayingChip(chip, index);
            }

            playingChips.Add(playingChip);

            AddedChip?.Invoke(playingChip);
        }
        public void Tick(double time, TimeSpan delta)
        {
            PlayingChipRoll prevRoll = null;

            for (int i = 0; i < playingChips.Count; i++)
            {
                PlayingChip playingChip = playingChips[i];
                Chip chip = playingChip.Chip;

                playingChip.Time = chip.Params.Time - time;
                playingChip.Position = chip.Params.Scroll * (chip.Params.BPM * (float)playingChip.Time);

                if (playingChip is PlayingChipRoll playingChipRoll)
                {
                    prevRoll = playingChipRoll;
                }
                else if (chip.ChipType == ChipType.RollEnd)
                {
                    prevRoll.RollLength = playingChip.Position - prevRoll.Position;
                    prevRoll = null;
                }

                TickedChip?.Invoke(playingChip);

                if (IsPlaying)
                {
                    if (!playingChip.IsMiss && playingChip.Time < -HitRange.Miss)
                    {
                        ChipMissUpdate(playingChip);
                        playingChip.IsMiss = true;
                    }

                    if (!playingChip.IsOver && playingChip.Time < 0)
                    {
                        ChipOverUpdate(playingChip);
                        playingChip.IsOver = true;
                    }
                }
            }

            for (int i = 0; i < bigNoteQueues.Count; i++)
            {
                BigNoteQueue bigNoteQueue = bigNoteQueues[i];
                bigNoteQueue.Time += delta;

                if (bigNoteQueue.Time > BigNoteQueueInterval)
                {
                    HitProcess(bigNoteQueue.PlayingChip, HitRange.GetJudge(bigNoteQueue.HitTime), false);

                    bigNoteQueues.Remove(bigNoteQueue);
                    i--;
                }
            }
        }

        public void TryHit(HitType hitType, bool bigHittable)
        {
            for (int i = 0; i < bigNoteQueues.Count; i++)
            {
                BigNoteQueue bigNoteQueue = bigNoteQueues[i];
                if (Chip.IsHittable(bigNoteQueue.PlayingChip.Chip.ChipType, hitType))
                {
                    HitProcess(bigNoteQueue.PlayingChip, HitRange.GetJudge(bigNoteQueue.HitTime), true);
                    bigNoteQueues.Remove(bigNoteQueue);

                    return;
                }
            }

            foreach (PlayingChip playingChip in playingChips)
            {
                if (playingChip.IsMiss) continue;
                if (playingChip.IsHit) continue;
                if (playingChip.Time < -HitRange.Miss || playingChip.Time > HitRange.Miss) continue;
                if (!Chip.IsHittable(playingChip.Chip.ChipType, hitType)) continue;

                bool isBig = Chip.IsBig(playingChip.Chip.ChipType);

                if (bigHittable || !isBig)
                {
                    HitProcess(playingChip, HitRange.GetJudge(playingChip.Time), isBig);
                }
                else if (isBig)
                {
                    bigNoteQueues.Add(new BigNoteQueue(playingChip));
                }

                break;
            }

        }

        private void HitProcess(PlayingChip playingChip, JudgeType judgeType, bool isBig)
        {
            playingChip.IsHit = true;
            HitUpdatedChip?.Invoke(playingChip);
        }


        private void ChipOverUpdate(PlayingChip playingChip)
        {
            OveredChip?.Invoke(playingChip);

            switch (playingChip.Chip.ChipType)
            {
                case ChipType.BPMChange:
                    {
                        BPM = playingChip.Chip.Params.BPM;
                    }
                    break;
                case ChipType.Scroll:
                    {
                        Scroll = playingChip.Chip.Params.Scroll;
                    }
                    break;
                case ChipType.Measure:
                    {
                        Measure = playingChip.Chip.Params.Measure;
                    }
                    break;
                case ChipType.GoGoStart:
                    {
                        GoGoStarted?.Invoke();
                    }
                    break;
                case ChipType.GoGoEnd:
                    {
                        GoGoEnded?.Invoke();
                    }
                    break;
            }
        }

        private void ChipMissUpdate(PlayingChip playingChip)
        {
            switch (playingChip.Chip.ChipType)
            {
                case ChipType.Don:
                case ChipType.Ka:
                case ChipType.DonBig:
                case ChipType.KaBig:
                    if (!playingChip.IsHit)
                    {
                        JudgeChip?.Invoke(playingChip, JudgeType.Miss);
                    }
                    break;
            }
        }
    }
}
