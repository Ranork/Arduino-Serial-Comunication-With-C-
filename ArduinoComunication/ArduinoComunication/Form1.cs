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

namespace ArduinoComunication
{
    public partial class Form1 : Form
    {
        SerialPort serialPort1 = new SerialPort();
        public Form1()
        {
            InitializeComponent();

            
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600;
            //serialPort1.Open();
        }

        int baslangic;
        int bitis;
        int artis;
        int gunceldeger=0;


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) { serialPort1.Close(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baslangic = Convert.ToInt32(textBox5.Text);
            artis = Convert.ToInt32(textBox6.Text);
            bitis = Convert.ToInt32(textBox7.Text);

            gunceldeger = baslangic;
            sayac.Text = "00"+gunceldeger.ToString()+".";


            serialPort1.PortName = textBox1.Text;
            serialPort1.BaudRate = Convert.ToInt32(textBox2.Text);

            timer2.Enabled = true;

            try
            {
                serialPort1.Open();
                serialPort1.Write(sayac.Text);
                serialPort1.Close();
            }
            catch
            {
                MessageBox.Show("Bağlantı noktasına mesaj gönderimi ile ilgili bir sorunla karşılaşıldı. Seri Port Ekranı açık ise kapatınız!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Open();

            int dataLength = serialPort1.BytesToRead;
            byte[] data = new byte[dataLength];
            int nbrDataRead = serialPort1.Read(data, 0, dataLength);
            if (nbrDataRead == 0)
            {
                serialPort1.Close();
                return;
            }
                

            sayac.Text = sayac.Text + Environment.NewLine + serialPort1.Read(data,0,dataLength);

            serialPort1.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (gunceldeger + artis <= bitis)
            {
                gunceldeger = gunceldeger + artis;
                if (gunceldeger < 10)
                {
                    sayac.Text = "00"+gunceldeger.ToString() + ".";
                }
                else if (gunceldeger < 100)
                {
                    sayac.Text = "0" + gunceldeger.ToString() + ".";
                }
                else
                {
                    sayac.Text = gunceldeger.ToString() + "."; 
                }
                

                serialPort1.Open();
                serialPort1.Write(sayac.Text);
                serialPort1.Close();
            }
            else
            {
                timer2.Enabled = false;
            }
            
        }
    }
}
