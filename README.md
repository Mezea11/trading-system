# Trading System (C#)

## Översikt

Ett enkelt konsolbaserat tradingsystem där användare kan registrera sig, logga in/ut, lägga upp objekt, bläddra bland andras objekt, skicka bytesförfrågningar, acceptera/avslå, samt se genomförda byten. All data sparas automatiskt lokalt och laddas in igen vid uppstart.

## Körning

- **Bygg:**

  ```bash
  dotnet build
  ```

- **Kör:**

  ```bash
  dotnet run --program.cs ./<trading-system>
  ```

## Användning (flöde)

1. **Registrera konto** → ange användarnamn och lösenord.
2. **Logga in** → autentisera med dina uppgifter.
3. **Lägg upp objekt** → titel, beskrivning, ev. kategori/värde.
4. **Bläddra objekt** → lista andra användares objekt.
5. **Skicka bytesförfrågan** → välj objekt och vilket av dina objekt du erbjuder.
6. **Hantera förfrågningar** → acceptera eller avslå inkommande förfrågningar.
7. **Se genomförda byten** → historik över accepterade förfrågningar.
8. **Logga ut**.

> Systemet sparar automatiskt vid varje tillståndsändring (t.ex. nytt konto, nytt objekt, ny förfrågan, accept/avslag). Vid start laddas all data automatiskt (i detta läge ligger allting i minnet under runtime.)

## Designbeslut (kort motivering)

Designbeslut

Jag har valt att använda ett enkelt kompositionsbaserat upplägg utan arv eftersom systemet inte kräver någon arvshierarki. Varje klass (t.ex. User, Item, TradeRequest) representerar en tydlig enhet och använder sammansättning för att hålla logiken enkel och tydlig. Det ger en flexibel struktur som är lätt att förstå och ändra vid behov.

Systemet lagrar data i minnet under körning. Detta val gjordes eftersom det är den metod vi gått igenom i kursen hittills, och att implementera filbaserad sparning hade krävt att skriva om stora delar av logiken. Fokus låg därför på att bygga ett stabilt, välstrukturerat system i minnet som fungerar korrekt innan eventuell filbaserad lagring införs.

Jag valde att hålla designen enkel och fokuserad på grundläggande funktionalitet. Samtidigt har varje metod i systemet felhantering för att undvika krascher vid ogiltig inmatning eller felaktiga tillstånd. Detta gör att programmet förblir stabilt även om användaren gör misstag, vilket bidrar till en bättre användarupplevelse och en mer simpel kodbas.

## Felhantering (kort)

- Ogiltig inloggning eller upptaget användarnamn ger felmeddelanden.
- Inga trades kan initieras på egna objekt.
- Accept kräver att förfrågan är _Pending_ och att rätt användare godkänner.
- If satser kollar om objekt är null, tomma värden eller om användare inte har objekt att byta

## Begränsningar & vidare arbete

- Ingen avancerad sökning eller sortering (medvetet, ingen LINQ för att det var del av kravspecifikationen för uppgiften).
- Enkel konsol-UI. Kan senare ersättas av GUI eller webb.
- Transaktion/rollback är förenklad; framtida arbete kan inkludera atomiska skrivningar och backup.
- Ingen logik implementerad för att spara data i textfiler, allting körs i minnet under run time
