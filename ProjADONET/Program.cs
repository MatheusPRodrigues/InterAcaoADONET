// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using ProjADONET;

Console.WriteLine("Hello, World!");

var connection = new SqlConnection(DBConnection.GetConnectionString());

#region "Insert"

//var pessoa = new Pessoa("Felipe", "45678901234", new DateOnly(1994, 7, 23));

//var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, dataNascimento) VALUES (@Nome, @Cpf, @DataNascimento); " +
//    $"SELECT SCOPE_IDENTITY();";

//connection.Open();

//var command = new SqlCommand(sqlInsertPessoa, connection);
//command.Parameters.AddWithValue("@Nome", pessoa.Nome);
//command.Parameters.AddWithValue("@Cpf", pessoa.Cpf);
//command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

//var pessoaId = Convert.ToInt32(command.ExecuteScalar());

//var telefone = new Telefone("11", "987654321", "Celular", pessoaId);

//var sqlInsertTelefone = $"INSERT INTO Telefones (ddd, numero, tipo, pessoaId) VALUES (@DDD, @Numero, @Tipo, @PessoaId)";

//command = new SqlCommand(sqlInsertTelefone, connection);
//command.Parameters.AddWithValue("@DDD", telefone.DDD);
//command.Parameters.AddWithValue("@Numero", telefone.Numero);
//command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
//command.Parameters.AddWithValue("@PessoaId", telefone.PessoaId);

//command.ExecuteNonQuery();

//connection.Close();

#endregion

#region "Select"

connection.Open();

var sqlSelectPessoas = "SELECT id, nome, cpf, dataNascimento FROM Pessoas";
var command = new SqlCommand(sqlSelectPessoas, connection);
var reader = command.ExecuteReader();

while (reader.Read())
{
    var pessoaLida = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
    );
    pessoaLida.SetId(reader.GetInt32(0));

    Console.WriteLine(pessoaLida);
}
reader.Close();
connection.Close();

#endregion

#region "SELECT PESSOA COM TELEFONES"

connection.Open();

var pessoas = new List<Pessoa>();

sqlSelectPessoas = "SELECT id, nome, cpf, dataNascimento FROM Pessoas";
using (command = new SqlCommand(sqlSelectPessoas, connection))
{
    using (reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var pessoaLida = new Pessoa(
                reader.GetString(1),
                reader.GetString(2),
                DateOnly.FromDateTime(reader.GetDateTime(3))
            );
            pessoaLida.SetId(reader.GetInt32(0));

            pessoas.Add(pessoaLida);
        }
        reader.Close();
    }
}

foreach (var p in pessoas)
{
    var sqlSelectTelefones = "SELECT ddd, numero, tipo FROM Telefones WHERE pessoaId = @PessoaId";
    using (command = new SqlCommand(sqlSelectTelefones, connection))
    {
        command.Parameters.AddWithValue("@PessoaId", p.Id);
        using (reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var telefoneLido = new Telefone(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    p.Id
                );
                p.Telefones.Add(telefoneLido);
            }
        }
    }
}
connection.Close();

Console.WriteLine("========== PESSOAS COM TELEFONE ==========");
foreach (var p in pessoas)
{
    if (p.Telefones.Count > 0)
    {
        Console.WriteLine(p);
        Console.WriteLine($"Telefones do {p.Nome}");
        foreach (var t in p.Telefones)
        {
            Console.WriteLine(t);
        }
    }
    else
        Console.WriteLine(p);

    Console.WriteLine("===========================================");
}

#endregion

#region "Update"

//connection.Open();

//var sqlUpdatePessoa = "UPDATE Pessoas SET nome = @Nome WHERE id = @Id";

//command = new SqlCommand(sqlUpdatePessoa, connection);
//command.Parameters.AddWithValue("@Nome", "Maurício Leonardo");
//command.Parameters.AddWithValue("@Id", 1);

//var linhas = command.ExecuteNonQuery();

//if (linhas > 0)
//    Console.WriteLine("Pessoa atualizada com sucesso!");
//else
//    Console.WriteLine("Falha na atualização!");

//connection.Close();

#endregion

#region "Delete"

//connection.Open();

//var sqlDeletePessoas = "DELETE FROM Pessoas WHERE id = @Id";

//command = new SqlCommand(sqlDeletePessoas, connection);
//command.Parameters.AddWithValue("@Id", 2);

//var linhas = command.ExecuteNonQuery();

//if (linhas > 0)
//    Console.WriteLine("Pessoa excluída com sucesso!");
//else
//    Console.WriteLine("Erro ao excluir pessoa!");

//connection.Close();

#endregion