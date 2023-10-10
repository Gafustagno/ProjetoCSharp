using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AT.Util;
using static AT.Arquivo;

namespace AT {
    internal class ContaCrud {

        public static void IncluirConta(ref List<Conta> contas) {
            int id;
            do             {
                id = ContaValida(contas);
                if (VerificarID(contas, id)) {
                    Console.WriteLine("Uma conta com este número já existe. Tente novamente.");
                }
            } while (VerificarID(contas, id));

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
                id = ValidaInt(Console.ReadLine());
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

        static void RealizarOperacao(Conta conta) {
            int opcao;
            while (true) {
                Console.WriteLine("Selecione a operação:");
                Console.WriteLine("1. Depósito");
                Console.WriteLine("2. Saque");

                opcao = ValidaInt(Console.ReadLine());

                if (opcao == 1) {
                    RealizarDeposito(conta);
                    break;
                }
                else if (opcao == 2) {
                    RealizarSaque(conta);
                    break;
                }
                else {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            }
        }

        public static void ExcluirConta(ref List<Conta> contas) {
            if (!VerificarLista(contas)) {
                return;
            }

            int id;
            Conta conta = null;

            do {
                Console.Write("Digite o número da conta (Id) a ser excluída: ");
                id = ValidaInt(Console.ReadLine());

                conta = contas.Find(c => c.Id == id);

                if (conta != null && VerificarCriteriosExclusao(conta)) {
                    contas.Remove(conta);
                    Console.WriteLine("Conta excluída com sucesso!");
                }
                else if (conta != null) {
                    Console.WriteLine("Operação de exclusão cancelada.");
                    return;
                }
                else {
                    Console.WriteLine("Conta não encontrada. Tente novamente.");
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
            int opcao = ValidaInt(Console.ReadLine());

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

        static void ListarClientesComSaldoNegativo(List<Conta> contas) {
            Console.WriteLine("\nClientes com saldo negativo:");
            foreach (var conta in contas) {
                if (conta.Saldo < 0) {
                    Console.WriteLine(conta);
                }
            }
        }

        static void ListarClientesComSaldoAcimaDeValor(List<Conta> contas) {
            double valorMinimo;

            Console.Write("\nDigite o valor mínimo de saldo: ");
            valorMinimo = ValidaDouble(Console.ReadLine());

            Console.WriteLine($"Clientes com saldo acima de {valorMinimo}:");
            foreach (var conta in contas) {
                if (conta.Saldo > valorMinimo) {
                    Console.WriteLine(conta);
                }
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
