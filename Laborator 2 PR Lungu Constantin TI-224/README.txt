# UDP Chat - README

## Descriere
Această aplicație este un client de chat bazat pe protocolul UDP, care permite comunicarea în grup prin multicast și trimiterea de mesaje private între utilizatori.

## Caracteristici
- Trimitere și recepționare de mesaje prin multicast.
- Trimitere de mesaje private către utilizatori specifici.
- Posibilitatea de a părăsi chatul cu comanda `/exit`.

## Cerințe de sistem
- .NET SDK instalat (minim .NET 5.0).
- Conexiune la rețea.

## Instalare și compilare
1. Descărcați și extrageți arhiva cu codul sursă.
2. Deschideți un terminal/command prompt în directorul sursă.
3. Compilați aplicația folosind comanda:
   ```sh
   dotnet build
   ```
4. Navigați către folderul cu fișierul executabil:
   ```sh
   cd bin/Debug/net5.0/
   ```
5. Rulați aplicația:
   ```sh
   dotnet UDPChat.dll
   ```

## Utilizare
1. La pornire, introduceți un nume de utilizator.
2. Scrieți mesaje și apăsați `Enter` pentru a le trimite către toți utilizatorii conectați.
3. Pentru a trimite un mesaj privat, folosiți comanda:
   ```sh
   /privat [IP] [mesaj]
   ```
   Exemplu:
   ```sh
   /privat 192.168.1.10 Salut!
   ```
4. Pentru a ieși din chat, introduceți comanda:
   ```sh
   /exit
   ```

## Note
- Aplicația folosește multicast pe adresa `239.0.0.222` și portul `5000`.
- Mesajele private sunt trimise direct între utilizatori utilizând protocolul UDP.



