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
        int numExercitosParaPassar = 0;
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
        public int[] getDadosAt()
        {
            return dadosAt;
        }
        public int[] getDadosDef()
        {
            return dadosDef;
        }
        public Territorio getTerritorioDefesa()
        {
            return this.defesaT;
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
            System.Threading.Thread.Sleep(250); //verificar esta linha caso de algum erro no jogo.
            dadosDef = defensorJog.lancarDados(numDadosDefesa);
            int[] exercitosPerdidos = MaquinaDeRegras.compararDados(dadosAt, numDadosAtaque, dadosDef, numDadosDefesa);
            //posição 0 -> exercitos Atacantes perdidos
            //posicao 1 -> exercitos Defensor perdidos
            atualizarExercitos(exercitosPerdidos[0], exercitosPerdidos[1]);

            if (defesaT.getNumeroExercito() <= 0)
            {
                if (ataqueT.getNumeroExercito() == 1)
                    numExercitosParaPassar = 0;
                if (ataqueT.getNumeroExercito() == 2)
                    numExercitosParaPassar = 1;
                if (ataqueT.getNumeroExercito() == 3)
                    numExercitosParaPassar = 2;
                if (ataqueT.getNumeroExercito() >= 4)
                    numExercitosParaPassar = 3;
            }



        }
        public int getNumExercitosParaPassar()
        {
            return numExercitosParaPassar;
        }

        public void atualizarExercitos(int ataque, int defesa)
        {
            int exercitoAtaqueNovo = ataqueT.getNumeroExercito() - ataque;
            int exercitoDefesaNovo = defesaT.getNumeroExercito() - defesa;
            Console.WriteLine("Atac: " + exercitoAtaqueNovo + " Def: " + exercitoDefesaNovo);
            ataqueT.setNumeroExercitos(exercitoAtaqueNovo);
            //ataqueT.setNumeroExercitosRemanejavel(ataqueT.getNumeroExercitoRemanejavel() - ataque);
            defesaT.setNumeroExercitos(exercitoDefesaNovo);
           // defesaT.setNumeroExercitosRemanejavel(defesaT.getNumeroExercitoRemanejavel() - defesa);
            if (exercitoDefesaNovo <= 0)
            {
                defesaT.getDono().removerTerritorio(defesaT);
                defesaT.setNovoDono(ataqueT.getDono());
                ataqueT.getDono().adicionarTerritorio(defesaT);

                if (atacanteJog.isIA())
                {
                   defesaT.setNumeroExercitos(defesaT.getNumeroExercito() + 1);
                   ataqueT.setNumeroExercitos(ataqueT.getNumeroExercito() - 1);
                   ataqueT.setNumeroExercitosRemanejavel(ataqueT.getNumeroExercitoRemanejavel() - 1);
                    //atacanteJog.remanejarExercitoAtaque(ataqueT, defesaT, exercitoAtaqueNovo - 1);
                }
            }      

        }







    }
}
