using GerenciaAparelhos.Models.Enum;
using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository;

namespace GerenciaAparelhos.Business
{
    public static class AparelhoBusiness
    {
        #region "Propriedades"

        private static AparelhoRepository _aparelhoRepository
        {
            get
            {
                return new AparelhoRepository();
            }
        }

        #endregion

        #region "Métodos Públicos"        

        public static AparelhoModel ConsultarAparelho(int id)
        {
            AparelhoModel result = new();

            try
            {
                var dados = _aparelhoRepository.ConsultarAparelho(id);

                if(dados != null)
                {
                    result = dados;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }      

        public static void CadastrarAparelho()
        {
            try
            {
                AparelhoModel mAparelho = new();
                string sTipos = string.Empty;

                foreach (int i in Enum.GetValues(typeof(TipoAparelho)))
                {
                    sTipos += $"|{i} -> {Enum.GetName(typeof(TipoAparelho), i)}| ";
                }

                Console.WriteLine();                
                Console.Write("Informe o nome do Aparelho: ");
                mAparelho.Nome = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("------------------");
                Console.WriteLine(String.Format("{0,6} | {1,-10} ", "ID", "TIPO"));
                Console.WriteLine("------------------");
                foreach (int tipo in Enum.GetValues(typeof(TipoAparelho)))
                {
                    Console.WriteLine(String.Format("{0,6} | {1,-10} ", tipo, Enum.GetName(typeof(TipoAparelho), tipo)));
                }
                Console.WriteLine("------------------");

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write($"Informe o tipo do aparelho à ser cadastrado: ");
                        mAparelho.Tipo = Enum.Parse<TipoAparelho>(Console.ReadLine().ToUpper());
                        break;
                    }
                    catch
                    {
                        Console.Write($"Tipo de aparelhjo inexistente. Favor tentar novamente!");
                        continue;
                    }
                }

                bool bAparelhoCadastrado = _aparelhoRepository.CadastrarAparelho(mAparelho);

                if(bAparelhoCadastrado)
                {
                    Console.WriteLine();
                    Console.WriteLine("Aparelho cadastrado com sucesso!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Não foi possível cadastrar o aparelho, favor tentar novamente!");
                }
            }
            catch 
            {
                throw;
            }

        }

        public static void ListarAparelhos()
        {
            try
            {
                var lstAparelhos = _aparelhoRepository.ListarAparelhos();                

                if (lstAparelhos.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-8}", "ID", "NOME", "TIPO"));
                    Console.WriteLine("-------------------------------------------------------");
                    foreach (var aparelho in lstAparelhos)
                    {
                        Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-8}", aparelho.Id, aparelho.Nome, aparelho.Tipo));
                    }
                    Console.WriteLine("-------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Nenhum aparelho cadastrado até o momento!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
