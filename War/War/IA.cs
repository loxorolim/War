using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    
    class IA : Jogador
    {
        public  void trocarCarta()
        {
            switch (this.getDificuldade())
            {
                case 0:
                    trocaAfobadaDeCarta();
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

        private void trocaAfobadaDeCarta(){
            List<CartaTerritorio> cartasJogador = this.getCartasJogador();
            if (cartasJogador.Count() >= 3)
            {
                for (int i = 0; i < cartasJogador.Count(); i++)
                {
                    for (int j = 0; j < cartasJogador.Count(); j++)
                    {
                        for (int k = 0; k < cartasJogador.Count(); k++)
                        {
                            if (i != k && i != j && j != k)
                            {
                                if (cartasJogador[i].Equals(cartasJogador[j]) && cartasJogador[i].Equals(cartasJogador[k]) && cartasJogador[k].Equals(cartasJogador[j]))
                                {
                                    distribuirExercito(MaquinaDeRegras.efetuaTroca(cartasJogador[i], cartasJogador[j], cartasJogador[k]));
                                }
                                if (!cartasJogador[i].Equals(cartasJogador[j]) && !cartasJogador[i].Equals(cartasJogador[k]) && !cartasJogador[k].Equals(cartasJogador[j]))
                                {
                                    distribuirExercito(MaquinaDeRegras.efetuaTroca(cartasJogador[i], cartasJogador[j], cartasJogador[k]));
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void distribuirExercito(int quantidade)
        {
            switch (this.getDificuldade())
            {
                case 0:
                    distribuiExercitoAleatorio(quantidade);
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

        private void distribuiExercitoAleatorio(int quantidade)
        {
            Random random = new Random();
            int randomNumber;
            for (int i = 0; i < quantidade; i++)
            {
                randomNumber = random.Next(0, this.getTerritorios().Count() - 1);
                this.getTerritorios()[randomNumber].setNumeroExercitos(1 + this.getTerritorios()[randomNumber].getNumeroExercito());
            }
        }

        //so ataca se o territorio atacante tiver mais territorios que o defensor
        private Batalha ataqueCauteloso()
        {
            Territorio atacante;
            Territorio defensor;
            int randomNumber;
            List<Territorio> vizinhos = new List<Territorio>();
            List<Territorio> possiveisAtacantes = new List<Territorio>();
            Random random = new Random();
            for (int i = 0; i < this.getTerritorios().Count; i++)
            {
                if (this.getTerritorios()[i].temVizinhoComMenosTropas() && this.getTerritorios()[i].getNumeroExercito() > 1)
                {
                    possiveisAtacantes.Add(getTerritorios()[i]);
                }
            }
            if (possiveisAtacantes.Count() == 0)
            {
                return null;
            }
            randomNumber = random.Next(0, possiveisAtacantes.Count - 1);
            atacante = possiveisAtacantes[randomNumber];
            for (int j = 0; j < atacante.getListaVizinhos().Count; j++)
            {
                if (!atacante.getListaVizinhos()[j].getDono().Equals(this) && atacante.getListaVizinhos()[j].getNumeroExercito()<atacante.getNumeroExercito())
                {
                    vizinhos.Add(atacante.getListaVizinhos()[j]);
                }
            }
            randomNumber = random.Next(0, vizinhos.Count - 1);
            defensor = vizinhos[randomNumber];
            return new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
        }

        private Batalha ataqueFullRandom()
        {
            Territorio atacante;
            Territorio defensor;
            int randomNumber;
            List<Territorio> vizinhos = new List<Territorio>();
            List<Territorio> possiveisAtacantes = new List<Territorio>();
            Random random = new Random();
            for (int i = 0; i < this.getTerritorios().Count; i++)
            {
                if (this.getTerritorios()[i].temVizinho() && this.getTerritorios()[i].getNumeroExercito() > 1)
                {
                    possiveisAtacantes.Add(getTerritorios()[i]);
                }
            }
            if (possiveisAtacantes.Count() == 0)
            {
                return null;
            }
            randomNumber = random.Next(0, possiveisAtacantes.Count - 1);
            atacante = possiveisAtacantes[randomNumber];
            for (int j = 0; j < atacante.getListaVizinhos().Count; j++)
            {
                if (!atacante.getListaVizinhos()[j].getDono().Equals(this))
                {
                    vizinhos.Add(atacante.getListaVizinhos()[j]);
                }
            }
            randomNumber = random.Next(0, vizinhos.Count - 1);
            defensor = vizinhos[randomNumber];
            return new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
        }

        private void ataqueEasy()
        {
            Batalha batalha = null;
            batalha = ataqueFullRandom();
            while (batalha != null)
            {
                //deve entrar uma validaçao de ataque aq
                batalha.iniciar();
                batalha = ataqueFullRandom();
            }
        }

        public override void atacar()
        {
            
            switch (this.getDificuldade())
            {
                case 0:
                    ataqueEasy();
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

        public override void remanejarExercito(Territorio origem, Territorio destino, int quantidade)
        {
            int randomNumber;
            Random random = new Random();
            randomNumber = random.Next(0, quantidade);
            if (MaquinaDeRegras.validaMovimentoRemanejamento(origem, destino))
            {
                origem.setNumeroExercitos(origem.getNumeroExercito() - randomNumber);
                destino.setNumeroExercitos(destino.getNumeroExercito() + randomNumber);
            }
        }

        public override void finalizarJogada()
        {
            switch (this.getDificuldade())
            {
                case 0:
                    remanejaFullRandom();
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

        private void remanejaFullRandom()
        {
            int randomNumber;
            Random random = new Random();

            for (int i = 0; i < this.getTerritorios().Count(); i++)
            {
                for (int j = 0; j < this.getTerritorios().Count(); j++)
                {
                    randomNumber = random.Next(0, this.getTerritorios()[i].getNumeroExercito() - 1);
                    remanejarExercito(this.getTerritorios()[i], this.getTerritorios()[j], randomNumber);
                }
            }
        }
             
    }
    
}
