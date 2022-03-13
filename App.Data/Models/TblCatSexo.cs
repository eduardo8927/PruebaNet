using System;
using System.Collections.Generic;

#nullable disable

namespace App.Data.Models
{
    public partial class TblCatSexo
    {
        public TblCatSexo()
        {
            TblUsuarios = new HashSet<TblUsuario>();
        }

        public int IntIdsexo { get; set; }
        public string StrSexo { get; set; }

        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}
