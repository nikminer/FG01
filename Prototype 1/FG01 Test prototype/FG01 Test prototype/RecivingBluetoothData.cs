using System;
using System.IO;
namespace FG01_Test_prototype
{
    class RecivingBluetoothData
    {
        public static string Recive(Stream stream, ref bool isConnected)
        {
            string s = string.Empty;
            do
            {
                int i = stream.ReadByte();
                if (i == 10)
                    break;
                else if (i == -1)
                {
                    isConnected = false;
                    stream.Close();
                    return ";";
                }
                else
                    s += Convert.ToChar(i);
            } while (true);
            return s.Replace('\r', ' ');
        }
    }
}
