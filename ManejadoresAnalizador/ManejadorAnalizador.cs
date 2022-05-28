using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAnalizador;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using System.Text.RegularExpressions;

namespace ManejadoresAnalizador
{
    public class ManejadorAnalizador
    {
        Match match;
        
        
        List<CamposDTG> campos = new List<CamposDTG>();
        string[] tokens; 

        /*public List<CamposDTG> Separar(TextBox Texto, DataGridView data) //7n+L(2n)
        {
            string[] lineas = Regex.Split(Texto.Text, "\r\n"); //n
            List<CamposDTG> campos = new List<CamposDTG>(); //n
            tokens = Texto.Text.Split('\n',' '); //n
            for (int i = 0; i < tokens.Length; i++) //L=tokens.length
            {               
                if (tokens[i].Contains(' ')) //N=1
                {
                    campos.Add(new CamposDTG(i+1 , tokens[i], WhatIs(tokens[i]), i+1)); //n
                }
                else 
                {
                    campos.Add(new CamposDTG(i+1 , tokens[i], WhatIs(tokens[i]), i));  //n    
                }
            }

            data.DataSource = campos.ToList(); //n
            data.AutoResizeColumns(); //n
            data.AutoResizeColumns(); //n
            return campos; //n
            
        }*/
        public List<CamposDTG> SepararRecursivo(TextBox Texto, DataGridView data, int indice) //12n
        {
            //string[] lineas = Regex.Split(Texto.Text, "\r\n"); //n
            tokens = Texto.Text.Split('\n', ' '); //n        
            if(indice < tokens.Length)
            {
                if (tokens[indice].Contains(' ')) //N=6
                {
                    campos.Add(new CamposDTG(indice + 1, tokens[indice], WhatIs(tokens[indice]), indice + 1)); //n                
                    indice++;//n
                    return SepararRecursivo(Texto, data, indice);//n
                }
                else
                {
                    campos.Add(new CamposDTG(indice + 1, tokens[indice], WhatIs(tokens[indice]), indice));//n                
                    indice++;//n
                    return SepararRecursivo(Texto, data, indice);//n
                }
            }
            data.DataSource = campos.ToList(); //n
            data.AutoResizeColumns(); //n
            data.AutoResizeColumns(); //n
            return campos; //n

        }
     
        public string WhatIs(string Texto) //14n
        {
            string es = "No Identificado"; //n
            /*if (IsReserved(Texto) == true) //N=1
            {
                es = "Palabra Reservada (Tipo de dato o Instruccion)"; //n
            }*/
            if (IsReservedRecursivo(Texto,0) == true) //N=1
            {
                es = "Palabra Reservada (Tipo de dato o Instruccion)"; //n
            }
            if (IsAsigned(Texto) == true)
            {
                es = "Expresion de asignacion"; //n
            }
            if (IsDelimiterOpen(Texto) == true)
            {
                es = "Delimitador de apertura"; //n
            }
            if (IsDelimiterClose(Texto) == true)
            {
                es = "Delimitador de cierre"; //n
            }
            if(IsCommentary(Texto)==true)
            {
                es = "Comentario"; //n
            }
            if(IsCondition(Texto)==true)
            {
                es = "Expresion de condicion"; //n
            }
            if(IsConditionMinusThan(Texto)==true)
            {
                es = "Expresion de condicion"; //n
            }
            if(IsInstructionParameter(Texto)==true)
            {
                es = "Parametros de instruccion"; //n
            }
            if(IsDirection(Texto)==true)
            {
                es = "Parametro Direccion de Giro"; //n               
            }
            if(IsPlusOperator(Texto)==true)
            {
                es = "Incremento de variable"; //n
            }
            if (IsRemainOperator(Texto) == true)
            {
                es = "Decremento de variable"; //n
            }
            if(IsVelocity(Texto)==true)
            {
                es = "Parametros de Velocidad"; //n
            }
            if (IsSpace(Texto) == true)
            {
                es = "Espaciado";
            }
            return es; //n
        }
        public string ForRecursivo(int indice, int limite)
        {
            string algo = "";
            if (indice < limite)
            {

                indice++;
                return ForRecursivo(indice, limite);
            }
            return algo;
        }
        public bool IsReservedRecursivo(string Texto, int indice) //7n
        {
            bool estado = false; //n
            string[] reservadas = { "int", "Encender", "si", "Proceso", "Retardo", "Giro.Derecha", "Giro.Izquierda", "Velocidad" };  //n
            if (indice < reservadas.Length) //N =1             
            {
                if (Texto.Contains(reservadas[indice])) //N=3
                {
                    estado = true;  //n                    
                }
                else  
                {      
                   indice++;//n
                   return IsReservedRecursivo(Texto, indice);//n
                }          
            }
            return estado; //n
        }
        /*public bool IsReserved(string Texto) //3n+L(n)
        {
            string[] reservadas = { "int", "Encender", "si", "Proceso", "Retardo", "Giro.Derecha", "Giro.Izquierda", "Velocidad" }; //n
            bool estado = false; //n
            for (int i = 0; i < reservadas.Length; i++) //L = reservadas.length
            {
                if (Texto.Contains(reservadas[i])) //N=1
                {
                    estado = true; //n
                }
            }
            return estado; //n
        }*/
        public bool IsSpace(string Texto) //4n, en total metodos de aqui para abajo 64n
        {
            bool estado = false;
            if (string.IsNullOrWhiteSpace(Texto))
            {
                estado = true;
            }
            return estado;
        }
        
