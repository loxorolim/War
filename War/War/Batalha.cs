using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Batalha
    {
        Jogador atacanteJog;
        Jogador defensorJog;
        Territorio ataqueT;
        Territorio defesaT;
        int[] dadosAt;
        int[] dadosDef;

        public Batalha(Jogador at, Jogador def, Territorio ataque, Territorio defesa)
        {
            this.atacanteJog = at;
            this.defensorJog = def;
            this.ataqueT = ataque;
            this.defesaT = defesa;
            dadosAt = new int[3];
            dadosDef = new int[3];
           
        }

        public void iniciar()
        {
            int numDadosDefesa, numDadosAtaque;

            switch (ataqueT.getNumeroExercito())
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

            switch (defesaT.getNumeroExercito())
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
            
            dadosAt = atacanteJog.lancarDados(numDadosAtaque);
            System.Threading.Thread.Sleep(500); //verificar esta linha caso de algum erro no jogo.
            dadosDef = defensorJog.lancarDados(numDadosDefesa);
            int[] exercitosPerdidos = MaquinaDeRegras.compararDados(dadosAt, numDadosAtaque, dadosDef, numDadosDefesa);
            //posição 0 -> exercitos Atacantes perdidos
            //posicao 1 -> exercitos Defensor perdidos
            atualizarExercitos(exercitosPerdidos[0], exercitosPerdidos[1]);
        }

        public void atualizarExercitos(int ataque, int defesa)
        {
            int exercitoAtaqueNovo = ataqueT.getNumeroExercito() - ataque;
            int exercitoDefesaNovo = defesaT.getNumeroExercito() - defesa;
            ataqueT.setNumeroExercitos(exercitoAtaqueNovo);
            defesaT.setNumeroExercitos(exercitoDefesaNovo);
            if (exercitoDefesaNovo <= 0)
            {
                defesaT.setNovoDono(ataqueT.getDono());
                atacanteJog.remanejarExercito(ataqueT, defesaT, exercitoAtaqueNovo - 1);
            }      

        }







    }
}
