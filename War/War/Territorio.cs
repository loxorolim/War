﻿using System;
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
        private int numExercitosRemanejaveis;
        private List<Territorio> vizinhos;
        private float posX, posY;

        public Territorio(string nome, int posX, int posY)
        {
            this.nome = nome;
            this.vizinhos = null;
            numExercitos = 0;
            numExercitosRemanejaveis = 0;
            dono = null; // vai passar o dono do territorio qnd fizer o sorteio
            this.posX = posX;
            this.posY = posY;
        }
        public Territorio()
        {
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

        public int getNumeroExercitoRemanejavel()
        {
            return numExercitosRemanejaveis;
        }

        public void setNumeroExercitosRemanejavel(int quantidade)
        {
            this.numExercitosRemanejaveis = quantidade;
        }

        public void setNovoDono(Jogador dono)
        {
            this.dono = dono;
        }

        public List<Territorio> getListaVizinhos()
        {
            return vizinhos;
        }

        public List<Territorio> getListaVizinhosInimigos()
        {
            List<Territorio> vizinhosInimigos = new List<Territorio>();
            foreach (Territorio vizinho in getListaVizinhos())           
            {
                if (!vizinho.getDono().Equals(dono))
                {
                    vizinhosInimigos.Add(vizinho);
                }
            }
            return vizinhosInimigos;
        }

        public List<Territorio> getListaVizinhosAmigos()
        {
            List<Territorio> vizinhosAmigos = new List<Territorio>();
            for (int i = 0; i < vizinhos.Count; i++)
            {
                if (vizinhos[i].getDono().Equals(dono))
                {
                    vizinhosAmigos.Add(vizinhos[i]);
                }
            }
            return vizinhosAmigos;
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
            for (int i = 0; i < vizinhos.Count(); i++)
            {
                if (!vizinhos[i].getDono().Equals(dono))
                {
                    return true;
                }
            }
            return false;
        }

        public bool temVizinhoComMenosTropas()
        {
            for (int i = 0; i < vizinhos.Count; i++)
            {
                if (!vizinhos[i].getDono().Equals(dono) && vizinhos[i].getNumeroExercito()<this.getNumeroExercito())
                {
                    return true;
                }
            }
            return false;
        }

        public bool temVizinhoComMenosQueDobroTropas()
        {
            for (int i = 0; i < vizinhos.Count; i++)
            {
                if (!vizinhos[i].getDono().Equals(dono) && vizinhos[i].getNumeroExercito() < this.getNumeroExercito()*2)
                {
                    return true;
                }
            }
            return false;
        }
        public void diminuirNumeroDeExercito(int n)
        {
            numExercitos -= n;
        }
        public void atribuirExercitosPendentes()
        {
            numExercitos += numExercitosRemanejaveis;
            numExercitosRemanejaveis = 0;
        }
    }
}
