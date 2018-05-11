using System.Linq;

namespace GestaoDeUsuarios.Shared.Base
{
    public static class FuncoesValidacao
    {
        public static bool IsCPFValido(string documento)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            documento = documento.SomenteNumeros();

            if (documento.Length != 11 || documento.Distinct().Count() == 1)
                return false;

            //1° Digito
            var tempdocumento = documento.Substring(0, 9);
            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempdocumento[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();

            //1° Digito
            tempdocumento = tempdocumento + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempdocumento[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return documento.EndsWith(digito);
        }
    }
}
