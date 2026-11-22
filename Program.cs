using System;
using System.IO;

// Munkamegosztás:
//   1. rész (Papp József Balázs): képernyő, panelek, kurzormozgatás, szóköz, menü, Ugrás középre.
//   2. rész (Moldovan Sebastian): Pont, Nyomda, Betolt, minta.txt, Cella törlése.
// A class és a Main csak a C# által megkövetelt keret, objektumorientált megoldás nincs benne.
class Program
{
    // ---- Elrendezés (konzoloszlopok és -sorok) ----
    const int SZEL = 60;      // rajzlap szélessége cellában
    const int MAG = 20;       // rajzlap magassága cellában
    const int BAL = 25;       // a rajzlap első konzoloszlopa (konzolpozíció X = BAL + x)
    const int FENT = 1;       // a rajzlap első konzolsora (konzolpozíció Y = FENT + y)
    const int PANEL = 24;     // a bal oldali panel szélessége

    // ---- Panelsorok ----
    const int MENUSOR = 2;    // itt kezdődik a menülista
    const int ALLAPOTSOR = 11;
    const int UZENETSOR = 16;
    const int SUGOSOR = 18;

    // ---- Rajzlap tartalma ----
    static char[,] rajz = new char[MAG, SZEL];
    static ConsoleColor[,] rajzSzin = new ConsoleColor[MAG, SZEL];

    // ---- Választható karakterek és színek ----
    static char[] karakterek = { '#', '*', '@', '+', 'o', 'X', '.' };
    static string[] szinNevek = { "cián", "piros", "zöld", "sárga", "kék", "fehér" };
    static ConsoleColor[] szinek = { ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Green,
                                     ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.White };

    // ---- Állapot ----
    static int x = 0;               // kurzor a rajzlapon (0, 0 = bal felső cella)
    static int y = 0;
    static bool lent = false;       // false = felemelt, true = letett eszköz
    static int karakterIndex = 0;
    static int szinIndex = 0;
    static bool fut = true;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (Console.WindowWidth < BAL + SZEL + 1 || Console.WindowHeight < 24)
        {
            Console.WriteLine("Az ablak túl kicsi. Legalább 86 oszlop x 24 sor kell.");
            Console.WriteLine("Nagyítsd az ablakot, majd indítsd újra a programot.");
            Console.ReadKey(true);
            return;
        }

        for (int sor = 0; sor < MAG; sor++)
        {
            for (int oszlop = 0; oszlop < SZEL; oszlop++)
            {
                rajz[sor, oszlop] = ' ';
                rajzSzin[sor, oszlop] = ConsoleColor.White;
            }
        }

        Console.CursorVisible = false;
        Console.Clear();
        KeretRajzol();
        RajzMind();
        PanelFrissit();

        while (fut)
        {
            Ir(UZENETSOR, "", false);
            ConsoleKeyInfo billentyu = Console.ReadKey(true);
            ConsoleKey k = billentyu.Key;

            if (k == ConsoleKey.UpArrow) Mozog(0, -1);
            else if (k == ConsoleKey.DownArrow) Mozog(0, 1);
            else if (k == ConsoleKey.LeftArrow) Mozog(-1, 0);
            else if (k == ConsoleKey.RightArrow) Mozog(1, 0);
            else if (k == ConsoleKey.Spacebar)
            {
                lent = !lent;
                if (lent)
                {
                    Pont(x, y);
                    Cella(x, y);
                }
                Allapot();
            }
            else if (k >= ConsoleKey.F1 && k <= ConsoleKey.F12)
            {
                int sorszam = k - ConsoleKey.F1;          // 0..11
                Nyomda(sorszam / 3, 3 + 2 * (sorszam % 3)); // alakzat 0..3, méret 3/5/7
            }
            else if (k == ConsoleKey.M)
            {
                Menu();
            }
            else if (k == ConsoleKey.X)
            {
                fut = false;
            }
        }

