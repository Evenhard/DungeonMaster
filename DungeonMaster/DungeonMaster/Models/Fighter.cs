using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DungeonMaster.Models
{
    public class Fighter : INotifyPropertyChanged
    {
        public int id { get; set; }
        public bool isMonster { get; set; } = false;

        public int Initiative { get; set; }
        public int XP { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }

        private int health { get; set; }
        public int Health
        {
            get { return health; }
            set { health = value; OnPropertyChanged("Health"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
