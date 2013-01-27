using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public class Ataque
    {
        Territorio atacante;
        Territorio defensor;
        int tropas;

        public Ataque(Territorio atacante, Territorio defensor, int tropas)
        {
            this.atacante = atacante;
            this.defensor = defensor;
            this.tropas = tropas;
        }

        public Territorio getAtacante()
        {
            return atacante;
        }

        public Territorio getDefensor()
        {
            return defensor;
        }
    }
}
