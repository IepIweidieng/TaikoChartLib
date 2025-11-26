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
using TaikoChartLib.PlayableTests.Graphics;
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

        private Difficulty currentDifficulty = Difficulty.Extreme;

        private TestLabel diffText;

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

            diffText = new TestLabel(GraphicsDevice, fontRenderer);

            Change(0);
            ChangeDiff(0);
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

            SpriteBatch.Draw(diffText.Texture2D, new Vector2(1280, 0), new Rectangle(0, 0, diffText.Texture2D.Width, diffText.Texture2D.Height), Color.White, 0, new Vector2(diffText.Texture2D.Width, 0), 1.0f, SpriteEffects.None, 0);

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
            if (KeyInfo.IsKeyJustDown(Keys.Left))
            {
                ChangeDiff(-1);
            }
            if (KeyInfo.IsKeyJustDown(Keys.Right))
            {
                ChangeDiff(1);
            }
        }

        private void Decide()
        {
            Item item = items[currentIndex];
            Game1.GoToPlay(item.FilePath, currentDifficulty);
        }

        private void Change(int change)
        {
            items[currentIndex].textLabel.Color = Color.White;
            currentIndex = Math.Clamp(currentIndex + change, 0, items.Length - 1);
            items[currentIndex].textLabel.Color = Color.Yellow;
        }

        private void ChangeDiff(int change)
        {
            currentDifficulty = (Difficulty)Math.Clamp((int)currentDifficulty + change, (int)Difficulty.Easy, (int)Difficulty.Extra);

            diffText.Text = currentDifficulty.ToString();
            switch (currentDifficulty)
            {
                case Difficulty.Easy:
                    diffText.Color = Color.LightBlue;
                    break;
                case Difficulty.Normal:
                    diffText.Color = Color.LightGreen;
                    break;
                case Difficulty.Hard:
                    diffText.Color = Color.Yellow;
                    break;
                case Difficulty.Extreme:
                    diffText.Color = Color.Red;
                    break;
                case Difficulty.Extra:
                    diffText.Color = Color.Purple;
                    break;
            }
        }
    }
}
