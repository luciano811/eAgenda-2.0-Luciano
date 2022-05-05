using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Infra.Arquivos
{
    public interface ISerializadorCompromissos
    {
        List<Compromisso> CarregarCompromissosDoArquivo();
        void GravarCompromissosEmArquivo(List<Compromisso> compromissos);
    }
}
