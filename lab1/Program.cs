// See https://aka.ms/new-console-template for more information

using lab1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Ініціалізуємо імітацію бази даних (в ній вже є початкові дані з SeedData)
DbContext dbContext = new DbContext();

// 2. Створюємо рівень Repository, передаючи туди DbContext
IPlayerRepository playerRepository = new PlayerRepository(dbContext);
IGameRepository gameRepository = new GameRepository(dbContext);

// 3. Створюємо рівень Service, передаючи туди інтерфейси репозиторіїв
IPlayerService playerService = new PlayerService(playerRepository);
IGameService gameService = new GameService(gameRepository, playerRepository, playerService);

Console.WriteLine("=== 1. ПОЧАТКОВИЙ СПИСОК ГРАВЦІВ У БАЗІ ДАНИХ ===");
PrintAllPlayers(playerService);

// 4. Створюємо нових гравців через сервіс
Console.WriteLine("\n=== 2. СТВОРЕННЯ НОВИХ ГРАВЦІВ ===");
playerService.CreateAccount("Максим", initialRating: 150, accountType: "Standard");
playerService.CreateAccount("Ірина", initialRating: 110, accountType: "HalfLoss");
Console.WriteLine("Нових гравців успішно додано до бази даних!");

Console.WriteLine("\n=== 3. ОНОВЛЕНИЙ СПИСОК УСІХ ГРАВЦІВ ===");
PrintAllPlayers(playerService);

// 5. Проводимо ігри, результати яких записуються в БД та змінюють рейтинги гравців
Console.WriteLine("\n=== 4. ПРОВЕДЕННЯ ІГОР ТА ЗАПИС РЕЗУЛЬТАТІВ У БД ===");


gameService.CreateGame(playerId: 1, opponentName: "Богдан", isWin: true, ratingPlayed: 30, gameType: "Standard");


gameService.CreateGame(playerId: 2, opponentName: "Алла", isWin: false, ratingPlayed: 30, gameType: "Standard");


gameService.CreateGame(playerId: 5, opponentName: "Олена", isWin: false, ratingPlayed: 50, gameType: "Training");


gameService.CreateGame(playerId: 3, opponentName: "Бот-суперник", isWin: true, ratingPlayed: 40, gameType: "Solo");

Console.WriteLine("Результати всіх ігор успішно записано до бази даних!");

// 6. Виводимо оновлені рейтинги гравців після ігор
Console.WriteLine("\n=== 5. СПИСОК ГРАВЦІВ ПІСЛЯ ЗІГРАНИХ ІГОР ===");
PrintAllPlayers(playerService);

// 7. Вивід списку ігор конкретного гравця (наприклад, Алли з Id = 1)
int targetPlayerId = 1;
var targetPlayer = playerService.ReadAccountById(targetPlayerId);
Console.WriteLine($"\n=== 6. ІСТОРІЯ ІГОР КОНКРЕТНОГО ГРАВЦЯ: {targetPlayer?.UserName} (Id={targetPlayerId}) ===");
var playerGames = gameService.ReadPlayerGamesByPlayerId(targetPlayerId);
foreach (var g in playerGames)
{
    string resultText = g.IsWin ? "Перемога" : "Поразка";
    Console.WriteLine($"Гра Id={g.Id} | Тип: {g.GameType} | Суперник: {g.OpponentName} | Результат: {resultText} | Ставка: {g.RatingPlayed}");
}

// 8. Вивід усіх ігор із бази даних
Console.WriteLine("\n=== 7. УСІ ЗІГРАНІ ІГРИ В БАЗІ ДАНИХ ===");
var allGames = gameService.ReadGames();
foreach (var g in allGames)
{
    var player = playerService.ReadAccountById(g.PlayerId);
    string playerName = player != null ? player.UserName : $"Id={g.PlayerId}";
    string resultText = g.IsWin ? "Перемога" : "Поразка";
    Console.WriteLine($"Гра Id={g.Id} | Гравець: {playerName} | Тип: {g.GameType} | Суперник: {g.OpponentName} | Результат: {resultText} | Ставка: {g.RatingPlayed}");
}

Console.WriteLine("\n=== РОБОТУ ПРОГРАМИ ЗАВЕРШЕНО ===");

// Допоміжний метод для красивого виводу гравців
static void PrintAllPlayers(IPlayerService service)
{
    var players = service.ReadAccounts();
    foreach (var p in players)
    {
        Console.WriteLine($"Id: {p.Id} | Ім'я: {p.UserName,-8} | Тип акаунта: {p.AccountType,-10} | Поточний рейтинг: {p.CurrentRating}");
    }
}