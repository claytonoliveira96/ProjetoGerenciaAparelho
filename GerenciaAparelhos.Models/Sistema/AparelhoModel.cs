using GerenciaAparelhos.Models.Enum;

namespace GerenciaAparelhos.Models.Sistema
{
    public class AparelhoModel
    {
        #region "Propriedades"

        public int Id               { get; set; }
        public string Nome          { get; set; }
        public TipoAparelho Tipo    { get; set; }

        #endregion
    }
}
