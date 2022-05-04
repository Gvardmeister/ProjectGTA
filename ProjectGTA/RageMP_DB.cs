using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace ProjectGTA
{
    internal class RageMP_DB
    {
        private static MySqlConnection _Connection;

        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Base { get; set; }

        public RageMP_DB()
        {
            Host = "localhost";
            User = "root";
            Pass = "";
            Base = "ragemp_base";
        }

        public static void InitConnection()
        {
            try
            {
                RageMP_DB rageMP_DB = new RageMP_DB();
                string SQLConnection = $"SERVER = {rageMP_DB.Host}; USER = {rageMP_DB.User}; PASSWORD = {rageMP_DB.Pass}; DATABASE = {rageMP_DB.Base}";

                _Connection = new MySqlConnection(SQLConnection);

                _Connection.Open();

                NAPI.Util.ConsoleOutput("Подключение к серверу MySQL, успешно завершено!");
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput("Не удалось подключиться к серверу MySQL!");
                NAPI.Util.ConsoleOutput("Обнаружено исключение: " + ex);
            }
        }
    }
}