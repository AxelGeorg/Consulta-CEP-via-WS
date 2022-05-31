using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultarCEPS
{
    public partial class FrmConsultarCEPs : Form
    {
        public FrmConsultarCEPs()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxCep.Text))
            {
                MessageBox.Show("Informe um CEP válido!!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                LimparCamposFrm();

                return;
            }

            using (var ws = new WSCorreios.AtendeClienteClient())
            {
                try
                {
                    var enderecoERP = ws.consultaCEP(this.textBoxCep.Text.Trim());

                    this.textBoxEstado.Text = enderecoERP.uf    ;
                    this.textBoxCidade.Text = enderecoERP.cidade;
                    this.textBoxBairro.Text = enderecoERP.bairro;
                    this.textBoxRua   .Text = enderecoERP.end   ;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposFrm();
        }

        private void LimparCamposFrm()
        {
            this.textBoxCep.Clear();
            this.textBoxEstado.Clear();
            this.textBoxCidade.Clear();
            this.textBoxBairro.Clear();
            this.textBoxRua.Clear();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
