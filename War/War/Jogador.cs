using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public abstract class Jogador
    {

        protected int cor;
        protected List<CartaTerritorio> cartasJogador = new List<CartaTerritorio>(5);
        protected CartaObjetivo objetivo;
        private List<Territorio> territorios = new List<Territorio>();

        public Jogador(int cor)
        {
            this.cor = cor;
           // this.objetivo = MaquinaDeRegras.sortearObjetivo();
            cartasJogador = null;
          //  this.setTerritorios();
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

        //Deve retornar os territorios que pertencem ao jogador
    /*    public List<Territorio> getTerritorios()
        {
            this.setTerritorios();
            return territorios;
        }*/
        public List<Territorio> getTerritorios()
        {
            return territorios;
        }
        public void adicionarTerritorio(Territorio ter)
        {
            territorios.Add(ter);
        }

        //Deve retornar os territorios que pertencem ao jogador
        public void setTerritorios()
        {
            foreach (Territorio territ in Tabuleiro.mapa)
            {
                if (territ.getDono().Equals(this))
                {
                    territorios.Add(territ);
                }
            }
        }

        public List<CartaTerritorio> getCartaTerritorio()
        {
            return cartasJogador;
        }

        public CartaObjetivo getObjetivo()
        {
            return objetivo;
        }


        public void receberCarta()
        {
            this.cartasJogador.Add(MaquinaDeRegras.darCartaTerritorio());
        }

        public abstract void trocarCarta(CartaTerritorio c1,CartaTerritorio c2,CartaTerritorio c3);

        public abstract void distribuirExercito(int quantidade);

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


        public abstract void remanejarExercito(Territorio origem, Territorio destino, int quantidade);

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

