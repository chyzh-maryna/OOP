using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public interface IPlayerRepository
    {
        void Create(PlayerEntity player);          
        List<PlayerEntity> ReadAll();              
        PlayerEntity? ReadById(int id);            
        void Update(PlayerEntity player);          
        void Delete(int id);                       
    }

    // Інтерфейс для роботи з іграми в БД (CRUD)
    public interface IGameRepository
    {
        void Create(GameEntity game);              
        List<GameEntity> ReadAll();                
        GameEntity? ReadById(int id);              
        List<GameEntity> ReadByPlayerId(int playerId); 
        void Update(GameEntity game);              
        void Delete(int id);                       
    }

}
