using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.Usuario
{
    public class UsuarioDto
    {
        public int Id { set; get; }
        public string Usuario { set; get; }
        public string Correo { set; get; }
        public string Sexo { set; get; }
        public bool BitEstatus {set;get;}
        public string Estatus { set; get; }
    }
}
