using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Events : Script
    {
        [ServerEvent(Event.ResourceStart)]

        public void OnResourceStart()
        {
            RageMP_DB.InitConnection();
        }

        [ServerEvent(Event.PlayerConnected)]

        public void OnPlayetConnected(Player player)
        {
            try
            {
                player.SendChatMessage("Добро пожаловать на сервер ~g~ ТестСервака.");

                if (RageMP_DB.IsAccountExist(player.Name))
                {
                    player.SendChatMessage("~w~ Данный аккаунт уже ~g~ зарегистрирован ~w~ на сервере. Используйте ~y~ /login ~w~ для авторизации.");
                }
                else
                {
                    player.SendChatMessage("~r~ Зарегистрируйте ~w~ аккаун на сервере. Используйте ~y~ /register ~w~ для регистрации.");
                }

                NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", true);
            }
            catch (Exception ex)
            {
                player.SendChatMessage("~r~ Внимание!" + ex.Message);
                NAPI.Util.ConsoleOutput("Внимание! При подключении к серверу обнаружено исключение: " + ex); 
            }
        }

        [ServerEvent(Event.PlayerSpawn)]

        public void OnPlayerSpawn(Player player)
        {
            player.Health = 100; // Для всех игроков на старте
            player.Armor = 0; // Для всех игроков на старте (0)


            player.SetSkin(0x8D8F1B10); // Добавил по фан, убрать
            player.SetWeaponAmmo((WeaponHash)0xFAD1F1C9, 5000); // Добавил по фан, убрать
            player.SetWeaponAmmo((WeaponHash)0xB1CA77B1, 5000); // Добавил по фан, убрать
        }
    }
}
