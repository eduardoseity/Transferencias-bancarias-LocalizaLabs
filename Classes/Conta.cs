namespace Transferências_bancárias
{
    class Conta
    {
        private int numeroConta {get; set;}
        private string nome {get; set;}
        private double saldo {get; set;}
        private double credito {get; set;}
        private string senha {get; set;}
        private TipoConta tipo {get; set;}

        public Conta(string _nome, int _numeroConta, double _saldo, double _credito, string _senha, TipoConta _tipo)
        {
            this.numeroConta = _numeroConta;
            this.nome = _nome;
            this.saldo = _saldo;
            this.credito = _credito;
            this.senha = _senha;
            this.tipo = _tipo;
        }

        public override string ToString()
        {
            return $"Número da conta: {this.numeroConta} \nTitular: {this.nome} \nSaldo: {this.saldo} \nCrédito {this.credito}";
        }

        public bool sacar(double valor)
        {
            if ((this.saldo + this.credito) < valor)
            {
                return false;
            }
            this.saldo -= valor;
            return true;
        }

        public bool depositar(double valor)
        {
            if (valor < 0)
            {
                return false;
            }

            this.saldo += valor;
            return true;
        }

        public bool transferir(Conta contaDestino, double valor)
        {
            if (!this.sacar(valor))
            {
                return false;
            }

            contaDestino.depositar(valor);
            return true;
        }
        public bool checarSenha(string senha)
        {
            if (senha != this.senha)
            {
                return false;
            }

            return true;
        }
    }
}