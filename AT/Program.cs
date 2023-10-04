using System;
using System.Collections.Generic;
using System.IO;
using static AT.ContaCrud;
using static AT.Arquivo;


namespace AT {
    public class Program {

        static void Main(string[] args) {

            var contas = CarregarDadosDoArquivo();

            bool sair = false;

            while (!sair) {
                int opcao = ExibirMenu();

                switch (opcao) {
                    case 1:
                        Console.WriteLine("Inclusão de conta selecionada");
                        IncluirConta(ref contas);
                        break;
                    case 2:
                        Console.WriteLine("Alteração de saldo selecionada");
                        AlterarSaldo(ref contas);
                        break;
                    case 3:
                        Console.WriteLine("Exclusão de conta selecionada");
                        ExcluirConta(ref contas);
                        break;
                    case 4:
                        Console.WriteLine("Relatórios gerenciais selecionados");
                        ExibirRelatoriosGerenciais(contas); 
                        break;
                    case 5:
                        Console.WriteLine("Saindo do programa");
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            SalvarDadosNoArquivo(contas);
        }

        static int ExibirMenu() {
            Console.WriteLine("\nSeja Bem-Vindo ao Banco Infnet");
            Console.WriteLine("Por favor, selecione uma opção:\n");
            Console.WriteLine("1. Inclusão de Conta");
            Console.WriteLine("2. Alteração de Saldo");
            Console.WriteLine("3. Exclusão de Conta");
            Console.WriteLine("4. Relatórios Gerenciais");
            Console.WriteLine("5. Sair do Programa");

            int opcao = Util.ValidaInt(Console.ReadLine());
            return opcao;
        }

    }
}