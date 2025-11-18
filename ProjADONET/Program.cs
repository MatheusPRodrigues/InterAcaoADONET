// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using ProjADONET;

Console.WriteLine("Hello, World!");

var connection = new SqlConnection(DBConnection.GetConnectionString());

#region "Insert"

//// INSERT PESSOA

//var pessoa = new Pessoa("Luiz Fernando", "54944105476", new DateOnly(1975, 10, 7));

//var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, dataNascimento) VALUES (@Nome, @Cpf, @DataNascimento); " +
//    $"SELECT SCOPE_IDENTITY();";

//connection.Open();

//var command = new SqlCommand(sqlInsertPessoa, connection);
//command.Parameters.AddWithValue("@Nome", pessoa.Nome);
//command.Parameters.AddWithValue("@Cpf", pessoa.Cpf);
//command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

//var pessoaId = Convert.ToInt32(command.ExecuteScalar()); // PEGO ID DO ÚLTIMO REGISTRO DE PESSOA

//// INSERT TELEFONES

//var telefone = new Telefone("21", "99431030", "Celular", pessoaId);
//var sqlInsertTelefone = $"INSERT INTO Telefones (ddd, numero, tipo, pessoaId) VALUES (@DDD, @Numero, @Tipo, @PessoaId)";

//command = new SqlCommand(sqlInsertTelefone, connection);
//command.Parameters.AddWithValue("@DDD", telefone.DDD);
//command.Parameters.AddWithValue("@Numero", telefone.Numero);
//command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
//command.Parameters.AddWithValue("@PessoaId", telefone.PessoaId);

//command.ExecuteNonQuery();

//telefone = new Telefone("21", "30302401", "Residencia", pessoaId);
//sqlInsertTelefone = $"INSERT INTO Telefones (ddd, numero, tipo, pessoaId) VALUES (@DDD, @Numero, @Tipo, @PessoaId)";

//command = new SqlCommand(sqlInsertTelefone, connection);
//command.Parameters.AddWithValue("@DDD", telefone.DDD);
//command.Parameters.AddWithValue("@Numero", telefone.Numero);
//command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
//command.Parameters.AddWithValue("@PessoaId", telefone.PessoaId);

//command.ExecuteNonQuery();

//// INSERT ENDERECOS

//var endereco = new Endereco("R. do Grau", 304, null, "Vila das Motos", "Belo Horizonte", "MG", "20541943", pessoaId);
//var sqlInsertEndereco = "INSERT INTO Enderecos (logradouro, numero, complemento, bairro, cidade, estado, cep, pessoaId) VALUES " +
//                        "(@Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Cep, @PessoaId)";

//command = new SqlCommand(sqlInsertEndereco, connection);
//command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
//command.Parameters.AddWithValue("@Numero", endereco.Numero);
//command.Parameters.AddWithValue("@Complemento", (endereco.Complemento is null || String.IsNullOrEmpty(endereco.Complemento))
//                                                                                 ? DBNull.Value :
//                                                                                 endereco.Complemento);
//command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
//command.Parameters.AddWithValue("@Cidade", endereco.Cidade);
//command.Parameters.AddWithValue("@Estado", endereco.Estado);
//command.Parameters.AddWithValue("@Cep", endereco.Cep);
//command.Parameters.AddWithValue("@PessoaId", endereco.PessoaId);

//command.ExecuteNonQuery();

//endereco = new Endereco("R. do Drift", 304, null, "Vila dos Cars", "Belo Horizonte", "MG", "20541952", pessoaId);
//sqlInsertEndereco = "INSERT INTO Enderecos (logradouro, numero, complemento, bairro, cidade, estado, cep, pessoaId) VALUES " +
//                        "(@Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Cep, @PessoaId)";

//command = new SqlCommand(sqlInsertEndereco, connection);
//command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
//command.Parameters.AddWithValue("@Numero", endereco.Numero);
//command.Parameters.AddWithValue("@Complemento", (endereco.Complemento is null || String.IsNullOrEmpty(endereco.Complemento))
//                                                                                 ? DBNull.Value :
//                                                                                 endereco.Complemento);
//command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
//command.Parameters.AddWithValue("@Cidade", endereco.Cidade);
//command.Parameters.AddWithValue("@Estado", endereco.Estado);
//command.Parameters.AddWithValue("@Cep", endereco.Cep);
//command.Parameters.AddWithValue("@PessoaId", endereco.PessoaId);

//command.ExecuteNonQuery();

//connection.Close();

#endregion

#region "Select"

//connection.Open();

//var sqlSelectPessoas = "SELECT id, nome, cpf, dataNascimento FROM Pessoas";
//command = new SqlCommand(sqlSelectPessoas, connection);
//var reader = command.ExecuteReader();

//while (reader.Read())
//{
//    var pessoaLida = new Pessoa(
//        reader.GetString(1),
//        reader.GetString(2),
//        DateOnly.FromDateTime(reader.GetDateTime(3))
//    );
//    pessoaLida.SetId(reader.GetInt32(0));

//    Console.WriteLine(pessoaLida);
//}
//reader.Close();
//connection.Close();

#endregion

#region "SELECT PESSOA COM TELEFONES E ENDEREÇOS"

connection.Open();

var pessoas = new List<Pessoa>();

var sqlSelectPessoas = "SELECT id, nome, cpf, dataNascimento FROM Pessoas";
using (var command = new SqlCommand(sqlSelectPessoas, connection))
{
    using (var reader = command.ExecuteReader())
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
    using (var command = new SqlCommand(sqlSelectTelefones, connection))
    {
        command.Parameters.AddWithValue("@PessoaId", p.Id);
        using (var reader = command.ExecuteReader())
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

    var sqlSelectEnderecos = "SELECT logradouro, numero, complemento, bairro, cidade, estado, cep FROM Enderecos WHERE pessoaId = @PessoaId";
    using (var command = new SqlCommand(sqlSelectEnderecos, connection))
    {
        command.Parameters.AddWithValue("@PessoaId", p.Id);
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var enderecoLido = new Endereco(
                    reader.GetString(0),
                    reader.GetInt32(1),
                    reader.IsDBNull(2) ? null : reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    p.Id
                );
                p.Enderecos.Add(enderecoLido);
            }
        }
    }
}
connection.Close();

Console.WriteLine("================= PESSOAS =================");
foreach (var p in pessoas)
{
    Console.WriteLine(p);
    Console.WriteLine();
    Console.WriteLine("===========================================");
    Console.WriteLine();
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