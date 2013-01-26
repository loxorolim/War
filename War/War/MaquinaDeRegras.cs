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

        //Compara os dados do exército atacante e do exército defensor e retorna o numero de soldados perdidos na rodada
        //pelo exército atacante.
        public static int[] compararDados(int[] dadosAtaque, int qtdDadosAtaque, int[] dadosDefesa, int qtdDadosDefesa)
        {
            int exercitosAtacantesPerdidos = 0;
            int exercitosDefesaPerdidos = 0;
            int[] exercitoPerdido = new int[2];
            ordenaVetor(dadosAtaque);
            ordenaVetor(dadosDefesa);

            //os 2 vetores tem a mesma quantidade
            if (qtdDadosAtaque == qtdDadosDefesa)
            {
                for (int i = 0; i < qtdDadosAtaque; i++)
                {
                    if (dadosAtaque[i] <= dadosDefesa[i])
                    {
                        exercitosAtacantesPerdidos++;
                    }
                    else
                    {
                        exercitosDefesaPerdidos++;
                    }
                }
            }
            else
            {
                if (qtdDadosAtaque > qtdDadosDefesa)
                {
                    for (int i = 0; i < qtdDadosDefesa; i++)
                    {
                        if (dadosAtaque[i] <= dadosDefesa[i])
                        {
                            exercitosAtacantesPerdidos++;
                        }
                        else
                        {
                            exercitosDefesaPerdidos++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < qtdDadosAtaque; i++)
                    {
                        if (dadosAtaque[i] <= dadosDefesa[i])
                        {
                            exercitosAtacantesPerdidos++;
                        }
                        else
                        {
                            exercitosDefesaPerdidos++;
                        }
                    }
                }
            }
            exercitoPerdido[0] = exercitosAtacantesPerdidos;
            exercitoPerdido[1] = exercitosDefesaPerdidos;

            return exercitoPerdido;

        }

        private static void ordenaVetor(int[] vetor)
        {
            int num;
            int tam = vetor.Length;
            for (int i = 0; i < tam; i++)
            {
                for (int j = 0; j < tam; j++)
                {
                    if (vetor[i] > vetor[j])
                    {
                        num = vetor[i];
                        vetor[i] = vetor[j];
                        vetor[j] = num;
                    }
                }

            }

        }


    }
}
