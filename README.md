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
   {
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=TaskDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "JwtSettings": {
    "ValidIssuer": "mipie",
    "ValidAudience": "mipie",
    "Secret": "IUbH8zDkLea58S3UllVuswtYQ3oxmFbC9"
  },
  "AllowedHosts": "*"
}
