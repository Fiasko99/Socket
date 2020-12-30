using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Объявление констант
            const string ip = "127.0.0.1";
            const int port = 8080;
            #endregion

            #region Инициализация конечного порта и сокета на TCP соединении
            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion

            #region Инициализация сообщения и его битового массива
            Console.Write("Введите ваше сообщение: ");
            string msg = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            #endregion

            tcpSocket.Connect(tcpEndPoint); // Подключение к сокетам

            tcpSocket.Send(data); // Отправка данных

            #region Инициализация буфера и сборщика строки для получения ответа
            byte[] buffer = new byte[256];
            int bufferSize = 0;
            StringBuilder res = new StringBuilder();
            #endregion

            do {
                bufferSize = tcpSocket.Receive(buffer);
                res.Append(Encoding.UTF8.GetString(buffer, 0, bufferSize));
            } while (tcpSocket.Available > 0);

            Console.WriteLine(res.ToString());

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Console.ReadLine();
        }
    }
}
