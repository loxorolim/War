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
        public static int HEIGHT = 600;
        public static int WIDTH = 800;
        public static float SCALE = 1;
        public static int BRANCO = 0 ;
        public static int PRETO = 1;
        public static int VERMELHO = 2;
        public static int VERDE = 3;
        public static int AZUL = 4;
        public static int AMARELO = 5;
        
        public static MouseState mouse;       
      
        public static float calculateScale()
        {
            return 800/1280;
        }
      
    }
}
