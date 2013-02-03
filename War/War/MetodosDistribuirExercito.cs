using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class MetodosDistribuirExercito
    {


        public void distribuiExercitoFullRandom(int quantidade, IA iA)
        {
            Random random = new Random();
            int randomNumber;
            for (int i = 0; i < quantidade; i++)
            {
                randomNumber = random.Next(0, iA.getTerritorios().Count() - 1);
                iA.getTerritorios()[randomNumber].setNumeroExercitos(1 + iA.getTerritorios()[randomNumber].getNumeroExercito());
                iA.getTerritorios()[randomNumber].setNumeroExercitosRemanejavel(1 + iA.getTerritorios()[randomNumber].getNumeroExercitoRemanejavel());
            }
        }

        public void distribuiExercitoTerritoriosBordaRandom(int quantidade, IA iA)
        {
            Random random = new Random();
            int randomNumber;
            List<Territorio> territoriosComBorda = iA.getTerritoriosBorda();
            for (int i = 0; i < quantidade; i++)
            {
                randomNumber = random.Next(0, iA.getTerritorios().Count() - 1);
                territoriosComBorda[randomNumber].setNumeroExercitos(1 + territoriosComBorda[randomNumber].getNumeroExercito());
                territoriosComBorda[randomNumber].setNumeroExercitosRemanejavel(1 + territoriosComBorda[randomNumber].getNumeroExercitoRemanejavel());
            }
        }

        //Mantem os territorios de borda com o mesmo numero de tropas
        public void distribuiExercitoTerritoriosBorda(int quantidade, IA iA)
        {
            List<Territorio> territoriosComBorda = iA.getTerritoriosBorda();
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
                bordaMenosTerrit.setNumeroExercitosRemanejavel(1 + bordaMenosTerrit.getNumeroExercitoRemanejavel());
            }
        }

        //Mantem os territorios de borda com o mesmo numero de tropas do territorio vizinho inimigo
        public void distribuiExercitoTerritoriosEgualizandoInimigo(int quantidade, IA iA)
        {
            List<Territorio> territoriosComBorda = iA.getTerritoriosBorda();
            while (quantidade > 0)
            {
                Territorio inimigo = null;
                Territorio meu = null;
                int dif = 0;
                foreach (Territorio territ in territoriosComBorda)
                {
                    foreach (Territorio territVizinho in territ.getListaVizinhosInimigos())
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
                        meu.setNumeroExercitosRemanejavel(meu.getNumeroExercitoRemanejavel() + dif);
                        quantidade = quantidade - dif;
                    }
                    else
                    {
                        meu.setNumeroExercitos(meu.getNumeroExercito() + quantidade);
                        meu.setNumeroExercitosRemanejavel(meu.getNumeroExercitoRemanejavel() + quantidade);
                        quantidade = 0;
                    }
                }
            }
        }       

    }
}
