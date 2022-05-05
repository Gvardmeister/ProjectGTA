using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Commands : Script
    {
        [Command("veh", "/veh спавнит т/с в координатах игрока", Alias = "vehicle")]

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
                NAPI.Util.ConsoleOutput("При создании объекта т/с обнаружено исключение: " + ex);
            }
        }

        [Command("freeze", "/freeze [ник игрока] [true/false]")]

        public void CmdFreeze(Player player, Player target, bool freezestatus)
        {
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
        }
    }
}
