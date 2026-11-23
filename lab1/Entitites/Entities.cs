using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{  
        // Клас про запис гравця в базі даних
        public class PlayerEntity
        {
            public int Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public int CurrentRating { get; set; }
            public string AccountType { get; set; } = string.Empty; // "Standard", "HalfLoss", "Streak"
        }

        // Клас про зіграну гру в базі даних
        public class GameEntity
        {
            public int Id { get; set; }
            public int PlayerId { get; set; } 
            public string OpponentName { get; set; } = string.Empty;
            public bool IsWin { get; set; }
            public int RatingPlayed { get; set; }
            public string GameType { get; set; } = string.Empty; // "Standard", "Training", "Solo"
        }

    
}
