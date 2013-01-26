using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public static class Tabuleiro
    {
        public static List<Jogador> jogadores;
        public static List<Territorio> mapa;

        public static int numJogadores;

        public static void adicionarJogador(Jogador jog)
        {
            jogadores.Add(jog);
        }

    }
}
