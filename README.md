Objašnjenje urađenog zadatka:

1. <b>Kreirati ASP.NET MVC/Web Forms aplikaciju koja prikazuje listu proizvoda. Osnovne informacije o proizvodu su: id, naziv, opis, kategorija, proizvođač, dobavljač, cena</b>
- Kreirana nova ASP.NET MVC aplikacija sa default podešavanjima i kreirana Model klasa sa zadatim atributima
- Kreiran kontroler Products sa metodom za dobijanje liste proizvoda, kao i odgovarajuća View Strana

2. <b>a. proizvodi se nalaze u JSON fajlu<br>
   b. proizvodi se nalaze u bazi</b>
- Kreirana lokalna SQL baza sa tabelom Products(Id,Name,Category,Manufacturer,Supplier,Price,Description)
  U web.config fajlu dodati sledeći ključevi:
  1) connString: u connectionStrings sekciji
  2) readDataFromJson (value: true, false) - ako je vrednost ključa true, onda se proizvodi čitaju iz JSON fajla, u suprotnom se čitaju iz baze
  3) jsonFileAddress - adresa na kojoj se nalazi JSON fajl sa proizvodima
  
 3. <b>Kreirati stranu za unos i izmenu proizvoda</b>
 - Kreirane metode Edit, View, Delete u Products kontroleru
 - Kreirane View strane Create, Edit
