using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace War
{
    public static class Global
    {
        public static float calculate16x9()
        {
            float scale = 1.0f;
            float x = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            scale = x / 3360;
            return scale;
        }
    }
}
