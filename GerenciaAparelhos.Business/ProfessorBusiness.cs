using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository;

namespace GerenciaAparelhos.Business
{
    public static class ProfessorBusiness
    {
        #region "Propriedades"

        private static ProfessorRepository _repository
        {
            get
            {
                return new ProfessorRepository();   
            }
        }

        #endregion

        #region "Métodos Públicos"

        public static List<ProfessorModel> ListarProfessores()
        {
            List<ProfessorModel> result = new();

            try
            {
                var dados = _repository.ListarProfessores();

                if(dados != null) 
                {
                    result.AddRange(dados);
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        #endregion
    }
}
