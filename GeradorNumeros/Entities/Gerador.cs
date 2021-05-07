using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Gerador
    {
        public static List<int> GerarJogo()
        {
            List<int> ls = new List<int>();
            bool continua = true;
            int numero;
            int i = 1;

            while (continua)
            {
                while (i < 16)
                {
                    numero = new Random().Next(26);
                    if (!ls.Contains(numero) && numero != 0)
                    {
                        ls.Add(numero);
                        i++;
                    }
                }

                if (Regras.Pares(ls) >= 6 && Regras.Pares(ls) <= 8 && Regras.ValidaQuadrantesLinha(ls) && Regras.ValidaQuadrantesColuna(ls))
                {
                    continua = false;
                }
                else
                {
                    ls.Clear();
                    i = 1;
                }
            }
            ls.Sort();
            return ls;
        }
    }
}
