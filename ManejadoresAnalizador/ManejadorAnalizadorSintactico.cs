using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ManejadoresAnalizador
{ 
    public class ManejadorAnalizadorSintactico
    {
        public string AnalizadorSintactico(DataGridView tabla)
        {         
            string r = "No se encontraron errores";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (AnalizadorAsignacion(tabla).Contains("Error"))
                {
                    r = AnalizadorAsignacion(tabla);      
                    break;
                }
                if (AnalizadorCondicion(tabla).Contains("Error"))
                {
                    r = AnalizadorCondicion(tabla);
                    break;
                }
                if (AnalizadorInstruccionRet(tabla).Contains("Error"))
                {
                    r = AnalizadorInstruccionRet(tabla);
                    break;
                }
                if (AnalizadorInstruccionGiro(tabla).Contains("Error"))
                {
                    r = AnalizadorInstruccionGiro(tabla);
                    break;
                }
                if (AnalizadorComentario(tabla).Contains("Error"))
                {
                    r = AnalizadorComentario(tabla);
                    break;
                }
                if (AnalizadorProceso(tabla).Contains("Error"))
                {
                    r = AnalizadorProceso(tabla);
                    break;
                }
                if (AnalizadorInstruccionEncender(tabla).Contains("Error"))
                {
                    r = AnalizadorInstruccionEncender(tabla);
                    break;
                } 
                if(AnalizadorOpIncremento(tabla).Contains("Error"))
                {
                    r = AnalizadorOpIncremento(tabla);
                    break;
                }
                if(AnalizadorOpDecremento(tabla).Contains("Error"))
                {
                    r = AnalizadorOpDecremento(tabla);
                    break;
                }
                if (AnalizadorInstruccionVelocidad(tabla).Contains("Error"))
                {
                    r = AnalizadorInstruccionVelocidad(tabla);
                    break;
                }
                if(AnalizadorDelimCierre(tabla).Contains("Error"))
                {
                    r = AnalizadorDelimCierre(tabla);
                    break;
                }
                if(AnalizadorDelimApertura(tabla).Contains("Error"))
                {
                    r = AnalizadorDelimApertura(tabla);
                    break;
                }
            }
            return r;
        }
        public string AnalizadorOpIncremento( DataGridView tabla)
        {
            string r = "";
            var contador = 0;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(tabla.Rows[i].Cells[1].Value.ToString().Contains("+"))
                { 
                    char[] incrementador = tabla.Rows[i].Cells[1].Value.ToString().ToCharArray();
                    contador = 0;
                    for (int a = 0; a < incrementador.Length; a++)
                    {
                        if (incrementador[a].ToString().Equals("+"))
                        {
                            contador++;
                        }
                    }
                    if(contador == 2)
                    {
                        r = "Correcto";                                       
                    }
                    if(contador > 2)
                    {
                        tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error: Se esperaba una expresion de incremento valida (Variable++).\nElimine los operadores sobrantes \n" +
                           "Token: " + tabla.Rows[i].Cells[1].Value.ToString();
                       
                        i = tabla.RowCount;
                    }
                    if (contador < 2)
                    {
                        tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error: Se esperaba una expresion de incremento valida (Variable++).\nAgregue el operador faltante \n" +
                           "Token: " + tabla.Rows[i].Cells[1].Value.ToString();
                        i = tabla.RowCount;
                    }
                }
            }
            return r;
        }
        public string AnalizadorOpDecremento(DataGridView tabla)
        {
            string r = "";
            var contador = 0;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("-"))
                {

                    char[] decrementador = tabla.Rows[i].Cells[1].Value.ToString().ToCharArray();
                    contador = 0;
                    for (int a = 0; a < decrementador.Length; a++)
                    {
                        if (decrementador[a].ToString().Equals("-"))
                        {
                            contador++;
                        }
                    }
                    if (contador == 2)
                    {
                        r = "Correcto";
                    }
                    if (contador > 2)
                    {
                        tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error: Se esperaba una expresion de decremento valida (Variable--).\nElimine los operadores sobrantes \n" +
                            "Token: "+ tabla.Rows[i].Cells[1].Value.ToString();
                        i = tabla.RowCount;
                    }
                    if (contador < 2)
                    {
                        tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error: Se esperaba una expresion de decremento valida (Variable--).\nAgregue el operador faltante \n" +
                           "Token: " + tabla.Rows[i].Cells[1].Value.ToString();
                        i = tabla.RowCount;
                    }
                }
            }
            return r;
        }
        public string AnalizadorAsignacion(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(tabla.Rows[i].Cells[1].Value.ToString().Equals("int"))
                {
                    r = "Correcto";
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Expresion de asignacion"))
                    {
                        r = "Correcto";
                    }                   
                    else
                    {
                        tabla.Rows[i+1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error ASIG-1: Se esperaba una expresion de asignacion valida despues de 'int'.\nint identificador=valorEntero;" +
                            "\nToken: "+tabla.Rows[i+1].Cells[0].Value.ToString()+" "+"''"+tabla.Rows[i + 1].Cells[1].Value.ToString()+"''";
                        break;
                    }
                }             
            }
            return r;
        }
        public string AnalizadorCondicion(DataGridView tabla)
        {   
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("si"))
                {
                   
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Expresion de condicion"))
                    {
                        
                        if (tabla.Rows[i + 2].Cells[2].Value.ToString().Equals("Delimitador de apertura"))
                        {
                        
                            for (int a = 1; a < tabla.Rows.Count; a++)
                            {
                                if (tabla.Rows[a].Cells[2].Value.ToString().Equals("Delimitador de cierre"))
                                {
                                    r = "correcto";
                                }
                                else 
                                {
                                    r = "Error CON-3: Se esperaba un delimitador de cierre para la condicion. " +
                                        "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                                }
                            }                           
                        } 
                        else
                        {
                            tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                            r = "Error CON-2: Se esperaba un delimitador de apertura para la condicion." +
                                " \nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        }
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error CON-1: Se esperaba una expresion de condicion valida dentro de los parentesis.\n si (identificador==/>=/<=/>/<valor)" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }  
            }
            return r;
        }
        public string AnalizadorInstruccionRet(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Retardo"))
                {
                    r = "Correcto";
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Parametros de instruccion"))
                    {
                        r = "Correcto";
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error INST-Retardo-01: Se esperaban parametros (Valor entero o Identificador)\n " +
                            "dentro del parentesis despues de la Instruccion. Retardo (Valor Entero/Identificador)" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }
            }
            return r;
        }
        public string AnalizadorInstruccionEncender(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Encender"))
                {
                    r = "Correcto";
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Parametros de instruccion"))
                    {
                        r = "Correcto";
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error INST-Encender-01: Se esperaban parametros (Valor entero o Identificador)\n " +
                            "dentro del parentesis despues de la Instruccion. Encender (0/1)" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }
            }
            return r;
        }
        public string AnalizadorInstruccionVelocidad(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Velocidad"))
                {
                    r = "Correcto";
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Parametros de Velocidad"))
                    {
                        r = "Correcto";
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error INST-Vel-01: Se esperaban parametros (Valor Entero:/Variable) o dentro de los\n" +
                            "parentesis despues de la instruccion.\n Velocidad (Pin:vel)/(pin:3)/(3:vel)" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }
            }
            return r;
        }
        public string AnalizadorInstruccionGiro(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Izquierda") || (tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Derecha")))
                {
                    r = "Correcto";
                    
                    if(tabla.Rows[i+1].Cells[2].Value.ToString().Equals("Parametro Direccion de Giro"))
                    {

                        r = "Correcto";
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error INST-Giro-01: Se esperaban parametros despues de la instruccion\n(variable/valor, variable/valor)" +
                            "dentro de los parentesis despues de la instruccion.\n Giro.Izquierda/Derecha (variable/valor, variable/valor)" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }
            }
            return r;
        }
        public string AnalizadorComentario(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("!!"))
                {                  
                  r = "Correcto";
                    if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Comentario"))
                    {
                        r = "Correcto";
                        if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("No Identificado"))
                        {
                            tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                            r = "Error COM-02: Asegurese de Separar las palabras por guiones bajos: !!Hola_Mundo" +
                                "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        }
                    }
                    else
                    {
                        tabla.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error COM-01: Asegurese de empezar con dos signos de exclamacion:!!Hola.\n Y de separar las palabras por guiones bajos: !!Hola_Mundo" +
                            "\nToken: " + tabla.Rows[i].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i].Cells[1].Value.ToString() + "''";
                    }
                }
            }
            return r;
        }
        public string AnalizadorDelimApertura(DataGridView tabla)
        {
            string r = "";
            int delContador = 0;
            int delApertura = 0;
            
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("Proceso"))
                {
                    delApertura= 1;
                }
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("si"))
                {
                    delApertura++;
                }

                if (tabla.Rows[i].Cells[2].Value.ToString().Contains("Delimitador de apertura"))
                {
                    delContador++;
                }
                if (delApertura == delContador)
                {
                    r = "Correcto";
                   
                }
                if (delApertura < delContador)
                {
                    r = "Error: Se encontro un delimitador de apertura sobrante, remuevalo.";
                   
                }
                if (delApertura > delContador)
                {

                    r = "Error: Se esperaba un delimitador de apertura para la instruccion, agreguelo.";
                  
                }
            }
            return r;
        }
        public string AnalizadorDelimCierre(DataGridView tabla)
        {
            string r = "";
            int delContador = 0;
            int delCierre = 0;
            for (int i = 0; i < tabla.RowCount; i++)
            {             
                if(tabla.Rows[i].Cells[1].Value.ToString().Contains("Proceso"))
                {
                    delCierre = 1;
                }
                if(tabla.Rows[i].Cells[1].Value.ToString().Contains("si"))
                {
                    delCierre++;
                }
                if (tabla.Rows[i].Cells[2].Value.ToString().Contains("Delimitador de cierre"))
                {
                    delContador++;
                }
                if(delCierre==delContador)
                {
                    r = "Correcto";
                }
                if(delCierre < delContador)
                {
                    
                    r = "Error: Se encontro un delimitador de cierre sobrante, remuevalo: "+tabla.Rows[i].Cells[1].Value.ToString();
                }
                if(delCierre > delContador)
                {
                    
                    r = "Error: Se esperaba un delimitador de cierre para la instruccion "+ tabla.Rows[i].Cells[1].Value.ToString();
                }
            }


            return r;
        }
        public string AnalizadorProceso(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Proceso"))
                {
                    r = "Correcto";
                    if (tabla.Rows[i + 1].Cells[2].Value.ToString().Equals("Parametros de instruccion"))
                    {
                        r = "Correcto";
                        if (tabla.Rows[i + 2].Cells[2].Value.ToString().Equals("Delimitador de apertura"))
                        {
                            r = "Correcto";
                        }
                        else
                        {
                            tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                            r = "Error PRO-2: Se esperaba un delimitador de apertura." +
                                " \nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        }
                    }
                    else
                    {
                        tabla.Rows[i + 1].DefaultCellStyle.BackColor = Color.Red;
                        r = "Error PRO-1: Se esperaba un contenedor de parametros () despues de la palabra reservada" +
                            "\nToken: " + tabla.Rows[i + 1].Cells[0].Value.ToString() + " " + "''" + tabla.Rows[i + 1].Cells[1].Value.ToString() + "''";
                        break;
                    }
                }
            }
            return r;
        }
    }
}
