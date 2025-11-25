using AudioLibrary.NET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests
{
    internal abstract class SceneBase : IDisposable
    {
        public static GraphicsDevice GraphicsDevice => GameCore.GraphicsDevice;
        public static GraphicsDeviceManager Graphics => GameCore.Graphics;
        public static SpriteBatch SpriteBatch => GameCore.SpriteBatch;
        public static ISoundDevice SoundDevice => GameCore.SoundDevice;

        public SceneBase()
        {
            Initialize();
        }

        public void Dispose()
        {
            Exiting();
        }

        public virtual void Initialize()
        {
            LoadContent();
        }

        protected abstract void LoadContent();
        public abstract void Exiting();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
