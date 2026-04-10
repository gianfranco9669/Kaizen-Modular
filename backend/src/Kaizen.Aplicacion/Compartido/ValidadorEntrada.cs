namespace Kaizen.Aplicacion.Compartido;

public static class ValidadorEntrada
{
    public static string Requerido(string? valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ValidacionNegocioException($"El campo '{campo}' es obligatorio.");
        }

        return valor.Trim();
    }

    public static decimal MayorACero(decimal valor, string campo)
    {
        if (valor <= 0)
        {
            throw new ValidacionNegocioException($"El campo '{campo}' debe ser mayor a cero.");
        }

        return valor;
    }

    public static int MayorACero(int valor, string campo)
    {
        if (valor <= 0)
        {
            throw new ValidacionNegocioException($"El campo '{campo}' debe ser mayor a cero.");
        }

        return valor;
    }
}
