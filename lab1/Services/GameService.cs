using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerService _playerService;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IPlayerService playerService)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _playerService = playerService;
        }

        public List<GameEntity> ReadGames()
        {
            return _gameRepository.ReadAll();
        }

        public List<GameEntity> ReadPlayerGamesByPlayerId(int playerId)
        {
            return _gameRepository.ReadByPlayerId(playerId);
        }

        public void CreateGame(int playerId, string opponentName, bool isWin, int ratingPlayed, string gameType = "Standard")
        {
            var playerEntity = _playerRepository.ReadById(playerId);
            if (playerEntity == null)
                throw new ArgumentException($"Гравця з Id={playerId} не знайдено в базі даних.");

            // Відновлюємо поліморфний акаунт гравця та створюємо об'єкт гри
            GameAccount account = _playerService.BuildAccountModel(playerEntity);
            int nextGameIndex = _gameRepository.ReadByPlayerId(playerId).Count + 1;

            Game game = CreateGameInstance(gameType, opponentName, isWin, nextGameIndex, ratingPlayed);

            // Застосовуємо поліморфну логіку перемоги/поразки
            if (isWin)
                account.WinGame(game);
            else
                account.LoseGame(game);

            // Оновлюємо рейтинг гравця в базі даних
            playerEntity.CurrentRating = account.CurrentRating;
            _playerRepository.Update(playerEntity);

            // Записуємо результат гри в базу даних
            var gameEntity = new GameEntity
            {
                PlayerId = playerId,
                OpponentName = opponentName,
                IsWin = isWin,
                RatingPlayed = ratingPlayed,
                GameType = gameType
            };

            _gameRepository.Create(gameEntity);
        }

        private Game CreateGameInstance(string gameType, string opponentName, bool isWin, int gameIndex, int ratingPlayed)
        {
            if (gameType == "Standard")
                return new StandardGame(opponentName, isWin, gameIndex, ratingPlayed);

            if (gameType == "Training")
                return new TrainingGame(opponentName, isWin, gameIndex, ratingPlayed);

            if (gameType == "Solo")
                return new SoloGame(opponentName, isWin, gameIndex, ratingPlayed);

            throw new ArgumentException("Невідомий тип гри: " + gameType);
        }
    }
}
