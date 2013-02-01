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
                    trocaAfobadaDeCarta();
                    break;
                case 2:
                    trocaAfobadaDeCarta();
                    break;
                case 3:
                    trocaAfobadaDeCarta();
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
            Random random = new Random();
            int randomNumber;
            switch (this.getDificuldade())
            {
                case 0:
                    distribuiExercitoAleatorio(quantidade);
                    break;
                case 1:
                    randomNumber = random.Next(0, 100);
                    if (randomNumber > 50)
                    {
                        distribuiExercitoTerritoriosBorda(quantidade);
                    }
                    else
                    {
                        distribuiExercitoTerritoriosEgualizandoInimigo(quantidade);
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

        private void distribuiExercitoTerritoriosBordaRandom(int quantidade)
        {
            Random random = new Random();
            int randomNumber;
            List<Territorio> territoriosComBorda = getTerritoriosBorda();
            for (int i = 0; i < quantidade; i++)
            {
                randomNumber = random.Next(0, this.getTerritorios().Count() - 1);
                territoriosComBorda[randomNumber].setNumeroExercitos(1 + territoriosComBorda[randomNumber].getNumeroExercito());
            }
        }

        //Mantem os territorios de borda com o mesmo numero de tropas
        private void distribuiExercitoTerritoriosBorda(int quantidade)
        {
            List<Territorio> territoriosComBorda = getTerritoriosBorda();
            for (int i = 0; i < quantidade; i++)
            {
                Territorio bordaMenosTerrit = territoriosComBorda[0];
                for (int j = 0; j < territoriosComBorda.Count(); j++)
                {
                    if (bordaMenosTerrit.getNumeroExercito() > territoriosComBorda[j].getNumeroExercito())
                    {
                        bordaMenosTerrit = territoriosComBorda[j];
                    }
                }
                bordaMenosTerrit.setNumeroExercitos(1 + bordaMenosTerrit.getNumeroExercito());
                }
        }

        //Mantem os territorios de borda com o mesmo numero de tropas do territorio vizinho inimigo
        private void distribuiExercitoTerritoriosEgualizandoInimigo(int quantidade)
        {
            List<Territorio> territoriosComBorda = getTerritoriosBorda();
            while (quantidade > 0)
            {
                Territorio inimigo = null;
                Territorio meu = null;
                int dif = 0;
                foreach (Territorio territ in territoriosComBorda)
                {
                    foreach(Territorio territVizinho in territ.getListaVizinhosInimigos())
                    {
                        if (territ.getNumeroExercito() < territVizinho.getNumeroExercito() && ((territ.getNumeroExercito() - territVizinho.getNumeroExercito()) > dif))
                        {
                            dif = territ.getNumeroExercito() - territVizinho.getNumeroExercito();
                            inimigo = territVizinho;
                            meu = territ;
                        }
                    }
                    
                }
                if (meu != null)
                {
                    if (dif < quantidade)
                    {
                        meu.setNumeroExercitos(meu.getNumeroExercito() + dif);
                        quantidade = quantidade - dif;
                    }
                    else
                    {
                        meu.setNumeroExercitos(meu.getNumeroExercito() + quantidade);
                        quantidade = 0;
                    }
                }
            }
        }


        private List<Territorio> getTerritoriosBorda()
        {
            List<Territorio> territoriosComBorda = new List<Territorio>();
            for (int i = 0; i < this.getNumTerritorios(); i++)
            {
                if (this.getTerritorios()[i].temVizinho())
                {
                    territoriosComBorda.Add(this.getTerritorios()[i]);
                }
            }
            return territoriosComBorda;
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

        private int getVitoriasAnteriores(Boolean[] vitoriasAteriores)
        {
            int quantVitoriasAnteriores = 0;
            for (int i = 0; i < vitoriasAteriores.Count(); i++)
            {
                if (vitoriasAteriores[i])
                {
                    quantVitoriasAnteriores++;
                }
            }
            return quantVitoriasAnteriores;
        }

        private void ataqueMediun()
        {
            Boolean[] vitoriasAteriores = new Boolean[5];        
            int batalhasAterioresCounter = 0;
            Batalha batalha = null;
            batalha = ataqueCauteloso();
            while (batalha != null)
            {
                //deve entrar uma validaçao de ataque aq
                batalha.iniciar();
                batalhasAterioresCounter++;
                if (batalhasAterioresCounter > vitoriasAteriores.Count()-1)
                {
                    batalhasAterioresCounter = 0;
                }
                if (batalha.getTerritorioDefesa().getDono().Equals(this))
                {
                    vitoriasAteriores[batalhasAterioresCounter] = true;
                }
                else
                {
                    vitoriasAteriores[batalhasAterioresCounter] = false;
                }
                if (this.getVitoriasAnteriores(vitoriasAteriores) >= 3)
                {
                    batalha = ataqueFullRandom();
                }
                else
                {
                    batalha = ataqueCauteloso();
                }               
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
                    ataqueMediun();
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

        private void remanejaFullRandom()
        {
            int randomNumber;
            Random random = new Random();
            for (int i = 0; i < this.getTerritorios().Count(); i++)
            {
                for (int j = 0; j < this.getTerritorios().Count(); j++)
                {
                    randomNumber = random.Next(0, this.getTerritorios()[i].getNumeroExercitoRemanejavel()-1);
                    if (MaquinaDeRegras.validaMovimentoRemanejamento(this.getTerritorios()[i], this.getTerritorios()[j], randomNumber))
                    {
                        this.getTerritorios()[i].setNumeroExercitos(this.getTerritorios()[i].getNumeroExercito() - randomNumber);
                        this.getTerritorios()[j].setNumeroExercitos(this.getTerritorios()[j].getNumeroExercito() + randomNumber);

                    }
                }
            }
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

        public override Boolean isIA()
        {
            return true;
        }
             
    }
    
}
