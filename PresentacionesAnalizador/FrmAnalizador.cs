using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntidadesAnalizador;
using ManejadoresAnalizador;
using System.IO;
using System.Diagnostics;
using System.IO.Ports;


namespace PresentacionesAnalizador
{
    public partial class FrmAnalizador : Form
    {
        int c = 0;
        string[] puertos;
        List<CamposDTG> campos;
        ManejadorAnalizador ma;
        ManejadorAnalizadorSintactico mas;
        ManejadorSemantico ms; 
        ManejadorTraduccion mt;
        public FrmAnalizador()
        {
            InitializeComponent();
            ma = new ManejadorAnalizador();
            ms = new ManejadorSemantico();
            mas = new ManejadorAnalizadorSintactico();
            campos = new List<CamposDTG>();
            mt = new ManejadorTraduccion();
            puertos = SerialPort.GetPortNames();
           
        }
       
        private void FrmAnalizador_Load(object sender, EventArgs e)
        {
            DtgContenido.Visible = false;
            for (int i = 0; i < puertos.Length; i++)
            {
                CmbPuertos.Items.Add(puertos[i].ToString());
            }
            BtnCompilar.Enabled = false;
            TxtTexto.Text = "Proceso ()\r\n<\r\n>";          
        }
        private void TxtTexto_TextChanged(object sender, EventArgs e)
        {
            c = 0;
            BtnCompilar.Enabled = true;
        }        
        private void BtnCompilar_Click(object sender, EventArgs e)
        {         
            if (AnalisisLexico()==true) //Por defecto se toma como: if(analisislexico()==TRUE)
            {
                for (int i = 0; i < DtgContenido.RowCount; i++)
                {              
                    if (DtgContenido.Rows[i].Cells[2].Value.ToString().Equals("No Identificado"))
                    {
                        LblLexico.Text = "Token no identificado: "+ '\u0022'+DtgContenido.Rows[i].Cells[1].Value.ToString()+'\u0022'+ " remuevalo.";
                        BtnCompilar.Enabled = false;
                        i = DtgContenido.RowCount;
                    }
                    else
                    {
                        AnalisisSintactico();
                        if (AnalisisSintactico() == true)
                        {
                            AnalisisSemantico();
                            if(AnalisisSemantico() == true)
                            {
                               
                                Traducir();
                                if (Traducir().Length > 0)
                                {
                                  
                                    if (c == 0)
                                    {
                                        if (CmbPlaca.SelectedItem == null && CmbPuertos.SelectedItem == null)
                                        {
                                            LblLexico.Text = "Compilacion correcta:\nSeleccione un puerto y una placa para proseguir con la carga";
                                            c = 0;
                                        }
                                        else
                                        {
                                            c = 1;
                                            Cargar(CmbPlaca.SelectedItem.ToString(), CmbPuertos.SelectedItem.ToString(), Traducir());                            
                                        }
                                    }
                                    if(c>0)
                                    {
                                        LblLexico.Text = "Compilado y cargado correctamente";
                                    }                                   
                                }
                            }
                        }
                    }
                }       
            }         
        }
        public void Cargar(string placa, string puerto, string traduccion)
        {
            try
            {                         
                    StreamWriter escrito = File.CreateText("traduccion.ino");
                    String contenido = traduccion;
                    escrito.WriteLine(contenido.ToString());
                    escrito.Flush();
                    escrito.Close();
                    StreamWriter sw = File.CreateText("cargar.bat");
                    sw.WriteLine("arduinouploader traduccion.ino " + placa + puerto);
                    sw.Close();
                    Process.Start("cargar.bat");
                    MessageBox.Show("Archivo cargado correctamente");            
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el archivo");
            }
        }

        //Metodos de Analisis
        public bool AnalisisLexico()
        {
            campos.Clear();
            campos = ma.SepararRecursivo(TxtTexto, DtgContenido, 0);
            //campos = ma.Separar(TxtTexto, DtgContenido);
            if (DtgContenido.RowCount > 0)
            {
                BtnCompilar.Enabled = true;
                return true;
            }
            else
            {
                BtnCompilar.Enabled = false;
                return false;
            }
        }
        public bool AnalisisSintactico()
        {
            string r = mas.AnalizadorSintactico(DtgContenido);
            if (r == "No se encontraron errores")
            {
                LblLexico.Text = r;
                BtnCompilar.Enabled = true;
                return true;
            }
            else
            {
                LblLexico.Text = r;
                BtnCompilar.Enabled = false;
                return false;
            }
        }
        public bool AnalisisSemantico()
        {
            string r = ms.Semantico(DtgContenido);
            if (r == "No se encontraron errores")
            {
                BtnCompilar.Enabled = true;
                LblLexico.Text = r;
                return true;
            }
            else
            {
                LblLexico.Text = r;
                BtnCompilar.Enabled = false;
                return false;
            }
        }
        public string Traducir()
        {
            string traduccion = mt.TransBuilder(DtgContenido);
            return traduccion;
        }
    } 
}
