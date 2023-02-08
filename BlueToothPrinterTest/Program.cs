using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO.Ports;

namespace BlueToothPrinterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SerialPort port = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One))
            {
                port.Open();
                 
                port.Write(new byte[] { 0x1b, 0x40 }, 0, 2); 
                port.Write(new byte[] { 0x1b, 0x61 }, 0, 2);
                port.Write(new byte[] { 0x01 }, 0, 1);
                port.Write(new byte[] {  0x1f, 0x11, 0x02, 0x04}, 0, 4);


                port.Write(new byte[] { 0x1d, 0x76, 0x30 }, 0, 3);
                port.Write(new byte[] { 0x00 }, 0, 1);
                port.Write(new byte[] { 0x30, 0x00 }, 0, 2);
                port.Write(new byte[] { 0xff, 0x00 }, 0, 2);

                int expectedDataLength = 12240;
                byte[] data = System.IO.File.ReadAllBytes(@"C:\Users\brien\OneDrive\Desktop\cat_low_res.bmp");

                int portionToSkip = data.Length - expectedDataLength;

                port.Write(data, portionToSkip, expectedDataLength);

               /* for (int row = 0; row < 255; row += 1)
                {
                    for (int col = 0; col < 48; col += 1)
                    {
                        port.Write(new byte[] { 0x5A }, 0, 1);
                    }
                }*/

                port.Write(new byte[] { 0x1b, 0x64  }, 0, 2);
                port.Write(new byte[] { 0x02 }, 0, 1);
                port.Write(new byte[] { 0x1b, 0x64 }, 0, 2);
                port.Write(new byte[] { 0x02 }, 0, 1);

                port.Write(new byte[] { 0x1f, 0x11, 0x08 }, 0, 3);
                port.Write(new byte[] { 0x1f, 0x11, 0x0E }, 0, 3);
                port.Write(new byte[] { 0x1f, 0x11, 0x07 }, 0, 3);
                port.Write(new byte[] { 0x1f, 0x11, 0x09 }, 0, 3); 

                port.Close();
            }
        }
    }
}
