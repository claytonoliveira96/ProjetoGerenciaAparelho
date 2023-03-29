using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository.Base;
using System.Reflection;
using System.Text;

namespace GerenciaAparelhos.Repository
{
    public class AparelhoRepository : BaseRepository<AparelhoModel>
    {
        #region "Propriedades"

        private string _caminhoDadosAparelho;

        #endregion

        #region "Construtor"

        public AparelhoRepository() : base()
        {
            _caminhoDadosAparelho = $"{Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName}/DadosAparelho.txt";

            if (!File.Exists(_caminhoDadosAparelho))
            {
                using (StreamWriter file = new StreamWriter(_caminhoDadosAparelho, true, Encoding.UTF8))
                {
                    file.WriteLine();
                }
            }
        }

        #endregion

        #region "Métodos Públicos"

        public List<AparelhoModel> ListarAparelhos()
        {
            List<AparelhoModel> result = new List<AparelhoModel>();

            var dados = GetDados(_caminhoDadosAparelho);

            if(dados != null)
            {
                result.AddRange(dados);
            }

            return result;
        }

        public AparelhoModel ConsultarAparelho(int id)
        {
            AparelhoModel result = new();

            var dados = GetDados(_caminhoDadosAparelho);

            if(dados != null)
            {
                result = dados.FirstOrDefault(p => p.Id.Equals(id));
            }

            return result;
        }

        public bool CadastrarAparelho(AparelhoModel aparelho)
        {
            bool result = false;

            var lstAparelhosCadastrados = GetDados(_caminhoDadosAparelho);
            aparelho.Id = 1;

            if (lstAparelhosCadastrados != null)
            {
                aparelho.Id = lstAparelhosCadastrados.Count() + 1;
            }

            result = Gravar<AparelhoModel>(aparelho, _caminhoDadosAparelho);
            
            return true;
        }

       
        #endregion
    }
}
