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
                        metodosDistribuirExercito.distribuiExercitoTerritoriosBorda(quantidade, this);
                    }
                    else
                    {
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
                    metodosAtaque.ataqueMediun(this);
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
            limpaExercitosRemanejaveis();
        }

        private void limpaExercitosRemanejaveis()
        {
            foreach (Territorio territ in this.getTerritorios())
            {
                territ.setNumeroExercitosRemanejavel(territ.getNumeroExercito()-1);
            }
        }

        public override void finalizarJogada()
        {                     
        }       

        public override Boolean isIA()
        {
            return true;
        }

        public override void remanejarExercitoAtaque(Territorio atacante, Territorio defensor, int quantidade)
        {
            int randomNumber;
            Random random = new Random();
            randomNumber = random.Next(0, atacante.getNumeroExercitoRemanejavel() - 1);
            if (MaquinaDeRegras.validaMovimentoRemanejamento(atacante, defensor, randomNumber))
            {
                atacante.setNumeroExercitos(atacante.getNumeroExercito() - randomNumber);
                defensor.setNumeroExercitos(defensor.getNumeroExercito() + randomNumber);
                defensor.setNumeroExercitosRemanejavel(defensor.getNumeroExercitoRemanejavel() + randomNumber);
            }
        }

    }
    
}
