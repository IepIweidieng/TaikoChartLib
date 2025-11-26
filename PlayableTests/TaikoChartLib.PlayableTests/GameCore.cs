using AudioLibrary.NET;
using AudioLibrary.NET.OpenAL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Input;

namespace TaikoChartLib.PlayableTests
{
    public class GameCore : Game
    {
        private static GameCore _app;
        public static GameCore app => _app;

        public static new GraphicsDevice GraphicsDevice { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static ISoundDevice SoundDevice { get; private set; }
        public static new ContentManager Content { get; private set; }
        public static GameTime GameTime { get; private set; }

        public GameCore()
        {
            _app = this;

            Graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = false,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Content = base.Content;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            InactiveSleepTime = TimeSpan.Zero;
        }

        protected override void Initialize()
        {
            SoundDevice = new SoundDeviceOpenAL();


            base.Initialize();

            GraphicsDevice = base.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void OnExiting(object sender, ExitingEventArgs args)
        {
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            InputManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {


            base.Draw(gameTime);
        }
    }
}
