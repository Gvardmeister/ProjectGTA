using System;
using System.Collections.Generic;
using System.Text;
using freemode; // Добавил и отработал Bcrypt 
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

        // НЕ ПИСАТЬ UserDB и Base
        public static void InitConnection()
        {
            try
            {
                RageMP_DB rageMP_DB = new RageMP_DB();
                string SQLConnection = $"SERVER = {rageMP_DB.Host}; USER = {rageMP_DB.User}; PASSWORD = {rageMP_DB.Pass}; DATABASE = {rageMP_DB.Base}";

                _Connection = new MySqlConnection(SQLConnection);

                _Connection.Open();

                NAPI.Util.ConsoleOutput("Подключение к серверу MySQL успешно установлено");
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput("Не удалось подключиться к серверу MySQL");
                NAPI.Util.ConsoleOutput("Обнаружено исключение: " + ex.ToString()); // Добавил ToString()

                NAPI.Task.Run(() => { Environment.Exit(0); }, delayTime: 5000);
            }
        }

        public static bool IsAccountExist(string name)
        {
            MySqlCommand command = _Connection.CreateCommand();

            command.CommandText = "SELECT * FROM accounts WHERE name = @name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void NewAccountRegister(Accounts account, string password)
        {
            try
            {
                string saltPW = BCrypt.HashPassword(password, BCrypt.GenerateSalt());

                MySqlCommand command = _Connection.CreateCommand();

                command.CommandText = "INSERT INTO accounts (name, pass, cash) VALUES (@name, @pass, @cash)"; //Такжже как и БД
                command.Parameters.AddWithValue("@name", account.Name);  
                command.Parameters.AddWithValue("@pass", saltPW);
                command.Parameters.AddWithValue("@cash", account.Cash);

                command.ExecuteReader();

                account.ID = (int)command.LastInsertedId;
            }
            catch(Exception ex)
            {
                NAPI.Util.ConsoleOutput("Обнаружено исключение: " + ex.ToString());
            }
        }

        public static void LoadAccount(Accounts account)
        {
            MySqlCommand command = _Connection.CreateCommand();

            command.CommandText = "SELECT * FROM accounts WHERE name = @name LIMIT 1";
            command.Parameters.AddWithValue("@name", account.Name);

            using MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                account.ID = reader.GetInt32("id");
                account.Cash = reader.GetInt32("cash");
            }
        }

        public static void SaveAccount(Accounts account)
        {
            MySqlCommand command = _Connection.CreateCommand();

            command.CommandText = "UPDATE accounts SET cash = @cash WHERE id = @id";
            command.Parameters.AddWithValue("@id", account.ID);
            command.Parameters.AddWithValue("@cash", account.Cash);
        }

        public static bool IsValidPassword(string name, string inputpass)
        {
            string TempPassword = "";

            MySqlCommand command = _Connection.CreateCommand();

            command.CommandText = "SELECT FROM accounts WHERE name = @name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                TempPassword = reader.GetString("pass");
            }

            if (BCrypt.CheckPassword(inputpass, TempPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
