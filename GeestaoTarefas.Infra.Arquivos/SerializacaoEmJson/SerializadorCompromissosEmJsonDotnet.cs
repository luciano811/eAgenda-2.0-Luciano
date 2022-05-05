using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorCompromissosEmJsonDotnet : ISerializadorCompromissos
    {
        private const string arquivoCompromissos = @"C:\temp\compromissos2.json";

        public List<Compromisso> CarregarCompromissosDoArquivo()
        {
            if (File.Exists(arquivoCompromissos) == false)
                return new List<Compromisso>();

            string compromissosJson = File.ReadAllText(arquivoCompromissos);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            return JsonConvert.DeserializeObject<List<Compromisso>>(compromissosJson, settings);
        }

        public void GravarCompromissosEmArquivo(List<Compromisso> compromissos)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            string compromissosJson = JsonConvert.SerializeObject(compromissos, settings);

            File.WriteAllText(arquivoCompromissos, compromissosJson);
        }
    }
}
