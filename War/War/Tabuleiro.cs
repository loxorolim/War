using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class Tabuleiro
    {
        private static Tabuleiro instance;

        private List<Jogador> jogadores;
        private List<Territorio> mapa;

        private int numJogadores;

        private Tabuleiro()
        {
            //Inicialização do tabuleiro (privada por ser singleton)
        }

        public static Tabuleiro getInstance()
        {
            if (instance == null){
                instance = new Tabuleiro();
            }
            return instance;
        }

        public List<Territorio> getMapa()
        {
            return this.mapa;
        }

        public void setMapa(List<Territorio> mapa)
        {
            this.mapa = mapa;
        }

        public List<Jogador> getJogadores()
        {
            return this.jogadores;
        }

        public void setJogadores(List<Jogador> jogadores)
        {
            this.numJogadores = jogadores.Count;
            this.jogadores = jogadores;
        }

        public int getNumJogadores()
        {
            return this.numJogadores;
        }
        public void adicionarJogador(Jogador jog)
        {
            jogadores.Add(jog);
        }

    }
}
