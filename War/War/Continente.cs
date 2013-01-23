using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class Continente
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
            //verifica se todos os paises de um continente possuem um mesmo dono
            Territorio t = territorios.ElementAt(0);
            Jogador dono = t.getDono();
            foreach (Territorio te in territorios)
            {
                if (!dono.igual(te.getDono()))
                    return false;
            }

            return true;
        }


    }
}
