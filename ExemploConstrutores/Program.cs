using System;
using ExemploConstrutores.Models;

namespace ExemploConstrutores
{
    class Progam
    {
        //lista dos usuários cadastrados é guardado na variavel pessoas
        static List<Pessoa> pessoas = new List<Pessoa>();
        static Pessoa? usuarioLogado;
        static Boolean uMestre;
        static Boolean uAdmnistrador;

        //cria o delegate
        public delegate void delegateTeste();
        //cria o evento
        public static event delegateTeste eventoTeste;
        static void Main(string[] args)
        {
            //inscreve o metodo eventHandler ao evento
            eventoTeste += eventHandler;

            string opc = ObterOpcao();
            Boolean master = false;

            // o progama continuara funcionando enquanto não for escolhida a opção 2
            while (opc != "2")
            {
                switch(opc)
                {
                    case "1":
                        //Verifica se é a primeira vez que o progama está sendo acessado, se for ele cria um login de usuário mestre
                        if (master == false)
                        {
                            Console.WriteLine("Como esse é seu primeiro login você vai precisar criar um usuário mestre");
                            Console.WriteLine();
                            cadastrar(true);
                            master = true;
                        }
                        else
                        {
                            //valida o login com os usuários cadastrados na função validar(), ela ira retornar null se as credenciais forem incorretas
                            usuarioLogado = validar();
                            if (usuarioLogado == null)
                            {
                                break;
                            }
                            else 
                            {
                                //faz o acesso do usuário que está gravado na variavel usuarioLogado
                                Acesso();
                            }
                        }
                    break;

                    default:
                        Console.WriteLine("Por favor, selecione uma das opções");
                    break;
                }
                opc = ObterOpcao();
            }
        }

        static string ObterOpcao()
        {
            //lista de opções do usuário
            Console.WriteLine();
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1- Logar usuário");
            Console.WriteLine("2- Sair");
            Console.WriteLine("");

            string opc = Console.ReadLine();
            Console.WriteLine();
            return opc;
        } 

        private static Pessoa? validar()
        {
            Console.WriteLine("Login");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            //faz a validação e verifica se os dados batem com algum usuário cadastrado
            foreach (var p in pessoas)
            {
                if (p.validarNome(nome))
                {
                    if (p.validarSenha(senha))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Usuário logado com sucesso!");
                        return p;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Nome ou senha incorretos");
            return null;
        }

        private static void cadastrar(Boolean mestre)
        {
            Console.WriteLine("Criando novo usuário");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            //cadastra o usuário mestre usando a função de construtor de Pessoa com três parâmetros
            if (mestre == true)
            {
                Pessoa p1 = new Pessoa(nome, senha, true);
                pessoas.Add(p1);
            }
            //cadastra um usuário normal usando a função de construtor de Pessoa com dois parâmetros
            else 
            {
                Pessoa p1 = new Pessoa(nome, senha);
                pessoas.Add(p1);
            }
        }
        public static void Acesso()
        {
            //checa se o acesso do usuário é de mestre ou administrador
            uMestre = usuarioLogado.isMestre();
            uAdmnistrador = usuarioLogado.isAdministrador();

            string fopc = funcaoOpcao();

            //o usuário ficara logado enquanto não escolher a opção 1
            while (fopc != "1")
            {
                switch(fopc)
                {
                    //cadastra um usuário comum, só pode ser acessado por um usuário mestre ou administrador
                    case "2":
                        if (uMestre == true || uAdmnistrador == true)
                        {
                            cadastrar(false);
                        }
                        else
                        {
                            Console.WriteLine("Por favor, selecione uma das opções");
                        }
                    break;

                    //instacia um objeto Log uma única vez, outras tentativas de instacia usara a mesma instacia
                    case "3":
                        if (uMestre == true || uAdmnistrador == true)
                        {
                            Log log = Log.GetInstance();
                            log.mensagem();

                            Console.WriteLine();
                            Console.WriteLine("Aperte enter para continuar");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Por favor, selecione uma das opções");
                        }
                    break;

                    // torna um usuário administrador, só pode ser acessado pelo usuário mestre
                    case "4":
                        if (uMestre == true)
                        {
                            tornarAdministrador();
                        }
                        else
                        {
                            Console.WriteLine("Por favor, selecione uma das opções");
                        }

                    break;

                    // retira o nivel de administrador de um usuário, só pode ser acessado pelo usuário mestre
                    case "5":
                        if (uMestre == true)
                        {
                            retirarAdministrador();
                        }
                        else
                        {
                            Console.WriteLine("Por favor, selecione uma das opções");
                        }
                    break;

                    default:
                        Console.WriteLine("Por favor, selecione uma das opções");
                    break;
                }
                fopc = funcaoOpcao();
            }
        }

        static string funcaoOpcao()
        {  
            // é mostrada uma mensagem especifica dependendo do nivel de acesso do usuário
            Console.WriteLine();
            Console.WriteLine($"Seja bem vindo {usuarioLogado.getNome()}");
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1- Deslogar");

            if(uMestre)
            {
                Console.WriteLine("2- Criar novo usuário");
                Console.WriteLine("3- Checar logs");
                Console.WriteLine("4- Dar administrador para um usuário");
                Console.WriteLine("5- Retirar administrador de um usuário");
                Console.WriteLine("Usuário MESTRE");
            }
            else if (uAdmnistrador)
            {
                Console.WriteLine("2- Criar novo usuário");
                Console.WriteLine("3- Checar logs");
                Console.WriteLine("Usuário ADMINISTRADOR");
            }

            Console.WriteLine("");

            string fopc = Console.ReadLine();
            Console.WriteLine();
            return fopc;
        }

        private static void tornarAdministrador()
        {
            Console.WriteLine("");
            Console.Write("Nome do usuário: ");
            string nome = Console.ReadLine();

            foreach (var p in pessoas)
            {
                if (p.validarNome(nome))
                {
                    //após executar a ação é chamado o evento para executar todos os metodos inscritos nele
                    p.tornarAdministrador();
                    eventoTeste();
                    break;
                }
            }
        }
        private static void retirarAdministrador()
        {
            Console.WriteLine("");
            Console.Write("Nome do usuário: ");
            string nome = Console.ReadLine();

            foreach (var p in pessoas)
            {
                if (p.validarNome(nome))
                {
                    //após executar a ação é chamado o evento para executar todos os metodos inscritos nele
                    p.retirarAdministrador();
                    eventoTeste();
                    break;
                }
            }
        }

        //metodo inscrito no evento, será executado toda vez que o evento é chamado
        public static void eventHandler()
        {
            Console.WriteLine("");
            Console.WriteLine("Ação concluida com sucesso");
        }
    }
}

