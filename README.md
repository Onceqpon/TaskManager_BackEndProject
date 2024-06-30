# Task Manager API

Task Manager API to aplikacja internetowa zbudowana przy użyciu ASP.NET Core do zarządzania zadaniami, projektami i kategoriami. Ta aplikacja zawiera uwierzytelnianie i autoryzację użytkowników przy użyciu JSON Web Tokens (JWT).

## Funkcje

- Rejestracja i logowanie użytkowników z uwierzytelnianiem JWT.
- Operacje CRUD dla zadań, projektów i kategorii.
- Testy jednostkowe i integracyjne.
- Integracja Swaggera do dokumentacji API.

## Technologie

- ASP.NET Core
- Entity Framework Core
- JWT (JSON Web Tokens)
- xUnit, Moq, FluentAssertions do testowania
- Swagger do dokumentacji API

## Jak zacząć

### Wymagania

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (lub możesz użyć opcji localdb)

### Instalacja

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/twojanazwa/task-manager-api.git
   cd task-manager-api

2. Skonfiguruj string połączenia z bazą danych w appsettings.json:
  ```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=TaskDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```
3. Zaktualizuj bazę danych:
```
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project WebApi
dotnet ef database update --project Infrastructure --startup-project WebApi
```
### Dokumentacja API
Dokumentacja API jest dostępna za pośrednictwem Swaggera. Po uruchomieniu aplikacji, przejdź do https://localhost:7062/swagger/index.html, aby zobaczyć dokumentację API.

### Testowanie
Testy jednostkowe i integracyjne są dostarczane za pomocą xUnit, Moq i FluentAssertions.

Przejdź do katalogu projektu testowego:
```
cd Tests
```
Uruchom testy:
```
dotnet test
```

### Użycie
Uwierzytelnianie
Aby uwierzytelnić użytkownika, wyślij żądanie POST do /api/authentication/login z następującym ładunkiem JSON:

```
{
  "loginName": "demo",
  "password": "Alfonso1234@"
}
```
Jeśli poświadczenia są poprawne, otrzymasz token JWT w odpowiedzi. Użyj tego tokena, aby uwierzytelniać kolejne żądania, dodając go w nagłówku Authorization:

```
Authorization: Bearer <your_token>
```

Np.

![image](https://github.com/Onceqpon/TaskManager_BackEndProject/assets/117514577/d9d78d13-95b0-40b5-afd3-d032a1a1780c)

Kopiujemy:
```
Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1lIjoiZGVtbyIsImdlbmRlciI6Im1hbGUiLCJlbWFpbCI6IkRhd2lkQG8yLnBsIiwiZXhwIjoxNzE5NzYzMTIwLCJqdGkiOiJjYzFhZjFhMy04ZjlhLTQxNTktOWYyZS1mOWQxZDI0N2FhOTQiLCJhdWQiOiJtaXBpZSIsImlzcyI6Im1pcGllIn0.qbyq7zL2EIfoN9mnUnuqwJ3KZBik-qif3IkhTGFEmaI
```
I wklejamy to w tym miejscu

![image](https://github.com/Onceqpon/TaskManager_BackEndProject/assets/117514577/be4a464e-75d4-460a-aa6b-1c39c03c91b2)


### Endpointy
Kategorie
```
GET /api/Categories - Pobierz wszystkie kategorie
GET /api/Categories/{id} - Pobierz kategorię po ID
POST /api/Categories - Utwórz nową kategorię
PUT /api/Categories/{id} - Zaktualizuj kategorię
DELETE /api/Categories/{id} - Usuń kategorię
```
Projekty
```
GET /api/Projects - Pobierz wszystkie projekty
GET /api/Projects/{id} - Pobierz projekt po ID
POST /api/Projects - Utwórz nowy projekt
PUT /api/Projects/{id} - Zaktualizuj projekt
DELETE /api/Projects/{id} - Usuń projekt
```
Zadania
```
GET /api/Tasks - Pobierz wszystkie zadania
GET /api/Tasks/{id} - Pobierz zadanie po ID
POST /api/Tasks - Utwórz nowe zadanie
PUT /api/Tasks/{id} - Zaktualizuj zadanie
DELETE /api/Tasks/{id} - Usuń zadanie
```

### Twórcy:
```
Dawid Pietruszka
Dawid Mida
```
