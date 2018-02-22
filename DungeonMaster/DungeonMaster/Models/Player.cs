using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonMaster.Models
{
    [Table("Player")]
    public class Player
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string PlayerName { get; set; }
        public string Class { get; set; }
    }
}
