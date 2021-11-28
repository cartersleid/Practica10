using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace Practica1_Sist.Sensado_y_Monitoreo
{
    public partial class Form1 : Form
    {
        SerialPort puertos;

        public Form1()
        {
            InitializeComponent();
            this.puertos = new SerialPort();
            puertos.DataReceived += Puertos_DataReceived;
        }
        private void Puertos_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                SerialPort DatosSerial = (SerialPort)sender;
                tbDatos.Text = DatosSerial.ReadLine();
                String[] Cadena;
                Cadena = tbDatos.Text.Split('/');
                tbDist.Text = Cadena[3];
                tbTemp.Text = Cadena[1];
                tbH.Text = Cadena[2];
                tbAct.Text = Cadena[4];
            }
            catch { }           
        }
        private void cmbPuertos_DropDown(object sender, EventArgs e)
        {
            cbPuertos.Items.Clear();
            String[] PuertosDisponibles = SerialPort.GetPortNames();
            foreach (string puerto in PuertosDisponibles)
            {
                cbPuertos.Items.Clear();
                cbPuertos.Items.Add(puerto);
            }
        }
        private void btConectar_Click(object sender, EventArgs e)
        {
            if(btConectar.Text=="Conectar")
            {
                puertos.BaudRate = 9600;
                try
                {
                    puertos.PortName = cbPuertos.SelectedItem.ToString();
                }
                catch
                {
                    MessageBox.Show("Seleccione un puerto, por favor","Error",MessageBoxButtons.OK);
                }
                if (!puertos.IsOpen)
                {
                    try
                    {
                        puertos.Open();
                        btConectar.Text = "Desconectar";
                    }
                    catch { }
                }                
            }
            else if (btConectar.Text == "Desconectar")
            {
                if (puertos.IsOpen)
                {
                    puertos.Close();
                    btConectar.Text = "Conectar";
                    puertos.Dispose();
                    tbDatos.Clear();
                }
            }
        }
    }
}