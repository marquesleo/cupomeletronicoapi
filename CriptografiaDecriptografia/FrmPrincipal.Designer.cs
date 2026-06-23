namespace CriptografiaDecriptografia
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPalavraNormal = new TextBox();
            label1 = new Label();
            btnCriptografar = new Button();
            txtPalavraDecriptografada = new TextBox();
            lblPalavraCriptografada = new Label();
            grpCryptografia = new GroupBox();
            groupBox1 = new GroupBox();
            txtPalavraDecodificada = new TextBox();
            lblPalavraDecodificada = new Label();
            txtPalavraCodificada = new TextBox();
            lblPalavraCodificada = new Label();
            btnDecriptografar = new Button();
            grpCryptografia.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPalavraNormal
            // 
            txtPalavraNormal.Location = new Point(28, 54);
            txtPalavraNormal.Name = "txtPalavraNormal";
            txtPalavraNormal.Size = new Size(236, 23);
            txtPalavraNormal.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 36);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 1;
            label1.Text = "Palavra normal";
            // 
            // btnCriptografar
            // 
            btnCriptografar.Location = new Point(89, 83);
            btnCriptografar.Name = "btnCriptografar";
            btnCriptografar.Size = new Size(110, 23);
            btnCriptografar.TabIndex = 2;
            btnCriptografar.Text = "Cryptografar";
            btnCriptografar.UseVisualStyleBackColor = true;
            btnCriptografar.Click += btnCriptografar_Click;
            // 
            // txtPalavraDecriptografada
            // 
            txtPalavraDecriptografada.Location = new Point(28, 130);
            txtPalavraDecriptografada.Name = "txtPalavraDecriptografada";
            txtPalavraDecriptografada.Size = new Size(236, 23);
            txtPalavraDecriptografada.TabIndex = 3;
            // 
            // lblPalavraCriptografada
            // 
            lblPalavraCriptografada.AutoSize = true;
            lblPalavraCriptografada.Location = new Point(28, 112);
            lblPalavraCriptografada.Name = "lblPalavraCriptografada";
            lblPalavraCriptografada.Size = new Size(124, 15);
            lblPalavraCriptografada.TabIndex = 4;
            lblPalavraCriptografada.Text = "Palavra Cryptografada";
            // 
            // grpCryptografia
            // 
            grpCryptografia.Controls.Add(txtPalavraDecriptografada);
            grpCryptografia.Controls.Add(lblPalavraCriptografada);
            grpCryptografia.Controls.Add(txtPalavraNormal);
            grpCryptografia.Controls.Add(label1);
            grpCryptografia.Controls.Add(btnCriptografar);
            grpCryptografia.Location = new Point(34, 12);
            grpCryptografia.Name = "grpCryptografia";
            grpCryptografia.Size = new Size(314, 180);
            grpCryptografia.TabIndex = 5;
            grpCryptografia.TabStop = false;
            grpCryptografia.Text = "CRIPTOGRAFIA";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtPalavraDecodificada);
            groupBox1.Controls.Add(lblPalavraDecodificada);
            groupBox1.Controls.Add(txtPalavraCodificada);
            groupBox1.Controls.Add(lblPalavraCodificada);
            groupBox1.Controls.Add(btnDecriptografar);
            groupBox1.Location = new Point(34, 198);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(314, 235);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "DECRYPTOGRAFIA";
            // 
            // txtPalavraDecodificada
            // 
            txtPalavraDecodificada.Location = new Point(28, 130);
            txtPalavraDecodificada.Name = "txtPalavraDecodificada";
            txtPalavraDecodificada.Size = new Size(236, 23);
            txtPalavraDecodificada.TabIndex = 3;
            // 
            // lblPalavraDecodificada
            // 
            lblPalavraDecodificada.AutoSize = true;
            lblPalavraDecodificada.Location = new Point(28, 112);
            lblPalavraDecodificada.Name = "lblPalavraDecodificada";
            lblPalavraDecodificada.Size = new Size(117, 15);
            lblPalavraDecodificada.TabIndex = 4;
            lblPalavraDecodificada.Text = "Palavra Decodificada";
            // 
            // txtPalavraCodificada
            // 
            txtPalavraCodificada.Location = new Point(28, 54);
            txtPalavraCodificada.Name = "txtPalavraCodificada";
            txtPalavraCodificada.Size = new Size(236, 23);
            txtPalavraCodificada.TabIndex = 0;
            // 
            // lblPalavraCodificada
            // 
            lblPalavraCodificada.AutoSize = true;
            lblPalavraCodificada.Location = new Point(28, 36);
            lblPalavraCodificada.Name = "lblPalavraCodificada";
            lblPalavraCodificada.Size = new Size(105, 15);
            lblPalavraCodificada.TabIndex = 1;
            lblPalavraCodificada.Text = "Palavra Codificada";
            // 
            // btnDecriptografar
            // 
            btnDecriptografar.Location = new Point(89, 83);
            btnDecriptografar.Name = "btnDecriptografar";
            btnDecriptografar.Size = new Size(110, 23);
            btnDecriptografar.TabIndex = 2;
            btnDecriptografar.Text = "Decryptografia";
            btnDecriptografar.UseVisualStyleBackColor = true;
            btnDecriptografar.Click += btnDecriptografar_Click;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(388, 477);
            Controls.Add(groupBox1);
            Controls.Add(grpCryptografia);
            Name = "FrmPrincipal";
            Text = "Form2";
            grpCryptografia.ResumeLayout(false);
            grpCryptografia.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtPalavraNormal;
        private Label label1;
        private Button btnCriptografar;
        private TextBox txtPalavraDecriptografada;
        private Label lblPalavraCriptografada;
        private GroupBox grpCryptografia;
        private GroupBox groupBox1;
        private TextBox txtPalavraDecodificada;
        private Label lblPalavraDecodificada;
        private TextBox txtPalavraCodificada;
        private Label lblPalavraCodificada;
        private Button btnDecriptografar;
    }
}