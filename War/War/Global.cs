﻿using System;
using System.Collections.Generic;
using System.Linq;
using CColor = System.Drawing.Color;
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
        public static int WIDTH = 800/*GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width*/;
        public static int HEIGHT = 600/*GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height*/;
        public static float SCALE = (float)Global.WIDTH / 800f;
        public enum Cor { White, Black, Red, Green, Blue, Yellow, Pink };
        public static Matrix ScalingMatrix = Matrix.CreateScale((float)Global.WIDTH / 800f, (float)Global.WIDTH / 800f, 1);
        //public const int BRANCO = 0;
        //public const int PRETO = 1;
        //public const int VERMELHO = 2;
        //public const int VERDE = 3;
        //public const int AZUL = 4;
        //public const int AMARELO = 5;
        
        public static Color getColor(int i)
        {
            CColor clrColor = CColor.FromName(System.Enum.GetName(typeof(Cor), i));
            return new Color(clrColor.R, clrColor.G, clrColor.B, clrColor.A);
            //switch(i)
            //{
            //    case 0:
            //        return Color.White;
            //    case 1:
            //        return Color.Black;
            //    case 2:
            //        return Color.Red;
            //    case 3:
            //        return Color.Green;
            //    case 4:
            //        return Color.Blue;
            //    case 5:
            //        return Color.Yellow;
            //    default:
            //        return Color.Pink;
            //}
        }
        public static String getColorName(int i){
            if(i <= 5)
                return System.Enum.GetName(typeof(Cor), i);
            return System.Enum.GetName(typeof(Cor),6);
            //switch (i)
            //{
            //    case 0:
            //        return "White";
            //    case 1:
            //        return "Black";
            //    case 2:
            //        return "Red";
            //    case 3:
            //        return "Green";
            //    case 4:
            //        return "Blue";
            //    case 5:
            //        return "Yellow";
            //    default:
            //        return "Pink";
            //}
        }
        public static MouseState mouse;       
      
        public static float calculateScale()
        {
            return 800/1280;
        }
      
    }
}
