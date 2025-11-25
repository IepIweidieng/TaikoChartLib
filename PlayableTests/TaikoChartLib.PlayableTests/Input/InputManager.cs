using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Input
{
    internal class InputManager
    {
        public static void Update(GameTime gameTime)
        {
            KeyInfo.Update(gameTime);
        }
    }
}
