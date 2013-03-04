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
            foreach(Territorio origem in iA.getTerritorios())            
            {
                foreach(Territorio destino in iA.getTerritorios())               
                {
                    randomNumber = random.Next(0, origem.getNumeroExercitoRemanejavel());
                    if (MaquinaDeRegras.validaMovimentoRemanejamento(origem, destino, randomNumber))
                    {
                        origem.setNumeroExercitos(origem.getNumeroExercito() - randomNumber);
                        destino.setNumeroExercitos(destino.getNumeroExercito() + randomNumber);
                        origem.setNumeroExercitosRemanejavel(origem.getNumeroExercitoRemanejavel() - randomNumber);
                    }
                }
            }
        }       
        
    }
}
