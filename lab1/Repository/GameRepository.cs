using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class GameRepository : IGameRepository
    {
        private readonly DbContext _context;

        public GameRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(GameEntity game)
        {
            game.Id = _context.Games.Count > 0 ? _context.Games.Max(g => g.Id) + 1 : 1;
            _context.Games.Add(game);
        }

        public List<GameEntity> ReadAll()
        {
            return _context.Games;
        }

        public GameEntity? ReadById(int id)
        {
            foreach (GameEntity g in _context.Games)
            {
                if (g.Id == id)
                {
                    return g;
                }
            }

            return null;
        }

        public List<GameEntity> ReadByPlayerId(int playerId)
        {
            // Повертаємо тільки ті ігри, які належать конкретному гравцю
            return _context.Games.Where(g => g.PlayerId == playerId).ToList();
        }

        public void Update(GameEntity updatedGame)
        {
            var existing = ReadById(updatedGame.Id);
            if (existing != null)
            {
                existing.PlayerId = updatedGame.PlayerId;
                existing.OpponentName = updatedGame.OpponentName;
                existing.IsWin = updatedGame.IsWin;
                existing.RatingPlayed = updatedGame.RatingPlayed;
                existing.GameType = updatedGame.GameType;
            }
        }

        public void Delete(int id)
        {
            var game = ReadById(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
        }
    }

}
