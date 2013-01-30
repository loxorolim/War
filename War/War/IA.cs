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
            for(int i=0; i<quantidade; i++){
                randomNumber = random.Next(0, this.getTerritorios().Count()-1);
                this.getTerritorios()[randomNumber].setNumeroExercitos(1 + this.getTerritorios()[randomNumber].getNumeroExercito());
            }
        }

        public override void atacar()
        {

            Territorio atacante;
            Territorio defensor;
            Batalha batalha = null;
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
                batalha = null;
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
            batalha = new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
            while (batalha != null)
            {
                //deve entrar uma validaçao de ataque aq
                batalha.iniciar();
                for (int i = 0; i < this.getTerritorios().Count; i++)
                {
                    if (this.getTerritorios()[i].temVizinho() && this.getTerritorios()[i].getNumeroExercito() > 1)
                    {
                        possiveisAtacantes.Add(getTerritorios()[i]);
                    }
                }
                if (possiveisAtacantes.Count() == 0)
                {
                    batalha = null;
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
            int randomNumber;
            Random random = new Random();
            
            for (int i = 0; i < this.getTerritorios().Count(); i++)
            {
                for (int j = 0; j < this.getTerritorios().Count(); j++)
                {
                    randomNumber = random.Next(0, this.getTerritorios()[i].getNumeroExercito()-1);
                    remanejarExercito(this.getTerritorios()[i], this.getTerritorios()[j], randomNumber);
                }
            }
        }
             
    }
    
}
