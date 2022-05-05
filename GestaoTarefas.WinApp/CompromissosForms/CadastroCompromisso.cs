using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp
{
    public partial class CadastroCompromisso : Form
    {
        private IRepositorioContato repositorioContato;
        //private IRepositorioCompromisso repositorioCompromisso;

        private Compromisso compromisso;
        //private Contato contato;
        //private ListagemContatos listagemContatos;
        //private ListagemCompromissos listagemCompromissos;

        public CadastroCompromisso()
        {
            SerializadorContatosEmJsonDotnet serializador = new SerializadorContatosEmJsonDotnet();

            repositorioContato = new RepositorioContatoEmArquivo(serializador);

            InitializeComponent();
            CarregarContatos();
        }

        public Compromisso Compromisso
        {
            get
            {
                return compromisso;
            }
            set
            {
                compromisso = value;
                txtNumero.Text = compromisso.Numero.ToString();
                txtTitulo.Text = compromisso.Titulo;
            }
        }      

        private void btnGravar_Click(object sender, EventArgs e)
        {            
            compromisso.Titulo = txtTitulo.Text;

            Contato contatoSelecionada = (Contato)listContatosPendentes.SelectedItem;

            //CASO QUEIRA OBRIGÁ-LO A ASSOCIAR A UM CONTATO, SÓ DESCOMENTAR ABAIXO:

            //if (contatoSelecionada == null)
            //{
            //    MessageBox.Show("Selecione uma contato primeiro",
            //    "Edição de Compromisso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //Para evitar a EXCEPTION
            if (contatoSelecionada == null)
            {
                return;
            }

            compromisso.ContatoAssociado = contatoSelecionada.Nome;

            /*
            CadastroContatos tela = new CadastroContatos();
            CadastroCompromisso tela1 = new CadastroCompromisso();

            tela.Contato = contatoSelecionada;

            tela.Compromisso = new Compromisso();

            DialogResult resultado = tela1.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioCompromisso.Inserir(tela.Compromisso);
                listagemCompromissos.CarregarCompromissos();
            }
            */

        }

        public void CarregarContatos()
        {
            List<Contato> contatosConcluidas = repositorioContato.SelecionarContatosConcluidas();
                        
            List<Contato> contatosPendentes = repositorioContato.SelecionarContatosPendentes();

            listContatosPendentes.Items.Clear();

            foreach (Contato t in contatosPendentes)
            {
                listContatosPendentes.Items.Add(t);
            }
        }
    }
}
