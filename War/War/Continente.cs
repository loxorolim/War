using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Continente
    {
        private string nome;
        private int recompensa;
        private List<Territorio> territorios;

        public Continente(string nome, int recompensa, List<Territorio> territorios)
        {
            this.nome = nome;
            this.recompensa = recompensa;
            this.territorios = territorios;
                 
        }

        public string getNome()
        {
            return nome;
        }

        public int getRecompensa()
        {
            return recompensa;
        }

        public Boolean continenteComandadoPorUnicoJogador()
        {
            int qtdTerritorios = territorios.Count;
            Territorio t = territorios.ElementAt(0);
            Jogador dono = t.getDono();
            for (int i = 1; i < qtdTerritorios; i++)
            {
                t = territorios.ElementAt(i);
                if (dono.igual(t.getDono()))
                    return false;
                
            }
            return true;
        }


    }
}
