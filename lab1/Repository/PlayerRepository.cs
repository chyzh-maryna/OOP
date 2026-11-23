using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DbContext _context;

        // Отримуємо DbContext через конструктор 
        public PlayerRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(PlayerEntity player)
        {
            // Автоматично генеруємо наступний вільний ID
            player.Id = _context.Players.Count > 0 ? _context.Players.Max(p => p.Id) + 1 : 1;
            _context.Players.Add(player);
        }

        public List<PlayerEntity> ReadAll()
        {
            return _context.Players;
        }

        public PlayerEntity? ReadById(int id)
        {
            foreach (PlayerEntity player in _context.Players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }

            return null;
        }

        public void Update(PlayerEntity updatedPlayer)
        {
            var existing = ReadById(updatedPlayer.Id);
            if (existing != null)
            {
                existing.UserName = updatedPlayer.UserName;
                existing.CurrentRating = updatedPlayer.CurrentRating;
                existing.AccountType = updatedPlayer.AccountType;
            }
        }

        public void Delete(int id)
        {
            var player = ReadById(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }
        }
    }
}
