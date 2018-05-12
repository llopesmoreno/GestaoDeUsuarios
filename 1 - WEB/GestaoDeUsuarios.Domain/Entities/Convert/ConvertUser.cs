using GestaoDeUsuarios.Shared;
using System.Collections.Generic;

namespace GestaoDeUsuarios.Domain.Entities.Convert
{
    public static class ConvertUser
    {
        public static UserDTO ToDTO(this User user) => new UserDTO(user.Name.FirstName, 
            user.Name.LastName, 
            user.CPF.Value, 
            user.Telefone, 
            user.Id.ToString());        

        public static List<UserDTO> ToDTO(this List<User> users)
        {
            var lista = new List<UserDTO>();
            users.ForEach(f => lista.Add(f.ToDTO()));
            return lista;
        }
    }
}
