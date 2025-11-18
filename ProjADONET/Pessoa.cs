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
        public List<Endereco> Enderecos { get; private set; } = new List<Endereco>();

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
            var toString =  $"Id: {Id}\nNome: {Nome}\nCpf: {Cpf}\nData Nascimento: {DataNascimento}";
            toString += "\n========== TELEFONES ==========";
            if (Telefones.Count > 0)
            {
                foreach (var t in Telefones)
                {
                    toString += $"\n{t}";
                }
            }
            else
            {
                toString += $"\n{Nome} não possui telefones!";
            }

            toString += "\n========== ENDEREÇOS ==========";
            if (Enderecos.Count > 0)
            {
                foreach (var e in Enderecos)
                {
                    toString += $"\n{e}";
                    if (e != Enderecos.Last())
                        toString += "\n";
                }
            }
            else
            {
                toString += $"\n{Nome} não possui endereços!";
            }

            return toString;
        }
    }
}
