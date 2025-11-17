using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjADONET
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public List<Telefone> Telefones { get; private set; } = new List<Telefone>();

        public Pessoa(string nome, string cpf, DateOnly dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public override string? ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nCpf: {Cpf}\nData Nascimento: {DataNascimento}";
        }
    }
}
