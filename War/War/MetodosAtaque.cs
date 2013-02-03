using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class MetodosAtaque
    {

        //so ataca se o territorio atacante tiver mais territorios que o defensor
        private Batalha ataqueCauteloso(IA iA)
        {
            Territorio atacante;
            Territorio defensor;
            int randomNumber;
            List<Territorio> vizinhos = new List<Territorio>();
            List<Territorio> possiveisAtacantes = new List<Territorio>();
            Random random = new Random();
            for (int i = 0; i < iA.getTerritorios().Count; i++)
            {
                if (iA.getTerritorios()[i].temVizinhoComMenosTropas() && iA.getTerritorios()[i].getNumeroExercito() > 1)
                {
                    possiveisAtacantes.Add(iA.getTerritorios()[i]);
                }
            }
            if (possiveisAtacantes.Count() == 0)
            {
                return null;
            }
            randomNumber = random.Next(0, possiveisAtacantes.Count - 1);
            atacante = possiveisAtacantes[randomNumber];
            for (int j = 0; j < atacante.getListaVizinhos().Count; j++)
            {
                if (!atacante.getListaVizinhos()[j].getDono().Equals(iA) && atacante.getListaVizinhos()[j].getNumeroExercito() < atacante.getNumeroExercito())
                {
                    vizinhos.Add(atacante.getListaVizinhos()[j]);
                }
            }
            randomNumber = random.Next(0, vizinhos.Count - 1);
            defensor = vizinhos[randomNumber];
            return new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
        }

        private Batalha ataqueFullRandom(IA iA)
        {
            Territorio atacante;
            Territorio defensor;
            int randomNumber;
            List<Territorio> vizinhos = new List<Territorio>();
            List<Territorio> possiveisAtacantes = new List<Territorio>();
            Random random = new Random();
            for (int i = 0; i < iA.getTerritorios().Count; i++)
            {
                if (iA.getTerritorios()[i].temVizinho() && iA.getTerritorios()[i].getNumeroExercito() > 1)
                {
                    possiveisAtacantes.Add(iA.getTerritorios()[i]);
                }
            }
            if (possiveisAtacantes.Count() == 0)
            {
                return null;
            }
            randomNumber = random.Next(0, possiveisAtacantes.Count - 1);
            atacante = possiveisAtacantes[randomNumber];
            for (int j = 0; j < atacante.getListaVizinhos().Count; j++)
            {
                if (!atacante.getListaVizinhos()[j].getDono().Equals(iA))
                {
                    vizinhos.Add(atacante.getListaVizinhos()[j]);
                }
            }
            randomNumber = random.Next(0, vizinhos.Count - 1);
            defensor = vizinhos[randomNumber];
            return new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
        }
        public void ataqueEasy(IA iA)
        {
            Batalha batalha = null;
            batalha = ataqueFullRandom(iA);
            while (batalha != null)
            {
                //deve entrar uma validaçao de ataque aq
                batalha.iniciar();
                batalha = ataqueFullRandom(iA);
            }
        }

        private int getVitoriasAnteriores(Boolean[] vitoriasAteriores)
        {
            int quantVitoriasAnteriores = 0;
            for (int i = 0; i < vitoriasAteriores.Count(); i++)
            {
                if (vitoriasAteriores[i])
                {
                    quantVitoriasAnteriores++;
                }
            }
            return quantVitoriasAnteriores;
        }

        public void ataqueMediun(IA iA)
        {
            Boolean[] vitoriasAteriores = new Boolean[5];
            int batalhasAterioresCounter = 0;
            Batalha batalha = null;
            batalha = ataqueCauteloso(iA);
            while (batalha != null)
            {
                //deve entrar uma validaçao de ataque aq
                batalha.iniciar();
                batalhasAterioresCounter++;
                if (batalhasAterioresCounter > vitoriasAteriores.Count() - 1)
                {
                    batalhasAterioresCounter = 0;
                }
                if (batalha.getTerritorioDefesa().getDono().Equals(iA))
                {
                    vitoriasAteriores[batalhasAterioresCounter] = true;
                }
                else
                {
                    vitoriasAteriores[batalhasAterioresCounter] = false;
                }
                if (getVitoriasAnteriores(vitoriasAteriores) >= 3)
                {
                    batalha = ataqueFullRandom(iA);
                }
                else
                {
                    batalha = ataqueCauteloso(iA);
                }
            }
        }
        
    }
}
