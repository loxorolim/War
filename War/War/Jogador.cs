using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    interface Jogador
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


        public void pegarCarta();

        public void trocarCarta();

        public void distribuirExercito();

        public void atacar();

        private void selecionaTerritorioAtacante();

        private void selecionaTerritorioAtacado();

        public void lancarDados();

        public void remanejarExercito();

        public void finalizarJogada();

        public Boolean igual(Jogador jogador)
        {
            if (this.cor.CompareTo(jogador.cor) == 0)
                return true;
            else
                return false;

        }
    }
}
