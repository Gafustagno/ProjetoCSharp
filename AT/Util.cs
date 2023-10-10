using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AT.Arquivo;

namespace AT {
    internal class Util {
        static List<Conta> contas = new List<Conta>();

        public static bool VerificarID(List<Conta> contas, int id)
        {
            return contas.Exists(c => c.Id == id);
        }

        public static double SaldoValido()
        {
            double saldo = -1;
            while (true)
            {
                Console.Write("Digite o saldo inicial da conta: ");
                saldo = ValidaDouble(Console.ReadLine());

                if (saldo >= 0)
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

        public static int ContaValida(List<Conta> contas)
        {
            int id;
            do
            {
                Console.Write("Digite o número da conta (Id): ");
                id = ValidaInt(Console.ReadLine());

                if (id <= 0)
                {
                    Console.WriteLine("O número da conta deve ser um inteiro maior que zero.");
                }
                else if (VerificarID(contas, id))
                {
                    Console.WriteLine("Uma conta com este número já existe. Por favor, escolha outro número de conta.");
                }

            } while (id <= 0 || VerificarID(contas, id));

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

        public static void RealizarDeposito(Conta conta) {
            double valor;

            do {
                Console.Write("Digite o valor a ser depositado: ");

                valor = ValidaDouble(Console.ReadLine());

                if (valor > 0) {
                    conta.Depositar(valor);
                    Console.WriteLine("Depósito realizado com sucesso!");
                    break;
                }
                else {
                    Console.WriteLine("O valor de depósito deve ser um número real maior que zero. Tente novamente.");
                }
            } while (true);
        }

        public static void RealizarSaque(Conta conta) {
            double valor;

            do {
                Console.Write("Digite o valor a ser sacado: ");

                valor = ValidaDouble(Console.ReadLine());

                if (valor > 0) {
                    conta.Sacar(valor);
                    Console.WriteLine("Saque realizado com sucesso!");
                    break;
                }
                else {
                    Console.WriteLine("O valor de saque deve ser um número real maior que zero. Tente novamente.");
                }
            } while (true);
        }

        public static bool VerificarCriteriosExclusao(Conta conta)
        {
            if (conta == null) {
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

        public static void MenuRelatoriosGerenciais() {
            Console.WriteLine("\nOpções de Relatórios Gerenciais:");
            Console.WriteLine("1. Listar clientes com saldo negativo");
            Console.WriteLine("2. Listar clientes com saldo acima de um determinado valor");
            Console.WriteLine("3. Listar todas as contas");
            Console.Write("Escolha uma opção: ");
        }

        public static int ValidaInt(string numero) {
            int resultado;
            bool validaNumero = int.TryParse(numero, out resultado);
            while (!validaNumero) {
                Console.WriteLine("Digite um valor numérico.");
                numero = Console.ReadLine();
                validaNumero = int.TryParse(numero, out resultado);
            }
            return resultado;
        }

        public static double ValidaDouble(string saldo) {
            double resultado;
            bool validaDouble = double.TryParse(saldo, out resultado);
            while (!validaDouble) {
                Console.WriteLine("Digite um valor numérico.");
                saldo = Console.ReadLine();
                validaDouble = double.TryParse(saldo, out resultado);
            }
            return resultado;
        }
    }
}
