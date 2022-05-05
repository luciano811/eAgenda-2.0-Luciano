using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace GestaoTarefas.Dominio
{
    [Serializable]
    public class Contato
    {
        private List<ItemContato> itens = new List<ItemContato>();

        public Contato()
        {
            DataCriacao = DateTime.Now;
        }

        public Contato(int n, string t) : this()
        {
            Numero = n;
            Nome = t;
            DataConclusao = null;
        }

        public int Numero { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public List<ItemContato> Itens { get { return itens; } }
        public bool emailEhValido { get; private set; }




        public override string ToString()
        {
            var percentual = CalcularPercentualConcluido();
                       
            return $"Número: {Numero},    Nome: {Nome},    Email: {Email},    Telefone: {Telefone},    Empresa: {Empresa},    Cargo: {Cargo}";
        }

        public void AdicionarItem(ItemContato item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
                itens.Add(item);
        }

        public void ConcluirItem(ItemContato item)
        {
            ItemContato itemContato = itens.Find(x => x.Equals(item));

            itemContato?.Concluir();

            var percentual = CalcularPercentualConcluido();

            if (percentual == 100)
                DataConclusao = DateTime.Now;
        }

        public void MarcarPendente(ItemContato item)
        {
            ItemContato itemContato = itens.Find(x => x.Equals(item));

            itemContato?.MarcarPendente();
        }

        public decimal CalcularPercentualConcluido()
        {
            if (itens.Count == 0)
                return 0;

            int qtdConcluidas = itens.Count(x => x.Concluido);

            var percentualConcluido = (qtdConcluidas / (decimal)itens.Count()) * 100;

            return Math.Round(percentualConcluido, 2);
        }
        public bool EmailEhValido(string emaiou)
        {
            try
            {
                MailAddress mail = new MailAddress(emaiou);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
