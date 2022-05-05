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
    public partial class CadastroItensContato : Form
    {
        private readonly Contato contato;

        public CadastroItensContato(Contato contato)
        {
            InitializeComponent();
            
            this.contato = contato;

            labelTituloContato.Text = contato.Nome;

            foreach (ItemContato item in contato.Itens)
            {
                listItensContato.Items.Add(item);
            }            
        }

        public List<ItemContato> ItensAdicionados 
        {
            get 
            {
                return listItensContato.Items.Cast<ItemContato>().ToList();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {            
            List<string> titulos = ItensAdicionados.Select(x => x.Titulo).ToList();

            if (titulos.Count == 0 || titulos.Contains(txtTituloItem.Text) == false)
            {
                ItemContato itemContato = new ItemContato();

                itemContato.Titulo = txtTituloItem.Text;

                listItensContato.Items.Add(itemContato);
            }
        }
    }
}
