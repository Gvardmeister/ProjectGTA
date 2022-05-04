using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Commands : Script
    {
        [Command("veh", "/veh спавнит авто в координатах игрока", Alias = "vehicle")]

        private void cmd_veh(Player player, string vehname, int color1, int color2)
        {
            uint vhash = NAPI.Util.GetHashKey(vehname);

            if (vhash <= 0)
            {
                player.SendChatMessage("~r~Неверная модель т/с");
            }

            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, color1, color2);
            vehicle.NumberPlate = "ADMIN";
            vehicle.Locked = false;
            vehicle.EngineStatus = true;
            player.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
        }
    }
}
