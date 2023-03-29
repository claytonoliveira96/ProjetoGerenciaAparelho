using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository.Base;
using System.Reflection;
using System.Text;

namespace GerenciaAparelhos.Repository
{
    public class ReservaRepository : BaseRepository<ReservaModel>
    {
        #region "Propriedades Privadas"

        private string _caminhoDadosReserva;

        #endregion

        #region "Construtor"

        public ReservaRepository() : base()
        {
            _caminhoDadosReserva = $"{Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName}/DadosReserva.txt";

            if (!File.Exists(_caminhoDadosReserva))
            {
                using (StreamWriter file = new StreamWriter(_caminhoDadosReserva, true, Encoding.UTF8))
                {
                    file.WriteLine();
                }
            }
        }

        #endregion

        #region "Métodos Públicos"
        
        public List<ReservaModel> ListarReservas()
        {
            List<ReservaModel> result = new();

            var dados = GetDados(_caminhoDadosReserva);

            if (dados != null)
            {
                result.AddRange(dados);
            }

            return result;
        }

        public List<ReservaModel> ListarReservaPorAparelho(int idAparelho)
        {
            List<ReservaModel> result = new();

            var dados = GetDados(_caminhoDadosReserva);

            if(dados != null)
            {
                result.AddRange(dados.Where(p => p.IdAparelho.Equals(idAparelho)));
            }

            return result;
        }

        public bool CadastrarReserva(ReservaModel reserva)
        {
            bool result = false;

            var lstAparelhosCadastrados = GetDados(_caminhoDadosReserva);
            reserva.Id = 1;

            if (lstAparelhosCadastrados != null)
            {
                reserva.Id = lstAparelhosCadastrados.Count() + 1;
            }

            result = Gravar<ReservaModel>(reserva, _caminhoDadosReserva);

            return true;
        }

        public bool LiberarReserva(int idReserva)
        {
            bool result = false;

            var lstAparelhosCadastrados = GetDados(_caminhoDadosReserva);

            lstAparelhosCadastrados.ForEach(p => { if (p.Id == idReserva) p.Devolucao = DateTime.Now;  } );

            result = Atualizar<ReservaModel>(lstAparelhosCadastrados, _caminhoDadosReserva);

            return true;
        }

        #endregion
    }
}
