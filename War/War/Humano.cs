using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class Humano : Jogador
    {

        public Humano(int cor) : base(cor)
        {
            base.cor = cor;
            //base.objetivo = MaquinaDeRegras.sortearObjetivo();
            base.cartasJogador = null;
        }


        public void trocarCarta(CartaTerritorio c1, CartaTerritorio c2, CartaTerritorio c3)
        {
            MaquinaDeRegras.efetuaTroca(c1, c2, c3);
        }

        public override void distribuirExercito(int quantidade)
        {
        }

        public override void atacar()
        {
          /* Territorio atacante;
           // atacante = pegar o territorio pela interface
           Territorio defensor;
           // Territorio defensor = pegar o territorio pela interface
           Batalha b = new Batalha(atacante.getDono(), defensor.getDono(), atacante, defensor);
           b.iniciar();
            */
        }

        public override void remanejarExercito(Territorio origem, Territorio destino, int quantidade){

            if (MaquinaDeRegras.validaMovimentoRemanejamento(origem, destino))
            {
                origem.setNumeroExercitos(origem.getNumeroExercito() - quantidade);
                destino.setNumeroExercitos(destino.getNumeroExercito() + quantidade);
            }
        }

        public override void finalizarJogada()
        {

        }
    }
}
