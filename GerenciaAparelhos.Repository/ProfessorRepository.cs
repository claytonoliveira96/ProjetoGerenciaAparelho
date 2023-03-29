using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository.Base;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace GerenciaAparelhos.Repository
{
    public class ProfessorRepository : BaseRepository<ProfessorModel>
    {
        #region "Propriedades Privadas"

        private string _caminhoDadosProfessor;

        #endregion

        #region "Construtor"

        public ProfessorRepository() : base()
        {
            _caminhoDadosProfessor = $"{Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName}/DadosProfessor.txt";

            if(!File.Exists(_caminhoDadosProfessor))
            {
                using (StreamWriter file = new StreamWriter(_caminhoDadosProfessor, true, Encoding.UTF8))
                {
                    List<ProfessorModel> lstProfessores = new();

                    for (var i = 1; i < 5; i++)
                    {
                        ProfessorModel model = new();

                        model.Id = i;
                        model.Nome = $"João {i}";
                        model.CPF = $"45{i}.89{i}.56{i}-{i}";

                        lstProfessores.Add(model);
                    }

                    file.WriteLine(JsonConvert.SerializeObject(lstProfessores));
                }
            }
        }

        #endregion

        #region "Propriedades"

        public List<ProfessorModel> ListarProfessores()
        {
            List<ProfessorModel> result = new();

            var dados = GetDados(_caminhoDadosProfessor);

            if(dados != null)
            {
                result.AddRange(dados);
            }

            return result;
        }

        #endregion
    }
}
