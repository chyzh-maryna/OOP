using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public abstract class GameAccount
    {
        protected readonly List<Game> gameHistory = new();

        public string UserName { get;  }
        private int currentRating;
        public int CurrentRating
        {
            get => currentRating;
            protected set => currentRating = value < 1 ? 1 : value;
           
        }



        public int GamesCount => gameHistory.Count;

        public IReadOnlyList<Game> History => gameHistory.AsReadOnly();


        public GameAccount(string userName, int initialRating = 100) // конструктор
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Ім'я користувача не може бути порожнім", nameof(userName));
            }

            if (initialRating < 1)
            {
                throw new ArgumentException("Початковий рейтин не може бути менше 1");
            }

            UserName = userName;
            CurrentRating = initialRating;

        }


        public virtual void WinGame(Game game)
        {
            int earnedRating = game.CalculateRating(game.RatingPlayed);
            CurrentRating += earnedRating;
            gameHistory.Add(game);

        }

        public virtual void LoseGame(Game game)
        {
            int lostRating = game.CalculateRating(game.RatingPlayed);
            CurrentRating -= lostRating;
            gameHistory.Add(game);
        }

        public virtual void GetStats()
        {
            Console.WriteLine("=== ІСТОРІЯ ІГОР ГРАВЦЯ: " + UserName + " (Тип: " + this.GetType().Name + ") ===");
            Console.WriteLine("Поточний рейтинг: " + CurrentRating + "\n");

            if (gameHistory.Count == 0)
            {
                Console.WriteLine("Гравець ще не грав");
            }
            else
            {
                foreach (var game in gameHistory)
                {
                    string gameResult = game.IsWin ? "Перемога" : "Поразка";
                    int actualPoints = game.CalculateRating(game.RatingPlayed);

                    Console.WriteLine("Гра №" + game.GameIndex + " | " +
                                      "Тип гри: " + game.GetType().Name + " | " +
                                      "Суперник: " + game.OpponentName + " | " +
                                      "Результат: " + gameResult + " | " +
                                      "Зміна рейтингу: " + actualPoints);
                }
            }
                Console.WriteLine("\nВсього зіграно ігор: " + GamesCount);
                Console.WriteLine("===========================================\n");
            
        
        }
    }

    public class StandardAccount : GameAccount {
        public StandardAccount(string userName, int initialRating = 100)  : base(userName, initialRating) { }

    }

    public class HalfLossAccount : GameAccount
    {
        public HalfLossAccount(string userName, int initialRating = 100) : base(userName, initialRating) { }

        public override void LoseGame(Game game)
        {
            int originalPoints = game.CalculateRating(game.RatingPlayed);
            int reducedPoints = originalPoints / 2; 

            CurrentRating -= reducedPoints;
            gameHistory.Add(game);
        }
    }


    public class StreakAccount : GameAccount
    {
        private int winStreak = 0;

        public StreakAccount(string userName, int initialRating = 100): base(userName, initialRating) { }

        public override void WinGame(Game game)
        {
            winStreak++;
            int earnedRating = game.CalculateRating(game.RatingPlayed);

            if (winStreak >= 2)
            {
                earnedRating += 10;
            }

            CurrentRating += earnedRating;
            gameHistory.Add(game);

        }

        public override void LoseGame(Game game)
        {
            winStreak = 0; 
            base.LoseGame(game);
        }

    }



}
