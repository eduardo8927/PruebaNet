using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WPruebaNet.Auth;

namespace WPruebaNet.Controllers
{
    public class UsuarioController : Controller
    {
        Usuario lU;
        public UsuarioController() {
            lU = new Usuario();
        }
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPut]
        public IActionResult AddUsuario(string correo, string usuario, string contrasenia, int sexo) {
            StatusCodeResult resultado = null;
            var resp = lU.AddUsuario(correo, usuario, contrasenia, sexo);
            switch (resp)
            {
                case 0:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
                    break;
                case 1:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
                    break;
                case 2:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict);
                    break;
                case 3:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    break;
            }
            return resultado;
        }

        [HttpPost]
        public ActionResult AuthUsuario(string usuario, string contrasenia)
        {
            StatusCodeResult result = null;
            var resp = lU.AuthUsuario(usuario, contrasenia);
            switch (resp.Item1) {
                case 0:
                    var userPrincipal = AppUsuarioExtension.CreaClaim(resp.Item2);
                    HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(160)//Duración de la sesión
                    });
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
                    break;
                case 1:
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
                    break;
                case 2:
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status203NonAuthoritative);
                    break;
            }
            return result;
        }

        [HttpDelete]
        public IActionResult DeleteUsuario(int idUsario)
        {
            StatusCodeResult resultado = null;
            var resp = lU.DeleteUsuario(idUsario);
            switch (resp)
            {
                case 0:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
                    break;
                case 1:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
                    break;
                case 2:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict);
                    break;
                case 3:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    break;
            }
            return resultado;
        }

        [HttpPost]
        public JsonResult GetUsuariosByEstatus(bool estatus) {
            var resp = lU.GetUsuariosByEstatus(estatus);
            return Json(resp);
        }


        [HttpPut]
        public IActionResult UpdateUsuario(int idUsuario,string correo, string usuario, string contrasenia)
        {
            StatusCodeResult resultado = null;
            var resp = lU.UpdateUsuario(idUsuario, correo, usuario, contrasenia);
            switch (resp)
            {
                case 0:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
                    break;
                case 1:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
                    break;
                case 2:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
                    break;
                case 3:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict);
                    break;
                case 4:
                    resultado = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    break;
            }
            return resultado;
        }

        [HttpPost]
        public JsonResult GetUsuariosById(int idUsuario)
        {
            var resp = lU.GetUsuariosById(idUsuario);
            return Json(resp);
        }

        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }
    }
}
