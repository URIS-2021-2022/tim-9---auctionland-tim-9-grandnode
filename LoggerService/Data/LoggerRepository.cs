using LoggerService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Data
{
    public class LoggerRepository : ILoggerRepository
    {
        public Message CreateMessage(Message message)
        {
            try
            {
                string file = @"Logs\logs.txt";
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(message.Time);
                sw.WriteLine(message.Information);
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return message;
        }
    }
}
