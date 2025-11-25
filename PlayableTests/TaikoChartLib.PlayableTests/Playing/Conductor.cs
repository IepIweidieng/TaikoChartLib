using AudioLibrary.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaikoChartLib.PlayableTests.Playing
{
    internal class Conductor
    {
        private static readonly TimeSpan DelayOffsettingInterval = TimeSpan.FromSeconds(1);

#nullable enable
        public ISoundInstance? Music { get; set; }
#nullable restore

        public float Offset { get; set; }

        public TimeSpan SystemTime
        {
            get
            {
                if (isPlaying)
                {
                    return GameCore.GameTime.TotalGameTime - beginTime;
                }
                else
                {
                    return stoppedTime;
                }
            }
            private set
            {
                stoppedTime = value;
                beginTime = GameCore.GameTime.TotalGameTime - value;
            }
        }

        public TimeSpan Time
        {
            get
            {
                return SystemTime + musicDelayOffset;
            }
            set
            {
                double targetMusicTime = value.TotalSeconds;
                if (Music is not null)
                {
                    if (targetMusicTime >= 0 && targetMusicTime < Music.Sound.Length)
                    {
                        Music.Time = targetMusicTime;
                    }
                    else
                    {
                        Music.Stop();
                    }
                }

                SystemTime = value;
            }
        }

        private double systemMusicTime => SystemTime.TotalSeconds + Offset;
        private bool isPlaying;
        private bool isMusicPlaying;

        private TimeSpan beginTime = GameCore.GameTime.TotalGameTime;
        private TimeSpan stoppedTime = TimeSpan.Zero;
        private TimeSpan musicDelayOffset;
        private TimeSpan delayOffsettingCounter;

        public void Play()
        {
            if (isPlaying) return;

            Time = stoppedTime;
            isPlaying = true;
        }

        public void Stop()
        {
            if (!isPlaying) return;

            stoppedTime = Time;

            isPlaying = false;
            isMusicPlaying = false;
        }

        public void Update()
        {
            if (!isPlaying) return;

            MusicUpdate();

            /*
            if (SoundInstance is not null && SoundInstance.Playing)
            {
                Time = TimeSpan.FromSeconds(SoundInstance.Time + Offset);
            }
            else
            {
                
            }*/
        }

        private void MusicUpdate()
        {
            if (Music is null) return;

            if (Music.Playing)
            {
                MusicDelayOffsetting();
            }

            if (!isMusicPlaying)
            {
                double targetTime = systemMusicTime;
                if (targetTime >= 0 && targetTime < Music.Sound.Length)
                {
                    Music.Time = targetTime;
                    Music.Play();

                    isMusicPlaying = true;
                }
            }
        }

        private void MusicDelayOffsetting()
        {
            delayOffsettingCounter += GameCore.GameTime.ElapsedGameTime;

            if (delayOffsettingCounter > DelayOffsettingInterval)
            {
                TimeSpan musicTime = TimeSpan.FromSeconds(Music.Time);
                musicDelayOffset = musicTime - SystemTime;

                delayOffsettingCounter = TimeSpan.Zero;
            }
        }
    }
}
