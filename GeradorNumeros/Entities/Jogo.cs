using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public class Jogo
    {
        public int Sequencial { get; set; }
        public DateTime Data { get; set; }
        public List<int> ListaNumeros { get; set; } = new List<int>();

        public Jogo()
        {

        }

        public Jogo(int sequencial, DateTime data, List<int> listaNumeros)
        {
            Sequencial = sequencial;
            Data = data;
            ListaNumeros = listaNumeros;
        }

        public void addNumero(int numero)
        {
            ListaNumeros.Add(numero);
        }
    }
}
