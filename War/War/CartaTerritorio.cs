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
    public class CartaTerritorio
    {
        public const string quadrado = "QUADRADO", triangulo = "TRIANGULO", circulo = "CIRCULO", coringa = "CORINGA";
        private string figura;
        private int tipo;
        private Boolean selecionada;
        private Texture2D territCardTexture;
        private Territorio territorio;

        public CartaTerritorio(string nomeImagem,string tipoFigura, Territorio territorio)   //necessário adicionar as imagens a cada cartaTerritorio
        {
            this.figura = nomeImagem;
            this.territorio = territorio;

            if (tipoFigura.CompareTo(quadrado) == 0)
                this.tipo = 0;
            else
                if (tipoFigura.CompareTo(triangulo) == 0)
                    this.tipo = 1;
                else
                    if(tipoFigura.CompareTo(circulo) == 0)
                       this.tipo = 2;
                    else
                        if (tipoFigura.CompareTo(coringa) == 0)
                            this.tipo = 3;
        }

        public Territorio getTerritorio()
        {
            return territorio;
        }
        
        public string getFigura()
        {
            return figura;
        }

        public int getTipo()
        {
            return tipo;
        }

        public Texture2D getTerritCardTexture()
        {
            return territCardTexture;
        }

        public void setTerritCardTexture(Texture2D t)
        {
            territCardTexture = t;
        }

        public Boolean isSelecionada()
        {
            return this.selecionada;
        }

        public void setSelecionada(Boolean selecionada)
        {
            this.selecionada = selecionada;
        }

    }
}
