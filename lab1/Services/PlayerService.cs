using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        // Приймаємо об'єкт, приведений до інтерфейсу рівня Repository
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void CreateAccount(string userName, int initialRating = 100, string accountType = "Standard")
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Ім'я гравця не може бути порожнім.", nameof(userName));

            if (initialRating < 1)
                throw new ArgumentException("Початковий рейтинг не може бути менше 1.", nameof(initialRating));

            var entity = new PlayerEntity
            {
                UserName = userName,
                CurrentRating = initialRating,
                AccountType = accountType
            };

            _playerRepository.Create(entity);
        }

        public List<PlayerEntity> ReadAccounts()
        {
            return _playerRepository.ReadAll();
        }

        public PlayerEntity? ReadAccountById(int id)
        {
            return _playerRepository.ReadById(id);
        }

        // Перетворюємо запис із БД у поліморфний клас ієрархії GameAccount
        public GameAccount BuildAccountModel(PlayerEntity entity)
        {
            if (entity.AccountType == "HalfLoss")
                return new HalfLossAccount(entity.UserName, entity.CurrentRating);

            if (entity.AccountType == "Streak")
                return new StreakAccount(entity.UserName, entity.CurrentRating);

            return new StandardAccount(entity.UserName, entity.CurrentRating);
        }
    }




}
