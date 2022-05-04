using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Events : Script
    {
        [ServerEvent(Event.PlayerConnected)]

        private void OnPlayetConnected(Player player)
        {
            player.SendChatMessage("Добро пожаловать на сервер ~g~ТестСервака");
        }

        [ServerEvent(Event.PlayerSpawn)]

        private void OnPlayerSpawn(Player player)
        {
            player.Health = 100; // Для всех игроков на старте
            player.Armor = 0; // Для всех игроков на старте (0)
        }
    }
}