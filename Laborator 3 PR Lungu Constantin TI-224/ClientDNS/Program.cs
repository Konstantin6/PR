using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

class DNSClient
{
    private static string dnsServer = "8.8.8.8"; // Implicit Google DNS

    static void Main()
    {
        Console.WriteLine("DNS Client - Comenzi disponibile:");
        Console.WriteLine("  resolve <domain> - Afiseaza IP-urile asociate unui domeniu");
        Console.WriteLine("  resolve <ip> - Afiseaza domeniile asociate unui IP");
        Console.WriteLine("  use dns <ip> - Schimba serverul DNS utilizat");
        Console.WriteLine("  exit - Iesire din aplicatie");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Command: ");
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command format.");
                continue;
            }

            if (parts[0].Equals("resolve", StringComparison.OrdinalIgnoreCase))
            {
                Resolve(parts[1]);
            }
            else if (parts.Length == 3 && parts[0].Equals("use", StringComparison.OrdinalIgnoreCase) && parts[1].Equals("dns", StringComparison.OrdinalIgnoreCase))
            {
                ChangeDNSServer(parts[2]);
            }
            else
            {
                Console.WriteLine("Unknown command.");
            }
        }
    }

    static void Resolve(string input)
    {
        if (IPAddress.TryParse(input, out _))
        {
            ReverseLookup(input);
        }
        else
        {
            ForwardLookup(input);
        }
    }

    static void ForwardLookup(string domain)
    {
        try
        {
            var hostEntry = Dns.GetHostEntry(domain);
            Console.WriteLine($"IP addresses for {domain}:");
            foreach (var ip in hostEntry.AddressList)
            {
                Console.WriteLine(ip);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("DNS lookup failed.");
        }
    }

    static void ReverseLookup(string ipAddress)
    {
        try
        {
            var hostEntry = Dns.GetHostEntry(ipAddress);
            Console.WriteLine($"Domain names for {ipAddress}:");
            Console.WriteLine(string.Join(", ", hostEntry.Aliases.Concat(new[] { hostEntry.HostName })));
        }
        catch (Exception)
        {
            Console.WriteLine("Reverse DNS lookup failed.");
        }
    }

    static void ChangeDNSServer(string newDns)
    {
        if (IPAddress.TryParse(newDns, out _))
        {
            dnsServer = newDns;
            Console.WriteLine($"Using new DNS server: {dnsServer}");
        }
        else
        {
            Console.WriteLine("Invalid DNS server address.");
        }
    }
}