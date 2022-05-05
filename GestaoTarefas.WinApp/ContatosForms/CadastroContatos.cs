using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp
{
    public partial class CadastroContatos : Form
    {
        private Contato contato;


        public CadastroContatos()
        {
            InitializeComponent();
        }

        public Contato Contato
        {
            get
            {
                return contato;
            }
            set
            {
                contato = value;
                txtNumero.Text = contato.Numero.ToString();
                txtNome.Text = contato.Nome;
                txtEmail.Text = contato.Email;
                txtTelefone.Text = contato.Telefone;
                txtEmpresa.Text = contato.Empresa;
                txtCargo.Text = contato.Cargo;
            }
        }

        public Compromisso Compromisso { get; internal set; }

        private void btnGravar_Click(object sender, EventArgs e)
        {           
            if (contato.EmailEhValido(txtEmail.Text) == false)
            {
                MessageBox.Show("Digite um email/telefone válido",
                "EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;              
            }

            contato.Nome = txtNome.Text;
            contato.Email = txtEmail.Text;
            contato.Telefone = txtTelefone.Text;
            contato.Empresa = txtEmpresa.Text;
            contato.Cargo = txtCargo.Text;
        }
                
    }
}
