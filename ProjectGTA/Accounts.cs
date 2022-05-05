using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Accounts
    {
        private const string _AccountKey = "PlayerKey";
        private int _ID;
        private string _Name;
        private long _Cash;

        // Возможно убрать параметры из конструктора
        public Accounts()
        {
            _Name = "";
            _Cash = 10000;
        }

        public Accounts(string Name, long Cash = 10000) 
        {
            _Name = Name;
            _Cash = Cash;
        }

        public int ID
        {
            get 
            { 
                return _ID; 
            }

            set
            {
                _ID = value;
            }
        }

        public string Name
        {
            get 
            { 
                return _Name; 
            }
            set
            {
                _Name = value;
            }
        }

        public long Cash
        {
            get
            {
                return _Cash;
            }
            set
            {
                _Cash = value;
            }
        }

        public static bool IsPlayerLoggedIn(Player player)
        {
            if (player != null)
            {
                return player.HasData(_AccountKey);
            }
            else
            {
                return false;
            }
        }
    }
}
