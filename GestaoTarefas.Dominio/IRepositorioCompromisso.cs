using System.Collections.Generic;

namespace GestaoTarefas.Dominio
{
    public interface IRepositorioCompromisso
    {
        void AdicionarItens(Compromisso compromissoSelecionada, List<ItemCompromisso> itens);
        void AtualizarItens(Compromisso compromissoSelecionada, List<ItemCompromisso> itensConcluidos, List<ItemCompromisso> itensPendentes);
        void Editar(Compromisso compromisso);
        void Excluir(Compromisso compromisso);
        void Inserir(Compromisso novaCompromisso);
        List<Compromisso> SelecionarTodos();

        List<Compromisso> SelecionarCompromissosConcluidas();

        List<Compromisso> SelecionarCompromissosPendentes();

    }
}