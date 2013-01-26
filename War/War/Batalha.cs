using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Batalha
    {
        Jogador atacante;
        Jogador defensor;
        Territorio ataque;
        Territorio defesa;
        int[] dadosAt;
        int[] dadosDef;

        public Batalha(Jogador at, Jogador def, Territorio ataque, Territorio defesa)
        {
            this.atacante = at;
            this.defensor = def;
            this.ataque = ataque;
            this.defesa = defesa;
            dadosAt = new int[3];
            dadosDef = new int[3];
           
        }

        public int[] atacar()
        {
            int numDadosDefesa, numDadosAtaque;

            switch (ataque.getNumeroExercito())
            {
                case 2:
                    numDadosAtaque = 1;
                    break;
                case 3:
                    numDadosAtaque = 2;
                    break;
                default:
                    numDadosAtaque = 3;
                    break;
            }

            switch (defesa.getNumeroExercito())
            {
                case 1:
                    numDadosDefesa= 1;
                    break;
                case 2:
                    numDadosDefesa = 2;
                    break;
                default:
                    numDadosDefesa = 3;
                    break;
            }
            
            dadosAt = atacante.lancarDados(numDadosAtaque);
            System.Threading.Thread.Sleep(500); //verificar esta linha caso de algum erro no jogo.
            dadosDef = defensor.lancarDados(numDadosDefesa);
            int[] exercitosPerdidos = MaquinaDeRegras.compararDados(dadosAt, numDadosAtaque, dadosDef, numDadosDefesa);
            //posição 0 -> exercitos Atacantes perdidos
            //posicao 1 -> exercitos Defensor perdidos
            return exercitosPerdidos;
        }






    }
}
