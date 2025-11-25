using AudioLibrary.NET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib;
using TaikoChartLib.PlayableTests.Graphics;
using TaikoChartLib.PlayableTests.Playing;
using TaikoChartLib.Playing;
using TaikoChartLib.TJA;

namespace TaikoChartLib.PlayableTests.Play
{
    internal class PlayScene : SceneBase
    {
        private const float NotePadding = 960.0f * 0.0042f;

#nullable enable
        private TaikoChart? _taikoChart;
        public TaikoChart? TaikoChart
        {
            get => _taikoChart;
            set
            {
                _taikoChart = value;
            }
        }

        private ISound? music;
        private ISoundInstance? musicInstance;
        private ChartProcessor? processor;
#nullable restore

        private Conductor conductor = new Conductor();

        private Sprite target;
        private TextureRegion faceDon;
        private TextureRegion faceKa;
        private TextureRegion faceDonBig;
        private TextureRegion faceKaBig;

        private List<PlayChip> chips = new List<PlayChip>();
        private List<Sprite> notes = new List<Sprite>();

        private Matrix laneTranslation = Matrix.CreateTranslation(100, 100, 0);

        private void SetMusic(Stream stream)
        {
            musicInstance?.Dispose();
            musicInstance = null;
            music?.Dispose();
            music = null;

            using WaveStream waveStream = AudioFile.GetWaveStream(stream);
            music = GameCore.SoundDevice.CreateSound(waveStream);
            musicInstance = music.CreateInstance();
            conductor.Music = musicInstance;
            conductor.Time = TimeSpan.FromSeconds(-1);

            conductor.Play();
        }

        public void SetChart(string path)
        {
            if (TaikoChart is IDisposable disposable)
            {
                disposable.Dispose();
            }
            TaikoChart = TJATaikoChart.LoadFromFilePath(path);

            using Stream stream = TaikoChart.GetAudio();
            if (stream is not null)
            {
                SetMusic(stream);
            }

            processor = new TJAChartProcessor();
            processor.AddedChip += OnChipAddedChip;
            processor.TickedChip += OnChipTickChip;

            processor.ChipsData = TaikoChart.Courses[Difficulty.Extreme].ChipsDatas[StyleSide.Single];
        }
        
        public PlayScene()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D texture = GameCore.Content.Load<Texture2D>("Texture/Notes");
            target = new Sprite(CreateNoteTextureRegion(texture, 0));
            faceDon = CreateNoteTextureRegion(texture, 1);
            faceKa = CreateNoteTextureRegion(texture, 2);
            faceDonBig = CreateNoteTextureRegion(texture, 3);
            faceKaBig = CreateNoteTextureRegion(texture, 4);
        }

        public override void Exiting()
        {
            musicInstance?.Dispose();
            musicInstance = null;
            music?.Dispose();
            music = null;
        }

        public override void Update(GameTime gameTime)
        {
            conductor.Update();
            processor?.Tick(conductor.Time.TotalSeconds + TaikoChart.SongOffset);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, transformMatrix: laneTranslation);

            target.Draw(SpriteBatch);

            notes.ForEach(x =>
            {
                x.Draw(SpriteBatch);
            });


            SpriteBatch.End();
        }

        private TextureRegion CreateNoteTextureRegion(Texture2D texture, int shift)
        {
            int width = 128;
            int height = 128;

            return new TextureRegion(texture, new Rectangle(width * shift, 0, width, height));
        }

#nullable enable
        private Sprite? CreateNoteSprite(Chip chip)
#nullable restore
        {
            switch(chip.ChipType)
            {
                case ChipType.Don:
                    return new Sprite(faceDon);
                case ChipType.Ka:
                    return new Sprite(faceKa);
                case ChipType.DonBig:
                    return new Sprite(faceDonBig);
                case ChipType.KaBig:
                    return new Sprite(faceKaBig);
                default:
                    return null;
            }
        }

        private void OnChipAddedChip(object? sender, ChipAddArgs args)
        {
#nullable enable
            Sprite? sprite = CreateNoteSprite(args.Chip);
#nullable restore

            if (sprite is not null)
            {
                //sprite.DepthLayer = args.Index;
                notes.Insert(0, sprite);
            }

            PlayChip playChip = new PlayChip(args.Chip, sprite);
            chips.Add(playChip);
        }

        private void OnChipTickChip(ref int index, ref double time, ref Chip chip, ref TCLVector2 position)
        {
            PlayChip playChip = chips[index];
            if (playChip.Sprite is Sprite sprite)
            {
                sprite.Position = new Vector2(position.X, position.Y) * NotePadding;
            }
        }
    }
}
