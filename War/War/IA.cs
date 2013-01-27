using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class IA : Jogador
    {
        public override void trocarCarta(CartaTerritorio c1, CartaTerritorio c2, CartaTerritorio c3)
        {
            throw new NotImplementedException();
        }

        public override void distribuirExercito(int quantidade)
        {
            Random random = new Random();
            int randomNumber;
            for(int i=0; i<quantidade; i++){
                randomNumber = random.Next(0, this.getTerritorios().Count()-1);
                this.getTerritorios()[randomNumber].setNumeroExercitos(1 + this.getTerritorios()[randomNumber].getNumeroExercito());
            }
        }

        public override Ataque atacar()
        {

            Territorio atacante;
            Territorio defensor;
            int randomNumber;
            int tropas;
            List<Territorio> vizinhos = new List<Territorio>();
            List<Territorio> possiveisAtacantes = new List<Territorio>();
            Random random = new Random();
            for (int i = 0; i < this.getTerritorios().Count; i++)
            {
                if (this.getTerritorios()[i].temVizinho() && this.getTerritorios()[i].getNumeroExercito() > 1)
                {
                    possiveisAtacantes.Add(getTerritorios()[i]);
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
                if (!atacante.getListaVizinhos()[j].getDono().Equals(this))
                {
                    vizinhos.Add(atacante.getListaVizinhos()[j]);
                }
            }
            randomNumber = random.Next(0, vizinhos.Count - 1);
            defensor = vizinhos[randomNumber];
            tropas = random.Next(1, atacante.getNumeroExercito() - 1);
            return new Ataque(atacante, defensor, tropas);
        }

    }
}
