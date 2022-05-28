using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManejadoresAnalizador
{
    public class ManejadorTraduccion
    {
        public string TransBuilder(DataGridView tabla)
        {
            string translation = "";
            if(traducirComentario(tabla).Length>0)
            {
                translation += traducirComentario(tabla);
            }
            if(traducirVariables(tabla).Length>0)
            {
                translation += traducirVariables(tabla);
            }
            if(traducirProcesoInicio(tabla).Length>0)
            {
                translation += traducirProcesoInicio(tabla);
            }      
            return translation;
        }
        public string traducirCondicion(DataGridView tabla, int i)
        {
            string r = "";
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[1].Value.ToString().Contains("si"))
                {
                    r += "if "+tabla.Rows[a+1].Cells[1].Value.ToString() + "\r\n{\r\n";
                    
                }              
                if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Delimitador de cierre"))
                {
                    r += traducirDelimitadorCierre(tabla, i);
                }
            }
            return r;
        }
        public string traducirVariables(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(tabla.Rows[i].Cells[1].Value.ToString().Contains("int"))
                {
                    r += tabla.Rows[i].Cells[1].Value.ToString() + " " + tabla.Rows[i + 1].Cells[1].Value.ToString()+"\r\n";
                }
            }
            
            return r + "\n\rvoid setup()\r\n{\r\n Serial.begin(9600);\r\n}\r\n";
        }
        //this method  finds if there are a instruccion inside the Process method, so when this method find a instruction inside then traduce the
        //method and build the traduction string.
        public string traducirProcesoInicio(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("Proceso"))
                {
                    r = "void loop()\r\n{\r\n";
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("Encender"))
                {
                    //Every method to traduce receives the DTG or the table of tokens and a integer, the integer will define the line where
                    // the instruccion is
                    r += traducirEncendido(tabla, i);
                    //r+= is used to store all the result strings in case if the condition is true or not
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("si"))
                {
                    r += traducirCondicion(tabla, i);
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("Retardo"))
                {
                    r += traducirRetardo(tabla, i);
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("Velocidad"))
                {
                    r += traducirVelocidad(tabla, i);
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Derecha"))
                {
                    r += traducirDirGiroDer(tabla, i);
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Izquierda"))
                {
                    r += traducirDirGiroIzq(tabla, i);
                }
                if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Incremento de variable"))
                {
                    r += traducirOperadorSuma(tabla, i);
                }
                if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Decremento de variable"))
                {
                    r += traducirOperadorResta(tabla, i);
                }
                if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Delimitador de cierre"))
                {
                    r += traducirDelimitadorCierre(tabla, i);
                }
            }       
            return r;
        }
        public string traducirEncendido(DataGridView tabla, int i)
        {
            string r = "";
            string[] valor;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if(tabla.Rows[a].Cells[1].Value.ToString().Equals("Encender") && tabla.Rows[a+1].Cells[2].Value.ToString().Equals("Parametros de instruccion"))
                {
                    valor = tabla.Rows[a+1].Cells[1].Value.ToString().Split('(', ')');
                    r = "digitalWrite ("+valor[1]+",HIGH);\r\n"+
                        "Serial.println(\u0022Encendido\u0022);\r\n";
                }
                else
                {
                    a = tabla.RowCount;
                }
            }
            return r;
        }
        public string traducirOperadorSuma(DataGridView tabla, int i)
        {
            string r = "";
            string[] ParamsValor;
            string variable;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[2].Value.ToString().Equals("Incremento de variable"))
                {
                    ParamsValor = tabla.Rows[a].Cells[1].Value.ToString().Split('+');
                    variable = ParamsValor[0];
                    r =variable+"++;";
                }
                else
                {
                    a = tabla.RowCount;
                }
            }
            return r+"\r\n";
        }
        public string traducirOperadorResta(DataGridView tabla, int i)
        {
            string r = "";
            string[] ParamsValor;
            string variable;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[2].Value.ToString().Equals("Decremento de variable"))
                {
                    ParamsValor = tabla.Rows[a].Cells[1].Value.ToString().Split('-');
                    variable = ParamsValor[0];
                    r = variable + "--;";
                }
                else
                {
                    a = tabla.RowCount;
                }
            }
            return r + "\r\n";
        }
        public string traducirDirGiroDer(DataGridView tabla, int i)
        {
            string r = "";
            string[] ParamsValor;
            string valor1;
            string valor2;
            for (int a = i; a <tabla.RowCount; a++)
            {
                if(tabla.Rows[a].Cells[1].Value.ToString().Equals("Giro.Derecha"))
                {
                    ParamsValor = tabla.Rows[a + 1].Cells[1].Value.ToString().Split('(', ')',',');
                    valor1 = ParamsValor[1];
                    valor2 = ParamsValor[2];
                    r = "digitalWrite("+valor1+",HIGH);\r\n" +
                         "digitalWrite("+valor2+",LOW);\r\n"+
                         "Serial.println(\u0022Girando a la derecha\u0022);\r\n";                    
                }              
            }
            return r;
        }
        public string traducirDirGiroIzq(DataGridView tabla, int i)
        {
            string r = "";
            string[] ParamsValor;
            string valor1;
            string valor2;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[1].Value.ToString().Equals("Giro.Izquierda"))
                {
                    ParamsValor = tabla.Rows[a + 1].Cells[1].Value.ToString().Split('(', ')', ',');
                    valor1 = ParamsValor[1];
                    valor2 = ParamsValor[2];
                    r = "digitalWrite(" + valor1 + ",HIGH);\r\n" +
                         "digitalWrite(" + valor2 + ",LOW);\r\n"+
                         "Serial.println(\u0022Girando a la izquierda\u0022);\r\n";
                }
            }
            return r;
        }
        public string traducirDelimitadorCierre(DataGridView tabla, int i)
        {
            string r = "";
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[2].Value.ToString().Equals("Delimitador de cierre"))
                {
                    r = "}\r\n";
                }
                else
                {
                    a = tabla.RowCount;
                }
            }
            return r;
        }
        public string traducirVelocidad(DataGridView tabla, int i)
        {
            string r = "";
            string[] ParamsValor;
            string valor1;
            string valor2;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[1].Value.ToString().Equals("Velocidad"))
                {
                    ParamsValor = tabla.Rows[a + 1].Cells[1].Value.ToString().Split('(', ')', ':');
                    valor1 = ParamsValor[1];
                    valor2 = ParamsValor[2];
                    r = "analogWrite("+valor1+","+valor2+");\r\n"+
                        "Serial.println(\u0022Velocidad establecida en: "+valor2+"\u0022);\r\n"; 
                }
                else
                {
                    a = tabla.RowCount;
                }
            }
            return r+"\r";
        }
        public string traducirRetardo(DataGridView tabla, int i )
        {
            string r = "";
            string[] valor;
            for (int a = i; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[1].Value.ToString().Equals("Retardo"))
                {
                    valor = tabla.Rows[a + 1].Cells[1].Value.ToString().Split('(', ')');
                    int ConvertMS = int.Parse(valor[1])*1000;
                    r = "delay(" + ConvertMS.ToString() +");\r\n";                     
                }   
                else
                {
                   a = tabla.RowCount;
                }
            }
            return r;
        }
        public string traducirComentario(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("!!"))
                {
                    r = tabla.Rows[i].Cells[1].Value.ToString().Replace('!', '/').Replace('_', ' ') + "\r\n";
                }

            }
            return r;
        }
    }
    
}
