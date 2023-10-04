using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AT.Util;
using static AT.Arquivo;

namespace AT {
    internal class ContaCrud {

        public static void IncluirConta(ref List<Conta> contas)
        {

            int id = ContaValida();
            string nome = NomeValido();
            double saldo = SaldoValido();

            Conta novaConta = new Conta(id, nome, saldo);
            contas.Add(novaConta);
            Console.WriteLine("Conta adicionada com sucesso!");
        }

        public static void AlterarSaldo(ref List<Conta> contas)
        {
            if (!VerificarLista(contas))
            {
                return;
            }

            int id;
            Conta conta;
            do
            {
                Console.Write("Digite o número da conta (Id): ");
                id = int.Parse(Console.ReadLine());
                conta = contas.Find(c => c.Id == id);

                if (conta == null)
                {
                    Console.WriteLine("Conta não encontrada. Tente novamente.");
                }
            } while (conta == null);

            if (VerificarCriteriosAlteracao(conta))
            {
                RealizarOperacao(conta);
            }
        }

        static void RealizarOperacao(Conta conta)
        {
            int opcao;
            while (true)
            {
                Console.WriteLine("Selecione a operação:");
                Console.WriteLine("1. Depósito");
                Console.WriteLine("2. Saque");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    if (opcao == 1)
                    {
                        RealizarDeposito(conta);
                        break;
                    }
                    else if (opcao == 2)
                    {
                        RealizarSaque(conta);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            }
        }

        public static void ExcluirConta(ref List<Conta> contas)
        {
            if (!VerificarLista(contas))
            {
                return;
            }

            int id;
            Conta conta = null;

            do
            {
                Console.Write("Digite o número da conta (Id) a ser excluída: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    conta = contas.Find(c => c.Id == id);

                    if (VerificarCriteriosExclusao(conta))
                    {
                        contas.Remove(conta);
                        Console.WriteLine("Conta excluída com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Operação de exclusão cancelada.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Valor inserido não é válido. Tente novamente.");
                }
            } while (conta == null || conta.Saldo != 0);
        }

        public static void ExibirRelatoriosGerenciais(List<Conta> contas)
        {

            if (!VerificarLista(contas))
            {
                return;
            }

            MenuRelatoriosGerenciais();
            int opcao = Util.ValidaInt(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    ListarClientesComSaldoNegativo(contas);
                    break;
                case 2:
                    ListarClientesComSaldoAcimaDeValor(contas);
                    break;
                case 3:
                    ListarTodasContas(contas);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void ListarClientesComSaldoNegativo(List<Conta> contas)
        {
            Console.WriteLine("\nClientes com saldo negativo:");
            foreach (var conta in contas)
            {
                if (conta.Saldo < 0)
                {
                    Console.WriteLine($"Conta ID: {conta.Id}, Nome: {conta.Nome}, Saldo: {conta.Saldo}");
                }
            }
        }

        static void ListarClientesComSaldoAcimaDeValor(List<Conta> contas)
        {
            Console.Write("\nDigite o valor mínimo de saldo: ");
            if (double.TryParse(Console.ReadLine(), out double valorMinimo))
            {
                Console.WriteLine($"Clientes com saldo acima de {valorMinimo}:");
                foreach (var conta in contas)
                {
                    if (conta.Saldo > valorMinimo)
                    {
                        Console.WriteLine($"Conta ID: {conta.Id}, Nome: {conta.Nome}, Saldo: {conta.Saldo}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Valor inválido.");
            }
        }

        static void ListarTodasContas(List<Conta> contas)
        {
            Console.WriteLine("\nTodas as contas:");
            foreach (var conta in contas)
            {
                Console.WriteLine(conta);
            }
        }

        static bool VerificarLista(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("\nNão existem contas para realizar esta operação.");
                return false;
            }
            return true;
        }

    }
}
