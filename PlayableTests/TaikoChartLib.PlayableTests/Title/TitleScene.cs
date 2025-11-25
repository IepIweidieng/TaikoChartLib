using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Font;
using TaikoChartLib.PlayableTests.Input;

namespace TaikoChartLib.PlayableTests.Title
{
    internal class TitleScene : SceneBase
    {
        private class Item
        {
            public bool Selected;
            public string FilePath;
            public TestLabel textLabel;

            public Item(FontRenderer fontRenderer, string path)
            {
                FilePath = path;
                textLabel = new TestLabel(GameCore.GraphicsDevice, fontRenderer)
                {
                    Text = path
                };
            }
        }

        private FontRenderer fontRenderer;
        private Item[] items = new Item[0];
        private int currentIndex;

        public TitleScene()
        {

        }


        public override void Initialize()
        {
            base.Initialize();

            string[] files = Directory.GetFiles("./", "*.tja", SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                GameCore.app.Exit();
            }

            items = new Item[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                Item item = new Item(fontRenderer, files[i]);
                items[i] = item; 
            }

            Change(0);
        }

        protected override void LoadContent()
        {
            fontRenderer = FontRenderer.FromFile(Path.Combine(GameCore.Content.RootDirectory, "Font/MPLUSRounded1c-Regular.ttf"), 30);
        }

        public override void Exiting()
        {
            fontRenderer.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            InputUpdate(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            Vector2 pos = Vector2.Zero;
            Vector2 padding = new Vector2(0, fontRenderer.Size);
            foreach (Item item in items)
            {
                SpriteBatch.Draw(item.textLabel.Texture2D, pos, Color.White);
                pos += padding;
            }

            SpriteBatch.End();
        }

        private void InputUpdate(GameTime gameTime)
        {
            if (KeyInfo.IsKeyJustDown(Keys.Enter))
            {
                Decide();
            }
            if (KeyInfo.IsKeyJustDown(Keys.Up))
            {
                Change(-1);
            }
            if (KeyInfo.IsKeyJustDown(Keys.Down))
            {
                Change(1);
            }
        }

        private void Decide()
        {
            Item item = items[currentIndex];
            Game1.GoToPlay(item.FilePath);
        }

        private void Change(int change)
        {
            items[currentIndex].textLabel.Color = Color.White;
            currentIndex = Math.Clamp(currentIndex + change, 0, items.Length - 1);
            items[currentIndex].textLabel.Color = Color.Yellow;
        }
    }
}
