using LoggerService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Data
{
    public interface ILoggerRepository
    {

        public Message CreateMessage(Message message);

    }
}
