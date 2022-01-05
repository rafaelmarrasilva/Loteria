using Loteria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNumPares.Enabled = false;
            txtMaiorSeq.Enabled = false;
            txtMenorSeq.Enabled = false;
            txtQtJogos.Enabled = false;
            txtSomaDezenas.Enabled = false;

            Parametros parametros = new Parametros();

            if (!String.IsNullOrEmpty(txtMaiorSeq.Text))
                parametros.addMaiorSequencia(int.Parse(txtMaiorSeq.Text));

            if (!String.IsNullOrEmpty(txtMenorSeq.Text))
                parametros.addMenorSequencia(int.Parse(txtMenorSeq.Text));

            if (!String.IsNullOrEmpty(txtNumPares.Text))
            {
                foreach (var item in txtNumPares.Text.Split(','))
                {
                    parametros.addPares(int.Parse(item));
                }
            }

            if (!String.IsNullOrEmpty(txtSomaDezenas.Text))
            {
                foreach (var item in txtSomaDezenas.Text.Split(','))
                {
                    parametros.addSomaDezenas(int.Parse(item));
                }
            }

            var jogosGerados = GerarJogo.GerarJogos(int.Parse(txtQtJogos.Text), parametros);

            foreach (var jogos in jogosGerados)
            {
                string numJogo = null;
                foreach (var num in jogos.ListaNumeros)
                {
                    numJogo += num.ToString().PadLeft(2,'0') + " - ";
                }
                numJogo = numJogo.Substring(0, numJogo.Length - 3) + "\r\n";
                rTxtResult.Text += numJogo;
            }
            rTxtResult.ReadOnly = true;

            btnGerar.Enabled = false;
            MessageBox.Show("Jogos gerados com sucesso!");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rTxtResult.Clear();
            txtNumPares.Clear();
            txtMaiorSeq.Clear();
            txtMenorSeq.Clear();
            txtQtJogos.Clear();
            txtSomaDezenas.Clear();
            txtSomaDezenas.Enabled = true;
            txtNumPares.Enabled = true;
            txtMaiorSeq.Enabled = true;
            txtMenorSeq.Enabled = true;
            txtQtJogos.Enabled = true;
            btnGerar.Enabled = true;
        }
    }
}
