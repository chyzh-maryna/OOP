using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public interface IPlayerService
    {
        void CreateAccount(string userName, int initialRating = 100, string accountType = "Standard");
        List<PlayerEntity> ReadAccounts();
        PlayerEntity? ReadAccountById(int id);
        GameAccount BuildAccountModel(PlayerEntity entity);
    }

    public interface IGameService
    {
        List<GameEntity> ReadGames();
        List<GameEntity> ReadPlayerGamesByPlayerId(int playerId);
        void CreateGame(int playerId, string opponentName, bool isWin, int ratingPlayed, string gameType = "Standard");
    }



}
