using System;
using System.Collections.Generic;

namespace Transferências_bancárias
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
        static string opcao;

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            opcao = menuInicial();
            
            while (opcao.ToUpper() != "X")
            {
                switch (opcao)
                {
                    case "1":
                        Console.ForegroundColor = ConsoleColor.White;
                        listarContas();
                        opcao = menuInicial();
                        break;
                    case "2":
                        Console.ForegroundColor = ConsoleColor.White;
                        cadastrarConta();
                        opcao = menuInicial();
                        break;
                    case "3": //Depositar
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.Write("Digite o número da conta: ");
                        var contaNum = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                    depositoLabel:
                        Console.Write("Digite o valor que deseja depositar: ");
                        double deposito = double.Parse(Console.ReadLine());
                        if (!listContas[contaNum].depositar(deposito))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Valor inválido!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine();
                            goto depositoLabel;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Depósito realizado com sucesso!");
                        Console.ReadKey();
                        Console.Clear();
                        opcao = menuInicial();

                        break;
                    case "4": // Sacar
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.Write("Digite o número da conta: ");
                        contaNum = int.Parse(Console.ReadLine());
                    senhaLabel:
                        Console.WriteLine();
                        Console.Write("Digite a senha: ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (!listContas[contaNum].checarSenha(Console.ReadLine()))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Senha invalida!");
                            Console.ForegroundColor = ConsoleColor.White;

                            goto senhaLabel;
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        Console.Write("Digite o valor que deseja sacar: ");
                        double valor = double.Parse(Console.ReadLine());
                        if (!listContas[contaNum].sacar(valor))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Saldo insuficiente!");
                        }
                        else 
                        {
                            Console.WriteLine();
                            Console.WriteLine("Saque realizado com sucesso!");
                        }
                        Console.WriteLine();
                        Console.WriteLine(listContas[contaNum].ToString());

                        Console.ReadKey();
                        Console.Clear();
                        opcao = menuInicial();
                        break;
                    case "5": // Transferir
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Digite o número da conta que o valor será debitado: ");
                        var contaDebito = int.Parse(Console.ReadLine());
                    senhaLabel2:
                        Console.WriteLine();
                        Console.Write("Digite a senha: ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (!listContas[contaDebito].checarSenha(Console.ReadLine()))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Senha inválida!");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto senhaLabel2;
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        Console.Write("Digite o valor da transferência: ");
                        var valorTranferencia = double.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.Write("Digite o número da conta de destino: ");
                        var contaDestino = int.Parse(Console.ReadLine());
                        if (!listContas[contaDebito].transferir(listContas[contaDestino],valorTranferencia)) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Saldo insuficiente!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine();
                        }
                        else {
                            Console.WriteLine();
                            Console.WriteLine("Tranferência realizada com sucesso!");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        opcao = menuInicial();
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Opção inválida! Por favor escolha uma opção da lista.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        opcao = menuInicial();
                        break;
                }
            }

            // Sai do programa
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Até breve!");
            return;

        }

        private static void listarContas()
        {
            Console.Clear();
            if (listContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada! :(");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("--------------------------");
            foreach (Conta c in listContas)
            {
                Console.WriteLine(c.ToString());
                Console.WriteLine("--------------------------");
            }
            Console.ReadKey();
        }

        private static void cadastrarConta()
        {
            string _nome;
            int _numeroConta;
            double _saldo;
            double _credito;
            string _senha;

            Console.Clear();
            Console.WriteLine("Cadastrar nova conta");      
            Console.WriteLine("------------------------");      
        tipoLabel:
            Console.WriteLine("Digite:");
            Console.WriteLine("[1] Para pessoa Física");
            Console.WriteLine("[2] Para pessoa Jurídica");
            Console.WriteLine();
            Console.Write("Opção: ");
            string tipo = Console.ReadLine();
            Console.WriteLine();
            TipoConta _tipo;
            if (tipo == "1")
            {
                _tipo = TipoConta.Física;
                Console.Write("Ok! Vamos cadastrar uma nova conta para pessoa ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("física.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (tipo == "2")
            {
                _tipo = TipoConta.Jurídica;
                Console.Write("Ok! Vamos cadastrar uma nova conta para pessoa ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("jurídica.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor inválido!");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                goto tipoLabel;
            }
            
            Console.WriteLine();
            Console.WriteLine("Agora digite o nome do titular da conta. ");
            _nome = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Qual é o limite de crédito?");
            _credito = double.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Qual é o saldo da conta?");
            _saldo = double.Parse(Console.ReadLine());

        passLabel:
            Console.WriteLine();
            Console.WriteLine("Agora digite uma senha.");
            Console.ForegroundColor = ConsoleColor.Black;
            _senha = Console.ReadLine();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Por favor repita a senha.");
            Console.ForegroundColor = ConsoleColor.Black;
            if (Console.ReadLine() != _senha)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ops! As senhas digitadas não são iguais.");
                Console.ForegroundColor = ConsoleColor.White;
                goto passLabel;
            }

            _numeroConta = listContas.Count;

            Conta conta = new Conta(_nome, _numeroConta, _saldo, _credito, _senha, _tipo);
            listContas.Add(conta);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta cadastrada com sucesso!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Pressione uma tecla para voltar ao menu inicial");
            Console.ReadKey();
            Console.Clear();
        }

        private static string menuInicial()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bem-vindo(a) ao Banco DIO Brasil");
            Console.WriteLine("-------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Por favor digite uma das opções abaixo:");
            Console.WriteLine("[1] Listar contas");
            Console.WriteLine("[2] Cadastrar nova conta");
            Console.WriteLine("[3] Depositar");
            Console.WriteLine("[4] Sacar");
            Console.WriteLine("[5] Transferir");
            Console.WriteLine("[X] Sair");
            Console.WriteLine();
            Console.Write("Opção: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            return Console.ReadLine();
        }
    }
}
