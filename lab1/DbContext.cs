using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{

        public class DbContext
        {
            public List<PlayerEntity> Players { get; set; }
            public List<GameEntity> Games { get; set; }

            public DbContext()
            {
                Players = new List<PlayerEntity>();
                Games = new List<GameEntity>();

                // Наповнюємо базу початковими даними (Seed Data)
                SeedData();
            }

            private void SeedData()
            {
                Players.Add(new PlayerEntity { Id = 1, UserName = "Алла", CurrentRating = 100, AccountType = "Standard" });
                Players.Add(new PlayerEntity { Id = 2, UserName = "Богдан", CurrentRating = 120, AccountType = "HalfLoss" });
                Players.Add(new PlayerEntity { Id = 3, UserName = "Олена", CurrentRating = 100, AccountType = "Streak" });

                Games.Add(new GameEntity { Id = 1, PlayerId = 1, OpponentName = "Богдан", IsWin = true, RatingPlayed = 30, GameType = "Standard" });
                Games.Add(new GameEntity { Id = 2, PlayerId = 2, OpponentName = "Алла", IsWin = false, RatingPlayed = 30, GameType = "Standard" });
            }


        }
    
}
