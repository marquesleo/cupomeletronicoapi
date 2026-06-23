using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptografiaDecriptografia
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnCriptografar_Click(object sender, EventArgs e)
        {
            var cripty = new Vestillo.Lib.Cripto();

            txtPalavraDecriptografada.Text = cripty.Encrypt(txtPalavraNormal.Text);
        }

        private void btnDecriptografar_Click(object sender, EventArgs e)
        {

            var cripty = new Vestillo.Lib.Cripto();

            txtPalavraDecodificada.Text = cripty.Decrypt(txtPalavraCodificada.Text);
        }
    }
}
