using AudioLibrary.NET;
using AudioLibrary.NET.OpenAL;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using TaikoChartLib.PlayableTests.Input;
using TaikoChartLib.PlayableTests.Play;
using TaikoChartLib.PlayableTests.Title;

namespace TaikoChartLib.PlayableTests
{
    public class Game1 : GameCore
    {
        static Game1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

#nullable enable
        private static SceneBase? currentScene;
#nullable restore

        internal static SceneBase ChangeScene(Func<SceneBase> sceneBuilder)
        {
            currentScene?.Dispose();
            currentScene = null;

            SceneBase scene = sceneBuilder.Invoke();
            currentScene = scene;

            GC.Collect();

            return scene;
        }

        internal static void GoToTitle() => ChangeScene(() => new TitleScene());
        internal static void GoToPlay(string path) => ChangeScene(() =>
        {
            PlayScene playScene = new PlayScene();
            playScene.SetChart(path);

            return playScene;
        });


        public Game1() : base()
        {
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            //post LoadContext


            GoToTitle();
        }

        protected override void LoadContent()
        {
        }

        protected override void OnExiting(object sender, ExitingEventArgs args)
        {
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            currentScene?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentScene?.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
