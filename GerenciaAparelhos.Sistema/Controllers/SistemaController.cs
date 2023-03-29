using GerenciaAparelhos.Business;

namespace GerenciaAparelhos.Sistema.Controllers
{
    public class SistemaController
    {
        #region "Propriedades Privadas"

        private static Dictionary<int, string>? _opcoesMenu;

        #endregion

        #region "Construtor"

        public SistemaController() 
        {
            _opcoesMenu = new Dictionary<int, string>();
            _opcoesMenu.Add(1, "Cadastrar Novo Aparelho");
            _opcoesMenu.Add(2, "Listar Todos Aparelhos");
            _opcoesMenu.Add(3, "Listar Todas as Reservas");
            _opcoesMenu.Add(4, "Listar Reservas Por Aparelho");
            _opcoesMenu.Add(5, "Reservar Aparelho");
            _opcoesMenu.Add(6, "Liberar Aparelho");
        }

        #endregion

        #region "Métodos Públicos"

        public void Iniciar()
        {      
            bool bEmExecucao = true;

            while(bEmExecucao)
            {
                try
                {
                    Menu();
                    Console.Write("[1 Para Sim | 2 Para Não] - Deseja continuar ? ");
                    var responsta = Console.ReadLine().ToString().ToUpper();

                    if (responsta.Contains("SIM") || responsta.Contains("1"))
                    {
                        bEmExecucao = true;
                    }
                    else
                    {
                        bEmExecucao = false;
                    }
                }
                catch
                {
                    continue;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Operação finlaizada!");
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Seja bem vindo ao sistema de gerenciamento de reserva de Aparelho!");
            Console.WriteLine();

            _opcoesMenu.ToList().ForEach(item =>
            {
                Console.WriteLine($"# {item.Key} | {item.Value}");
            });

            string opcao = string.Empty;
            
            Console.WriteLine();
            Console.Write("Informe o número da opção desejada: ");
            opcao = Console.ReadLine();

            while (string.IsNullOrEmpty(opcao) || _opcoesMenu.Where(p => p.Key.Equals(int.Parse(opcao))).Count() <= 0)
            {
                try
                {
                    Console.WriteLine("Opção inválida, tente novamente!");
                    Console.Write("Informe o número da opção desejada: ");
                    opcao = Console.ReadLine();
                }
                catch
                {
                    continue;
                }
            }

            ExecutaFuncaoMenu(int.Parse(opcao));

            Console.WriteLine();
        }

        #endregion

        #region "Métodos Privados"

        private static void ExecutaFuncaoMenu(int funcaoEscolhida)
        {
            switch (funcaoEscolhida)
            {
                case 1:
                    AparelhoBusiness.CadastrarAparelho();
                    break;
                case 2:
                    AparelhoBusiness.ListarAparelhos();
                    break;
                case 3:
                    ReservaBusiness.ListarReservas();
                    break;
                case 4:
                    ReservaBusiness.ListarReservasPorAparelho();
                    break;
                case 5:
                    ReservaBusiness.ReservarAparelho();
                    break;
                case 6:
                    ReservaBusiness.LiberarReserva();
                    break;
            }
        }

        #endregion
    }
}
