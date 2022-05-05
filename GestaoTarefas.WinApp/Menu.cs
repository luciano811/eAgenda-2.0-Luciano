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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            CarregarMenu();
        }

        private void CarregarMenu()
        {
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {            
                ListagemTarefas tela = new ListagemTarefas();

                DialogResult resultado = tela.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListagemContatos tela1 = new ListagemContatos();

            DialogResult resultadoo = tela1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListagemCompromissos tela2 = new ListagemCompromissos();

            DialogResult resultadoo = tela2.ShowDialog();
        }
    }
}
