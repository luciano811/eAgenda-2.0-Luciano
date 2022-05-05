using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorCompromissosEmBinario : ISerializadorCompromissos
    {
        private const string arquivoCompromissos = @"C:\temp\compromissos.bin";
        
        public void GravarCompromissosEmArquivo(List<Compromisso> compromissos)
        {
            BinaryFormatter serializador = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, compromissos);

            byte[] bytesCompromissos = ms.ToArray();

            File.WriteAllBytes(arquivoCompromissos, bytesCompromissos);
        }

        public List<Compromisso> CarregarCompromissosDoArquivo()
        {
            if (File.Exists(arquivoCompromissos) == false)
                return new List<Compromisso>();

            BinaryFormatter serializador = new BinaryFormatter();

            byte[] bytesCompromissos = File.ReadAllBytes(arquivoCompromissos);

            MemoryStream ms = new MemoryStream(bytesCompromissos);

            return (List<Compromisso>)serializador.Deserialize(ms);
        }
    }
}
