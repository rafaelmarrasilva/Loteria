
namespace Gerador
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtQtJogos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumPares = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaiorSeq = new System.Windows.Forms.TextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.rTxtResult = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMenorSeq = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSomaDezenas = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(60, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Informe a qtde de jogos desejados:";
            // 
            // txtQtJogos
            // 
            this.txtQtJogos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtQtJogos.Location = new System.Drawing.Point(432, 70);
            this.txtQtJogos.Margin = new System.Windows.Forms.Padding(4);
            this.txtQtJogos.Name = "txtQtJogos";
            this.txtQtJogos.Size = new System.Drawing.Size(85, 25);
            this.txtQtJogos.TabIndex = 1;
            this.txtQtJogos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(60, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(344, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Informe quantos números pares o jogo deve ter:";
            // 
            // txtNumPares
            // 
            this.txtNumPares.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNumPares.Location = new System.Drawing.Point(432, 116);
            this.txtNumPares.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumPares.Name = "txtNumPares";
            this.txtNumPares.Size = new System.Drawing.Size(85, 25);
            this.txtNumPares.TabIndex = 2;
            this.txtNumPares.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(60, 163);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Informe a MAIOR sequencia de números:";
            // 
            // txtMaiorSeq
            // 
            this.txtMaiorSeq.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMaiorSeq.Location = new System.Drawing.Point(432, 163);
            this.txtMaiorSeq.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaiorSeq.Name = "txtMaiorSeq";
            this.txtMaiorSeq.Size = new System.Drawing.Size(85, 25);
            this.txtMaiorSeq.TabIndex = 3;
            this.txtMaiorSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGerar
            // 
            this.btnGerar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGerar.Location = new System.Drawing.Point(60, 498);
            this.btnGerar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(96, 51);
            this.btnGerar.TabIndex = 6;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.button1_Click);
            // 
            // rTxtResult
            // 
            this.rTxtResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rTxtResult.Location = new System.Drawing.Point(60, 327);
            this.rTxtResult.Name = "rTxtResult";
            this.rTxtResult.Size = new System.Drawing.Size(548, 152);
            this.rTxtResult.TabIndex = 6;
            this.rTxtResult.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(512, 498);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 51);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(69, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "(sep. virgula, caso sejá mais de um)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(300, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "Informe a MENOR sequencia de números:";
            // 
            // txtMenorSeq
            // 
            this.txtMenorSeq.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMenorSeq.Location = new System.Drawing.Point(432, 209);
            this.txtMenorSeq.Name = "txtMenorSeq";
            this.txtMenorSeq.Size = new System.Drawing.Size(85, 25);
            this.txtMenorSeq.TabIndex = 4;
            this.txtMenorSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(366, 21);
            this.label7.TabIndex = 13;
            this.label7.Text = "Informe o valor da MENOR e MAIOR soma do jogo:";
            // 
            // txtSomaDezenas
            // 
            this.txtSomaDezenas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSomaDezenas.Location = new System.Drawing.Point(432, 254);
            this.txtSomaDezenas.Name = "txtSomaDezenas";
            this.txtSomaDezenas.Size = new System.Drawing.Size(85, 25);
            this.txtSomaDezenas.TabIndex = 5;
            this.txtSomaDezenas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(69, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "(sep. virgula, caso sejá mais de um)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 623);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSomaDezenas);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMenorSeq);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rTxtResult);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtMaiorSeq);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNumPares);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQtJogos);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQtJogos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumPares;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaiorSeq;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.RichTextBox rTxtResult;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMenorSeq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSomaDezenas;
        private System.Windows.Forms.Label label8;
    }
}

