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
    public class CartaObjetivo
    {
        private Vector2 objCardPos;
        private Texture2D objCardTexture;
        private string imgFile;
        private string description;
        private Jogador owner;
        private int identificador;


        public CartaObjetivo(int id,float x, float y, string imgFile, string description)  //necessário adicionar as imagens a cada CartaObjetivo
        {
            this.identificador = id;
            this.description = description;
            this.objCardPos = new Vector2(x, y);
            this.imgFile = imgFile;
        }

        public int getIdentificador()
        {
           return this.identificador;
        }

        public Vector2 getObjCardPosition(){
            return objCardPos;
        }
        public void setObjCardPosition(float x, float y){
            objCardPos.X = x;
            objCardPos.Y = y;
        }

        public Texture2D getObjCardTexture(){
            return objCardTexture;
        }

        public void setObjCardTexture(Texture2D t){
            objCardTexture = t;
        }

        public string getImgFile()
        {
            return imgFile;
        }

        public void setImgFile(string s)
        {
            imgFile = s;
        }

        public void setDescription(string s)
        {
            description = s;
        }

        public string getDescription()
        {
            return description;
        }

        public Jogador getOwner()
        {
            return owner;
        }

        public void setOwner(Jogador j)
        {
            owner = j;
        }

        public Boolean isOwned(){
            if (getOwner() != null)
                return true;
            return false;
        }

        
    }
}
