using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatServer
{
    static List<TcpClient> clients = new List<TcpClient>();
    static TcpListener server;

    static void Main()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Server started on port 5000");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                lock (clients) clients.Add(client);
                Console.WriteLine("Client connected!");

                Thread thread = new Thread(HandleClient);
                thread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Server error: " + ex.Message);
        }
    }

    static void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        try
        {
            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break; // Client disconnected

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + message);
                BroadcastMessage(message, client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client error: " + ex.Message);
        }
        finally
        {
            lock (clients) clients.Remove(client);
            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }

    static void BroadcastMessage(string message, TcpClient sender)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        lock (clients)
        {
            foreach (TcpClient client in clients)
            {
                if (client != sender) // Nu trimite mesajul expeditorului
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                    }

                    catch
                    {
                        // Client deconectat
                    }
                }
            }
        }
    }
}
