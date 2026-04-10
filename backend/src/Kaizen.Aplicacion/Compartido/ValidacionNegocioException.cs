namespace Kaizen.Aplicacion.Compartido;

public class ValidacionNegocioException : Exception
{
    public int CodigoHttp { get; }

    public ValidacionNegocioException(string mensaje, int codigoHttp = 400) : base(mensaje)
    {
        CodigoHttp = codigoHttp;
    }
}
