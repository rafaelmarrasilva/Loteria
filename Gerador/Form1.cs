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
            try
            {

                txtNumPares.Enabled = false;
                txtMaiorSeq.Enabled = false;
                txtMenorSeq.Enabled = false;
                txtQtJogos.Enabled = false;
                txtSomaDezenas.Enabled = false;
                chkQC.Enabled = false;
                txtQC1.Enabled = false;
                txtQC1.Enabled = false;
                txtQC2.Enabled = false;
                txtQC3.Enabled = false;
                txtQC4.Enabled = false;
                txtQC5.Enabled = false;
                chkQL.Enabled = false;
                txtQL1.Enabled = false;
                txtQL1.Enabled = false;
                txtQL2.Enabled = false;
                txtQL3.Enabled = false;
                txtQL4.Enabled = false;
                txtQL5.Enabled = false;

                Parametros parametros = new Parametros();

                if (!String.IsNullOrEmpty(txtMaiorSeq.Text))
                {
                    if (int.TryParse(txtMaiorSeq.Text, out int vTxtMaiorSeq))
                        parametros.addMaiorSequencia(vTxtMaiorSeq);
                    else
                        throw new InvalidProgramException("Só é permitido digitar numeros");
                }


                if (!String.IsNullOrEmpty(txtMenorSeq.Text))
                {
                    if (int.TryParse(txtMenorSeq.Text, out int vTxtMenorSeq))
                        parametros.addMenorSequencia(vTxtMenorSeq);
                    else
                        throw new InvalidProgramException("Só é permitido digitar numeros");
                }
                    

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

                if (chkQC.Checked == true)
                {
                    if ((String.IsNullOrEmpty(txtQC1.Text) || String.IsNullOrEmpty(txtQC2.Text) || String.IsNullOrEmpty(txtQC3.Text) || String.IsNullOrEmpty(txtQC4.Text) || String.IsNullOrEmpty(txtQC5.Text)))
                        throw new InvalidProgramException("Todos os campos precisam ser preenchidos.");
                    else
                    {
                        parametros.addQuadColuna(txtQC1.Text);
                        parametros.addQuadColuna(txtQC2.Text);
                        parametros.addQuadColuna(txtQC3.Text);
                        parametros.addQuadColuna(txtQC4.Text);
                        parametros.addQuadColuna(txtQC5.Text);
                    }
                }

                if (chkQL.Checked == true)
                {
                    if ((String.IsNullOrEmpty(txtQL1.Text) || String.IsNullOrEmpty(txtQL2.Text) || String.IsNullOrEmpty(txtQL3.Text) || String.IsNullOrEmpty(txtQL4.Text) || String.IsNullOrEmpty(txtQL5.Text)))
                        throw new InvalidProgramException("Todos os campos precisam ser preenchidos.");

                    else
                    {
                        parametros.addQuadLinha(txtQL1.Text);
                        parametros.addQuadLinha(txtQL2.Text);
                        parametros.addQuadLinha(txtQL3.Text);
                        parametros.addQuadLinha(txtQL4.Text);
                        parametros.addQuadLinha(txtQL5.Text);
                    }
                }

                var jogosGerados = new List<Jogo>();

                if (int.TryParse(txtQtJogos.Text, out int vTxtQtJogos))
                    jogosGerados = GerarJogo.GerarJogos(vTxtQtJogos, parametros);
                else
                    throw new InvalidProgramException("Só é permitido digitar numeros");


                foreach (var jogos in jogosGerados)
                {
                    string numJogo = null;
                    foreach (var num in jogos.ListaNumeros)
                    {
                        numJogo += num.ToString().PadLeft(2, '0') + " - ";
                    }
                    numJogo = numJogo.Substring(0, numJogo.Length - 3) + "\r\n";
                    rTxtResult.Text += numJogo;
                }
                rTxtResult.ReadOnly = true;

                btnGerar.Enabled = false;
                MessageBox.Show("Jogos gerados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnClear_Click(null, null);
            }
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
            chkQC.Enabled = true;
            chkQC.Checked = false;
            txtQC1.Clear();
            txtQC2.Clear();
            txtQC3.Clear();
            txtQC4.Clear();
            txtQC5.Clear();
            chkQL.Enabled = true;
            chkQL.Checked = false;
            txtQL1.Clear();
            txtQL2.Clear();
            txtQL3.Clear();
            txtQL4.Clear();
            txtQL5.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtQC1.Enabled = false;
            txtQC2.Enabled = false;
            txtQC3.Enabled = false;
            txtQC4.Enabled = false;
            txtQC5.Enabled = false;

            txtQL1.Enabled = false;
            txtQL2.Enabled = false;
            txtQL3.Enabled = false;
            txtQL4.Enabled = false;
            txtQL5.Enabled = false;

        }

        private void chkQC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQC.Checked == true)
            {
                txtQC1.Enabled = true;
                txtQC2.Enabled = true;
                txtQC3.Enabled = true;
                txtQC4.Enabled = true;
                txtQC5.Enabled = true;
            }
            else
            {
                txtQC1.Enabled = false;
                txtQC1.Enabled = false;
                txtQC2.Enabled = false;
                txtQC3.Enabled = false;
                txtQC4.Enabled = false;
                txtQC5.Enabled = false;
            }
        }

        private void chkQL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQL.Checked == true)
            {
                txtQL1.Enabled = true;
                txtQL2.Enabled = true;
                txtQL3.Enabled = true;
                txtQL4.Enabled = true;
                txtQL5.Enabled = true;
            }
            else
            {
                txtQL1.Enabled = false;
                txtQL1.Enabled = false;
                txtQL2.Enabled = false;
                txtQL3.Enabled = false;
                txtQL4.Enabled = false;
                txtQL5.Enabled = false;
            }
        }
    }
}
