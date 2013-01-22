using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class CartaObjetivo
    {
        private string descricao;

        public CartaObjetivo(string descricao)  //necessário adicionar as imagens a cada CartaObjetivo
        {
            this.descricao = descricao;
        }

        public string getDescricao()
        {
            return descricao;
        }

    }
}
