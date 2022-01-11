using Loteria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        List<Resultado> resultImporta = new List<Resultado>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtNumPares.Enabled = false;
                txtMaiorSeq.Enabled = false;
                txtMenorSeq.Enabled = false;
                txtQtJogos.Enabled = false;
                txtSomaDezenas.Enabled = false;
                txtMatriz.Enabled = false;
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

                if (String.IsNullOrEmpty(txtQtJogos.Text))
                    throw new InvalidProgramException("É obrigatório informar quantos jogos deseja gerar.");

                int vTxtQtJogos;
                if (!int.TryParse(txtQtJogos.Text, out vTxtQtJogos))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (!String.IsNullOrEmpty(txtNumPares.Text))
                {
                    foreach (var item in txtNumPares.Text.Split(','))
                    {
                        if (int.TryParse(item, out int vTxtNumPares))
                            parametros.addPares(vTxtNumPares);
                        else
                            throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                    }
                }

                if (!String.IsNullOrEmpty(txtMaiorSeq.Text))
                {
                    if (int.TryParse(txtMaiorSeq.Text, out int vTxtMaiorSeq))
                        parametros.addMaiorSequencia(vTxtMaiorSeq);
                    else
                        throw new InvalidProgramException("Só é permitido digitar números.");
                }

                if (!String.IsNullOrEmpty(txtMenorSeq.Text))
                {
                    if (int.TryParse(txtMenorSeq.Text, out int vTxtMenorSeq))
                        parametros.addMenorSequencia(vTxtMenorSeq);
                    else
                        throw new InvalidProgramException("Só é permitido digitar números.");
                }

                if (!String.IsNullOrEmpty(txtSomaDezenas.Text))
                {
                    string[] splitSomaDezenas = txtSomaDezenas.Text.Split(',');
                    if (splitSomaDezenas.Length != 2)
                        throw new InvalidProgramException("É necessario informar dois valores separados por ','.");
                    else
                    {
                        foreach (var item in txtSomaDezenas.Text.Split(','))
                        {
                            if (int.TryParse(item, out int vTxtSomaDezenas))
                                parametros.addSomaDezenas(vTxtSomaDezenas);
                            else
                                throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                        }
                    }
                }

                if (!String.IsNullOrEmpty(txtMatriz.Text))
                {
                    foreach (var item in txtMatriz.Text.Split(','))
                    {
                        if (int.TryParse(item, out int vTxtMatriz))
                            parametros.addMatriz(vTxtMatriz);
                        else
                            throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
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

                var jogosGerados = GerarJogo.GerarJogos(vTxtQtJogos, parametros, resultImporta);

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
                MessageBox.Show("Jogos gerados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClear_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQtJogos.Clear();
            txtNumPares.Clear();
            txtMaiorSeq.Clear();
            txtMenorSeq.Clear();
            txtSomaDezenas.Clear();
            txtMatriz.Clear();
            rTxtResult.Clear();

            txtQtJogos.Enabled = true;
            txtNumPares.Enabled = true;
            txtMaiorSeq.Enabled = true;
            txtMenorSeq.Enabled = true;
            txtSomaDezenas.Enabled = true;
            txtMatriz.Enabled = true;
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

        private void btnImporta_Click(object sender, EventArgs e)
        {
        }

        private void Analisador_Click(object sender, EventArgs e)
        {

        }

        private void btnClearImp_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            try
            {
                rTxtNumMaisSorteados.Clear();
                rTxtQtdPares.Clear();
                rTxtSomaDezenas.Clear();
                rTxtQuadranteLinha.Clear();
                rTxtQuadranteColuna.Clear();
                rTxtNumAtrasado.Clear();
                rTxtMapaResultados.Clear();

                if (resultImporta.Count() == 0)
                    throw new InvalidProgramException("Os resultados não foram importados.\r\nFavor importar.");

                if (!(int.TryParse(txtQtConcAnalisar.Text, out int vTxtQtConcAnalisar)))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                var dictNumMaisSorteados = Estatisticas.NumMaisSorteados(resultImporta, vTxtQtConcAnalisar);

                foreach (KeyValuePair<int, int> item in dictNumMaisSorteados)
                {
                    var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtConcAnalisar) * 100.0);
                    if (String.IsNullOrEmpty(rTxtNumMaisSorteados.Text))
                        rTxtNumMaisSorteados.Text += "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                    else
                        rTxtNumMaisSorteados.Text += "\r\n" + "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                }

                var dictQtdPares = Estatisticas.QuantPares(resultImporta, vTxtQtConcAnalisar);
                foreach (KeyValuePair<int, int> item in dictQtdPares)
                {
                    var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtConcAnalisar) * 100.0);
                    if (String.IsNullOrEmpty(rTxtQtdPares.Text))
                        rTxtQtdPares.Text += "Qtd Par " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                    else
                        rTxtQtdPares.Text += "\r\n" + "Qtd Par " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                }

                var dictSomaDezenas = Estatisticas.SomaDezenas(resultImporta, vTxtQtConcAnalisar);
                foreach (KeyValuePair<string, int> item in dictSomaDezenas)
                {
                    var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtConcAnalisar) * 100.0);
                    if (String.IsNullOrEmpty(rTxtSomaDezenas.Text))
                        rTxtSomaDezenas.Text += item.Key + " - " + item.Value + " - " + valPercent + "%";
                    else
                        rTxtSomaDezenas.Text += "\r\n" + item.Key + " - " + item.Value + " - " + valPercent + "%";
                }

                var dictQuadranteLinha = Estatisticas.QuadrantesLinha(resultImporta, vTxtQtConcAnalisar);
                int contQL = 1;
                while (contQL <= 5)
                {
                    foreach (KeyValuePair<string, int> item in dictQuadranteLinha.Where(p => p.Key.Contains("QL" + contQL)).OrderByDescending(s => s.Value))
                    {
                        var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtConcAnalisar) * 100.0);
                        if (String.IsNullOrEmpty(rTxtQuadranteLinha.Text))
                            rTxtQuadranteLinha.Text += item.Key + " - " + item.Value + " - " + valPercent + "%";
                        else
                            rTxtQuadranteLinha.Text += "\r\n" + item.Key + " - " + item.Value + " - " + valPercent + "%";
                    }
                    contQL++;
                    rTxtQuadranteLinha.Text += "\r\n---------------";
                }

                var dictQuadranteColuna = Estatisticas.QuadrantesColuna(resultImporta, vTxtQtConcAnalisar);
                int contQC = 1;
                while (contQC <= 5)
                {
                    foreach (KeyValuePair<string, int> item in dictQuadranteColuna.Where(p => p.Key.Contains("QC" + contQC)).OrderByDescending(s => s.Value))
                    {
                        var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtConcAnalisar) * 100.0);
                        if (String.IsNullOrEmpty(rTxtQuadranteColuna.Text))
                            rTxtQuadranteColuna.Text += item.Key + " - " + item.Value + " - " + valPercent + "%";
                        else
                            rTxtQuadranteColuna.Text += "\r\n" + item.Key + " - " + item.Value + " - " + valPercent + "%";
                    }
                    contQC++;
                    rTxtQuadranteColuna.Text += "\r\n---------------";
                }

                var dictNumAtrasados = Estatisticas.NumerosAtrasados(resultImporta);
                foreach (KeyValuePair<int,int> item in dictNumAtrasados)
                {
                    if (String.IsNullOrEmpty(rTxtNumAtrasado.Text))
                        rTxtNumAtrasado.Text += "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value;
                    else
                        rTxtNumAtrasado.Text += "\r\n" + "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value;
                }

                int qtAnalise = 0;
                if (int.Parse(txtQtConcAnalisar.Text) > 30)
                    qtAnalise = 30;
                else
                    qtAnalise = int.Parse(txtQtConcAnalisar.Text);

                var resultMapaResultados = Estatisticas.MapaResultado(resultImporta, qtAnalise);
                foreach (var item in resultMapaResultados)
                {
                    rTxtMapaResultados.Text += item + "\r\n";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImporta_Click_1(object sender, EventArgs e)
        {
            btnImporta.Enabled = false;
            rTxtResulImport.Clear();
            rTxtResumoImpor.Clear();
            rTxtResulImport.ReadOnly = true;
            rTxtResumoImpor.ReadOnly = true;

            try
            {
                if (String.IsNullOrEmpty(txtNomeArquivo.Text))
                    throw new InvalidProgramException("É obrigatórios informar o nome do arquivo.");

                (var result, string resumoImport) = Regras.CarregarResultados(txtNomeArquivo.Text);
                resultImporta = result;

                foreach (var item in resultImporta.Where(c => c.Concurso >= resultImporta.Max(w => w.Concurso) - 30).OrderByDescending(s => s.Concurso))
                {
                    string Corpo = item.Concurso + " - " + item.Data.ToString("dd/MM/yyyy") + " - ";
                    string Itens = null;
                    foreach (var num in item.Numeros)
                    {
                        Itens += num.ToString().PadLeft(2, '0') + " - ";
                    }
                    rTxtResulImport.Text += Corpo + Itens.Substring(0, Itens.Length - 3) + "\r\n";
                }

                string[] resultadoImportacao = resumoImport.Split(',');
                rTxtResumoImpor.Text += "Linhas Importada: " + resultadoImportacao[0] + "\r\n";
                rTxtResumoImpor.Text += "Linhas com Erro : " + resultadoImportacao[1] + "\r\n";
                rTxtResumoImpor.Text += "Total de Linhas : " + (int.Parse(resultadoImportacao[0]) + int.Parse(resultadoImportacao[1])).ToString();

                MessageBox.Show("Arquivo Importado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnClearImp_Click_1(object sender, EventArgs e)
        {
            resultImporta.Clear();
            rTxtResulImport.Clear();
            rTxtResumoImpor.Clear();
            txtNomeArquivo.Clear();
            btnImporta.Enabled = true;
        }

        private void btnConferir_Click(object sender, EventArgs e)
        {
            rTxtResultadosConferidos.Clear();
            try
            {
                if (String.IsNullOrEmpty(txtResultConcurso.Text))
                    throw new InvalidProgramException("Favor preencher o resultado do concurso.");

                if (String.IsNullOrEmpty(txtArqJogos.Text))
                    throw new InvalidProgramException("Favor preencher o caminho dos jogos para validação.");

                List<int> resultadoConcuros = new List<int>();
                foreach (var item in txtResultConcurso.Text.Split(','))
                {
                    if (int.TryParse(item, out int value))
                        resultadoConcuros.Add(value);
                    else
                        throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                }
                if (resultadoConcuros.Count() != 15)
                    throw new InvalidProgramException("O resultado deve ter 15 números e não " + resultadoConcuros.Count());

                var listaConferida = Regras.Conferencia(resultadoConcuros, txtArqJogos.Text);
                foreach (var resultado in listaConferida.OrderByDescending(p => p.Acertos))
                {
                    rTxtResultadosConferidos.Text += "Você acertou " + resultado.Acertos.ToString().PadLeft(2, '0') + " - (";
                    string a = null;
                    foreach (var item in resultado.Jogo)
                    {
                        a += item.ToString().PadLeft(2, '0') + " - ";
                    }
                    rTxtResultadosConferidos.Text += a.Substring(0, a.Length - 3) + ")\r\n";
                }

                var groupListaConferida = listaConferida.GroupBy(p => p.Acertos).Select(group => new {Acertos = group.Key, Conta = group.Count()}).OrderByDescending(p => p.Acertos);

                foreach (var item in groupListaConferida)
                {
                    rTxtResultadoConferidosCond.Text += item.Acertos + " acertos em " + item.Conta + " jogos\r\n";
                }

                MessageBox.Show("Jogos validados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearConferencia_Click(object sender, EventArgs e)
        {
            rTxtResultadosConferidos.Clear();
            txtResultConcurso.Clear();
            txtArqJogos.Clear();
            rTxtResultadoConferidosCond.Clear();
        }

        private void btnSelArqResult_Click(object sender, EventArgs e)
        {
            txtNomeArquivo.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.ReadOnlyChecked = true;
                openFileDialog.ShowReadOnly = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtNomeArquivo.Text = openFileDialog.FileName;
                    MessageBox.Show("Arquivo localizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSelArqJogos_Click(object sender, EventArgs e)
        {
            txtArqJogos.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.ReadOnlyChecked = true;
                openFileDialog.ShowReadOnly = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtArqJogos.Text = openFileDialog.FileName;
                    MessageBox.Show("Arquivo localizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
