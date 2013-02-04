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

        public static Boolean validaPaisVizinho(Territorio origem, Territorio destino)
        {
            foreach (Territorio vizinho in origem.getListaVizinhos())
            {
                if (destino.Equals(vizinho) && !paisesComMesmoDono(destino, vizinho))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean paisAtacanteComExercito(Territorio origem)
        {
            if (origem.getNumeroExercito() > 1)
            {
                return true;
            }
            return false;
        }


        public static Boolean validaMovimentoRemanejamento(Territorio origem, Territorio destino, int quantidade)
        {
            foreach (Territorio vizinho in origem.getListaVizinhos())
            {
                if (destino.Equals(vizinho) && paisesComMesmoDono(destino, vizinho) && quantidade < origem.getNumeroExercitoRemanejavel())
                {
                    return true;
                }
            }
            return false;
        }

        private static bool paisesComMesmoDono(Territorio destino, Territorio vizinho)
        {
            return destino.getDono().Equals(vizinho.getDono());
        }

        public static void sortearTerritorios()
        {
            Random r = new Random();
            
            List<int> jogadores = new List<int>();
            
            for (int i = 0; i < Tabuleiro.jogadores.Count; i++)
            {
                jogadores.Add(i);
            }
            List<Territorio> temp = Tabuleiro.mapa;
            foreach (Territorio territorio in temp)
            {
                int indexJogador = r.Next(0, jogadores.Count);
                territorio.setNovoDono(Tabuleiro.jogadores[jogadores[indexJogador]]);
                territorio.setNumeroExercitos(1);
                Tabuleiro.jogadores[jogadores[indexJogador]].adicionarTerritorio(territorio);
                jogadores.RemoveAt(indexJogador);
                if (jogadores.Count == 0)
                {
                    for (int i = 0; i < Tabuleiro.jogadores.Count; i++)
                    {
                        jogadores.Add(i);
                    }
                }
            }

        }
        //public static void sortearTerritorios()
        //{
        //    Random r = new Random();
        //    int numJogadores = Tabuleiro.jogadores.Count;
        //    int i = 0;
        //    List<Territorio> temp = Tabuleiro.mapa;
        //    for(int j = 0; j<Tabuleiro.mapa.Count;j++)
        //    {
        //        int index = r.Next(0, temp.Count);
        //        Territorio ter = temp[index];
        //        temp.Remove(ter);
                
                
        //        if (i < numJogadores)
        //        {
        //            ter.setNovoDono(Tabuleiro.jogadores[i]);
        //            Tabuleiro.jogadores[i].adicionarTerritorio(ter);
                    
        //        }
        //        else
        //        {
        //            i = 0;
        //            ter.setNovoDono(Tabuleiro.jogadores[i]);
        //            Tabuleiro.jogadores[i].adicionarTerritorio(ter);
                    
        //        }
        //        ter.setNumeroExercitos(1);
        //        i++;
        //    }

        //}

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

        public static CartaTerritorio darCartaTerritorio()
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

        public static int distribuicaoDeExercito(Jogador jogador)
        {
            int qtdTerritorios = 0;
            int qtdExercito = 0;

            foreach (Territorio t in Tabuleiro.mapa)
            {
                if (t.getDono().Equals(jogador))
                {
                    qtdTerritorios += 1;
                }
            }
            qtdExercito = qtdTerritorios / 2;
            return qtdExercito;

        }

        private static void devolveCartas(CartaTerritorio carta1, CartaTerritorio carta2, CartaTerritorio carta3)
        {
            cartas.Add(carta1);
            cartas.Add(carta2);
            cartas.Add(carta3);
        }

        public static Boolean validaTroca(CartaTerritorio carta1, CartaTerritorio carta2, CartaTerritorio carta3)
        {
            if (CartaTerritorio.coringa.Equals(carta1.getFigura()) || CartaTerritorio.coringa.Equals(carta2.getFigura()) || CartaTerritorio.coringa.Equals(carta3.getFigura()))
            {
                //possui carta coringa
                return true;
            }
            if (carta1.getFigura().Equals(carta2.getFigura()) && carta2.getFigura().Equals(carta3.getFigura()))
            {
                //tres figuras iguais
                return true;
            }
            if (!carta1.getFigura().Equals(carta2.getFigura()) && !carta2.getFigura().Equals(carta3.getFigura()) &&
                !carta1.getFigura().Equals(carta3.getFigura()))
            {
                //tres figuras diferentes
                return true;
            }
            return false;
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
