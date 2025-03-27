# Aplicație de tip chat utilizând TCP

## Descriere

Aceasta este o aplicație client-server scrisă în C# care permite mai multor clienți să comunice între ei prin intermediul unui server central. Serverul gestionează conexiunile și retransmite mesajele primite către toți clienții conectați.

## Cerințe

- .NET SDK (minim .NET 6.0)
- Visual Studio 2022 sau un alt mediu de dezvoltare compatibil cu C#

## Structura Proiectului

```
|-- Server
|   |-- Program.cs
|
|-- Client
|   |-- Program.cs
|
|-- README.md
```

## Cum să compilezi și să rulezi aplicația

### 1. Descărcare

Salvează arhiva cu proiectul și extrage fișierele într-un director.

### 2. Compilare și rulare a Serverului

1. Deschide un terminal și navighează în directorul `Server`.
2. Rulează comanda:
   ```sh
   dotnet run
   ```
3. Serverul va porni și va asculta conexiuni pe portul 5000.

### 3. Compilare și rulare a Clientului

1. Deschide un alt terminal și navighează în directorul `Client`.
2. Rulează comanda:
   ```sh
   dotnet run
   ```
3. Clientul se va conecta la server și va putea trimite mesaje.

### 4. Testare cu mai mulți clienți

Pentru a testa mai mulți clienți simultan:

- Rulează mai multe instanțe ale clientului folosind aceeași comandă `dotnet run` într-un alt terminal.
- Trimite mesaje și observă cum acestea sunt retransmise tuturor clienților conectați.

## Închidere aplicație

- Serverul poate fi oprit apăsând `Ctrl + C` în terminal.
- Clientul poate fi închis tastând `exit` sau închizând fereastra terminalului.

## Probleme și depanare

- **Eroare de port ocupat**: Dacă serverul nu pornește, asigură-te că portul 5000 nu este folosit de altă aplicație.
- **Clientul nu se conectează**: Verifică dacă serverul rulează înainte de a lansa clientul.

---

