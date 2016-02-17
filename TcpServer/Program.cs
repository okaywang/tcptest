using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Listen(IPAddress.Any, 9731);
        }

        static void Listen(IPAddress ip, int port)
        {
            var listener = new TcpListener(new IPEndPoint(ip, port));
            listener.Start();

            using (var client = listener.AcceptTcpClient())
            {
                using (var stream = client.GetStream())
                {
                    while (true)
                    {
                        var buffer = new byte[256];
                        var count = stream.Read(buffer, 0, buffer.Length);

                        var data = new byte[count];
                        Array.Copy(buffer, data, data.Length);
                        var text = System.Text.Encoding.Default.GetString(data);
                        var result = text.ToUpper();

                        var resultBytes = System.Text.Encoding.Default.GetBytes(result);
                        stream.Write(resultBytes, 0, resultBytes.Length);
                    }
                }
            }
        }
    }
}
