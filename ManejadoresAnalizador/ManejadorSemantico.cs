using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ManejadoresAnalizador
{
    public class ManejadorSemantico
    {
        public string Semantico(DataGridView tabla)
        {
            string r = "No se encontraron errores";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(ReglaUno(tabla).Contains("Error"))
                {
                    r = ReglaUno(tabla);
                    break;
                }
                if(ReglaDos(tabla).Contains("Error"))
                {
                    r = ReglaDos(tabla);
                    break;
                }
                if(ReglaTres(tabla).Contains("Error"))
                {
                    r = ReglaTres(tabla);
                    break;
                }
                if(ReglaCuatro(tabla).Contains("Error"))
                {
                    r = ReglaCuatro(tabla);
                    break;
                }
                if(ReglaCinco(tabla).Contains("Error"))
                {
                    r = ReglaCinco(tabla);
                    break;
                }
                //reglas de programa
                if (RA_Uno(tabla).Contains("Error"))
                {
                    r = RA_Uno(tabla);
                    break;
                }
                if (RA_dos(tabla).Contains("Error"))
                {
                    r = RA_dos(tabla);
                    break;
                }
                if (RA_Tres(tabla).Contains("Error"))
                {
                    r = RA_Tres(tabla);
                    break;
                }
                if (RA_Cuatro(tabla).Contains("Error"))
                {
                    r = RA_Cuatro(tabla);
                    break;
                }
                if (RA_Cinco(tabla).Contains("Error"))
                {
                    r = RA_Cinco(tabla);
                    break;
                }
                if(RA_Seis(tabla).Contains("Error"))
                {
                    r = RA_Seis(tabla);
                    break;
                }
                if(RA_Siete(tabla).Contains("Error"))
                {
                    r = RA_Siete(tabla);
                    break;
                }
                if(RA_Ocho(tabla).Contains("Error"))
                {
                    r = RA_Ocho(tabla);
                    break;
                }
                if (RA_Nueve(tabla).Contains("Error"))
                {
                    r = RA_Nueve(tabla);
                    break;
                }
            }
            return r;
        }
        public string ReglaUno(DataGridView tabla)
        {
           string r = "";
           int c = 0;
            for (int a = 0; a < tabla.RowCount-1; a++)
            {
                if(tabla.Rows[a].Cells[1].Value.ToString().Equals("int") || tabla.Rows[a].Cells[2].Value.ToString().Equals("Expresion de asignacion"))
                {
                    for (int i = a - 1; i >= 0; i--)
                    {
                        if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Proceso"))
                        {
                            c++;
                            if (c == 1)
                            {
                                r = " Error: Las variables deben ir siempre fuera y antes que el metodo Proceso";
                            }
                        }
                    }
                }          
            }
            return r;
        }
        public string ReglaDos(DataGridView tabla)
        {
            string r = "";
            for (int a = 0; a < tabla.RowCount; a++)
            {
                if(tabla.Rows[a].Cells[2].Value.ToString().Equals("Expresion de asignacion"))
                {
                        if (tabla.Rows[a + 1].Cells[2].Value.ToString().Equals("Expresion de asignacion"))
                        {
                            r = "Error: No puede haber una expresion de asignacion despues de otra expresion de asignacion\nAnteponga el tipo de dato";
                        }            
                }
            }
            return r;
        }
        public string ReglaTres(DataGridView tabla)
        {
            string r = "";
            for (int a = 0; a < tabla.RowCount-1; a++)
            {
                if(tabla.Rows[a].Cells[1].Value.ToString().Equals("Velocidad") || tabla.Rows[a].Cells[1].Value.ToString().Equals("Retardo") || tabla.Rows[a].Cells[1].Value.ToString().Equals("Giro")|| tabla.Rows[a].Cells[1].Value.ToString().Equals("Encender"))
                {
                    for (int i = a; i < tabla.RowCount-1; i++)
                    {
                        if(tabla.Rows[i].Cells[1].Value.ToString().Equals("Proceso"))
                        {
                            r = "Error: Las funciones deben ir dentro del metodo Proceso";
                        }
                    }               
                }
            }
            return r;
        }
        public string ReglaCuatro(DataGridView tabla)
        {
            int c = 0;
            string r = "";
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if(tabla.Rows[i].Cells[1].Value.ToString().Contains("Proceso"))
                {
                    c++;
                    if (c == 2)
                    {
                        r = "Error: Solo debe existir un metodo proceso";
                    }
                }
            }
            return r;
        }    
        public string ReglaCinco(DataGridView tabla)
        {
            string r = "";
            if(!tabla.Rows[tabla.RowCount-1].Cells[2].Value.ToString().Equals("Delimitador de cierre"))
            {
                r = "Error: La ultima linea debe ser un delimitador de cierre remueva el token o el espaciado";
            }
            return r;
        }
        //Reglas de Instruccion

        //verificar que la expresion de encender reciba un valor o una variable declarada
        public string RA_Uno(DataGridView tabla)
        {
            string r = "";
            string[] parametro;
            string idVariable = "";
            for (int i = 0; i < tabla.RowCount - 1; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Encender"))
                {
                    parametro = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('(', ')');
                    idVariable = parametro[1].ToString();
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(idVariable, out num);
                        if(isNumber)
                        {
                            r = "";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(idVariable))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + idVariable + " no declarada. Declare la variable";
                                }
                            }
                            else
                            {
                                r = "Error: Variable " + idVariable + " no declarada. Declare la variable";
                            }
                        }
                       
                    }
                }
            }
            return r;
        }
        public string RA_dos(DataGridView tabla)
        {
            string r = "";
            string[] parametro;
            string idVariable = "";
            for (int i = 0; i < tabla.RowCount - 1; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("si"))
                {
                    parametro = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('(', ')', '<', '=','>');
                    idVariable = parametro[1].ToString();
                    for (int a = 0; a < tabla.RowCount; a++)
                    {                       
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(idVariable))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + idVariable + " no declarada. Declare la variable.";
                                    i = tabla.RowCount;
                                }
                            }
                            else
                            {
                                r = "Error: Variable " + idVariable + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }    
                    }
                }
            }
            return r;
        }
        public string RA_Tres(DataGridView tabla)
        {
            string r = "";
            for (int i = 0; i < tabla.RowCount - 1; i++)
            {
                if (tabla.Rows[i].Cells[2].Value.ToString().Equals("Expresion de asignacion"))
                {
                    r = "";
                    try
                    {
                        if (tabla.Rows[i - 1].Cells[1].Value.ToString().Equals("int"))
                        {
                            r = "";
                        }
                        else
                        {
                            r = "Error: Expresion sin tipo de dato.";
                        }
                    }
                    catch
                    {
                        r = "Error: Expresion sin tipo de dato.";
                    }
                   
                }
            }
            return r;
        }
        //Verificar que no haya dos o mas variables con el mismo nombre
        public string RA_Cuatro(DataGridView tabla)
        {
            string r = "";
            string[] asignacion;
            string idAsign = "";
            int contador = 0;
            for (int i = 0; i < tabla.RowCount - 1; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("int"))
                {
                    asignacion = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('=', ';');
                    idAsign = asignacion[0].ToString();
                }
            }
            for (int a = 0; a < tabla.RowCount; a++)
            {
                if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                {
                    if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(idAsign))
                    {
                        contador++;
                        if (contador > 1)
                        {
                            r = "Error: Variable " + idAsign + " ya declarada.";

                        }
                        else
                        {
                            r = "";
                        }
                    }
                }
            }
            return r;
        }
        //Verificar que la funcion de operadores de incremento y decremento solo se use sobre variables declaradas
        public string RA_Cinco(DataGridView tabla)
        {
            string r = "";
            string identificador = "";
            string[] operador;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Contains("+") || tabla.Rows[i].Cells[1].Value.ToString().Contains("-"))
                {
                    operador = tabla.Rows[i].Cells[1].Value.ToString().Split('+', '-');
                    identificador = operador[0];
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(identificador, out num);
                        if (isNumber)
                        {
                            r = "Error: Los operadores de incremento y decremento estan reservados para su uso sobre\nvariables declaradas";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(identificador))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                    i = tabla.RowCount;
                                }
                            }
                            else
                            {
                                r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }
                        }
                    }
                }
            }
            return r;
        }
        //Verificar que la funcion velocidad en el primer parametro reciba variables declaradas o numeros enteros
        public string RA_Seis(DataGridView tabla)
        {
            string r = "";
            string identificador = "";           
            string[] variable;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Velocidad"))
                {
                    variable = tabla.Rows[i+1].Cells[1].Value.ToString().Split('(', ')',':');
                    identificador = variable[1];
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(identificador, out num);
                        if (isNumber)
                        {
                            r = "";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(identificador))
                                {
                                    r = "";                              
                                    a = tabla.RowCount;                                   
                                }                                                                          
                                else
                                {
                                    r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                    i = tabla.RowCount;
                                }
                                
                            }
                            else
                            {
                                r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }
                        }
                    }
                }
            }
            return r;
        }
        //Verificar que la funcion velocidad en el segundo parametro reciba variables declaradas o numeros enteros
        public string RA_Siete(DataGridView tabla)
        {
            string r = "";
            string identificador = "";          
            string[] variable;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Velocidad"))
                {
                    variable = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('(', ')', ':');
                    identificador = variable[2];
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(identificador, out num);
                        if (isNumber)
                        {
                            r = "";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(identificador))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                    i = tabla.RowCount;
                                }

                            }
                            else
                            {
                                r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }
                        }
                    }
                }
            }
            return r;
        }
        //Verificar que la funcion Giro.Izquierda/Derecha reciba un valor de tipo entero o una variable declarada en el primer parametro
        public string RA_Ocho(DataGridView tabla)
        {
            string r = "";
            string identificador = "";
            string[] variable;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Izquierda") || tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Derecha"))
                {
                    variable = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('(', ')', ',');
                    identificador = variable[1];
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(identificador, out num);
                        if (isNumber)
                        {
                            r = "";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(identificador))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                    i = tabla.RowCount;
                                }
                            }
                            else
                            {
                                r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }
                        }
                    }
                }
            }
            return r;
        }
        //Verificar que la funcion Giro.Izquierda/Derecha reciba un valor de tipo entero o una variable declarada en el segundo parametro
        public string RA_Nueve(DataGridView tabla)
        {
            string r = "";
            string identificador = "";
            string[] variable;
            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Izquierda") || tabla.Rows[i].Cells[1].Value.ToString().Equals("Giro.Derecha"))
                {
                    variable = tabla.Rows[i + 1].Cells[1].Value.ToString().Split('(', ')', ',');
                    identificador = variable[2];
                    for (int a = 0; a < tabla.RowCount; a++)
                    {
                        int num;
                        bool isNumber = int.TryParse(identificador, out num);
                        if (isNumber)
                        {
                            r = "";
                        }
                        else
                        {
                            if (tabla.Rows[a].Cells[1].Value.ToString().Equals("int"))
                            {
                                if (tabla.Rows[a + 1].Cells[1].Value.ToString().Contains(identificador))
                                {
                                    r = "";
                                    a = tabla.RowCount;
                                }
                                else
                                {
                                    r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                    i = tabla.RowCount;
                                }
                            }
                            else
                            {
                                r = "Error: Variable " + identificador + " no declarada. Declare la variable";
                                i = tabla.RowCount;
                            }
                        }
                    }
                }
            }
            return r;
        }
    }
}
