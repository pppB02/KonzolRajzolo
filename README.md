# Konzolos rajzoló

C# konzolprogram, amellyel szabad kézzel lehet rajzolni, alakzatokat (nyomdákat) lehet lerakni, és szövegfájlból lehet mintát betölteni. A projekt két tanuló közös munkája: Papp József Balázs és Moldovan Sebastian.

## Indítás

1. Kell hozzá a .NET SDK
2. A projekt mappájában add ki a parancsot:

```powershell
dotnet run
```

Visual Studióban a `ConsoleDrawer.csproj` megnyitása után az F5 vagy a Ctrl+F5 indítja.

Az ablak legyen legalább **86 oszlop × 24 sor** méretű. Ha kisebb, a program ezt kiírja, és nem indul el.

## A képernyő

- **Bal oldal:** menü, állapotjelző és súgó (24 oszlop széles).
- **Jobb oldal:** a rajzlap, 60 × 20 cella. A bal felső cella koordinátája (0, 0). A rajzlap a képernyő szélességének 70%-át foglalja el.
- A kurzor szürke hátterű cellaként látszik.
- Az állapotjelző mutatja az (x, y) pozíciót, a rajzkaraktert, a rajzszínt és az eszköz állapotát (felemelve vagy letéve).

## Vezérlés rajzolás közben

| Billentyű | Hatás |
|---|---|
| Nyilak | A kurzor mozgatása cellánként |
| Szóköz | Az eszköz felemelése vagy letétele |
| F1–F12 | Nyomda (lásd lent) |
| M | Menü megnyitása |
| X | Kilépés |

- A program az eszköz felemelt állapotával indul.
- Letett eszköznél a kurzor cellájába és minden lépés célcellájába bekerül a kiválasztott karakter és szín.
- Felemelt eszköznél a mozgás csak a kurzort viszi.
- A rajzlap szélén túl nem lehet mozogni. Ilyenkor Beep szól, a kurzor és a rajz nem változik.

## Menü

Az `M` billentyű megnyitja a menüt. A választás módját a panel alján látható súgó jelzi:

- **Fel/Le:** választás (a kiválasztott sor kiemelt, előtte `>` áll).
- **Enter:** megnyitás.
- **Esc:** vissza a rajzhoz.

Menüpontok: Karakter választása, Szín választása, Minta betöltése, Ugrás középre, Cella törlése, Kilépés.

Hibás billentyű esetén Beep szól és a „Hibás választás!” üzenet jelenik meg. A menü nyitva marad, így újra lehet próbálni. Menü közben a rajzvezérlés nem működik.

- **Karakterek:** `# * @ + o X .` (alapértelmezett: `#`).
- **Színek:** cián, piros, zöld, sárga, kék, fehér (alapértelmezett: cián).

## Nyomdák

A nyomda középpontja mindig a kurzor. A nyomda felemelt és letett eszköznél is működik. A kurzor és az eszköz állapota nem változik. A nyomda felülírja a saját cellái alatti korábbi jeleket.

| Billentyű | Alakzat | Méret |
|---|---|---|
| F1 / F2 / F3 | X | 3×3 / 5×5 / 7×7 |
| F4 / F5 / F6 | Kereszt | 3×3 / 5×5 / 7×7 |
| F7 / F8 / F9 | Négyzetkeret | 3×3 / 5×5 / 7×7 |
| F10 / F11 / F12 | Rombuszkeret | 3×3 / 5×5 / 7×7 |

Az 5×5-ös minták (`#` a kiválasztott karakter):

```
X       Kereszt   Négyzet   Rombusz
#...#   ..#..     #####     ..#..
.#.#.   ..#..     #...#     .#.#.
..#..   #####     #...#     #...#
.#.#.   ..#..     #...#     .#.#.
#...#   ..#..     #####     ..#..
```

Rajzolás előtt a program ellenőrzi a teljes befoglaló négyzetet. Ha az alakzat nem fér el a rajzlapon, Beep szól, és a rajz egyáltalán nem változik.

## Szövegfájl betöltése

1. `M`, majd **Minta betöltése**.
2. Írd be a fájl nevét, és nyomj Entert. A mintafájl neve: `minta.txt`. Ez a program mellett (a projekt mappájában, és build után a kimeneti mappában is) megtalálható.
3. A fájl első sora a rajzlap bal felső cellájából indul, a többi sor alatta. A szóközök megtartják a minta elrendezését.
4. A betöltött karakterek fehér színűek. A betöltés lecseréli a korábbi rajzot. Utána folytatható a rajzolás.

Ha a fájl hiányzik, nem olvasható, üres, vagy nem fér el a rajzlapon (több mint 20 sor vagy 60 karakternél hosszabb sor), Beep szól és hibaüzenet jelenik meg. A korábbi rajz változatlan marad. A program felkínálja az újrapróbálást (`I` = újra, bármi más = vissza).