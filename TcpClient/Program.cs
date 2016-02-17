using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    class Program
    {

        static void Main(string[] args)
        {
            var client = new System.Net.Sockets.TcpClient();
            client.Connect("192.168.7.227", 9731);

            while (true)
            {
                var line = Console.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    Send(client, line);
                }
            }
        }

        static void Send(System.Net.Sockets.TcpClient client, string text)
        {
            var stream = client.GetStream();
            var bytes = System.Text.Encoding.Default.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);


            var buffer = new byte[256];
            var count = stream.Read(buffer, 0, buffer.Length);
            var data = new byte[count];
            Array.Copy(buffer, data, data.Length);
            var result = System.Text.Encoding.Default.GetString(data);
            Console.WriteLine(result);
        }
    }
}
