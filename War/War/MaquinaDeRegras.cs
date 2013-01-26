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
        public static int exercitosRecompensa = 4;

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
            Random r = new Random();

            List<Jogador> jogadores = Tabuleiro.jogadores;
            foreach (Territorio territorio in Tabuleiro.mapa)
            {
                int indexJogador = r.Next(0, jogadores.Count);
                territorio.setNovoDono(jogadores[indexJogador]);
                jogadores.RemoveAt(indexJogador);
                if (jogadores.Count == 0)
                {
                    jogadores = Tabuleiro.jogadores;
                }
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

        public static int efetuaTroca(CartaTerritorio carta1, CartaTerritorio carta2, CartaTerritorio carta3)
        {
            int retorno = 0;
            if (validaTroca(carta1, carta2, carta3))
            {
                retorno = exercitosRecompensa;
                if (exercitosRecompensa < 12)
                {
                    exercitosRecompensa = exercitosRecompensa + 2;
                }
                else
                {
                    if (exercitosRecompensa == 12)
                    {
                        exercitosRecompensa = 15;
                    }
                    else
                    {
                        exercitosRecompensa = exercitosRecompensa + 5;
                    }
                }
                devolveCartas(carta1, carta2, carta3);
            }
            return retorno;
        }

        private static void devolveCartas(CartaTerritorio carta1, CartaTerritorio carta2, CartaTerritorio carta3)
        {
            cartas.Add(carta1);
            cartas.Add(carta2);
            cartas.Add(carta3);
        }

        public static Boolean validaTroca(CartaTerritorio carta1, CartaTerritorio carta2, CartaTerritorio carta3)
        {

            return true;
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
