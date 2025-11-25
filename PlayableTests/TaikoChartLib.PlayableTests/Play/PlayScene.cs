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
        private TextureRegion faceRoll;
        private TextureRegion bodyRoll;
        private TextureRegion tailRoll;
        private TextureRegion faceRollBig;
        private TextureRegion bodyRollBig;
        private TextureRegion tailRollBig;
        private TextureRegion faceBalloon;
        private TextureRegion faceKusudama;

        private List<PlayChip> chips = new List<PlayChip>();
        private List<NoteSprite> notes = new List<NoteSprite>();

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
            faceRoll = CreateNoteTextureRegion(texture, 5);
            bodyRoll = CreateNoteTextureRegion(texture, 6);
            tailRoll = CreateNoteTextureRegion(texture, 7);
            faceRollBig = CreateNoteTextureRegion(texture, 8);
            bodyRollBig = CreateNoteTextureRegion(texture, 9);
            tailRollBig = CreateNoteTextureRegion(texture, 10);
            faceBalloon = CreateNoteTextureRegion(texture, 11, 2);
            faceKusudama = CreateNoteTextureRegion(texture, 13);
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

        private TextureRegion CreateNoteTextureRegion(Texture2D texture, int shift, int length = 1)
        {
            int width = 128 * length;
            int height = 128;

            return new TextureRegion(texture, new Rectangle(width * shift, 0, width, height));
        }

#nullable enable
        private NoteSprite? CreateNoteSprite(Chip chip)
#nullable restore
        {
            switch(chip.ChipType)
            {
                case ChipType.Don:
                    return new NoteSprite(faceDon);
                case ChipType.Ka:
                    return new NoteSprite(faceKa);
                case ChipType.DonBig:
                    return new NoteSprite(faceDonBig);
                case ChipType.KaBig:
                    return new NoteSprite(faceKaBig);
                case ChipType.Roll:
                    return new RollNoteSprite(faceRoll, bodyRoll, tailRoll);
                case ChipType.RollBig:
                    return new RollNoteSprite(faceRollBig, bodyRollBig, tailRollBig);
                case ChipType.Balloon:
                    return new NoteSprite(faceBalloon);
                case ChipType.Kusudama:
                    return new NoteSprite(faceKusudama);
                default:
                    return null;
            }
        }

        private void OnChipAddedChip(PlayingChip playingChip)
        {
#nullable enable
            NoteSprite? sprite = CreateNoteSprite(playingChip.Chip);
#nullable restore

            if (sprite is not null)
            {
                //sprite.DepthLayer = args.Index;
                notes.Insert(0, sprite);
            }

            PlayChip playChip = new PlayChip(playingChip, sprite);
            chips.Add(playChip);
        }

        private Vector2 GetCoord(TCLVector2 pos)
        {
            return new Vector2(pos.X, pos.Y) * NotePadding;
        }

        private void OnChipTickChip(PlayingChip playingChip)
        {
            PlayChip playChip = chips[playingChip.Index];

            Vector2 position = GetCoord(playChip.PlayingChip.Position);
            if (playChip.Sprite is NoteSprite sprite)
            {
                sprite.Position = GetCoord(playChip.PlayingChip.Position);
            }

            if (playingChip is PlayingChipRoll playingChipRoll && playChip.Sprite is RollNoteSprite rollNoteSprite)
            {
                Vector2 length = GetCoord(playingChipRoll.RollLength);
                rollNoteSprite.Length = length.Length();
            }
        }
    }
}
