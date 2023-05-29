using System;
using System.IO.Ports;      // v4.7.0 -- do not update!
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace MOCU
{
    public static class CedrusHandler
    {
        private static SerialPort _serialPort;
        private static bool _killTask = false;
        public static Thread connectionMonitoring;

        private static void Init()
        {
            _serialPort = new SerialPort("COM3", baudRate: 9600, Parity.None, dataBits: 8, StopBits.One);   // 9600 is just default
            //_serialPort.DataReceived += GetAnswer;      // function called when received data
            _serialPort.Open();
        }

        public static void GetAnswer()  // object sender, SerialDataReceivedEventArgs e
        {
            // WTF is going on here?
            //_serialPort.BaseStream.Flush();

            byte[] value = new byte[1];

            GUI.textboxesDictionary["INFO"].Text += $"{_serialPort.Read(value, 0, 1)} - ";
            GUI.textboxesDictionary["INFO"].Text += $"{value[0]} \r\n";
        }

        private static void ConnectionMonitoring ()
        {
            while(!_killTask)
            {
                if (!_serialPort.IsOpen)
                {
                    try
                    {
                        Init();

                        GUI.statusesDictionary["CEDRUS"].BackColor = GUI.statusesColorsDictionary["GOOD"];
                    } catch (Exception ex)
                    {
                        GUI.statusesDictionary["CEDRUS"].BackColor = GUI.statusesColorsDictionary["ERROR"];
                    }
                }

                Thread.Sleep(300);      // every 300 ms check status of Cedrus device
            }
        }

        public static void Connect ()
        {
            try
            {
                Init();
            }
            catch (Exception error)
            {
                //GUI.textboxesDictionary["WARNINGS"].Text = error.ToString();
                throw new Exception("Error in Cedrus connection");
            }
            finally
            {
                // anyway start cedrus monitoring
                connectionMonitoring = new Thread(new ThreadStart(ConnectionMonitoring));
                connectionMonitoring.Start();
            }
        }

        public static void Disconnect()
        {
            _killTask = true;   // sleep(300)
            _serialPort.Close();
            //connectionMonitoring.Abort();
        }
    }
}
