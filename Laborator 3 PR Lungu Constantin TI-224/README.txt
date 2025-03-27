# README - DNS Client

## Descriere
Aceasta este o aplicație simplă de tip client DNS scrisă în C#. Aplicația permite utilizatorului să efectueze interogări DNS pentru a rezolva domenii în adrese IP și invers, precum și să schimbe serverul DNS utilizat.

## Cerințe
- .NET SDK (minim versiunea 6.0)
- Conexiune la internet

## Compilare și rulare

1. **Compilare:**
   
   Deschide un terminal în directorul unde se află fișierul sursă `DNSClient.cs` și rulează comanda:
   ```sh
   dotnet build
   ```
   Aceasta va genera fișierul executabil în directorul `bin/Debug/net6.0/`.

2. **Rulare:**
   
   După compilare, execută aplicația folosind comanda:
   ```sh
   dotnet run
   ```
   Sau, dacă dorești să rulezi direct fișierul executabil:
   ```sh
   ./bin/Debug/net6.0/DNSClient
   ```

## Utilizare

După rularea aplicației, utilizatorul poate introduce următoarele comenzi:

- `resolve <domeniu>` - Afișează adresele IP asociate unui domeniu.
- `resolve <ip>` - Afișează domeniile asociate unui IP.
- `use dns <ip>` - Schimbă serverul DNS utilizat.
- `exit` - Închide aplicația.

Exemple:
```
Command: resolve google.com
IP addresses for google.com:
142.250.185.78

Command: resolve 8.8.8.8
Domain names for 8.8.8.8:
dns.google

Command: use dns 1.1.1.1
Using new DNS server: 1.1.1.1
```

