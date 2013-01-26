using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public abstract class Jogador
    {

        private int cor;
        private List<CartaTerritorio> cartasJogador = new List<CartaTerritorio>(5);
        private CartaObjetivo objetivo;

        public Jogador(int cor)
        {
            this.cor = cor;
            this.objetivo = MaquinaDeRegras.sortearObjetivo();
            cartasJogador = null;
        }

        public int getCor()
        {
            return cor;
        }

        public int getNumTerritorios()
        {
            int numTerritorios = 0;

            foreach (Territorio territ in Tabuleiro.mapa)
            {
                if (territ.getDono().Equals(this))
                {
                    numTerritorios++;
                }
            }
            return numTerritorios;
        }

        public List<CartaTerritorio> getCartaTerritorio()
        {
            return cartasJogador;
        }

        public CartaObjetivo getObjetivo()
        {
            return objetivo;
        }


        public abstract void pegarCarta();

        public abstract void trocarCarta();

        public abstract void distribuirExercito();

        public abstract void atacar();

        public int[] lancarDados(int quantidade)
        {
            int[] numSorteados = new int[quantidade];
            Random r = new Random();
            System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < quantidade; i++)
            {
                numSorteados[i] = r.Next(1, 7);
            }

            return numSorteados;

        }


        public abstract void remanejarExercito();

        public abstract void finalizarJogada();

        public Boolean igual(Jogador jogador)
        {
            if (this.cor.CompareTo(jogador.cor) == 0)
                return true;
            else
                return false;

        }

        public override bool Equals(object jog)
        {
            return this.cor.Equals(((Jogador)jog).getCor());
        }

    }

}

