using System;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace GestaoDeUsuarios.Web.Util
{
    public class ClientWebApi
    {
        public static T RealizarRequisicao<T>(string endereco) where T : class
        {
            var retorno = RequisitarString(endereco);

            if (typeof(T) == typeof(string))
                return retorno == null ? null : (T)Convert.ChangeType(retorno, typeof(T));

            return retorno == null ? (T)Activator.CreateInstance(typeof(T)) : Json.Decode<T>(retorno);
        }

        public static T RealizarRequisicao<T, TParametro>(string endereco, TParametro parametros, FormMethod metodo = FormMethod.Post) where T : class
        {
            var parametrosJson = Json.Encode(parametros);
            var retorno = RequisitarString(endereco, metodo.ToString(), parametrosJson);

            if (typeof(T) == typeof(string))
                return retorno == null ? null : (T)Convert.ChangeType(retorno, typeof(T));

            var jsonSerializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };

            return retorno == null ? (T)Activator.CreateInstance(typeof(T)) : jsonSerializer.Deserialize<T>(retorno);
        }

        private static string RequisitarString(string endereco, string metodo = null, string parametros = null)
        {
            using (var cliente = new WebClient())
            {
                try
                {
                    cliente.Encoding = Encoding.UTF8;

                    if (string.IsNullOrEmpty(parametros))
                        return cliente.DownloadString(endereco);

                    cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var retorno = cliente.UploadString(endereco, metodo, parametros);
                    return retorno;
                }
                catch (WebException)
                {
                    return null;
                }
            }
        }
    }
}
