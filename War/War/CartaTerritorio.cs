﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class CartaTerritorio
    {
        public const string quadrado = "QUADRADO", triangulo = "TRIANGULO", circulo = "CIRCULO", coringa = "CORINGA";
        private string figura;
        private int tipo;

        public CartaTerritorio(string nomeImagem,string tipoFigura)   //necessário adicionar as imagens a cada cartaTerritorio
        {
            this.figura = nomeImagem;

            if (tipoFigura.CompareTo(quadrado) == 0)
                this.tipo = 0;
            else
                if (tipoFigura.CompareTo(triangulo) == 0)
                    this.tipo = 1;
                else
                    if(tipoFigura.CompareTo(circulo) == 0)
                       this.tipo = 2;
        }


        public string getFigura()
        {
            return figura;
        }

        public int getTipo()
        {
            return tipo;
        }


    }
}
