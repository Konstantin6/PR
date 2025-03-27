using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatClient
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Trying to connect to server...");
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 5000);
            Console.WriteLine("Connected to server!");

            NetworkStream stream = client.GetStream();

            Thread receiveThread = new Thread(() => ReceiveMessages(stream));
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                if (string.IsNullOrEmpty(message)) continue;

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not connect to server: " + ex.Message);
        }
    }

    static void ReceiveMessages(NetworkStream stream)
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("\nMessage from server: " + message);
            }
            catch
            {
                Console.WriteLine("Disconnected from server.");
                break;
            }
        }
    }
}
