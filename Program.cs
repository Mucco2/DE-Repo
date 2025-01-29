using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Velkomstbesked og forklaring af spillet
        Console.WriteLine("====================================");
        Console.WriteLine("   Velkommen til Dungeon Escape!");
        Console.WriteLine("====================================");
        Console.WriteLine("Find nøglen (N) og gå til udgangen (U) for at vinde.");
        Console.WriteLine("Men pas på! Hvis du laver for mange forkerte træk, falder du i en fælde!");
        Console.WriteLine("Tryk på en tast for at starte spillet...");
        Console.ReadKey();

        // Udvidet labyrint layout
        char[,] labyrint = {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', 'N', '#' },
            { '#', ' ', '#', ' ', '#', ' ', '#', '#', '#', '#', '#' },
            { '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', '#', '#', ' ', '#', '#', '#', ' ', '#', ' ', '#' },
            { '#', 'S', ' ', ' ', ' ', ' ', '#', ' ', ' ', 'U', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
        };

        // Spillerens startposition og variabler
        int spillerX = 5, spillerY = 1;
        bool hasKey = false;
        int wrongMoves = 0;
        const int maxWrongMoves = 5;

        Console.Clear();

        while (true) {
            // Tegn labyrinten
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("   Dungeon Escape - Spil");
            Console.WriteLine("====================================");
            for (int i = 0; i < labyrint.GetLength(0); i++) {
                for (int j = 0; j < labyrint.GetLength(1); j++) {
                    if (i == spillerX && j == spillerY)
                        Console.Write('P'); // Spillerens position
                    else
                        Console.Write(labyrint[i, j]);
                }
                Console.WriteLine();
            }
            // Spil instruktioner og status beskeder 
            Console.WriteLine("\nFind nøglen (N) og gå til udgangen (U) for at vinde.");
            Console.WriteLine("Men pas på! Hvis du laver for mange forkerte træk, falder du i en fælde!");
            Console.WriteLine($"Forkerte træk: {wrongMoves}/{maxWrongMoves}");
            Console.WriteLine("\nBrug W (op), A (venstre), S (ned), D (højre) til at bevæge dig.");
            Console.Write("Indtast dit valg: ");
            // Læs spillerens input og flyt spilleren i labyrinten
            char move = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
            // Beregn ny position
            int newX = spillerX;
            int newY = spillerY;
            // Flyt spilleren
            switch (move) {
                case 'w': newX--; break;
                case 'a': newY--; break;
                case 's': newX++; break;
                case 'd': newY++; break;
                default:
                    Console.WriteLine("Ugyldigt input! Brug kun W, A, S eller D.");
                    wrongMoves++;
                    Thread.Sleep(1000);
                    continue;
            }

            // Kontroller gyldighed af træk
            if (newX >= 0 && newX < labyrint.GetLength(0) && newY >= 0 && newY < labyrint.GetLength(1)) {
                if (labyrint[newX, newY] == '#') {
                    Console.WriteLine("Du kan ikke gå igennem vægge!");
                    wrongMoves++;
                }
                else if (labyrint[newX, newY] == 'N') {
                    hasKey = true;
                    labyrint[newX, newY] = ' ';
                    spillerX = newX;
                    spillerY = newY;
                    Console.WriteLine("Du har samlet nøglen!");
                }
                else if (labyrint[newX, newY] == 'U') {
                    if (hasKey) {
                        Console.WriteLine("Tillykke! Du har vundet spillet!");
                        break;
                    }
                    else {
                        Console.WriteLine("Du skal finde nøglen først!");
                        wrongMoves++;
                    }
                }
                else {
                    spillerX = newX;
                    spillerY = newY;
                }
            }
            else {
                Console.WriteLine("Ugyldigt træk! Prøv igen.");
                wrongMoves++;
            }

            // Tjek om spilleren har lavet for mange forkerte træk
            if (wrongMoves >= maxWrongMoves) {
                Console.WriteLine("Du har lavet for mange forkerte træk! Du er faldet i en fælde!");
                Console.WriteLine("Game Over!");
                break;
            }

            Thread.Sleep(1500); // Gør det lettere at følge med
        }
    }
}
