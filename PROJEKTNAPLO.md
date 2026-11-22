# Projektnapló – Konzolos rajzoló

GitHub-repó: https://github.com/pppB02/KonzolRajzolo.git

## Munkamegosztás

| Tanuló | Feladat | Saját funkció |

| Papp József Balázs | **1. rész:** képernyő és panelek, állapotjelző, látható kurzor, kurzormozgatás és határellenőrzés, szóköz (eszköz fel/le), menü, hibajelzés (`KeretRajzol`, `Ir`, `Allapot`, `Sugo`, `PanelFrissit`, `Cella`, `RajzMind`, `Mozog`, `Hiba`, `Valaszt`, `Menu`, `Main`). | **Ugrás középre**: a kurzor a rajzlap közepére, a (30, 10) cellába ugrik. Menü → Ugrás középre. |
| Moldovan Sebastian | **2. rész:** a 12 nyomda és határellenőrzésük, szövegfájl betöltése hibakezeléssel, `minta.txt` (`Pont`, `Nyomda`, `Betolt`). | **Cella törlése**: a kurzor cellájából törli a jelet (szóköz lesz). Menü → Cella törlése. |

## Közös döntések

- A rajzlap 60 × 20 cella, a bal felső cella (0, 0). A rajzlap első konzoloszlopa a 25., ezért a konzolpozíció vízszintes értéke 25 + x, a függőleges értéke 1 + y. A kurzormozgatás és a nyomdák ugyanazt a rajzolást használják.
- A rajzlap a 85 oszlopnyi felület 70%-a (legalább kétharmad kell).
- Alapértelmezett karakter: `#`. Alapértelmezett szín: cián.
- Karakterek: `# * @ + o X .`. Színek: cián, piros, zöld, sárga, kék, fehér.
- Menü: `M` nyitja, Fel/Le választ, Enter megnyit, Esc visszalép. Kilépés rajzolás közben: `X`.
- Fájlbetöltés: a fájlnevet billentyűzetről kéri be a program. A betöltött karakterek fehérek. Mintafájl: `minta.txt`.
- Üres fájlt hibának veszünk, hogy véletlenül ne törölje a rajzot.

## Fontosabb fejlesztési lépések

2026.11.22 - Egész program.

## Tesztelés

Tesztelési lista: lásd `TESZTLISTA.md`.

| Dátum | Ki tesztelt | Talált hiba | Javítás (commit) |
2026.11.22 | Moldovan Sebastian | Nem | Nem kellett

## AI-használat
AI-t használtunk:
- A projekt feladat értelmezésére.
- Dokumentációra, hibakeresésre és kisebb kódrészltekre.
- Kreatív változó nevek és rövid de tömör és tartalmas leírások készítése.

### Papp József Balázs

- Melyik részhez kért segítséget: Formák rajzolásának értelmezése
- Mit tanult belőle: Hogyan kell egy szabályt felírni egy forma kirajzolásához úgy hogy csak a cella pozícióját ismerjük.

- Melyik részhez kért segítséget: Tömbök működése.
- Mit tanult belőle: Tömbök működését.

### Moldovan Sebastian

- Melyik részhez kért segítséget: Minta betöltése.
- Mit tanult belőle: Ha egy utasítás hibásan fut le (pl. nem sikerül betölteni egy fájlt) akkor nem álljon le a program hanem egy általunk megadott hibaüzenettel fejezze be a futást.

- Melyik részhez kért segítséget: Formák kirajzolása for ciklussal
- Mit tanult belőle: Hogyan kell egy szabályt felírni egy forma kirajzolásához úgy hogy csak a cella pozícióját ismerjük. pl.: X forma.

### Az AI által készített részek (a tanulóknak át kell nézniük és el kell tudniuk magyarázni)
- A nyomdák határellenőrzése: 3×3-as nyomdánál 1, 5×5-ösnél 2, 7×7-esnél 3 cellának kell elférnie a középponttól mind a négy irányban.
- A fájlbetöltés ellenőrzése a rajz módosítása előtt (`File.ReadAllLines`, `try`/`catch`).
- Tömbök működése.
- Alakzatok nyomtatása.
