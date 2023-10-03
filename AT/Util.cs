using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AT.Arquivo;

namespace AT {
    internal class Util {
        static List<Conta> contas = new List<Conta>();

        public static double SaldoValido()
        {
            double saldo = -1;
            while (saldo < 0)
            {
                Console.Write("Digite o saldo inicial da conta (Saldo): ");
                if (double.TryParse(Console.ReadLine(), out saldo) && saldo >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("O saldo inicial da conta deve ser um número real maior ou igual a zero.");
                }
            }
            return saldo;
        }

        public static int ContaValida()
        {
            int id;
            do
            {
                Console.Write("Digite o número da conta (Id): ");

                var valido = int.TryParse(Console.ReadLine(), out id);

                if (!valido)
                {
                    Console.WriteLine("Digite um valor válido.");
                }
                else if (id <= 0)
                {
                    Console.WriteLine("O número da conta deve ser um inteiro maior que zero.");
                }
                else if (contas.Exists(c => c.Id == id))
                {
                    Console.WriteLine("Uma conta com este número já existe. Por favor, escolha outro número de conta.");
                }

            } while (id <= 0 || contas.Exists(c => c.Id == id));
            return id;
        }

        public static string NomeValido()
        {
            string nome = "";
            while (string.IsNullOrEmpty(nome) || nome.Split(' ').Length < 2)
            {
                Console.Write("Digite o nome do correntista (Nome e Sobrenome): ");
                nome = Console.ReadLine();
                if (string.IsNullOrEmpty(nome) || nome.Split(' ').Length < 2)
                {
                    Console.WriteLine("O nome do correntista deve conter pelo menos dois nomes (nome e sobrenome).");
                }
            }
            return nome;
        }

        public static bool VerificarCriteriosAlteracao(Conta conta)
        {
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada.");
                return false;
            }
            return true;
        }

        public static void RealizarDeposito(Conta conta)
        {
            double valor;

            do
            {
                Console.Write("Digite o valor a ser depositado: ");

                if (double.TryParse(Console.ReadLine(), out valor) && valor > 0)
                {
                    conta.Depositar(valor);
                    Console.WriteLine("Depósito realizado com sucesso!");
                    Console.WriteLine(" ");
                    break;
                }
                else
                {
                    Console.WriteLine("O valor de depósito deve ser um número real maior que zero. Tente novamente.");
                }
            } while (true);
        }

        public static void RealizarSaque(Conta conta)
        {
            double valor;

            do
            {
                Console.Write("Digite o valor a ser sacado: ");

                if (double.TryParse(Console.ReadLine(), out valor) && valor > 0)
                {
                    conta.Sacar(valor);
                    Console.WriteLine("Saque realizado com sucesso!");
                    break;
                }
                else
                {
                    Console.WriteLine("O valor de saque deve ser um número real maior que zero. Tente novamente.");
                }
            } while (true);
        }

        public static bool VerificarCriteriosExclusao(Conta conta)
        {
            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada. Tente novamente.");
                return false;
            }

            if (conta.Saldo != 0)
            {
                Console.WriteLine("Não é possível excluir a conta porque o saldo não é igual a zero. O saldo remanescente é: " + conta.Saldo);
                return false;
            }
            return true;
        }

        public static void MenuRelatoriosGerenciais()
        {
            Console.WriteLine("\nOpções de Relatórios Gerenciais:");
            Console.WriteLine("1. Listar clientes com saldo negativo");
            Console.WriteLine("2. Listar clientes com saldo acima de um determinado valor");
            Console.WriteLine("3. Listar todas as contas");
            Console.Write("Escolha uma opção: ");
        }
    }
}
