using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class Territorio
    {
        private string nome;
        private Continente continente;
        private Jogador dono;
        private int numExercitos;
        private List<Territorio> vizinhos;
        private float posX, posY;

        public Territorio(string nome, int posX, int posY)
        {
            this.nome = nome;
            this.vizinhos = null;
            numExercitos = 0;
            dono = null; // vai passar o dono do territorio qnd fizer o sorteio
            this.posX = posX;
            this.posY = posY;
        }

        public float getPosX()
        {
            return posX;
        }

        public float getPosY()
        {
            return posY;
        }

        public string getNome()
        {
            return nome;
        }

        public Continente getContinente()
        {
            return continente;
        }

        public void setContinente(Continente cont)
        {
            this.continente = cont;
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

        public void setListaVizinhos(List<Territorio> vizinhos)
        {
            this.vizinhos = vizinhos;
        }

        public override bool Equals(object territ)
        {
            return this.nome.Equals(((Territorio)territ).getNome());
        }

        public bool temVizinho()
        {
            for (int i = 0; i < vizinhos.Count; i++)
            {
                if (!vizinhos[i].getDono().Equals(dono))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
