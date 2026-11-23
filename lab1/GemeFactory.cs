using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class GemeFactory
    {
        public static Game CreateGame(string gameType, int gameIndex, string opponentName, bool isWin, int baseRating)
        {
            Game createdGame;

            if (gameType == "Standard")
            {
                createdGame = new StandardGame(opponentName, isWin, gameIndex, baseRating);
            }
            else if (gameType == "Training")
            {
                createdGame = new TrainingGame(opponentName, isWin, gameIndex, baseRating);
            }
            else if (gameType == "Solo")
            {
                createdGame = new SoloGame(opponentName, isWin, gameIndex, baseRating);
            }

            else
            {
                // Якщо передали невідомий тип гри, кидаємо звичний ArgumentException
                throw new ArgumentException("Невідомий тип гри: " + gameType);
            }

            // Повертаємо об'єкт, який автоматично приводиться до базового типу Game 
            return createdGame;



        }





    }
}
