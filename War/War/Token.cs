using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace War
{
    class Token
    {
        private Vector2 tokenPosition;
        private Texture2D tokenTexture;
        private String imgFile;
        private Color color;
        public Token(int x, int y, Color c)
        {
            tokenPosition.X = x;
            tokenPosition.Y = y;
            imgFile = "circle";
            color = c;

        }
        public Vector2 getPosition()
        {
            return this.tokenPosition;
        }
        public void setUpdatePosition(int x, int y)
        {
            tokenPosition.X += x;
            tokenPosition.Y += y;
        }
        public String getImgFile()
        {
            return imgFile;
        }
        public void setTexture(Texture2D t)
        {
            tokenTexture = t;
        }
        public Texture2D getTexture()
        {
            return tokenTexture;
        }
        public Color getColor()
        {
            return color;
        }
    }
}
