using DungeonMaster.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonMaster.ViewModels
{
    public class InfoMonsterViewModel : BaseViewModel
    {
        public Monster Mob { get; set; }
        public InfoMonsterViewModel(Monster mob = null)
        {
            Title = mob?.Name;
            Mob = mob;
        }
    }
}
