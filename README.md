# Conference Manager API

API для управления конференциями, включающее функционал для работы с пользователями, ролями, конференциями, докладами, спонсорами и локациями.

## Технологии

- .NET 8
- Entity Framework Core
- SQL Server
- ASP.NET Core Web API
- Swagger/OpenAPI

## Требования

- .NET 8 SDK
- SQL Server (локальный или удаленный)
- Visual Studio 2022 или VS Code

## Установка и запуск

1. Клонируйте репозиторий:
```bash
git clone https://github.com/your-username/conference-manager.git
cd conference-manager
```

2. Настройте подключение к базе данных:
   - Откройте файл `appsettings.json`
   - Измените строку подключения в секции `ConnectionStrings`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=ConferenceManager;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. Примените миграции базы данных:
```bash
dotnet ef database update
```

4. Запустите приложение:
```bash
dotnet run
```

Приложение будет доступно по адресу: `https://localhost:5296` и `http://localhost:5296`

## API Endpoints

### Пользователи

- `GET /api/Users` - Получить список всех пользователей
- `GET /api/Users/{id}` - Получить пользователя по ID
- `GET /api/Users/email/{email}` - Получить пользователя по email
- `GET /api/Users/username/{username}` - Получить пользователя по имени пользователя
- `POST /api/Users` - Создать нового пользователя
- `PUT /api/Users/{id}` - Обновить пользователя
- `DELETE /api/Users/{id}` - Удалить пользователя
- `GET /api/Users/{id}/profile` - Получить профиль пользователя
- `PUT /api/Users/{id}/profile` - Обновить профиль пользователя

### Роли

- `GET /api/Roles` - Получить список всех ролей
- `GET /api/Roles/{id}` - Получить роль по ID
- `POST /api/Roles` - Создать новую роль
- `PUT /api/Roles/{id}` - Обновить роль
- `DELETE /api/Roles/{id}` - Удалить роль

### Конференции

- `GET /api/Conferences` - Получить список всех конференций
- `GET /api/Conferences/{id}` - Получить конференцию по ID
- `POST /api/Conferences` - Создать новую конференцию
- `PUT /api/Conferences/{id}` - Обновить конференцию
- `DELETE /api/Conferences/{id}` - Удалить конференцию

### Доклады

- `GET /api/Presentations` - Получить список всех докладов
- `GET /api/Presentations/{id}` - Получить доклад по ID
- `POST /api/Presentations` - Создать новый доклад
- `PUT /api/Presentations/{id}` - Обновить доклад
- `DELETE /api/Presentations/{id}` - Удалить доклад

### Спонсоры

- `GET /api/Sponsors` - Получить список всех спонсоров
- `GET /api/Sponsors/{id}` - Получить спонсора по ID
- `POST /api/Sponsors` - Создать нового спонсора
- `PUT /api/Sponsors/{id}` - Обновить спонсора
- `DELETE /api/Sponsors/{id}` - Удалить спонсора
- `POST /api/Sponsors/conference/{conferenceId}/sponsor/{sponsorId}` - Добавить спонсора к конференции
- `GET /api/Sponsors/conference/{conferenceId}` - Получить спонсоров конференции

### Локации

- `GET /api/Locations` - Получить список всех локаций
- `GET /api/Locations/{id}` - Получить локацию по ID
- `POST /api/Locations` - Создать новую локацию
- `PUT /api/Locations/{id}` - Обновить локацию
- `DELETE /api/Locations/{id}` - Удалить локацию

## Примеры запросов

### Создание пользователя
```http
POST /api/Users
Content-Type: application/json

{
    "username": "john_doe",
    "password": "password123",
    "email": "john@example.com",
    "roleId": 1,
    "firstName": "John",
    "lastName": "Doe",
    "bio": "Software Developer",
    "affiliation": "Tech Corp",
    "website": "https://johndoe.com",
    "phoneNumber": "+1234567890",
    "profilePicture": "https://example.com/profile.jpg"
}
```

### Создание конференции
```http
POST /api/Conferences
Content-Type: application/json

{
    "name": "Tech Conference 2024",
    "description": "Annual technology conference",
    "startDate": "2024-06-01T09:00:00",
    "endDate": "2024-06-03T18:00:00",
    "locationId": 1,
    "maxParticipants": 500,
    "registrationDeadline": "2024-05-15T23:59:59"
}
```

### Добавление спонсора к конференции
```http
POST /api/Sponsors/conference/1/sponsor/1
```

## Структура базы данных

### Users
- Id (PK)
- Username
- Password
- Email
- RoleId (FK)

### UserProfiles
- Id (PK)
- UserId (FK)
- FirstName
- LastName
- Bio
- Affiliation
- Website
- PhoneNumber
- ProfilePicture

### Roles
- Id (PK)
- Name

### Conferences
- Id (PK)
- Name
- Description
- StartDate
- EndDate
- LocationId (FK)
- MaxParticipants
- RegistrationDeadline

### Presentations
- Id (PK)
- Title
- Description
- ConferenceId (FK)
- SpeakerId (FK)
- StartTime
- EndTime
- Room

### Sponsors
- Id (PK)
- Name
- Description
- Website
- Logo

### Locations
- Id (PK)
- Name
- Address
- City
- Country
- Capacity
- Facilities

## Лицензия

MIT License 