# Tesztelési lista

| # | Teszt | Várt eredmény | Eredmény |
|---|---|---|---|
| 1 | Program indítása | Bal oldalt menü és állapot, jobb oldalt rajzlap. Pozíció (0, 0), eszköz felemelve, karakter `#`, szín cián. A kurzor látható. | műküdik |
| 2 | Nyilak, felemelt eszköz | A kurzor mozog, az állapotjelző frissül, nem marad nyom. | műküdik |
| 3 | Szóköz, majd nyilak | Az állapot „letéve”, a kurzor cellájában és minden lépés célcellájában megjelenik a jel. | műküdik |
| 4 | Szóköz újra | Az állapot „felemelve”, a mozgás nem rajzol. | műküdik |
| 5 | Mozgás a bal felső sarokból balra és felfelé | Beep, a pozíció (0, 0) marad, a rajz nem változik. | műküdik |
| 6 | Mozgás a jobb alsó sarokba (59, 19), majd jobbra és lefelé | Beep, a pozíció marad, a rajz nem változik. | műküdik |
| 7 | F1–F3 a rajzlap közepén | X alakú nyomda 3×3, 5×5, 7×7, a kurzor és az eszköz állapota nem változik. | műküdik |
| 8 | F4–F6 a rajzlap közepén | Kereszt 3×3, 5×5, 7×7, a kurzor és az eszköz állapota nem változik. | műküdik |
| 9 | F7–F9 a rajzlap közepén | Négyzetkeret 3×3, 5×5, 7×7, a kurzor és az eszköz állapota nem változik. | műküdik |
| 10 | F10–F12 a rajzlap közepén | Rombuszkeret 3×3, 5×5, 7×7, a kurzor és az eszköz állapota nem változik. | műküdik |
| 11 | Nyomda felemelt és letett eszközzel | Mindkét állapotban ugyanúgy működik. | műküdik |
| 12 | Nyomda a régi jelek fölött | A nyomda saját cellái felülírják a régi jeleket. | műküdik |
| 13 | F3 a (2, 10) pozícióban | Beep, a rajz teljesen változatlan. | műküdik |
| 14 | F2 a (1, 10) pozícióban | Beep, a rajz teljesen változatlan. | műküdik |
| 15 | F3 a (3, 3) pozícióban | Az alakzat teljes egészében kirajzolódik. | műküdik |
| 16 | F12 a sarkokban és a széleken | Ha a befoglaló négyzet kilóg: Beep, nincs részleges rajz. | műküdik |
| 17 | `M`, Fel/Le, Esc | A kiemelt sor és a `>` jel mozog, Esc után a nyilak újra a kurzort mozgatják. | műküdik |
| 18 | Menüben más billentyű (például `a`) | Beep és „Hibás választás!”, a menü nyitva marad. | műküdik |
| 19 | Karakter és szín választása | Az állapotjelző frissül, az új rajzok az új karakterrel és színnel készülnek. | műküdik |
| 20 | Minta betöltése `minta.txt`-vel | A rajz helyére a minta kerül a bal felső sarokból, fehér színnel, a szóközök megmaradnak. Betöltés után lehet rajzolni. | műküdik |
| 21 | Betöltés nem létező fájlnévvel | Beep, „Hiba: nem olvasható!”, a rajz változatlan. Az `I` billentyű újrapróbálást ad. | műküdik |
| 22 | Betöltés 21 soros fájllal | Beep, „Hiba: nem fér el!”, a rajz változatlan. | műküdik |
| 23 | Betöltés 61 karakteres sort tartalmazó fájllal (akár csak az utolsó sor hosszú) | Beep, „Hiba: nem fér el!”, a rajz változatlan. | műküdik |
| 24 | Betöltés üres fájllal | Beep, „Hiba: üres a fájl!”, a rajz változatlan. | műküdik |
| 25 | Ugrás középre (Papp József Balázs) | A kurzor a (30, 10) pozícióba kerül, az eszköz állapota nem változik, nem rajzol. | műküdik |
| 26 | Cella törlése (Moldovan Sebastian) | A kurzor cellája üres lesz, a kurzor és az eszköz állapota nem változik. | műküdik |
| 27 | `X` rajzolás közben, illetve Menü → Kilépés | A program kilép. | műküdik |
| 28 | Túl kicsi ablak | Kiírja a minimális méretet, és kilép. | ??? |
