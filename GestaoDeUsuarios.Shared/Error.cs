using System;
using GestaoDeUsuarios.Shared.Base;

namespace GestaoDeUsuarios.Shared
{
    public class Error : DTOBase
    {
        public Error()
        {

        }

        public Error(string key, string value)
        {
            this.key = key;
            this.value = value;            
        }

        public string key { get; set; }
        public string value { get; set; }
    }
}
