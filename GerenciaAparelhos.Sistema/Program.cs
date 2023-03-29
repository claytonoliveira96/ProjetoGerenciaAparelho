using GerenciaAparelhos.Sistema.Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var aparelhoController = new SistemaController();
        aparelhoController.Iniciar();
        Environment.Exit(0);
    }
}