using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoTarefas.Dominio
{
    [Serializable]
    public class Compromisso
    {
        private List<ItemCompromisso> itens = new List<ItemCompromisso>();

        public Compromisso()
        {
            DataCriacao = DateTime.Now;
        }

        public Compromisso(int n, string t) : this()
        {
            Numero = n;
            Titulo = t;
            DataConclusao = null;
        }

        public int Numero { get; set; }
        public string Titulo { get; set; }
        public string ContatoAssociado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public List<ItemCompromisso> Itens { get { return itens; } }
        public bool Encerrado { get; set; }



        public override string ToString()
        {
            var percentual = CalcularPercentualConcluido();

            if (DataConclusao.HasValue)
            {
                return $"Número:     {Numero},     Título: {Titulo},    Contato Associado: {ContatoAssociado}  " +
                    $"Concluída: {DataConclusao.Value.ToShortDateString()}";
            }

            
            return $"Número: {Numero},     Título: {Titulo},      Contato Associado: {ContatoAssociado},     Data de abertura: {DataCriacao}   ";
        }

        public void AdicionarItem(ItemCompromisso item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
                itens.Add(item);
        }

        public void ConcluirItem(ItemCompromisso item)
        {
            ItemCompromisso itemCompromisso = itens.Find(x => x.Equals(item));

            itemCompromisso?.Concluir();

            var percentual = CalcularPercentualConcluido();

            if (percentual == 100)
                DataConclusao = DateTime.Now;
        }

        public static explicit operator Compromisso(Contato v)
        {
            throw new NotImplementedException();
        }

        public void MarcarPendente(ItemCompromisso item)
        {
            ItemCompromisso itemCompromisso = itens.Find(x => x.Equals(item));

            itemCompromisso?.MarcarPendente();
        }

        public decimal CalcularPercentualConcluido()
        {
            if (itens.Count == 0)
                return 0;

            int qtdConcluidas = itens.Count(x => x.Concluido);

            var percentualConcluido = (qtdConcluidas / (decimal)itens.Count()) * 100;

            return Math.Round(percentualConcluido, 2);
        }


    }
}