        Console.ResetColor();
        Console.Clear();
        Console.CursorVisible = true;
    }

    // ================= Kirajzolás =================
    // 1. rész (Papp József Balázs)

    // Szöveg a bal panel egy sorába. A sor többi része törlődik.
    static void Ir(int sor, string szoveg, bool kiemel)
    {
        if (szoveg.Length > PANEL) szoveg = szoveg.Substring(0, PANEL);
        Console.SetCursorPosition(0, sor);
        if (kiemel)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.Write(szoveg.PadRight(PANEL));
        Console.ResetColor();
    }

    static void KeretRajzol()
    {
        for (int sor = 0; sor < 23; sor++)
        {
            Console.SetCursorPosition(BAL - 1, sor);
            Console.Write("|");
        }
        Console.SetCursorPosition(0, 0);
        Console.Write("KONZOLOS RAJZOLÓ");
        Console.SetCursorPosition(BAL, 0);
        Console.Write("RAJZLAP");
    }

    // Egy rajzlapcella kirajzolása. A kurzor helyén szürke háttér van.
    static void Cella(int cx, int cy)
    {
        Console.SetCursorPosition(BAL + cx, FENT + cy);
        if (cx == x && cy == y)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
        Console.ForegroundColor = rajzSzin[cy, cx];
        Console.Write(rajz[cy, cx]);
        Console.ResetColor();
    }

    static void RajzMind()
    {
        for (int sor = 0; sor < MAG; sor++)
        {
            for (int oszlop = 0; oszlop < SZEL; oszlop++)
            {
                Cella(oszlop, sor);
            }
        }
    }

    static void Allapot()
    {
        Ir(ALLAPOTSOR, "Pozíció (" + x + ", " + y + ")", false);
        Ir(ALLAPOTSOR + 1, "Karakter " + karakterek[karakterIndex], false);
        Ir(ALLAPOTSOR + 2, "Szín " + szinNevek[szinIndex], false);
        if (lent) Ir(ALLAPOTSOR + 3, "Eszköz letéve", false);
        else Ir(ALLAPOTSOR + 3, "Eszköz felemelve", false);
    }

    static void Sugo(string a, string b, string c, string d)
    {
        Ir(SUGOSOR, a, false);
        Ir(SUGOSOR + 1, b, false);
        Ir(SUGOSOR + 2, c, false);
        Ir(SUGOSOR + 3, d, false);
    }

    // A bal panel visszaállítása rajzolás módra.
    static void PanelFrissit()
    {
        for (int sor = MENUSOR; sor < ALLAPOTSOR; sor++)
        {
            Ir(sor, "", false);
        }
        Ir(MENUSOR, "M  Menü", false);
        Ir(MENUSOR + 1, "X  Kilépés", false);
        Allapot();
        Sugo("Nyilak: mozgás", "Szóköz: eszköz", "F1-F12: nyomda", "M: menü  X: kilépés");
    }

    static void Hiba(string uzenet)
    {
        Console.Beep();
        Ir(UZENETSOR, uzenet, false);
    }

    // ================= Rajzolás =================

    // 2. rész (Moldovan Sebastian): Pont, Nyomda. A Mozog az 1. rész (Papp József Balázs).

    // Az aktuális karakter és szín beírása egy cellába (kirajzolás nélkül).
    static void Pont(int cx, int cy)
    {
        rajz[cy, cx] = karakterek[karakterIndex];
        rajzSzin[cy, cx] = szinek[szinIndex];
    }

    static void Mozog(int dx, int dy)
    {
        int ujX = x + dx;
        int ujY = y + dy;

        if (ujX < 0 || ujX >= SZEL || ujY < 0 || ujY >= MAG)
        {
            Hiba("Hiba: rajzlap széle!");
            return;
        }

        int regiX = x;
        int regiY = y;
        x = ujX;
        y = ujY;
        if (lent)
        {
            Pont(x, y);
        }
        Cella(regiX, regiY);
        Cella(x, y);
        Allapot();
    }

    // forma: 0 = X, 1 = kereszt, 2 = négyzetkeret, 3 = rombuszkeret
    // meret: 3, 5 vagy 7
    static void Nyomda(int forma, int meret)
    {
        int r = meret / 2;   // ennyi cella van a középponttól minden irányban

        // Előbb a teljes befoglaló négyzetet ellenőrizzük, csak utána rajzolunk.
        if (x - r < 0 || x + r >= SZEL || y - r < 0 || y + r >= MAG)
        {
            Hiba("Hiba: nem fér el!");
            return;
        }

        for (int dy = -r; dy <= r; dy++)
        {
            for (int dx = -r; dx <= r; dx++)
            {
                int ax = Math.Abs(dx);
                int ay = Math.Abs(dy);
                bool jeloles = false;

                if (forma == 0) jeloles = (ax == ay);
                else if (forma == 1) jeloles = (dx == 0 || dy == 0);
                else if (forma == 2) jeloles = (Math.Max(ax, ay) == r);
                else if (forma == 3) jeloles = (ax + ay == r);

                if (jeloles)
                {
                    Pont(x + dx, y + dy);
                }
            }
        }
        RajzMind();
    }

    // ================= Menü =================
    // 1. rész (Papp József Balázs)

    // Lista a bal panelen. Fel/Le választ, Enter jóváhagy, Esc visszalép.
    // Visszaadja a választott sorszámot, Esc esetén -1-et.
    static int Valaszt(string cim, string[] lista, int kezdo)
    {
        int kiv = kezdo;
        for (int sor = MENUSOR; sor < ALLAPOTSOR; sor++)
        {
            Ir(sor, "", false);
        }
        Sugo("Fel/Le: választás", "Enter: megnyitás", "Esc: vissza a rajzhoz", "");

        while (true)
        {
            Ir(MENUSOR, cim, false);
            for (int i = 0; i < lista.Length; i++)
            {
                if (i == kiv) Ir(MENUSOR + 1 + i, "> " + lista[i], true);
                else Ir(MENUSOR + 1 + i, "  " + lista[i], false);
            }

            ConsoleKey k = Console.ReadKey(true).Key;
            Ir(UZENETSOR, "", false);

            if (k == ConsoleKey.UpArrow)
            {
                kiv = kiv - 1;
                if (kiv < 0) kiv = lista.Length - 1;
            }
            else if (k == ConsoleKey.DownArrow)
            {
                kiv = kiv + 1;
                if (kiv >= lista.Length) kiv = 0;
            }
            else if (k == ConsoleKey.Enter)
            {
                return kiv;
            }
            else if (k == ConsoleKey.Escape)
            {
                return -1;
            }
            else
            {
                Hiba("Hibás választás!");
            }
        }
    }

    static void Menu()
    {
        string[] fomenu = { "Karakter választása", "Szín választása", "Minta betöltése",
                            "Ugrás középre", "Cella törlése", "Kilépés" };
        int v = Valaszt("MENÜ", fomenu, 0);

        if (v == 0)
        {
            string[] lista = new string[karakterek.Length];
            for (int i = 0; i < karakterek.Length; i++)
            {
                lista[i] = karakterek[i].ToString();
            }
            int uj = Valaszt("Karakter választása", lista, karakterIndex);
            if (uj >= 0) karakterIndex = uj;
        }
        else if (v == 1)
        {
            int uj = Valaszt("Szín választása", szinNevek, szinIndex);
            if (uj >= 0) szinIndex = uj;
        }
        else if (v == 2)
        {
            Betolt();
        }
        else if (v == 3)
        {
            UgrasKozepre();
        }
        else if (v == 4)
        {
            CellaTorles();
        }
        else if (v == 5)
        {
            fut = false;
        }

        PanelFrissit();
    }

    // ================= Szövegfájl betöltése =================
    // 2. rész (Moldovan Sebastian)

    static void Betolt()
    {
        while (true)
        {
            Ir(UZENETSOR, "Fájlnév:", false);
            Ir(UZENETSOR + 1, "", false);
            Console.SetCursorPosition(0, UZENETSOR + 1);
            Console.CursorVisible = true;
            string nev = Console.ReadLine();
            Console.CursorVisible = false;

            string uzenet = "";
            string[] sorok = null;

            if (nev == null || nev.Trim() == "")
            {
                uzenet = "Hiba: nincs fájlnév!";
            }
            else
            {
                try
                {
                    sorok = File.ReadAllLines(nev.Trim());
                }
                catch (Exception)
                {
                    uzenet = "Hiba: nem olvasható!";
                }
            }

            // A rajzot csak azután módosítjuk, hogy minden sor méretét ellenőriztük.
            if (uzenet == "")
            {
                if (sorok.Length == 0)
                {
                    uzenet = "Hiba: üres a fájl!";
                }
                else if (sorok.Length > MAG)
                {
                    uzenet = "Hiba: nem fér el!";
                }
                else
                {
                    for (int i = 0; i < sorok.Length; i++)
                    {
                        if (sorok[i].Length > SZEL)
                        {
                            uzenet = "Hiba: nem fér el!";
                        }
                    }
                }
            }

            if (uzenet == "")
            {
                for (int sor = 0; sor < MAG; sor++)
                {
                    for (int oszlop = 0; oszlop < SZEL; oszlop++)
                    {
                        char c = ' ';
                        if (sor < sorok.Length && oszlop < sorok[sor].Length)
                        {
                            c = sorok[sor][oszlop];
                            if (c < ' ') c = ' ';   // tabulátor stb. helyett szóköz
                        }
                        rajz[sor, oszlop] = c;
                        rajzSzin[sor, oszlop] = ConsoleColor.White;
                    }
                }
                Ir(UZENETSOR + 1, "", false);
                RajzMind();
                return;
            }

            Hiba(uzenet);
            Ir(UZENETSOR + 1, "Újra? (I/N)", false);
            RajzMind();   // a beírt fájlnév esetleg átlógott a rajzlapra
            ConsoleKey k = Console.ReadKey(true).Key;
            Ir(UZENETSOR + 1, "", false);
            if (k != ConsoleKey.I)
            {
                return;
            }
        }
    }

    // ================= Saját funkciók =================

    // Papp József Balázs: a kurzor a rajzlap közepére ugrik.
    // Csak pozicionál, az eszköz állapotát nem változtatja, és letett eszköznél sem rajzol.
    static void UgrasKozepre()
    {
        int regiX = x;
        int regiY = y;
        x = SZEL / 2;
        y = MAG / 2;
        Cella(regiX, regiY);
        Cella(x, y);
    }

    // Moldovan Sebastian: az aktuális cella törlése (szóköz lesz benne).
    // A kurzor és az eszköz állapota nem változik.
    static void CellaTorles()
    {
        rajz[y, x] = ' ';
        rajzSzin[y, x] = ConsoleColor.White;
        Cella(x, y);
    }
}
