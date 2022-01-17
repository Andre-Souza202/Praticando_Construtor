namespace ExemploConstrutores.Models
{
    public class Pessoa
    {
        //variaveis readonly só podem ter seus valores alterados quando são declaradas e na função do construtor
        private readonly string nome;
        private readonly string senha;
        private Boolean mestre;
        private Boolean administrador;

        //Construtor para criar um usuário mestre
        public Pessoa(string nome, string senha, Boolean mestre)
        {
            this.nome = nome;
            this.senha = senha;
            this.mestre = mestre;
        }

        //Construtor para criar um usuário normal
        public Pessoa(string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }

        public Boolean validarNome(string nome)
        {
            if (this.nome == nome)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean validarSenha(string senha)
        {
            if (this.senha == senha)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string getNome()
        {
            return nome;
        }

        public Boolean isMestre()
        {
            return mestre;
        }

        public Boolean isAdministrador()
        {
            return administrador;
        }

        public void tornarAdministrador()
        {
            administrador = true;
        }
        public void retirarAdministrador()
        {
            administrador = false;
        }
    }
}