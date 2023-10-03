﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT {
    internal class Arquivo {
        static List<Conta> contas = new List<Conta>();

        public static void CarregarDadosDoArquivo()
        {

            if (File.Exists("contas.csv"))
            {

                using (StreamReader reader = new StreamReader("contas.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        string linha = reader.ReadLine();
                        string[] partes = linha.Split(',');

                        if (partes.Length == 3)
                        {
                            int id = int.Parse(partes[0]);
                            string nome = partes[1];
                            double saldo = double.Parse(partes[2]);

                            Conta novaConta = new Conta(id, nome, saldo);
                            contas.Add(novaConta);

                        }
                    }
                }
            }
        }
        public static void SalvarDadosNoArquivo()
        {
            using (StreamWriter writer = new StreamWriter("contas.csv"))
            {
                foreach (var conta in contas)
                {
                    string linha = $"{conta.Id},{conta.Nome},{conta.Saldo}";
                    writer.WriteLine(linha);
                }
            }
        }
    }
}
