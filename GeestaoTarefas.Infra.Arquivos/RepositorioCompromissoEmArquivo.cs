using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace GestaoTarefas.Infra.Arquivos
{
    public class RepositorioCompromissoEmArquivo : IRepositorioCompromisso
    {

        private readonly ISerializadorCompromissos serializador;
        List<Compromisso> compromissos;
        private int contador = 0;

        public RepositorioCompromissoEmArquivo(ISerializadorCompromissos serializador)
        {
            this.serializador = serializador;

            compromissos = serializador.CarregarCompromissosDoArquivo();

            if (compromissos.Count > 0)
                contador = compromissos.Max(x => x.Numero);
        }

        public List<Compromisso> SelecionarTodos()
        {
            return compromissos;
        }

        public void Inserir(Compromisso novaCompromisso)
        {
            novaCompromisso.Numero = ++contador;
            compromissos.Add(novaCompromisso);

            serializador.GravarCompromissosEmArquivo(compromissos);
        }

        public void Editar(Compromisso compromisso)
        {
            foreach (var item in compromissos)
            {
                if (item.Numero == compromisso.Numero)
                {
                    item.Titulo = compromisso.Titulo;
                    break;
                }
            }

            serializador.GravarCompromissosEmArquivo(compromissos);
        }

        public void Excluir(Compromisso compromisso)
        {
            compromissos.Remove(compromisso);

            serializador.GravarCompromissosEmArquivo(compromissos);
        }

        public void AdicionarItens(Compromisso compromissoSelecionada, List<ItemCompromisso> itens)
        {
            foreach (var item in itens)
            {
                compromissoSelecionada.AdicionarItem(item);
            }

            serializador.GravarCompromissosEmArquivo(compromissos);
        }

        public void AtualizarItens(Compromisso compromissoSelecionada,
            List<ItemCompromisso> itensConcluidos, List<ItemCompromisso> itensPendentes)
        {
            foreach (var item in itensConcluidos)
            {
                compromissoSelecionada.ConcluirItem(item);
            }

            foreach (var item in itensPendentes)
            {
                compromissoSelecionada.MarcarPendente(item);
            }

            serializador.GravarCompromissosEmArquivo(compromissos);
        }

        public List<Compromisso> SelecionarCompromissosConcluidas()
        {
            return compromissos.Where(x => x.DataConclusao.HasValue).ToList();
        }

        public List<Compromisso> SelecionarCompromissosPendentes()
        {
            return compromissos.Where(x => x.DataConclusao == null).ToList();
            
        }
    }
}
