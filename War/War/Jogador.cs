using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public abstract class Jogador
    {

        private string cor;
        private int numTerritorios;
        private List<CartaTerritorio> cartasJogador = new List<CartaTerritorio>(5);
        private CartaObjetivo objetivo;
        private Boolean objetivoConcluido = false;
        private List<Territorio> territorios = new List<Territorio>();

        public Jogador(string cor,CartaObjetivo objetivo)
        {
            this.cor = cor;
            this.objetivo = objetivo; //ou this.objetivo = MaquinaDeRegras.sorteaObjetivo();
 //         territorios = MaquinaDeRegras.sorteaTerritorios();
 //         this.numTerritorios = totalTerritorios / numeroDeJogadores;
            cartasJogador = null;
        }

      

        public string getCor()
        {
            return cor;
        }

        public int getNumTerritorios()
        {
            return numTerritorios;
        }

        public void setNumTerritorios(int num)
        {
            this.numTerritorios = num;
        }

        public List<CartaTerritorio> getCartaTerritorio()
        {
            return cartasJogador;
        }

        public CartaObjetivo getObjetivo()
        {
            return objetivo;
        }

        public List<Territorio> getTerritorios(){
            return territorios;
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
    }

    }

