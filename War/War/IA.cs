using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    
    class IA : Jogador
    {
        MetodosAtaque metodosAtaque = new MetodosAtaque();
        MetodosDistribuirExercito metodosDistribuirExercito = new MetodosDistribuirExercito();
        MetodosRemanejamento metodosRemanejamento = new MetodosRemanejamento();
        MetodosTrocaCarta metodosTrocaCarta = new MetodosTrocaCarta();

        public  void trocarCarta()
        {
            switch (this.getDificuldade())
            {
                case 0:
                    metodosTrocaCarta.trocaAfobadaDeCarta(this);
                    break;
                case 1:
                    metodosTrocaCarta.trocaAfobadaDeCarta(this);
                    break;
                case 2:
                    metodosTrocaCarta.trocaAfobadaDeCarta(this);
                    break;
                case 3:
                    metodosTrocaCarta.trocaAfobadaDeCarta(this);
                    break;
                default:
                    //Erro
                    break;
            }           
        }
        
        public override void distribuirExercito(int quantidade)
        {
            Random random = new Random();
            int randomNumber;
            switch (this.getDificuldade())
            {
                case 0:
                    metodosDistribuirExercito.distribuiExercitoFullRandom(quantidade, this);
                    break;
                case 1:
                    randomNumber = random.Next(0, 100);
                    if (randomNumber > 50)
                    {
                        //Mantem os territorios de borda com o mesmo numero de tropas
                        metodosDistribuirExercito.distribuiExercitoTerritoriosBorda(quantidade, this);
                    }
                    else
                    {
                        //Mantem os territorios de borda com o mesmo numero de tropas do territorio vizinho inimigo
                        metodosDistribuirExercito.distribuiExercitoTerritoriosEgualizandoInimigo(quantidade, this);
                    }
                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    //Erro
                    break;
            }
            limpaExercitosRemanejaveis();
        }       

        public override void atacar()
        {            
            switch (this.getDificuldade())
            {
                case 0:
                    metodosAtaque.ataqueEasy(this);
                    break;
                case 1:
                    metodosAtaque.ataqueMedium(this);
                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    //Erro
                    break;
            }            
        }

        public void remanejarExercito()
        {
            switch (this.getDificuldade())
            {
                case 0:
                    metodosRemanejamento.remanejaFullRandom(this);                    
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    //Erro
                    break;
            }
        }        

        public override void finalizarJogada()
        {
            this.limpaExercitosRemanejaveis();         
        }       

        public override Boolean isIA()
        {
            return true;
        }

        public override void remanejarExercitoAtaque(Territorio atacante, Territorio defensor, int quantidade)
        {
            switch (this.getDificuldade())
            {
                case 0:
                    metodosRemanejamento.remanejarExercitoAtaqueFullRandom(atacante, defensor, quantidade);
                    break;
                case 1:
                    metodosRemanejamento.remanejarExercitoAtaqueFullRandom(atacante, defensor, quantidade);
                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    //Erro
                    break;
            }
        }

    }
    
}
