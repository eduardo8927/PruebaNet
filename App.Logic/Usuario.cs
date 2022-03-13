using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Service;
using App.Data.Models;
using App.Util;
using App.Dto.Usuario;

namespace App.Logic
{
    public class Usuario
    {
        UsuarioService sU;
        /// <summary>
        /// Constructur por default
        /// </summary>
        public Usuario() {
            sU = new UsuarioService();
        }

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        /// <param name="correo">Correo electronicó</param>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="contrasenia">Contraseña</param>
        /// <param name="Sexo">Identificador del sexo</param>
        /// <returns>Numero de error</returns>
        public int AddUsuario(string correo, string usuario, string contrasenia, int Sexo) {
            int error = 0;
            var usr = sU.GetUsuarioByCorreo(correo);
            //Valida correo a registrar
            if (usr == null)
            {
                usr = sU.GetUsuarioByUser(usuario);
                if (usr == null) {
                    var usrNew = new TblUsuario();
                    usrNew.StrCorreo = correo;
                    usrNew.StrUsuario = usuario;
                    usrNew.StrPassword = Encrypt.EncryptString(contrasenia,KeyEncrypts.KeyPassword,KeyEncrypts.KeyVector);
                    usrNew.IntIdsexo = Sexo;
                    usrNew.BitEstatus = true;
                    if (!sU.AddUsuario(usrNew))
                        error = 3;//Error al insertar registro en BD
                }
                else
                    error = 2;//El usuario a registrar ya existe
            }
            else
                error = 1;//El correo a registrar ya existe
            return error;
        }

        /// <summary>
        /// Autentifica si un usuario se encuentra restrado en la base de datos 
        /// </summary>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="contrasenia">Contraseña</param>
        /// <returns>Numero de error y objeto usuario</returns>
        public (int, AppUser) AuthUsuario(string usuario, string contrasenia) {
            int error = 0;
            AppUser usrApp =null;
            var usr = sU.GetUsuarioByUser(usuario);
            if (usr != null)
            {
                var passEncry= Encrypt.EncryptString(contrasenia, KeyEncrypts.KeyPassword, KeyEncrypts.KeyVector);
                if (passEncry == usr.StrPassword) {
                    usrApp = new AppUser();
                    usrApp.idUsuario = usr.IntIdusuario;
                    usrApp.usuario = usr.StrUsuario;
                    usrApp.correo = usr.StrCorreo;
                }                    
                else 
                    error = 2;//Contraseña incorrecta

            }
            else
                error = 1;//Usuario no existe
            return (error,usrApp);
        }

        /// <summary>
        /// Obtiene listado de usuario filtrados por su estatus
        /// </summary>
        /// <param name="estatus">Estus del usuario True o False</param>
        /// <returns>Numero de error</returns>
        public List<UsuarioDto> GetUsuariosByEstatus(bool estatus) {
            return sU.GetUsuariosByEstatus(estatus);
        }

        /// <summary>
        /// Cambia el estatus del usuario de Activo a Inactivo, esto es una eliminacion logicá
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Numero de error</returns>
        public int DeleteUsuario(int idUsuario) {
            int error = 0;
            var usr = sU.GetUsuarioById(idUsuario);
            if (usr != null) {
                if (usr.BitEstatus)
                {
                    usr.BitEstatus = false;
                    if (!sU.UpdateUsuario(usr))
                        error = 3; //Error al actualizar
                }
                else
                    error = 2; // El usuario ya esta eliminado  
            }
            else
                error = 1; //No existe el usuario

            return error;
        }

        /// <summary>
        /// Actualizá información del usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <param name="correo">Correo electronicó</param>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="contrasenia">Contraseña</param>
        /// <returns>Retorna un numero de error</returns>
        public int UpdateUsuario(int idUsuario, string correo, string usuario, string contrasenia)
        {
            int error = 0;
            var usr = sU.GetUsuarioById(idUsuario);
            //Valida correo a registrar
            if (usr != null)
            {
                var usrAux1 = sU.GetUsuarioByUser(usuario);
                if (usrAux1 != null && usrAux1.IntIdusuario != idUsuario)
                    error = 2;// El usuario ya se esta utilizando
                else {
                    var usrAux2 = sU.GetUsuarioByCorreo(correo);
                    if (usrAux2 != null && usrAux2.IntIdusuario != idUsuario)
                        error = 3;// El correo ya se esta utilizando
                }
            }
            else
                error = 1;//El usuario no existe
            if (error == 0) {
                usr.StrCorreo = correo;
                usr.StrUsuario = usuario;
                usr.StrPassword = contrasenia;
                if (!sU.UpdateUsuario(usr))
                    error = 4;
            }
            return error;
        }

        public TblUsuario GetUsuariosById(int idUsuario)
        {
            return sU.GetUsuarioById(idUsuario);
        }
    }
}
