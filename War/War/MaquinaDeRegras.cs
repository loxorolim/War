using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public static class MaquinaDeRegras
    {

        public static CartaObjetivo[] objetivos = new CartaObjetivo[] {
            new CartaObjetivo(1,-450, -636, "Cartas/objetivos/1", "Destruir completamente os exércitos brancos."),
            new CartaObjetivo(2,-450, -636, "Cartas/objetivos/2", "Destruir completamente os exércitos azuis."),
            new CartaObjetivo(3,-450, -636, "Cartas/objetivos/3", "Destruir completamente os exércitos pretos."),
            new CartaObjetivo(4,-450, -636, "Cartas/objetivos/4", "Destruir completamente os exércitos verdes"),
            new CartaObjetivo(5,-450, -636, "Cartas/objetivos/5", "Destruir completamente os exércitos vermelhos"),
            new CartaObjetivo(6,-450, -636, "Cartas/objetivos/6", "Destruir completamente os exércitos amarelos"),
            new CartaObjetivo(7,-450, -636, "Cartas/objetivos/7", "Conquistar 18 territórios, à sua escolha, com, pelo menos, 2 exércitos em cada um."),
            new CartaObjetivo(8,-450, -636, "Cartas/objetivos/8", "Conquistar 24 territórios à sua escolha"),
            new CartaObjetivo(9,-450, -636, "Cartas/objetivos/9", "Conquistar, em sua totalidade, os continentes Desert Plains e Rainbow Peninsula"),
            new CartaObjetivo(10,-450, -636, "Cartas/objetivos/10", "Conquistar, em sua totalidade, os continentes Rocky Cliffs e Snowy Ridges"),
            new CartaObjetivo(11,-450, -636, "Cartas/objetivos/11", "Conquistar, em sua totalidade, os continentes Metal Islands e Wild Woods"),
            new CartaObjetivo(12,-450, -636, "Cartas/objetivos/12", "Conquistar, em sua totalidade, os continentes Rocky Cliffs, Snowy Ridges e um terceiro à sua escolha"),
            new CartaObjetivo(13,-450, -636, "Cartas/objetivos/13", "Conquistar, em sua totalidade, os continentes Rainbow Peninsula, Snowy Ridges e um terceiro à sua escolha"),
            new CartaObjetivo(14,-450, -636, "Cartas/objetivos/14", "Conquistar, em sua totalidade, os continentes Wild Woods e Rocky Cliffs")
        };

        public static List<CartaTerritorio> cartas;
        public static int exercitosRecompensa = 4;
        private static int contadorVez = 0; //utilizado para ver qual é o jogador da vez

        //retorna quem é o jogador atual
        public static Jogador jogadorAtual()
        {
            return Tabuleiro.jogadorDaVez;
        }

        //sorteia no inicio do jogo qual vai ser a ordem dos jogadores
        public static void sorteaOrdemJogadores()
        {
            int qtdJogadores = Tabuleiro.numJogadores;
            int tam = qtdJogadores;
            List<int> nums = new List<int>();
            for (int i = 0; i < qtdJogadores; i++)
            {
                nums.Add(i);
            }
            List<int> ordem = new List<int>();
            Random r = new Random();
            for (int i = 0; i < tam; i++)
            {
                int j = r.Next(0, nums.Count);
                ordem.Add(nums[j]);
                nums.Remove(nums[j]);
                qtdJogadores--;             
            }
            Tabuleiro.ordemDeJogadores = ordem;
            Tabuleiro.jogadorDaVez = Tabuleiro.jogadores[ordem[0]];
        }

        //método chamado quando o Jogador finalizar a jogada
        public static void passaVez()
        {
            contadorVez++;
            if (contadorVez == Tabuleiro.numJogadores)
            {
                contadorVez = 0;
            }
            Tabuleiro.jogadorDaVez = Tabuleiro.jogadores[Tabuleiro.ordemDeJogadores[contadorVez]];
            distribuicaoDeExercito(Tabuleiro.jogadorDaVez);
            bonusDeExercitoPorContinente(Tabuleiro.jogadorDaVez);

        }


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
            
            for (int i = 0; i < Tabuleiro.numJogadores; i++)
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

        public static void sortearObjetivo(Jogador j)
        {
            Random random = new Random();
            int randomIndex = 0;
            Boolean sorteou = false;


            while (!sorteou)
            {
                randomIndex = random.Next(0, 13);
                if (!objetivos[randomIndex].isOwned())
                {
                    objetivos[randomIndex].setOwner(j);
                    j.setObjetivo(objetivos[randomIndex]);
                    sorteou = true;
                }
            }
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

        //Distribui a quantidade de exercitos baseado na quantidade de territorios do Jogador
        public static void distribuicaoDeExercito(Jogador jogador)
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
            jogador.addExercitosParaColocar(qtdExercito);
           // return qtdExercito;

        }

        //Calcula o bonus de exercito caso o jogador possua todo o continente
        public static void bonusDeExercitoPorContinente(Jogador jogador)
        {
            int qtdExercito = 0;
            Boolean mesmoDono = false;
            foreach (Continente c in Tabuleiro.continentes)
            {
                mesmoDono = c.continenteComandadoPorUnicoJogador();
                if (mesmoDono)
                {
                    Territorio t = c.getTerritorios().ElementAt(0);
                    if(t.getDono().Equals(jogador)){
                        qtdExercito += c.getRecompensa();
                    }
                }

            }
            jogador.addExercitosParaColocar(qtdExercito);
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
        //public static void adicionarExercitosIniciais()
        //{
        //    foreach(Jogador jog in Tabuleiro.jogadores)
        //    {
        //        int num = distribuicaoDeExercito(jog);
        //        jog.addExercitosParaColocar(num);
        //    }
        //}
        public static void adicionarExercitosParaSeremColocados(Jogador jog,int n)
        {
            jog.addExercitosParaColocar(n);
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

        public static Boolean verificaVitoria()
        {
            CartaObjetivo c = Tabuleiro.jogadorDaVez.getObjetivo();
            Boolean vitoria = false;
            switch (c.getIdentificador())
            {
                case 1:
                    vitoria = verificaExercitoMorto(0);
                    // destruir os exercitos brancos
                    break;
                case 2:
                    vitoria = verificaExercitoMorto(4);    
                // destruir os exercitos azul
                    break;
                case 3:
                    vitoria = verificaExercitoMorto(1);
                    // destruir os exercitos preto
                    break;
                case 4:
                    vitoria = verificaExercitoMorto(3);
                    // destruir os exercitos verde
                    break;
                case 5:
                    vitoria = verificaExercitoMorto(2);
                    // destruir os exercitos vermelho
                    break;
                case 6:
                    vitoria = verificaExercitoMorto(5);
                    // destruir os exercitos amarelo
                    break;
                case 7:
                    vitoria = verificaQtdTerritorios(18);
                    break;
                case 8:
                    vitoria = verificaQtdTerritorios(24);
                    break;
                case 9:
                    vitoria = verificaContinentesConquistados("Desert Plains", "Rainbow Peninsula", false);
                    //conquistar Desert Plains e Rainbow Peninsula
                    break;
                case 10:
                    vitoria = verificaContinentesConquistados("Rocky Cliffs", "Snowy Ridges", false);
                    // Rocky Cliffs e Snowy Ridges 
                    break;
                case 11:
                    vitoria = verificaContinentesConquistados("Metal Islands", "Wild Woods", false);
                    //Metal Islands e Wild Woods
                    break;
                case 12:
                    vitoria = verificaContinentesConquistados("Rocky Cliffs", "Snowy Ridges", true);
                    // Rocky Cliffs, Snowy Ridges e um terceiro
                    break;
                case 13:
                    vitoria = verificaContinentesConquistados("Rainbow Peninsula", "Snowy Ridges", true);
                    //Rainbow Peninsula, Snowy Ridges e um terceiro
                    break;
                case 14:
                    vitoria = verificaContinentesConquistados("Wild Woods", "Rocky Cliffs", false);
                    //Wild Woods e Rocky Cliffs
                    break;
                }
            return vitoria;

        }

        //Usado para verificar objetivo
        private static Boolean verificaExercitoMorto(int cor)
        {
            Jogador inimigo = getJogadorDesejado(cor);
            if (inimigo.Equals(null) || inimigo.Equals(Tabuleiro.jogadorDaVez))
                return verificaQtdTerritorios(24);
            else{
                if (inimigo.getTerritorios().Count == 0)
                {
                    //inimigo morreu
                    if (inimigo.getStatusJogador())
                    {
                        //o jogador atual acabou de matar o inimigo
                        //é necessário setar a variavel VIVO para False
                        return true;
                    }
                    else
                    {
                        //o inimigo foi morto por outro jogador
                        //o objetivo muda para conquistar 24 territorios
                        return verificaQtdTerritorios(24);
                    }
                }
               }

            return true;
        }

        private static Jogador getJogadorDesejado(int cor)
        {
            foreach (Jogador j in Tabuleiro.jogadores)
            {
                if (j.getCor().CompareTo(cor) == 0)
                    return j;
            }
            return null;
        }

        //Usado para verificar objetivo
        private static Boolean verificaQtdTerritorios(int quantidade)
        {
            Jogador atual = Tabuleiro.jogadorDaVez;
            switch (quantidade)
            {
                case 18:
                    int contador = 0;
                    if (atual.getTerritorios().Count >= quantidade)
                    {
                        foreach (Territorio t in Tabuleiro.jogadorDaVez.getTerritorios())
                        {
                            if (t.getNumeroExercito() < 2)
                                return false;
                            else
                                contador++;
                        }
                        if (contador == 18)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                        

                case 24:
                    if (atual.getTerritorios().Count == quantidade)
                        return true;
                    else
                        return false;
                
                default:
                    return false;
        
            }
        }

        //Usado para verificar Objetivo
        private static Boolean verificaContinentesConquistados(String c1, String c2, Boolean terceiro)
        {
            Jogador atual = Tabuleiro.jogadorDaVez;
            Continente cont1 = getContinenteDesejado(c1);
            Continente cont2 = getContinenteDesejado(c2);

            foreach (Territorio t in cont1.getTerritorios())
            {
                if (!t.getDono().Equals(atual))
                    return false;
            }

            foreach (Territorio t in cont2.getTerritorios())
            {
                if (!t.getDono().Equals(atual))
                    return false;
            }

            if (terceiro)
            {
                Boolean dono = true;
                foreach (Continente continente in Tabuleiro.continentes)
                {
                    if (!(continente.Equals(cont1) || continente.Equals(cont2)))
                    {
                        dono = true;
                        foreach (Territorio t in continente.getTerritorios())
                        {
                            if (!t.getDono().Equals(atual))
                            {
                                dono = false;
                                break;
                            }
                        }
                        if (dono)
                            return true;
                    }
                }
                return false;
                
            }
            
            return true;
            
        }

        private static Continente getContinenteDesejado(String nome)
        {
            foreach (Continente c in Tabuleiro.continentes)
            {
                if (c.getNome().CompareTo(nome) == 0)
                    return c;
            }
            return null;
        }

    }
}
