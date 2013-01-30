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
        private Vector2 tokenPos;
        private Texture2D tokenTexture;
        private String imgFile;
        private int currentFrame;
        private int numberOfFrames;
        private Color color;
        private int numberOfSoldiers;
        private Territorio territorio;
        public Token(float x, float y, int num, Territorio ter)
        {

            tokenPos = new Vector2(x, y);
            currentFrame = 0;
            numberOfFrames = num;
            territorio = ter;



        }
        public Token(float x, float y, int num, Color c, int numSoldiers,Territorio ter)
        {

            tokenPos = new Vector2(x, y);
            currentFrame = 0;
            numberOfFrames = num;
            color = c;
            numberOfSoldiers = numSoldiers;
            territorio = ter;


        }
        public Token()
        {
        }
        public Color getColor()
        {
            return color;
        }
        public int getNumberOfSoldiers()
        {
            return numberOfSoldiers;
        }
        public Boolean isInRange(int x, int y)
        {
            return true;
        }
        public Vector2 getTokenPosition()
        {
            return tokenPos;
        }
        public Texture2D getTokenTexture()
        {
            return tokenTexture;
        }
        public String getImgFile()
        {
            return imgFile;
        }
        public void setTokenPosition(Vector2 v)
        {
            tokenPos = v;
        }
        public void setTokenTexture(Texture2D t)
        {
            tokenTexture = t;
        }
        public void getImgFile(String s)
        {
            imgFile = s;
        }
        public Boolean isCollided(float x, float y)
        {
            if (x > tokenPos.X && x < tokenPos.X + tokenTexture.Width / numberOfFrames && y > tokenPos.Y && y < tokenPos.Y + tokenTexture.Height)
                return true;
            else
                return false;
        }
        public void changeCurrentFrame(float x, float y)
        {
            if (x > tokenPos.X && x < tokenPos.X + tokenTexture.Width / 2 && y > tokenPos.Y && y < tokenPos.Y + tokenTexture.Height)
                currentFrame = 1;
            else
                currentFrame = 0;
        }
        public void setNextFrame()
        {
            if (currentFrame == numberOfFrames - 1)
                currentFrame = 0;
            else
                currentFrame += 1;
        }
        public void setPreviousFrame()
        {
            if (currentFrame == 0)
                currentFrame = numberOfFrames - 1;
            else
                currentFrame -= 1;
        }
        public Rectangle getCurrentFrame()
        {
            int x = tokenTexture.Width / numberOfFrames;
            int y = tokenTexture.Height;
            return new Rectangle(currentFrame * x, 0, x, y);

        }
        public int getNumberOfFrame()
        {
            return currentFrame;
        }
        public void setNumberOfSoldiers(int soldiers)
        {
            numberOfSoldiers = soldiers;
        }
        public int setNumberOfSoldiers()
        {
            return numberOfSoldiers;
        }
        public Territorio getTerritorio()
        {
            return territorio;
        }
        public void setTerritorio(Territorio ter)
        {
            territorio = ter;
        }
    }
}
