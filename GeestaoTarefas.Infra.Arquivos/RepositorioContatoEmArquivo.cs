using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace GestaoTarefas.Infra.Arquivos
{
    public class RepositorioContatoEmArquivo : IRepositorioContato
    {

        private readonly ISerializadorContatos serializador;
        List<Contato> contatos;
        private int contador = 0;

        public RepositorioContatoEmArquivo(ISerializadorContatos serializador)
        {
            this.serializador = serializador;

            contatos = serializador.CarregarContatosDoArquivo();

            if (contatos.Count > 0)
                contador = contatos.Max(x => x.Numero);
        }

        public List<Contato> SelecionarTodos()
        {
            return contatos;
        }

        public void Inserir(Contato novaContato)
        {
            novaContato.Numero = ++contador;
            contatos.Add(novaContato);

            serializador.GravarContatosEmArquivo(contatos);
        }

        public void Editar(Contato contato)
        {
            foreach (var item in contatos)
            {
                if (item.Numero == contato.Numero)
                {
                    item.Nome = contato.Nome;
                    break;
                }
            }

            serializador.GravarContatosEmArquivo(contatos);
        }

        public void Excluir(Contato contato)
        {
            contatos.Remove(contato);

            serializador.GravarContatosEmArquivo(contatos);
        }

        public void AdicionarItens(Contato contatoSelecionada, List<ItemContato> itens)
        {
            foreach (var item in itens)
            {
                contatoSelecionada.AdicionarItem(item);
            }

            serializador.GravarContatosEmArquivo(contatos);
        }

        public void AtualizarItens(Contato contatoSelecionada,
            List<ItemContato> itensConcluidos, List<ItemContato> itensPendentes)
        {
            foreach (var item in itensConcluidos)
            {
                contatoSelecionada.ConcluirItem(item);
            }

            foreach (var item in itensPendentes)
            {
                contatoSelecionada.MarcarPendente(item);
            }

            serializador.GravarContatosEmArquivo(contatos);
        }

        public List<Contato> SelecionarContatosConcluidas()
        {
            return contatos.Where(x => x.CalcularPercentualConcluido() == 100).ToList();
        }

        public List<Contato> SelecionarContatosPendentes()
        {
            return contatos.Where(x => x.CalcularPercentualConcluido() < 100).ToList();
        }
    }
}
