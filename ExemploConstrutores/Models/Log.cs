namespace ExemploConstrutores.Models
{
    public class Log
    {

        private static Log _log;

        //uma variavel constante não pode ser alterada
        public const string PropriedadeLog = "Aqui estaria ligado a outro lugar, como por exemplo um banco de dados, mas foi progamado de uma forma que só sera instaciado uma única vez, outras chamadas do objeto Log ira usar a mesma instancia";

        private Log()
        {
            
        }

        //o metodo vai instaciar um objeto Log, se já estiver sido instaciado, ele usara a mesma instacia
        public static Log GetInstance()
        {
            if (_log == null)
            {
                _log = new Log();
            }
            return _log;
        }

        public void mensagem()
        {
            Console.WriteLine(PropriedadeLog);
        }
    }
}