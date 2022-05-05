using System;
using System.Diagnostics;
using GTANetworkAPI;

namespace ProjectGTA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Process.Start(@"D:\RageMp\server-files\ragemp-server.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine("~r~ Внимание! ", ex.Message); // Проверить текст
            }
        }
    }
}
