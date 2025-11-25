using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Input
{
    internal static class KeyInfo
    {
        private static KeyboardState prevgiousState;
        private static KeyboardState state = Keyboard.GetState();

        public static void Update(GameTime gameTime)
        {
            prevgiousState = state;
            state = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key) => state.IsKeyDown(key);
        public static bool IsKeyUp(Keys key) => state.IsKeyUp(key);

        public static bool IsKeyJustDown(Keys key) => state.IsKeyDown(key) && !prevgiousState.IsKeyDown(key);
        public static bool IsKeyJustUp(Keys key) => state.IsKeyUp(key) && !prevgiousState.IsKeyUp(key);
    }
}
