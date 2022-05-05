using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Accounts
    {
        private const string _AccountKey = "PlayerKey";
        private Player _Player;
        private int _ID;
        private string _Name;
        private long _Cash;

        // Возможно убрать параметры из конструктора
        public Accounts()
        {
            _Name = "";
            _Cash = 10000;
        }

        public Accounts(Player player, string Name, long Cash = 10000) 
        {
            _Name = Name;
            _Cash = Cash;
            _Player = player;
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

        public void Register(string name, string password, Player player)
        {
            try
            {
                RageMP_DB.NewAccountRegister(this, password);
                Login(_Player, true);
            }
            catch (Exception ex)
            {
                player.SendChatMessage("~r~ Внимание! ~w~ Обнаружено исключение: " + ex.Message);
            }
        }

        public void Login(Player player, bool IsFirstLogin)
        {
            try
            {
                RageMP_DB.LoadAccount(this);

                if (IsFirstLogin)
                {
                    player.SendChatMessage("Вы успешно ~g~ зарегистрировались!");
                }
                else
                {
                    player.SendChatMessage("Вы успешно ~g~ авторизовались!");
                }

                player.SetData(_AccountKey, this);
            }
            catch (Exception ex)
            {
                player.SendChatMessage("~r~ Внимание! ~w~ Обнаружено исключение на этапе ~g~ регистрации/авторизации: " + ex.Message);
            }
        }
    }
}
