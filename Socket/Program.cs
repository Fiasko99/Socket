using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Объявление констант
            const string ip = "127.0.0.1";
            const int port = 8080;
            #endregion

            #region Инициилизация конечного порта и сокета на TCP соединении
            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion

            #region Инициализация сокета и константной очереди
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);
            #endregion

            #region Прослушивание сокета в цикле
            while (true)
            {
                Socket listener = tcpSocket.Accept();

                #region Инициализация буфера и сборщика строки
                byte[] buffer = new byte[256];
                int bufferSize = 0;
                StringBuilder data = new StringBuilder();
                #endregion

                do
                {
                    #region Определение размера сообщения и его декодирование с последующим добавлением в переменную данных
                    bufferSize = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, bufferSize));
                    #endregion

                } while (listener.Available > 0);

                Console.WriteLine(data.ToString());

                listener.Send(Encoding.UTF8.GetBytes("Hi from socket"));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            #endregion
        }
    }
}
