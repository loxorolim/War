using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public abstract class Jogador
    {

        public const int easy = 0, medium = 1, hard = 2, insane = 3;
        protected int cor;
        protected List<CartaTerritorio> cartasJogador = new List<CartaTerritorio>(5);
        protected CartaObjetivo objetivo;
        private List<Territorio> territorios = new List<Territorio>();
        private int dificuldade;
        private int exercitosParaColocar;
        private Boolean vivo = true;
        private Boolean conquistouTerritorio = false;

        //acrescentei o construtor com a dificuldade da IA
        public Jogador(int cor, int dificuldade)
        {
            this.cor = cor;
            this.dificuldade = dificuldade;
            MaquinaDeRegras.sortearObjetivo(this);
          //  this.setTerritorios();
        }

        public Jogador(int cor)
        {
            this.cor = cor;
            // this.objetivo = MaquinaDeRegras.sortearObjetivo();
            //  this.setTerritorios();
        }

        public Jogador()
        {
        }

        public Boolean getStatusJogador()
        {
            return vivo;
        }

        public void setJogadorMorto()
        {
            this.vivo = true;
        }

        public int getDificuldade()
        {
            return this.dificuldade;
        }

      
        public int getCor()
        {
            return cor;
        }

        public int getNumTerritorios()
        {
            int numTerritorios = 0;

            foreach (Territorio territ in Tabuleiro.mapa)
            {
                if (territ.getDono().Equals(this))
                {
                    numTerritorios++;
                }
            }
            return numTerritorios;
        }

      
        public List<Territorio> getTerritorios()
        {
            return territorios;
        }

        public List<Territorio> getTerritoriosBorda()
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
        public void addExercitosParaColocar(int n)
        {
            exercitosParaColocar += n;
        }
        public void removeExercitoParacolocar()
        {
            exercitosParaColocar--;
        }
        public int getNumExercitoParacolocar()
        {
            return exercitosParaColocar;
        }

        public void adicionarTerritorio(Territorio ter)
        {
            territorios.Add(ter);
        }
        public void removerTerritorio(Territorio ter)
        {
            //territorios.Remove(ter);
            for (int i = 0; i < territorios.Count; i++)
            {
                if(territorios[i].getNome().Equals(ter.getNome()))
                {
                    territorios.RemoveAt(i);
                    break;
                }
            }
        }
        //Deve retornar os territorios que pertencem ao jogador
        public void setTerritorios()
        {
            foreach (Territorio territ in Tabuleiro.mapa)
            {
                if (territ.getDono().Equals(this))
                {
                    territorios.Add(territ);
                }
            }
        }

        public List<CartaTerritorio> getCartasJogador()
        {
            return cartasJogador;
        }

        public CartaObjetivo getObjetivo()
        {
            return objetivo;
        }

        public void setObjetivo(CartaObjetivo co) {
            this.objetivo = co;
        }


        public void receberCarta()
        {
            if(this.cartasJogador.Count < 5){
                this.cartasJogador.Add(MaquinaDeRegras.darCartaTerritorio());
            }
        }

        
        public abstract void distribuirExercito(int quantidade);

        public abstract void atacar();
        

        public int[] lancarDados(int quantidade)
        {
            int[] numSorteados = new int[quantidade];
            Random r = new Random();
       //     System.Threading.Thread.Sleep(1000);

            for (int i = 0; i < quantidade; i++)
            {
                numSorteados[i] = r.Next(1, 7);
            }

            return numSorteados;

        }

        //A interface irá chamar esta função quando o jogador clicar no botao "Finalizar Jogada"
        public void finalizarJogada() {
            this.limpaExercitosRemanejaveis();
        }
        //public void finalizarJogada()
        //{b 
        //    MaquinaDeRegras.passaVez();
        //}


        public abstract Boolean isIA();

        public void limpaExercitosRemanejaveis()
        {
            foreach (Territorio territ in this.getTerritorios())
            {
                territ.setNumeroExercitosRemanejavel(territ.getNumeroExercito() - 1);
            }
        }

        public Boolean igual(Jogador jogador)
        {
            if (this.cor.CompareTo(jogador.cor) == 0)
                return true;
            else
                return false;

        }

        public override bool Equals(object jog)
        {
            return this.cor.Equals(((Jogador)jog).getCor());
        }

        public Boolean getConquistouTerritorio()
        {
            return conquistouTerritorio;
        }

        public void setConquistouTerritorio(Boolean conquistouTerritorio)
        {
            this.conquistouTerritorio = conquistouTerritorio;
        }

        public abstract void remanejarExercitoAtaque(Territorio atacante, Territorio defensor, int quantidade);
       
    }

}

