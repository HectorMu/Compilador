using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAnalizador
{
    public class CamposDTG
    {
        public int No { get; set; }
        public string Token { get; set; }
        public string Descripcion { get; set; }
        public int  Linea { get; set; }

        public CamposDTG(int no, string token, string descripcion, int linea)
        {
            No = no;
            Token = token;
            Descripcion = descripcion;
            Linea = linea;
        }
    }
}
