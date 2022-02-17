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
        string nomeLoteriaImportada = String.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (nomeLoteriaImportada != "LotoFacil")
                    throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

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
                btnJogoPote.Enabled = false;
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
            btnJogoPote.Enabled = true;

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
                if(nomeLoteriaImportada != "LotoFacil")
                    throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

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
                foreach (KeyValuePair<int, int> item in dictNumAtrasados)
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
        private void btnClearAnaLotoFacil_Click(object sender, EventArgs e)
        {
            rTxtQtdPares.Clear();
            rTxtNumMaisSorteados.Clear();
            rTxtQuadranteColuna.Clear();
            rTxtQuadranteLinha.Clear();
            rTxtSomaDezenas.Clear();
            rTxtNumAtrasado.Clear();
            rTxtMapaResultados.Clear();
            txtQtConcAnalisar.Clear();
        }

        private void btnImporta_Click_1(object sender, EventArgs e)
        {
            btnImporta.Enabled = false;
            cbImportaLoteria.Enabled = false;
            rTxtResulImport.Clear();
            rTxtResumoImpor.Clear();
            rTxtResulImport.ReadOnly = true;
            rTxtResumoImpor.ReadOnly = true;

            try
            {
                if (String.IsNullOrEmpty(txtNomeArquivo.Text))
                    throw new InvalidProgramException("É obrigatórios informar o nome do arquivo.");

                if (String.IsNullOrEmpty(cbImportaLoteria.Text))
                    throw new InvalidProgramException("É obrigatórios selecionar uma loteria");

                nomeLoteriaImportada = cbImportaLoteria.Text;

                (var result, string resumoImport) = Regras.CarregarResultados(txtNomeArquivo.Text, nomeLoteriaImportada);
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
            cbImportaLoteria.Enabled = true;
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

                var groupListaConferida = listaConferida.GroupBy(p => p.Acertos).Select(group => new { Acertos = group.Key, Conta = group.Count() }).OrderByDescending(p => p.Acertos);

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

        private void btnJogoPote_Click(object sender, EventArgs e)
        {
            try
            {
                if (nomeLoteriaImportada != "LotoFacil")
                    throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

                MessageBox.Show("Todas as regras serão ignoradas e será utilizado um sistema de Potes.\r\n" +
                            "Pote1 contém 15 números.\r\n" +
                            "Pote2 contém 10 números.\r\n" +
                            "A cada jogo gerado os potes são alimentados com novos números basedos nos mais e menos sorteados.\r\n" +
                            "Iniciando o Pote1 com as 15 mais dos últimos 7 jogos e Pote2 com os 10 menos.\r\n" +
                            "Demais jogos os potes são abastecidos com múltiplos de 7.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Parametros parametros = new Parametros();

                if (String.IsNullOrEmpty(txtQtJogos.Text))
                    throw new InvalidProgramException("É obrigatório informar quantos jogos deseja gerar.");

                int vTxtQtJogos;
                if (!int.TryParse(txtQtJogos.Text, out vTxtQtJogos))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                var result = GerarJogo.GerarJogosPote(vTxtQtJogos, parametros, resultImporta);

                foreach (var jogos in result)
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
                btnJogoPote.Enabled = false;
                MessageBox.Show("Jogos gerados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGerarPoteMegaSena_Click(object sender, EventArgs e)
        {
            if (nomeLoteriaImportada != "MegaSena")
                throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

            int vTxtQtdeJogosMegaSena = 0;
            int vTxtQtdPote1MegaSena = 0;
            int vTxtQtdPote2MegaSena = 0;
            int vTxtGPote1MegaSenaMaisSorteados = 0;
            int vTxtGPote1ResultMegaSena = 0;

            try
            {
                if (resultImporta.Count() == 0)
                    throw new InvalidProgramException("Os resultados não foram importados.\r\nFavor importar.");

                if (String.IsNullOrEmpty(txtQtdJogsMegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar quantos jogos deseja gerar.");

                if (!int.TryParse(txtQtdJogsMegaSena.Text, out vTxtQtdeJogosMegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (String.IsNullOrEmpty(txtGPote1MegaSena.Text) && chkPreenchePote1MegaSena.Checked == false)
                    throw new InvalidProgramException("É obrigatório informar os números do pote 1.");

                if (String.IsNullOrEmpty(txtQtdPote1MegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar quantos números deverão ser escolhidos no Pote 1.");

                if (!int.TryParse(txtQtdPote1MegaSena.Text, out vTxtQtdPote1MegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (String.IsNullOrEmpty(txtGPote2MegaSena.Text) && chkPote2Auto.Checked == false)
                    throw new InvalidProgramException("É obrigatório informar os números do pote 2 ou marcar a opção Preencher Pote 2...");

                if (String.IsNullOrEmpty(txtQtdPote2MegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar a quantidade de dezemas");

                if (!int.TryParse(txtQtdPote2MegaSena.Text, out vTxtQtdPote2MegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (chkPreenchePote1MegaSena.Checked && String.IsNullOrEmpty(txtGPote1MegaSenaMaisSorteados.Text))
                    throw new InvalidProgramException("É obrigatório informar a quantidade.");

                if (chkPreenchePote1MegaSena.Checked && String.IsNullOrEmpty(txtGPote1ResultMegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar a quantidade.");

                if (chkPreenchePote1MegaSena.Checked && !int.TryParse(txtGPote1MegaSenaMaisSorteados.Text, out vTxtGPote1MegaSenaMaisSorteados))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (chkPreenchePote1MegaSena.Checked && !int.TryParse(txtGPote1ResultMegaSena.Text, out vTxtGPote1ResultMegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");


                List<int> listaPote1 = new List<int>();
                if (chkPreenchePote1MegaSena.Checked)
                {
                    var dictNumMaisSorteados = Estatisticas.NumMaisSorteados(resultImporta, vTxtGPote1ResultMegaSena);

                    if (dictNumMaisSorteados.Where(x => x.Value > 0).Count() < vTxtGPote1MegaSenaMaisSorteados)
                        throw new InvalidProgramException("A Qtde. de resultados analisados trouxe uma qtde. de numeros menores do que o sugerido.\rInforme uma qtde. maior de resultados ou\rInforma uma qtde. menor de numeros mais sorteados. ");

                    var dic = dictNumMaisSorteados.Where(x => x.Value > 0).Take(vTxtGPote1MegaSenaMaisSorteados);

                    foreach (KeyValuePair<int, int> item in dic)
                    {
                        listaPote1.Add(item.Key);
                    }

                }
                else
                {
                    foreach (var item in txtGPote1MegaSena.Text.Split(','))
                    {
                        if (int.TryParse(item, out int value))
                            listaPote1.Add(value);
                        else
                            throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                    }
                }

                List<int> listaPote2 = new List<int>();
                if (chkPote2Auto.Checked == false)
                {
                    foreach (var item in txtGPote2MegaSena.Text.Split(','))
                    {
                        if (int.TryParse(item, out int value))
                            listaPote2.Add(value);
                        else
                            throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                    }
                }

                txtGPote1MegaSena.Enabled = false;
                txtGPote2MegaSena.Enabled = false;
                txtQtdJogsMegaSena.Enabled = false;
                txtQtdPote1MegaSena.Enabled = false;
                txtQtdPote2MegaSena.Enabled = false;
                txtGPote2MegaSena.Enabled = false;
                btnGerarMegaSena.Enabled = false;
                btnGerarPoteMegaSena.Enabled = false;
                chkPote2Auto.Enabled = false;

                txtLinha1MegaSena.Enabled = false;
                txtLinha2MegaSena.Enabled = false;
                txtLinha3MegaSena.Enabled = false;
                txtLinha4MegaSena.Enabled = false;
                txtLinha5MegaSena.Enabled = false;
                txtLinha6MegaSena.Enabled = false;

                txtGPote1MegaSenaMaisSorteados.Enabled = false;
                txtGPote1ResultMegaSena.Enabled = false;
                chkPreenchePote1MegaSena.Enabled = false;

                var jogosGerados = GerarJogo.GerarJogosPoteMegaSena(vTxtQtdeJogosMegaSena, listaPote1, vTxtQtdPote1MegaSena, listaPote2, vTxtQtdPote2MegaSena, resultImporta);

                foreach (var jogos in jogosGerados)
                {
                    jogos.ListaNumeros.Sort();
                    string numJogo = null;
                    foreach (var num in jogos.ListaNumeros)
                    {
                        numJogo += num.ToString().PadLeft(2, '0') + " - ";
                    }
                    numJogo = numJogo.Substring(0, numJogo.Length - 3) + "\r\n";
                    rTxtJogosMegaSena.Text += numJogo;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnClearJogoMegaSena_Click(object sender, EventArgs e)
        {
            rTxtJogosMegaSena.Clear();
            txtGPote1MegaSena.Clear();
            txtGPote2MegaSena.Clear();
            txtQtdJogsMegaSena.Clear();
            txtQtdPote1MegaSena.Clear();
            txtQtdPote2MegaSena.Clear();

            txtLinha1MegaSena.Clear();
            txtLinha2MegaSena.Clear();
            txtLinha3MegaSena.Clear();
            txtLinha4MegaSena.Clear();
            txtLinha5MegaSena.Clear();
            txtLinha6MegaSena.Clear();

            txtGPote1MegaSenaMaisSorteados.Clear();
            txtGPote1ResultMegaSena.Clear();

            chkPreenchePote1MegaSena.Checked = false;
            chkPote2Auto.Checked = false;
            chkPote2Auto.Enabled = true;
            txtGPote1MegaSena.Enabled = true;
            txtGPote2MegaSena.Enabled = true;
            txtQtdJogsMegaSena.Enabled = true;
            txtQtdPote1MegaSena.Enabled = true;
            txtGPote2MegaSena.Enabled = true;
            txtQtdPote1MegaSena.Enabled = true;
            txtQtdPote2MegaSena.Enabled = true;
            btnGerarMegaSena.Enabled = true;
            btnGerarPoteMegaSena.Enabled = true;
            chkGeraLinhaMegaSena.Enabled = true;
            chkGeraLinhaMegaSena.Checked = false;
        }

        private void chkGeraLinhaMegaSena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeraLinhaMegaSena.Checked == true)
            {
                txtLinha1MegaSena.Enabled = true;
                txtLinha2MegaSena.Enabled = true;
                txtLinha3MegaSena.Enabled = true;
                txtLinha4MegaSena.Enabled = true;
                txtLinha5MegaSena.Enabled = true;
                txtLinha6MegaSena.Enabled = true;
            }
            else
            {
                txtLinha1MegaSena.Enabled = false;
                txtLinha2MegaSena.Enabled = false;
                txtLinha3MegaSena.Enabled = false;
                txtLinha4MegaSena.Enabled = false;
                txtLinha5MegaSena.Enabled = false;
                txtLinha6MegaSena.Enabled = false;
            }
        }

        private void btnGerarMegaSena_Click(object sender, EventArgs e)
        {
            int vTxtQtdPote2MegaSena = 0;
            int vTxtQtdeJogosMegaSena = 0;

            try
            {
                if (nomeLoteriaImportada != "MegaSena")
                    throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

                if (String.IsNullOrEmpty(txtQtdJogsMegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar quantos jogos deseja gerar.");

                if (!int.TryParse(txtQtdJogsMegaSena.Text, out vTxtQtdeJogosMegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (String.IsNullOrEmpty(txtQtdPote2MegaSena.Text))
                    throw new InvalidProgramException("É obrigatório informar a quantidade de dezemas.");

                if (!int.TryParse(txtQtdPote2MegaSena.Text, out vTxtQtdPote2MegaSena))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                if (vTxtQtdPote2MegaSena < 6)
                {
                    throw new InvalidProgramException("O valor mimino é 6.");
                }

                Parametros parametros = new Parametros();

                if (chkGeraLinhaMegaSena.Checked == true)
                {
                    if ((String.IsNullOrEmpty(txtLinha1MegaSena.Text) || String.IsNullOrEmpty(txtLinha2MegaSena.Text) || String.IsNullOrEmpty(txtLinha3MegaSena.Text) || String.IsNullOrEmpty(txtLinha4MegaSena.Text) || String.IsNullOrEmpty(txtLinha5MegaSena.Text) || String.IsNullOrEmpty(txtLinha6MegaSena.Text)))
                        throw new InvalidProgramException("Todos os campos precisam ser preenchidos.");
                    else
                    {
                        if (!int.TryParse(txtLinha1MegaSena.Text, out int vTxtLinha1MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if (!int.TryParse(txtLinha2MegaSena.Text, out int vTxtLinha2MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if (!int.TryParse(txtLinha3MegaSena.Text, out int vTxtLinha3MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if (!int.TryParse(txtLinha4MegaSena.Text, out int vTxtLinha4MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if (!int.TryParse(txtLinha5MegaSena.Text, out int vTxtLinha5MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if (!int.TryParse(txtLinha6MegaSena.Text, out int vTxtLinha6MegaSena))
                            throw new InvalidProgramException("Só é permitido digitar números.");

                        if ((vTxtLinha1MegaSena + vTxtLinha2MegaSena + vTxtLinha3MegaSena + vTxtLinha4MegaSena + vTxtLinha5MegaSena + vTxtLinha6MegaSena) != vTxtQtdPote2MegaSena)
                            throw new InvalidProgramException("A soma das linhas tem que ser igual a quantidade de dezenas.");

                        parametros.addQuadLinha(txtLinha1MegaSena.Text);
                        parametros.addQuadLinha(txtLinha2MegaSena.Text);
                        parametros.addQuadLinha(txtLinha3MegaSena.Text);
                        parametros.addQuadLinha(txtLinha4MegaSena.Text);
                        parametros.addQuadLinha(txtLinha5MegaSena.Text);
                        parametros.addQuadLinha(txtLinha6MegaSena.Text);
                    }
                }

                chkPote2Auto.Enabled = false;
                chkGeraLinhaMegaSena.Enabled = false;
                txtQtdJogsMegaSena.Enabled = false;
                txtQtdPote2MegaSena.Enabled = false;
                txtLinha1MegaSena.Enabled = false;
                txtLinha2MegaSena.Enabled = false;
                txtLinha3MegaSena.Enabled = false;
                txtLinha4MegaSena.Enabled = false;
                txtLinha5MegaSena.Enabled = false;
                txtLinha6MegaSena.Enabled = false;

                var jogosGerados = GerarJogo.GerarJogosMegaSena(vTxtQtdeJogosMegaSena, vTxtQtdPote2MegaSena, parametros, resultImporta);

                foreach (var jogos in jogosGerados)
                {
                    jogos.ListaNumeros.Sort();
                    string numJogo = null;
                    foreach (var num in jogos.ListaNumeros)
                    {
                        numJogo += num.ToString().PadLeft(2, '0') + " - ";
                    }
                    numJogo = numJogo.Substring(0, numJogo.Length - 3) + "\r\n";
                    rTxtJogosMegaSena.Text += numJogo;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnAnaClearMegaSena_Click(object sender, EventArgs e)
        {
            txtQtdAnaMegaSena.Clear();
            rTxtNumMaisMegaSena.Clear();
            txtQtdAnaMegaSena.Enabled = true;
        }

        private void btnAnaMegaSena_Click(object sender, EventArgs e)
        {
            int vTxtQtdAnaMegaSena = 0;
            txtQtdAnaMegaSena.Enabled = false;
            rTxtNumMaisMegaSena.ReadOnly = true;

            try
            {
                if (nomeLoteriaImportada != "MegaSena")
                    throw new InvalidProgramException("É necessario importar os resultados dessa loteria.");

                if (resultImporta.Count() == 0)
                    throw new InvalidProgramException("Os resultados não foram importados.\r\nFavor importar.");

                if (!(int.TryParse(txtQtdAnaMegaSena.Text, out vTxtQtdAnaMegaSena)))
                    throw new InvalidProgramException("Só é permitido digitar números.");

                var dictNumMaisSorteados = Estatisticas.NumMaisSorteados(resultImporta, vTxtQtdAnaMegaSena);

                foreach (KeyValuePair<int, int> item in dictNumMaisSorteados)
                {
                    var valPercent = Math.Truncate(((double)item.Value / (double)vTxtQtdAnaMegaSena) * 100.0);
                    if (String.IsNullOrEmpty(rTxtNumMaisMegaSena.Text))
                        rTxtNumMaisMegaSena.Text += "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                    else
                        rTxtNumMaisMegaSena.Text += "\r\n" + "Bola " + item.Key.ToString().PadLeft(2, '0') + " - " + item.Value + " - " + valPercent + "%";
                }

                var listaMapaResultadoMegaSena = Estatisticas.MapaResultadoMegaSena(resultImporta, vTxtQtdAnaMegaSena);

                foreach (var item in listaMapaResultadoMegaSena)
                {
                    if (String.IsNullOrEmpty(rTxtMapaResultMS.Text))
                        rTxtMapaResultMS.Text = item;
                    else
                        rTxtMapaResultMS.Text += "\r\n" + item;
                }   

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkPote2Auto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPote2Auto.Checked)
            {
                txtGPote2MegaSena.Enabled = false;
                txtGPote2MegaSena.Clear();
            }
                
            else
            {
                txtGPote2MegaSena.Enabled = true;
                txtGPote2MegaSena.Clear();
            }
        }

        private void chkPreenchePote1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPreenchePote1MegaSena.Checked)
            {
                txtGPote1MegaSenaMaisSorteados.Clear();
                txtGPote1MegaSenaMaisSorteados.Enabled = true;
                txtGPote1MegaSenaMaisSorteados.Visible = true;

                txtGPote1ResultMegaSena.Clear();
                txtGPote1ResultMegaSena.Enabled = true;
                txtGPote1ResultMegaSena.Visible = true;

                label53.Visible = true;

                label54.Visible = true;

                txtGPote1MegaSena.Enabled = false;
                txtGPote1MegaSena.Clear();
            }
            
            else
            {
                txtGPote1MegaSenaMaisSorteados.Clear();
                txtGPote1MegaSenaMaisSorteados.Enabled = false;
                txtGPote1MegaSenaMaisSorteados.Visible = false;

                txtGPote1ResultMegaSena.Clear();
                txtGPote1ResultMegaSena.Enabled = false;
                txtGPote1ResultMegaSena.Visible = false;

                label53.Visible = false;

                label54.Visible = false;

                txtGPote1MegaSena.Enabled = true;
                txtGPote1MegaSena.Clear();
            }
        }
    }
}
