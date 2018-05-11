using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GestaoDeUsuarios.Shared.Base
{
    public static class ExtensaoDeString
    {
        public static string SomenteNumeros(this string texto)
            => texto.ExtraiNumerosLetras(Numeros: true, Letras: false);

        private static string ExtraiNumerosLetras(this string texto, bool Numeros, bool Letras, string CaracteresAdicionaisPermitidos = "", bool Diacriticos = false)
        {
            if (string.IsNullOrWhiteSpace(texto))
            { return string.Empty; }

            var regExLetras = new Regex("[a-zA-ZÀ-ÿ]+");
            string retorno = new string(texto.Where(c => CaracteresAdicionaisPermitidos.Contains(c)
                                                      || (Letras && regExLetras.IsMatch(c.ToString()))
                                                      || (Numeros && char.IsNumber(c))).ToArray());
            if (!Diacriticos)
            { retorno = retorno.RemoveDiacriticos(); }

            return retorno;
        }

        private static string RemoveDiacriticos(this string Texto)
        {
            var stFormD = Texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < stFormD.Length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(stFormD[i]);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
