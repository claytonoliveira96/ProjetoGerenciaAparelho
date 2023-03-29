namespace GerenciaAparelhos.Models.Sistema
{
    public class ReservaModel
    {
         #region "Propriedades"

        public int Id               { get; set; }
        public int IdAparelho       { get; set; }
        public int IdProfessor      { get; set; }
        public DateTime Inclusao    { get; set; }
        public DateTime Reserva     { get; set; }
        public DateTime Devolucao   { get; set; }

        #endregion
    }
}
