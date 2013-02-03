using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class MetodosRemanejamento
    {
        public void remanejaFullRandom(IA iA)
        {
            int randomNumber;
            Random random = new Random();
            for (int i = 0; i < iA.getTerritorios().Count(); i++)
            {
                for (int j = 0; j < iA.getTerritorios().Count(); j++)
                {
                    randomNumber = random.Next(0, iA.getTerritorios()[i].getNumeroExercitoRemanejavel() - 1);
                    if (MaquinaDeRegras.validaMovimentoRemanejamento(iA.getTerritorios()[i], iA.getTerritorios()[j], randomNumber))
                    {
                        iA.getTerritorios()[i].setNumeroExercitos(iA.getTerritorios()[i].getNumeroExercito() - randomNumber);
                        iA.getTerritorios()[j].setNumeroExercitos(iA.getTerritorios()[j].getNumeroExercito() + randomNumber);
                        iA.getTerritorios()[i].setNumeroExercitosRemanejavel(iA.getTerritorios()[i].getNumeroExercitoRemanejavel() - randomNumber);
                    }
                }
            }
        }

        public void remanejarExercitoAtaqueFullRandom(Territorio atacante, Territorio defensor, int quantidade)
        {
            int randomNumber;
            Random random = new Random();
            randomNumber = random.Next(0, atacante.getNumeroExercitoRemanejavel() - 1);
            if (MaquinaDeRegras.validaMovimentoRemanejamento(atacante, defensor, randomNumber))
            {
                atacante.setNumeroExercitos(atacante.getNumeroExercito() - randomNumber);
                defensor.setNumeroExercitos(defensor.getNumeroExercito() + randomNumber);
                atacante.setNumeroExercitosRemanejavel(atacante.getNumeroExercitoRemanejavel() - randomNumber);
            }
        }
        
    }
}
