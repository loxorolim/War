using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Territorio
    {
        private string nome;
        private Continente continente;
        private Jogador dono;
        private int numExercitos;
        private List<Territorio> vizinhos;

        public Territorio(string nome, Continente continente, List<Territorio> vizinhos)
        {
            this.nome = nome;
            this.continente = continente;
            this.vizinhos = vizinhos;
            numExercitos = 0;
            dono = null;
        }

        public string getNome()
        {
            return nome;
        }

        public Continente getContinente()
        {
            return continente;
        }

        public Jogador getDono()
        {
            return dono;
        }

        public int getNumeroExercito()
        {
            return numExercitos;
        }

        public void setNumeroExercitos(int quantidade)
        {
            this.numExercitos = quantidade;
        }

        public void setNovoDono(Jogador dono)
        {
            this.dono = dono;
        }

        public List<Territorio> getListaVizinhos()
        {
            return vizinhos;
        }


    }
}
