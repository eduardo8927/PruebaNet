using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Dto.Usuario;
using System.Security.Claims;
using System.Reflection;

namespace WPruebaNet.Auth
{
    public static class AppUsuarioExtension
    {
        /// <summary>
        /// Convierte objeto  Claim principal a AppUser
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>Devuelve objeto usuario</returns>
        public static AppUser ConvertClaimToUser(this ClaimsPrincipal principal)
        {
            AppUser user = null;
            if (principal != null)
            {
                user = new AppUser();
                PropertyInfo[] properties = typeof(AppUser).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        //así obtenemos el nombre del atributo
                        string nombreAtributo = property.Name;
                        //así obtenemos el valor del atributo
                        var valor = Convert.ChangeType(principal.Claims.FirstOrDefault(c => c.Type == nombreAtributo)?.Value, property.PropertyType); 
                        property.SetValue(user, valor);
                    }
                    catch { }
                }
            }
            return user;
        }

        /// <summary>
        /// Convierte Claim principal a AppUser
        /// </summary>
        /// <param name="usuario">AppUser</param>
        /// <returns>Devuelve claim principal para la sesión</returns>
        public static ClaimsPrincipal CreaClaim(AppUser usuario)
        {
            var identity = new ClaimsIdentity("AuthSGA");
            if (usuario != null)
            {
                PropertyInfo[] properties = typeof(AppUser).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    //así obtenemos el nombre del atributo
                    String nombreAtributo = property.Name;
                    //así obtenemos el valor del atributo
                    String valor = property.GetValue(usuario) == null ? string.Empty : property.GetValue(usuario).ToString();
                    identity.AddClaim(new Claim(nombreAtributo, valor));
                }
            }
            return new ClaimsPrincipal(identity);
        }
    }
}
