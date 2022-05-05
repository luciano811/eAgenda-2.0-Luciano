using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;

namespace GestaoTarefas.WinApp
{
    public partial class ListagemContatos : Form
    {
        private IRepositorioContato repositorioContato;

        public ListagemContatos()
        {
            //SerializadorContatosEmBinario serializador = new SerializadorContatosEmBinario();

            //SerializadorContatosEmXml serializador = new SerializadorContatosEmXml();

            //SerializadorContatosEmJson serializador = new SerializadorContatosEmJson();

            SerializadorContatosEmJsonDotnet serializador = new SerializadorContatosEmJsonDotnet();

            repositorioContato = new RepositorioContatoEmArquivo(serializador);

            InitializeComponent();
            CarregarContatos();
        }

        public void CarregarContatos()
        {
            
            List<Contato> contatosPendentes = repositorioContato.SelecionarContatosPendentes();


            var acontatosPendentes = contatosPendentes
                .OrderBy(s => s.Cargo);

            listContatosPendentes.Items.Clear();

            foreach (Contato t in acontatosPendentes)
            {
                listContatosPendentes.Items.Add(t);
            }
        }

        private void btnInserir_Click(object sender, System.EventArgs e)
        {
            
            CadastroContatos tela = new CadastroContatos();
            tela.Contato = new Contato();

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                if(tela.Contato.Email != null)
                repositorioContato.Inserir(tela.Contato);

                CarregarContatos();
            }
        }

        private void btnEditar_Click(object sender, System.EventArgs e)
        {

            Contato contatoSelecionada = (Contato)listContatosPendentes.SelectedItem;

            if (contatoSelecionada == null)
            {
                MessageBox.Show("Selecione uma contato primeiro",
                "Edição de Contatos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CadastroContatos tela = new CadastroContatos();

            tela.Contato = contatoSelecionada;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioContato.Editar(tela.Contato);
                CarregarContatos();
            }
        }

        private void btnExcluir_Click(object sender, System.EventArgs e)
        {
            Contato contatoSelecionada = (Contato)listContatosPendentes.SelectedItem;

            if (contatoSelecionada == null)
            {
                MessageBox.Show("Selecione uma contato primeiro",
                "Exclusão de Contatos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a contato?",
                "Exclusão de Contatos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioContato.Excluir(contatoSelecionada);
                CarregarContatos();
            }
        }

        private void btnCadastrarItens_Click(object sender, System.EventArgs e)
        {
            Contato contatoSelecionada = (Contato)listContatosPendentes.SelectedItem;

            if (contatoSelecionada == null)
            {
                MessageBox.Show("Selecione uma contato primeiro",
                "Edição de Contatos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CadastroItensContato tela = new CadastroItensContato(contatoSelecionada);

            if (tela.ShowDialog() == DialogResult.OK)
            {
                List<ItemContato> itens = tela.ItensAdicionados;

                repositorioContato.AdicionarItens(contatoSelecionada, itens);

                CarregarContatos();
            }
        }                
    }
}
