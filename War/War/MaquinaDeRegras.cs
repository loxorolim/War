using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class MaquinaDeRegras
    {
        private static MaquinaDeRegras instance;

        private List<CartaObjetivo> objetivos;

        private List<CartaTerritorio> cartas;

        private Tabuleiro tabuleiro = Tabuleiro.getInstance();

        private MaquinaDeRegras()
        {
            //Inicialização da máquina de regras (privada por ser singleton)
        }

        public static MaquinaDeRegras getInstance(){
            if (instance == null){
                instance = new MaquinaDeRegras();
            }
            return instance;
        }

        public Boolean validaIntencaoAtaque(Territorio origem, Territorio destino)
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

        public Boolean validaMovimento(Territorio origem, Territorio destino){
            foreach (Territorio vizinho in origem.getListaVizinhos())
            {
                if (destino.Equals(vizinho) && mesmoDono(destino, vizinho))
                {
                    return true;
                }
            }
            return false;
        }

        private bool mesmoDono(Territorio destino, Territorio vizinho)
        {
            return destino.getDono().Equals(vizinho.getDono());
        }

        public void sortearTerritorios(){

            foreach (Territorio territorio in this.tabuleiro.getMapa())
            {
                //Dúvida: Como será a seleção de territórios no mapa? Talvez
                //não seja possível armazenar o mapa como uma lista de territórios
            }

        }

        public CartaObjetivo sortearObjetivo(){
            
            Random random = new Random();
            int randomIndex = 0;
            Boolean sorteou = false;

            while (!sorteou)
            {
                randomIndex = random.Next(0, (this.objetivos.Count - 1));
                if (!this.objetivos[randomIndex].temDono)
                {
                    this.objetivos[randomIndex].temDono = true;
                    sorteou = true;
                }
            }
            return this.objetivos[randomIndex];
            
        }

        public CartaTerritorio darCartaDeConquista(){
            Random random = new Random();
            int randomIndex = random.Next(0, (this.cartas.Count-1));

            CartaTerritorio cartaSorteada = this.cartas[randomIndex];

            this.cartas.RemoveAt(randomIndex);

            return cartaSorteada;
        }

        public void setCartas(List<CartaTerritorio> cartas)
        {
            this.cartas = cartas;
        }

        public void setObjetivos(List<CartaObjetivo> objetivos)
        {
            this.objetivos = objetivos;
        }

    }
}
