using System;
using System.Collections.Generic;

#nullable disable

namespace App.Data.Models
{
    public partial class TblUsuario
    {
        public int IntIdusuario { get; set; }
        public string StrUsuario { get; set; }
        public string StrCorreo { get; set; }
        public string StrPassword { get; set; }
        public bool BitEstatus { get; set; }
        public DateTime DateCreacion { get; set; }
        public int IntIdsexo { get; set; }

        public virtual TblCatSexo IntIdsexoNavigation { get; set; }
    }
}
