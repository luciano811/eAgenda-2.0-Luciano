using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorContatosEmBinario : ISerializadorContatos
    {
        private const string arquivoContatos = @"C:\temp\contatos.bin";
        
        public void GravarContatosEmArquivo(List<Contato> contatos)
        {
            BinaryFormatter serializador = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, contatos);

            byte[] bytesContatos = ms.ToArray();

            File.WriteAllBytes(arquivoContatos, bytesContatos);
        }

        public List<Contato> CarregarContatosDoArquivo()
        {
            if (File.Exists(arquivoContatos) == false)
                return new List<Contato>();

            BinaryFormatter serializador = new BinaryFormatter();

            byte[] bytesContatos = File.ReadAllBytes(arquivoContatos);

            MemoryStream ms = new MemoryStream(bytesContatos);

            return (List<Contato>)serializador.Deserialize(ms);
        }
    }
}
