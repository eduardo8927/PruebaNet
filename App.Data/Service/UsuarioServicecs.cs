using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using App.Dto.Usuario;

namespace App.Data.Service
{
    public class UsuarioService
    {
        /// <summary>
        /// Agrega nuevo usuario a la base de datos.
        /// </summary>
        /// <param name="usuario"> Usuario</param>
        /// <returns>Devuelve un valor boleano: True si la operación se realizó con exitó, de lo contrario devuelve False</returns>
        public bool AddUsuario(TblUsuario usuario) {
            bool resultado = false;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    dbContext.TblUsuarios.Add(usuario);
                    resultado = dbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza información de un objeto usuario
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Devuelve un valor boleano: True si la operación se realizó con exitó, de lo contrario devuelve False</returns>
        public bool UpdateUsuario(TblUsuario usuario) {
            bool resultado = false;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    dbContext.Entry(usuario).State = EntityState.Modified;
                    resultado = dbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Elimina un usuario de la base de datos
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Devuelve un valor boleano: True si la operación se realizó con exitó, de lo contrario devuelve False</returns>
        public bool DeleteUsuario(TblUsuario usuario) {
            bool resultado = false;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    dbContext.TblUsuarios.Remove(usuario);
                    resultado = dbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene usuarios filtrados por IdUsuario
        /// </summary>
        /// <param name="idUsuario">IdUsuario</param>
        /// <returns>Devuelve objeto usuario que conicida con el filtro establecido</returns>
        public TblUsuario GetUsuarioById(int idUsuario)
        {
            TblUsuario resultado = null;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    resultado = (from u in dbContext.TblUsuarios
                                 where u.IntIdusuario == idUsuario
                                 select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene usuario filtrado por su nombre usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <returns>Devuelve objeto usuario que conicida con el filtro establecido</returns>
        public TblUsuario GetUsuarioByUser(string username)
        {
            TblUsuario resultado = null;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    resultado = (from u in dbContext.TblUsuarios
                                 where u.StrUsuario == username
                                 select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene usuario filtrodo por su correo.
        /// </summary>
        /// <param name="correo">Correo electronico del usuario</param>
        /// <returns>Devuelve objeto usuario que conicida con el filtro establecido</returns>
        public TblUsuario GetUsuarioByCorreo(string correo)
        {
            TblUsuario resultado = null;
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    resultado = (from u in dbContext.TblUsuarios
                                 where u.StrCorreo == correo
                                 select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene  listado de usuarios filtrado por su estatus
        /// </summary>
        /// <param name="estatus">Estatus del usuario : True=Activo  o False=Inactivo</param>
        /// <returns></returns>
        public List<UsuarioDto> GetUsuariosByEstatus(bool estatus) {
            var lsResultado = new List<UsuarioDto>();
            try
            {
                using (var dbContext = new pruebanetContext())
                {
                    lsResultado = (from u in dbContext.TblUsuarios
                                   join cs in dbContext.TblCatSexos on u.IntIdsexo equals cs.IntIdsexo
                                   where u.BitEstatus==estatus
                                   select new UsuarioDto
                                   {
                                       Id = u.IntIdusuario,
                                       Usuario = u.StrUsuario,
                                       Correo = u.StrCorreo,
                                       Sexo = cs.StrSexo,
                                       BitEstatus = u.BitEstatus,
                                       Estatus = (u.BitEstatus ? "Si" : "No"),
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lsResultado;
        }
    }
}
