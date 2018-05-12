using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoDeUsuarios.Shared.Base
{
    public abstract class DTOBase 
    {
        public DTOBase()
        {

        }

        public DTOBase(string id)
        {
            this.id = id;
        }

        public string id { get; set; }
    }
}
