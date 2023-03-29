using Newtonsoft.Json;
using System.Text;

namespace GerenciaAparelhos.Repository.Base
{
    public abstract class BaseRepository<T>
    {
        #region "Métodos Protegidos"

        protected List<T> GetDados(string caminhoDados)
        {
            var result = Activator.CreateInstance<List<T>>();

            try
            {
                result = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(caminhoDados));
            }
            catch
            {
                return result;
            }

            return result;
        }

        public bool Gravar<T>(T model, string caminhoDados)
        {
            bool result = false;

            try
            {
                var dadosGuardados = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(caminhoDados));
                string sJsonGravar = string.Empty;

                if (dadosGuardados == null)
                {
                    dadosGuardados = Activator.CreateInstance<List<T>>();
                }

                dadosGuardados.Add(model);
                sJsonGravar = JsonConvert.SerializeObject(dadosGuardados);

                using (StreamWriter file = new StreamWriter(caminhoDados, false, Encoding.UTF8))
                {
                    file.WriteLine(sJsonGravar);
                }

                result = true;
            }
            catch
            {
                return result;
            }

            return result;
        }

        public bool Atualizar<T>(List<T> models, string caminhoDados)
        {
            bool result = false;

            try
            {
                string sJsonGravar = JsonConvert.SerializeObject(models);

                using (StreamWriter file = new StreamWriter(caminhoDados, false, Encoding.UTF8))
                {
                    file.WriteLine(sJsonGravar);
                }

                result = true;
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
