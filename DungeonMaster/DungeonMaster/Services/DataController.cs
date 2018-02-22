using DungeonMaster.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DungeonMaster.Services
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }

    public class DataController
    {
        private static SQLiteAsyncConnection database = null;
        private static DataController instance = null;

        public static DataController Controller
        {
            get { return (instance = instance ?? new DataController()); }
        }

        private DataController()
        {
            database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("DungeonMaster.db"));

            database.CreateTableAsync<Monster>(CreateFlags.ImplicitPK);
            database.CreateTableAsync<Player>(CreateFlags.ImplicitPK);
        }

        //Монстры
        public Task<List<Monster>> GetMonsters()
        {
            return database.Table<Monster>().ToListAsync();
        }

        public Task<Monster> GetMonster(int id)
        {
            return database.Table<Monster>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateMonster(Monster mob)
        {
            return database.UpdateAsync(mob);
        }

        public async Task AddMonster(Monster mob)
        {
            await database.CreateTableAsync<Monster>(CreateFlags.ImplicitPK);
            await database.InsertAsync(mob);
        }

        public async Task DeleteMonster(Monster mob)
        {
            await database.DeleteAsync(mob);
        }


        //Игроки
        public Task<List<Player>> GetHeroes()
        {
            return database.Table<Player>().ToListAsync();
        }

        public Task<Player> GetHero(int id)
        {
            return database.Table<Player>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateMonster(Player hero)
        {
            return database.UpdateAsync(hero);
        }

        public async Task AddHero(Player hero)
        {
            await database.CreateTableAsync<Player>(CreateFlags.ImplicitPK);
            await database.InsertAsync(hero);
        }

        public async Task DeletePlayer(Player hero)
        {
            await database.DeleteAsync(hero);
        }


        //Действия с базой данных
        public async Task ClearAllDataBases()
        {
            await database.DropTableAsync<Monster>();
            await database.DropTableAsync<Player>();
        }


        //public Task SaveSpellAsync(SpellItem item)
        //{
        //    if (item.Id != 0)
        //    {
        //        return database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return database.InsertAsync(item);
        //    }
        //}

        //public Task DeleteSpellAsync(SpellItem item)
        //{
        //    return database.DeleteAsync(item);
        //}

        //public async Task<int> ElementsCount<T>() where T : new()
        //{
        //    return await connection.Table<T>().CountAsync();
        //}

    }
}
