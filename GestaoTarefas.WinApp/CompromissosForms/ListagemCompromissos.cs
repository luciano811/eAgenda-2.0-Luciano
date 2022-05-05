using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp
{
    public partial class ListagemCompromissos : Form
    {
        private IRepositorioCompromisso repositorioCompromisso;
        //private Compromisso compromisso;


        public ListagemCompromissos()
        {
            //SerializadorCompromissosEmBinario serializador = new SerializadorCompromissosEmBinario();

            //SerializadorCompromissosEmXml serializador = new SerializadorCompromissosEmXml();

            //SerializadorCompromissosEmJson serializador = new SerializadorCompromissosEmJson();

            SerializadorCompromissosEmJsonDotnet serializador = new SerializadorCompromissosEmJsonDotnet();

            repositorioCompromisso = new RepositorioCompromissoEmArquivo(serializador);

            InitializeComponent();
            CarregarCompromissos();
        }

        public void CarregarCompromissos()
        {
            List<Compromisso> compromissosConcluidas = repositorioCompromisso.SelecionarCompromissosConcluidas();

            listCompromissosConcluidas.Items.Clear();

            foreach (Compromisso t in compromissosConcluidas)
            {                
                listCompromissosConcluidas.Items.Add(t);
            }

            List<Compromisso> compromissosPendentes = repositorioCompromisso.SelecionarCompromissosPendentes();

            listCompromissosPendentes.Items.Clear();
            
            foreach (Compromisso t in compromissosPendentes)
            {                
                listCompromissosPendentes.Items.Add(t);
            }
        }

        private void btnInserir_Click(object sender, System.EventArgs e)
        {
            CadastroCompromisso tela = new CadastroCompromisso();
            tela.Compromisso = new Compromisso();

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioCompromisso.Inserir(tela.Compromisso);
                CarregarCompromissos();
            }
        }

        private void btnEditar_Click(object sender, System.EventArgs e)
        {

            Compromisso compromissoSelecionada = (Compromisso)listCompromissosPendentes.SelectedItem;

            if (compromissoSelecionada == null)
            {
                MessageBox.Show("Selecione uma compromisso primeiro",
                "Edição de Compromissos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CadastroCompromisso tela = new CadastroCompromisso();

            tela.Compromisso = compromissoSelecionada;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioCompromisso.Editar(tela.Compromisso);
                CarregarCompromissos();
            }
        }

        private void btnExcluir_Click(object sender, System.EventArgs e)
        {
            Compromisso compromissoSelecionada = (Compromisso)listCompromissosPendentes.SelectedItem;

            if (compromissoSelecionada == null)
            {
                MessageBox.Show("Selecione uma compromisso primeiro",
                "Exclusão de Compromissos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a compromisso?",
                "Exclusão de Compromissos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioCompromisso.Excluir(compromissoSelecionada);
                CarregarCompromissos();
            }
        }

        private void btnFinalizar_Click(object sender, System.EventArgs e)
        {
            Compromisso compromissoSelecionada = (Compromisso)listCompromissosPendentes.SelectedItem;

            if (compromissoSelecionada == null)
            {
                MessageBox.Show("Selecione um compromisso primeiro",
                "Edição de Compromissos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CadastroCompromisso tela = new CadastroCompromisso();

            tela.Compromisso = compromissoSelecionada;

            compromissoSelecionada.DataConclusao = DateTime.Now;
                        
            CarregarCompromissos();

            
        }
    }
}
