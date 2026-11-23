using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public abstract class Game
    {
        
        public int GameIndex { get; }
        public string OpponentName { get; }
        public bool IsWin { get; }
        public int RatingPlayed { get; }


        protected Game(string opponentName, bool isWin, int gameIndex, int ratingPlayed)  
        {

            if (string.IsNullOrWhiteSpace(opponentName))
            {
                throw new ArgumentException("Ім'я користувача не може бути порожнім", nameof(opponentName));
            }

            GameIndex = gameIndex;
            OpponentName = opponentName;
            IsWin = isWin;
            RatingPlayed = ratingPlayed;

        }

        public abstract int CalculateRating(int baseRating);
    }


    public class StandardGame : Game
    {
        public StandardGame(string opponentName, bool isWin, int gameIndex, int ratingPlayed) 
            : base(opponentName, isWin, gameIndex, ratingPlayed)
        {
        }

        public override int CalculateRating(int baseRating)
        {
            if (baseRating < 0)
            {
                throw new ArgumentException(nameof(baseRating), "Ставка не може бути від'ємною.");
            }

            return baseRating;
        }



    }

    public class TrainingGame : Game
    {
        public TrainingGame(string opponentName, bool isWin, int gameIndex, int ratingPlayed) : base(opponentName,   isWin, gameIndex, ratingPlayed) 
        {
        }

        public override int CalculateRating(int baseRating)
        {
            return 0; // Тренувальна гра дає 0 зміни рейтингу
        }



    }


    public class SoloGame : Game
    {
        public SoloGame(string opponentName, bool isWin, int gameIndex, int ratingPlayed) : base(opponentName, isWin, gameIndex, ratingPlayed)
        {
        }

        public override int CalculateRating(int baseRating)
        {
            if (baseRating < 0)
            {
                throw new ArgumentException("Ставка не може бути від'ємною.");
            }
            return baseRating * 2; 
        }

    }





}
