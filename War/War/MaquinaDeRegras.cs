using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public static class MaquinaDeRegras
    {

        public static List<CartaObjetivo> objetivos;
        public static List<CartaTerritorio> cartas;

        public static Boolean validaIntencaoAtaque(Territorio origem, Territorio destino)
        {
            foreach (Territorio vizinho in origem.getListaVizinhos())
            {
                if (destino.Equals(vizinho) && !mesmoDono(destino, vizinho))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean validaMovimento(Territorio origem, Territorio destino)
        {
            foreach (Territorio vizinho in origem.getListaVizinhos())
            {
                if (destino.Equals(vizinho) && mesmoDono(destino, vizinho))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool mesmoDono(Territorio destino, Territorio vizinho)
        {
            return destino.getDono().Equals(vizinho.getDono());
        }

        public static void sortearTerritorios()
        {

            foreach (Territorio territorio in Tabuleiro.mapa)
            {
                
            }

        }

        public static CartaObjetivo sortearObjetivo()
        {
            
            Random random = new Random();
            int randomIndex = 0;
            Boolean sorteou = false;

            while (!sorteou)
            {
                randomIndex = random.Next(0, (objetivos.Count));
                if (!objetivos[randomIndex].temDono)
                {
                    objetivos[randomIndex].temDono = true;
                    sorteou = true;
                }
            }
            return objetivos[randomIndex];
            
        }

        public static CartaTerritorio darCartaDeConquista()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, (cartas.Count));

            CartaTerritorio cartaSorteada = cartas[randomIndex];

            cartas.RemoveAt(randomIndex);

            return cartaSorteada;
        }


    }
}
