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
        public static int HEIGHT = 1024;
        public static int WIDTH = 1280;
        public static float SCALE = 1;
        public static MouseState mouse;       
      
        public static float calculateScale()
        {
            return WIDTH / 1920;
        }
      
    }
}
