using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    
    class IA : Jogador
    {
        public const int easy = 0, medium = 1, hard = 2, insane = 3;
        MetodosAtaque metodosAtaque = new MetodosAtaque();
        MetodosDistribuirExercito metodosDistribuirExercito = new MetodosDistribuirExercito();
        MetodosRemanejamento metodosRemanejamento = new MetodosRemanejamento();
        MetodosTrocaCarta metodosTrocaCarta = new MetodosTrocaCarta();
        private int dificuldade;
        private int tunosDecorridos = 0;
        private bool ganhouBatalha = false;
        

        public IA(int cor, int dificuldade)
        {
            this.cor = cor;
            this.dificuldade = dificuldade;
            MaquinaDeRegras.sortearObjetivo(this);
          //  this.setTerritorios();
        }

       
        public void jogaTurno()
        {
            ganhouBatalha = false;
            if (tunosDecorridos>0)
            {
                Console.WriteLine("IA "+this.getDificuldade()+" jogando turno "+tunosDecorridos+"!");
                this.trocarCarta();
                Console.WriteLine("IA distribuindo " + this.getNumExercitoParacolocar()+" exercitos!");
                this.distribuirExercito(this.getNumExercitoParacolocar());
                Console.WriteLine("IA atacando!");
                this.atacar();
                if (ganhouBatalha)
                {
                    Console.WriteLine("IA recebeu carta!");
                    receberCarta();
                }
                this.limpaExercitosRemanejaveis();
                Console.WriteLine("IA Remanejando!");
                this.remanejarExercito();
                if (MaquinaDeRegras.verificaVitoria())
                {
                    War.CurrentState = War.GameState.Victory;
                    VictoryComponent.victorPlayer = this;
                    //mostra tela de vitoria do jogador atual
                }
            }
            else
            {
                Console.WriteLine("IA jogando!");
                Console.WriteLine("IA distribuindo " + this.getNumExercitoParacolocar() + " exercitos!");
                this.distribuirExercito(this.getNumExercitoParacolocar());
                PlayableComponent.firstCounter--;
            }
            tunosDecorridos++;
        }

        public void setGanhouBatalha()
        {
            this.ganhouBatalha = true;
        }

        public int getDificuldade()
        {
            return this.dificuldade;
        }

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
                    //randomNumber = random.Next(0, 100);
                    //if (randomNumber > 50)
                    //{
                        //Mantem os territorios de borda com o mesmo numero de tropas
                        metodosDistribuirExercito.distribuiExercitoTerritoriosBorda(quantidade, this);
                    //}
                    //else
                    //{
                        //Mantem os territorios de borda com o mesmo numero de tropas do territorio vizinho inimigo
                       // metodosDistribuirExercito.distribuiExercitoTerritoriosEqualizandoInimigo(quantidade, this);
                    //}
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
                    metodosRemanejamento.remanejaFullRandom(this); 
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

        public override Boolean isIA()
        {
            return true;
        }       
    }
    
}
