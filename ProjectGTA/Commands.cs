using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Commands : Script
    {
        [Command("veh", "Укажите - ~y~ /veh ~w~ [название], [основной/дополнительный]", Alias = "vehicle")]

        public void CmdVeh(Player player, string vehname, int color1, int color2)
        {
            try
            {
                uint vhash = NAPI.Util.GetHashKey(vehname);

                if (vhash <= 0)
                {
                    player.SendChatMessage("~r~ Неверная модель т/с");
                }

                Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, color1, color2);
                vehicle.NumberPlate = "ADMIN";
                vehicle.Locked = false;
                vehicle.EngineStatus = true;
                player.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
            }
            catch (Exception ex)
            {
                player.SendChatMessage("~r~ Внимание! ~w~ При создании объекта т/с обнаружено исключение: " + ex.Message);
            }
        }

        [Command("freeze", " Укажите - /freeze [ник игрока], [true/false]")] //Ник игрока писать слитно

        public void CmdFreeze(Player player, Player target, bool freezestatus)
        {
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
        }

        [Command("login", "/login [пароль]")]

        public void CmdLogin(Player player, string password)
        {
            if (Accounts.IsPlayerLoggedIn(player))
            {
                player.SendNotification("~r~ Вы уже авторизованы.");
                return;
            }

            if (!RageMP_DB.IsAccountExist(player.Name))
            {
                player.SendNotification("~r~ Вы не зарегистрированы.");
                return;
            }

            if (!RageMP_DB.IsValidPassword(player.Name, password))
            {
                player.SendNotification("~r~ Неверный пароль.");
                return;
            }

            Accounts account = new Accounts(player, player.Name);
            account.Login(player, false);

            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }

        [Command("register", "/register [пароль]")]

        public void CmdRegisterPass(Player player, string password)
        {
            if (Accounts.IsPlayerLoggedIn(player))
            {
                player.SendNotification("~r~ Вы уже авторизованы.");
                return;
            }

            if (!RageMP_DB.IsAccountExist(player.Name))
            {
                player.SendNotification("~r~ Вы уже зарегистрированы.");
                return;
            }

            Accounts account = new Accounts(player, player.Name);
            account.Register(player.Name, password, player);

            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }
    }
}
