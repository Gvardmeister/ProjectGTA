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
            player.SendChatMessage("Добро пожаловать на сервер ~g~(название моегомоего сервера)");
        }

        [ServerEvent(Event.PlayerSpawn)]

        private void OnPlayerSpawn(Player player)
        {
            player.Health = 100; // на видео было 50
            player.Armor = 100; // на видео было 50
        }
    }
}