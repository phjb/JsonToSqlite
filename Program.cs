using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.Data.Sqlite;

namespace JsonToSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertSqlite insert = new InsertSqlite();
            // Console.WriteLine($"Lendo os Estados");
            // var estados = ReadJson.ReadEstado();
            // Console.WriteLine($"Inserindo os estados no banco");
            // insert.InsertEstado(estados);

            Console.WriteLine($"Lendo as Cidades");
            var cidades = ReadJson.ReadCidade();
            Console.WriteLine($"Inserindo as cidades no banco");
            insert.InsertCidade(cidades);
        }
    }
    public class ReadJson
    {
        public static List<Estado> ReadEstado()
        {
            using (StreamReader file = File.OpenText(@"Files\Estados.json"))
            {
                return JsonConvert.DeserializeObject<List<Estado>>(file.ReadToEnd());
            }
        }
        public static List<Cidade> ReadCidade()
        {
            using (StreamReader file = File.OpenText(@"Files\Cidades.json"))
            {
                return JsonConvert.DeserializeObject<List<Cidade>>(file.ReadToEnd());
            }
        }
    }
    public class InsertSqlite
    {
        private static SqliteConnection conn;
        private static SqliteConnection DbConnection()
        {
            conn = new SqliteConnection("Data Source=E:/dev/Projetos/AspNetCore/SistemaEscolar/src/Infrastructure/sqlite/schooldb.sqlite");
            conn.Open();
            return conn;
        }
        public void InsertEstado(List<Estado> estados)
        {
            foreach (var estado in estados)
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    // define parameters and their values
                    cmd.CommandText = "INSERT INTO Estado (id, nome, sigla) VALUES (@id, @nome, @sigla) ";
                    cmd.Parameters.Add("@id", SqliteType.Integer).Value = estado.Id;
                    cmd.Parameters.Add("@nome", SqliteType.Text, 50).Value = estado.Nome;
                    cmd.Parameters.Add("@sigla", SqliteType.Text, 2).Value = estado.Sigla;
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void InsertCidade(List<Cidade> cidades)
        {
            foreach (var cidade in cidades)
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    // define parameters and their values
                    cmd.CommandText = "INSERT INTO Cidade (id, nome, id_estado) VALUES (@id, @nome, @estado) ";
                    cmd.Parameters.Add("@id", SqliteType.Integer).Value = cidade.Id;
                    cmd.Parameters.Add("@nome", SqliteType.Text, 50).Value = cidade.Nome;
                    cmd.Parameters.Add("@estado", SqliteType.Text, 2).Value = cidade.Estado;
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }

    public class Estado
    {
        public string Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }

    public class Cidade
    {
        public string Id { get; set; }
        public string Estado { get; set; }
        public string Nome { get; set; }

    }



}
