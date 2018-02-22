using DungeonMaster.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMaster.Services
{
    public class AddTestFighters
    {
        public static async Task JustDoIt()
        {
            var mobs = new List<Monster>
            {
               new Monster { Name = "Скелет", Health = 36, XP = 250, Description = "Прото обычный скелет из костей" },
               new Monster { Name = "Зомби", Health = 36, XP = 250, Description = "Прото обычный скелет из костей" },
               new Monster { Name = "Оборотень", Health = 36, XP = 250, Description = "Прото обычный скелет из костей" },
               new Monster { Name = "Суперзлобный голем колдун", Health = 360, XP = 1200, Description = "Прото обычный скелет из костей" }
            };

            var heroes = new List<Player>
            {
                new Player { Name = "Гунтер", PlayerName = "Еврей", Class = "Жрец Бури" },
                new Player { Name = "Асмодея", PlayerName = "Настя", Class = "Жрец Войны" },
                new Player { Name = "Тархун", PlayerName = "Катя", Class = "Варвар Берсеркер" },
            };

            foreach (var mob in mobs)
                await App.Database.AddMonster(mob);

            foreach (var hero in heroes)
                await App.Database.AddHero(hero);
        }
    }
}