        //los metodos de aqui para abajo cada uno vale 5n y juntos suman la cantidad de 60n
        public bool IsVelocity(string Texto)//4n+n = 5n
        {
            string velocidad = @"\050\w*\d*\072\w*\d*\051"; //n
            bool estado = false; //n
            match = Regex.Match(Texto, velocidad); //n
            if (match.Success) //N=2
            {
                estado = true; //n
            }
            return estado; //n
        }
        public bool IsDirection(string Texto) //4n+n = 5n
        {
            string Direccion = @"\050\w*\d*\054\w*\d*\051"; //n
            bool estado = false; //n
            match = Regex.Match(Texto, Direccion); //n
            if(match.Success) //N=1
            {
                estado = true; //n
            }
            return estado; //n
        }     
        //bueno aqui me di cuenta que todos mis identificadores tienen la misma estructura, asi que todos valen 5n, por lo tanto,
        //12 metodos por 5n, son igual a 55n
        public bool IsInstructionParameter(string Texto)
        { 
            string Instruccion = @"\050\b[a-zA-Z]\w*\051|\050\b[0-9]\d*\051|\050\051";
            bool estado = false;
            match = Regex.Match(Texto, Instruccion);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsAsigned(string Texto)
        {
            string Asignacion = @"\w=\b[0-9]\d*;";
            bool estado = false;
            match = Regex.Match(Texto, Asignacion);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsDelimiterOpen(string Texto)
        {
            string DelimitadorOpen = @"<";
            bool estado = false;
            match = Regex.Match(Texto, DelimitadorOpen);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsDelimiterClose(string Texto)
        {
            string DelimitadorClose = @">";
            bool estado = false;
            match = Regex.Match(Texto, DelimitadorClose);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsCommentary(string Texto)
        {
            string comment = @"!!([a-zA-Z0-9]*)_.*";
            bool estado = false;
            match = Regex.Match(Texto, comment);
             if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsCondition(string Texto)
        { 
            string biggerthan = @"\050\w*>\b[0-9]\d*\051|\050\w*>=\b[0-9]\d*\051|\050\w*<=\b[0-9]\d*\051|\050\w*==\b[0-9]\d*\051|\050\w*\074\b[0-9]\d*\051";
            bool estado = false;
            match = Regex.Match(Texto, biggerthan);
            if(match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsConditionMinusThan(string Texto)
        {
            string EqualTo = @"\050\w*<\b[0-9]\d*\051";
            bool estado = false;
            match = Regex.Match(Texto, EqualTo);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsPlusOperator(string Texto)
        {
            string operador = @"\b[a-zA-Z]\w*\053\053";
            bool estado = false;
            match = Regex.Match(Texto, operador);
            if(match.Success)
            {
                estado = true;
            }
            return estado;
        }
        public bool IsRemainOperator(string Texto)
        {
            string operador = @"\b[a-zA-Z]\w*\055\055";
            bool estado = false;
            match = Regex.Match(Texto, operador);
            if (match.Success)
            {
                estado = true;
            }
            return estado;
        }
    }
}
