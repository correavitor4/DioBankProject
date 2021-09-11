using System;
using System.Collections.Generic;
using System.IO;


namespace PublicDioBank
{
    class Program
    {

        //Armazena a lista de contas
        static List<Conta> listContas = new List<Conta>();

        
        static void Main(string[] args)
        {
            listContas = lerDados();
            Console.Beep();
            string opcaoUsuario = obterOpacaoUsuario();

            while(opcaoUsuario != "X"){

                switch(opcaoUsuario){
                    case "1":
                        ListarContas();
                        
                        break;
                    case "2":
                        InserirConta();
                        Console.WriteLine("Pronto");
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    

                    default:
                        throw new ArgumentOutOfRangeException();

                }

                opcaoUsuario = obterOpacaoUsuario();
            }

            Console.WriteLine("Obrigado por atualizar nossos serviços!");
            Console.ReadLine();
        }

        private static void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indiceOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da contade destino: ");
            int indiceDestino = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor da transferência: ");
            double valor = double.Parse(Console.ReadLine());
            
            listContas[indiceOrigem].transferir(valor,listContas[indiceDestino]);
            
        }

        private static void Depositar()
        {
            Console.WriteLine("Digite o número da conta: ");
            int nConta =int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[nConta].depositar(valorDeposito);
        }

        private static void Sacar()
        {
            
            Console.WriteLine("Digite o número da conta: ");
            int nConta =int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listContas[nConta].sacar(valorSaque);
        }

        private static void ListarContas()
        {
            if(listContas.Count==0){
                Console.WriteLine("Nenhuma conta cadastrada!");

            }
            else{
                for(int i=0;i<listContas.Count;i++){
                    Conta conta= listContas[i];
                    Console.Write("{0} - ",i);
                    Console.WriteLine(conta.ToString());
                }
            }
        }

        private static void InserirConta()
        {
            Console.WriteLine("Digite 1 para conta física, e 2 para jurídica");
            int entradaTipoConta = int.Parse(Console.ReadLine());
            while(entradaTipoConta!=1 && entradaTipoConta!= 2){
                Console.WriteLine("Digite novamente o tipo de conta, mas dessa vez digite uma opção válida");
                Console.WriteLine("1 - Física");
                Console.WriteLine("2- jurídica");
                Console.WriteLine();
                entradaTipoConta = int.Parse(Console.ReadLine());
            
            }

            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o saldo inicial: ");
            double saldoInicial = double.Parse(Console.ReadLine());
            while(saldoInicial<0){
                Console.WriteLine("O saldo inicial não pode ser negativo, digite novamente:");
                saldoInicial = double.Parse(Console.ReadLine());
            }


            Console.WriteLine("Digite o crédito inicial: ");
            double creditoInicial = double.Parse(Console.ReadLine());
            while(creditoInicial<0){
                Console.WriteLine("O crédito não pode ser negativo, digite novamente: ");
                creditoInicial = double.Parse(Console.ReadLine());
            }


            Conta novaConta= new Conta(tipoConta: (TipoConta)entradaTipoConta,
            nomeCliente,
            saldoInicial,
            creditoInicial);

            
            gravarConta(novaConta);

            
            //Adiciona nova conta à lista de contas                    
            listContas.Add(novaConta);
            Console.WriteLine("Prontinho");
        }

        public static string obterOpacaoUsuario(){
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static List<Conta> lerDados(){
            
            List<Conta> retorno = new List<Conta>();
            
            var sr = new StreamReader("Dados/Dados.txt");
            var line = sr.ReadLine();

            while(!string.IsNullOrWhiteSpace(line)){
                
                retorno.Add(lerConta(line));
                line= sr.ReadLine();
            }
            
            sr.Close();

            return retorno;
            
        }

        private static Conta lerConta(string line){
            int counter =0;
            TipoConta tipoConta = TipoConta.PessoaFisica;

            string saldoString = "";
            string creditoString = "";

            string nome="";
            double saldo;
            double credito;
            for(int i=0;i<line.Length;i++){
                if(line[i]!=',')
                {
                    switch(counter){
                        case 0:
                            var tp = line[i];
                            var tpI = int.Parse(tp.ToString());
                            tipoConta= (TipoConta)tpI;
                            
                            break;
                        case 1:
                            nome+=line[i];
                            break;
                        case 2:
                            saldoString += line[i];
                            break;
                        default:
                            creditoString+=line[i];
                            break;
                
                            
                    }
                }
                else{
                    counter++;
                }

                
            }

            saldo = double.Parse(saldoString);
            credito = double.Parse(creditoString);

            Conta novaConta = new Conta(tipoConta,nome,saldo,credito);

            return novaConta;
        }
        

        private static void  gravarConta(Conta conta){


            //Responsável por ler as linhas que já estão gravadas no arquivo .txt, e as salva na lista "linhas"
            List<string> linhas = new List<string>();
            var sr = new StreamReader("Dados/Dados.txt");
            var line = sr.ReadLine();

            while(!string.IsNullOrWhiteSpace(line)){
                linhas.Add(line);
                Console.WriteLine("sssd");
                line=sr.ReadLine();
            }

            sr.Close();

            try{
                StreamWriter sw = new StreamWriter("Dados/Dados.txt");


                //regrava as linhas que já estavam salvas anteriormente
                foreach(string l in linhas){
                    sw.WriteLine(l);
                }

                int tip;
                string nome = conta.Getnome();
                double saldo = conta.Getsaldo();
                double credito = conta.Getcredito();



                if(conta.GettipoConta()==TipoConta.PessoaFisica){
                    tip = 1;
                }
                else{
                    tip =2;
                }
                
                
                sw.WriteLine(tip.ToString()+','+nome+','+saldo.ToString()+','+credito.ToString());
                sw.Close();
                
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            
            

        }


    }
}
