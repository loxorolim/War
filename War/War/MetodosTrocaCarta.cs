using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class MetodosTrocaCarta
    {
        public void trocaAfobadaDeCarta(IA iA)
        {
            List<CartaTerritorio> cartasJogador = iA.getCartasJogador();
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
                                    iA.distribuirExercito(MaquinaDeRegras.efetuaTroca(cartasJogador[i], cartasJogador[j], cartasJogador[k]));
                                }
                                if (!cartasJogador[i].Equals(cartasJogador[j]) && !cartasJogador[i].Equals(cartasJogador[k]) && !cartasJogador[k].Equals(cartasJogador[j]))
                                {
                                    iA.distribuirExercito(MaquinaDeRegras.efetuaTroca(cartasJogador[i], cartasJogador[j], cartasJogador[k]));
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
