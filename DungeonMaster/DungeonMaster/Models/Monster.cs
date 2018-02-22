using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonMaster.Models
{
    [Table("Monster")]
    public class Monster
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
        public int Id { get; set; }

        public int Health { get; set; }
        public int XP { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
