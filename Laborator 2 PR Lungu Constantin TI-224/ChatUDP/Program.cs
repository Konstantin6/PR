using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private const int GeneralPort = 5000;
    private const string MulticastAddress = "239.0.0.222";

    private static UdpClient multicastClient;
    private static UdpClient privateClient;
    private static IPEndPoint multicastEndPoint;

    static async Task Main(string[] args)
    {
        Console.WriteLine("Introduceți numele de utilizator:");
        string username = Console.ReadLine();

        // Inițializare endpoint multicast
        multicastEndPoint = new IPEndPoint(IPAddress.Parse(MulticastAddress), GeneralPort);

        // Configurare UDP pentru multicast
        multicastClient = new UdpClient();
        multicastClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        multicastClient.Client.Bind(new IPEndPoint(IPAddress.Any, GeneralPort));
        multicastClient.JoinMulticastGroup(IPAddress.Parse(MulticastAddress));

        // Configurare UDP pentru mesaje private cu ReuseAddress
        privateClient = new UdpClient();
        privateClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        privateClient.Client.Bind(new IPEndPoint(IPAddress.Any, GeneralPort));

        Console.WriteLine("Aplicația de chat a pornit.");
        Console.WriteLine("Scrieți un mesaj și apăsați Enter pentru a trimite în canalul general.");
        Console.WriteLine("Pentru a trimite un mesaj privat, utilizați formatul: /privat [IP] [mesaj]");
        Console.WriteLine("Pentru a ieși, scrieți /exit");

        // Pornire ascultare mesaje
        Task receiveMulticastTask = ReceiveMulticastMessagesAsync();
        Task receivePrivateTask = ReceivePrivateMessagesAsync();

        while (true)
        {
            string message = Console.ReadLine();
            if (message.Equals("/exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            else if (message.StartsWith("/privat "))
            {
                string[] parts = message.Split(' ', 3);
                if (parts.Length == 3 && IPAddress.TryParse(parts[1], out IPAddress targetIp))
                {
                    string privateMessage = parts[2];
                    await SendMessageAsync(privateMessage, targetIp.ToString(), GeneralPort, username, isPrivate: true);
                }
                else
                {
                    Console.WriteLine("Format incorect. Utilizați: /privat [IP] [mesaj]");
                }
            }
            else
            {
                await SendMessageAsync(message, MulticastAddress, GeneralPort, username);
            }
        }

        multicastClient.DropMulticastGroup(IPAddress.Parse(MulticastAddress));
        multicastClient.Close();
        privateClient.Close();
    }

    private static async Task SendMessageAsync(string message, string ipAddress, int port, string username, bool isPrivate = false)
    {
        try
        {
            string fullMessage = isPrivate ? $"[Privat de la {username}]: {message}" : $"[{username}]: {message}";
            byte[] data = Encoding.UTF8.GetBytes(fullMessage);
            using (UdpClient senderClient = new UdpClient())
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                await senderClient.SendAsync(data, data.Length, endPoint);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la trimiterea mesajului: {ex.Message}");
        }
    }

    private static async Task ReceiveMulticastMessagesAsync()
    {
        try
        {
            while (true)
            {
                UdpReceiveResult result = await multicastClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                Console.WriteLine(message);
            }
        }
        catch (ObjectDisposedException)
        {
            // Socket-ul a fost închis, ieșim din metodă
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la primirea mesajului multicast: {ex.Message}");
        }
    }

    private static async Task ReceivePrivateMessagesAsync()
    {
        try
        {
            while (true)
            {
                UdpReceiveResult result = await privateClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                Console.WriteLine(message);
            }
        }
        catch (ObjectDisposedException)
        {
            // Socket-ul a fost închis, ieșim din metodă
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la primirea mesajului privat: {ex.Message}");
        }
    }
}
