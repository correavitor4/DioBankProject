using System;
namespace PublicDioBank
{
    public class Conta
    {
        private TipoConta tipoConta;

        

        private string nome;
        private double saldo;
        private double credito;

        //Método construtor da classe
        public Conta(TipoConta tipoConta,string nome,double saldo,double credito)
        {
            this.tipoConta = tipoConta;
            this.nome = nome;
            this.credito = credito;
            this.saldo = saldo;

        }


        //Region responsável por armazenar modificadores e acessadores
        #region seters e geters
        public double Getcredito()
        {
            return credito;
        }

        public void Setcredito(double value)
        {
            credito = value;
        }

        public double Getsaldo()
        {
            return saldo;
        }

        public void Setsaldo(double value)
        {
            saldo = value;
        }

        public string Getnome()
        {
            return nome;
        }

        public void Setnome(string value)
        {
            nome = value;
        }

        public TipoConta GettipoConta()
        {
            return tipoConta;
        }

        public void SettipoConta(TipoConta value)
        {
            tipoConta = value;
        }

        #endregion
    

        public bool sacar(double saque){
            //Faz a validação do saque
            if(saque>this.saldo+this.credito){
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            else{
                this.saldo-=saque;
                Console.WriteLine("Sucesso! O saldo atual da conta de {0} é de R${1}!",this.nome,this.saldo);
                return true;
            }
        }

        public void depositar(double valorDeposito){
            this.saldo+= valorDeposito;
        }

        public bool transferir(double valorTransferencia,Conta contaDestino){
            //Faz a validação do saque
            if(valorTransferencia>this.saldo+this.credito){
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            else{
                this.saldo-=valorTransferencia;
                contaDestino.depositar(valorTransferencia);
                Console.WriteLine("O valor da transferência foi de {0}. O saldo atual da conta de origem é de {1} e o da conta de destino é {2}",valorTransferencia,this.saldo,contaDestino.Getsaldo());
                return true;
            }
        }

        public override string ToString(){
            string retorno = "";
            retorno += "TupoConta: " + this.tipoConta + "  |  ";
            retorno += "Nome: "+this.nome + "  |  ";
            retorno += "Saldo: "+ this.saldo + "  |  ";
            retorno += "Crédito: "+this.credito + "  |  ";
            return retorno;
        }

        
    
    }
}