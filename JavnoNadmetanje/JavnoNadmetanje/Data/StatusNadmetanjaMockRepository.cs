using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class StatusNadmetanjaMockRepository : IStatusNadmetanjaRepository
    {
        public static List<StatusNadmetanja> statusNadmetanjas { get; set; } = new List<StatusNadmetanja>();

        public StatusNadmetanjaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            statusNadmetanjas.AddRange(new List<StatusNadmetanja>
            {
                new StatusNadmetanja
                {
                    StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                    NazivStatusaNadmetanja = "Prvi krug"
                },
                new StatusNadmetanja
                {
                    StatusNadmetanjaID = Guid.Parse("b1ad846b-f76f-4485-8c89-08e2dfebd112"),
                    NazivStatusaNadmetanja = "Drugi krug sa starim uslovima"
                },
                new StatusNadmetanja
                {
                    StatusNadmetanjaID = Guid.Parse("d85b4a71-27e4-4626-9a3e-0412430e03d6"),
                    NazivStatusaNadmetanja = "Drugi krug sa novim uslovima"
                }
            });
        }

        public StatusNadmetanjaConfirmationDto CreateStatusNadmetanja(StatusNadmetanja statusNadmetanja)
        {
            statusNadmetanja.StatusNadmetanjaID = Guid.NewGuid();
            statusNadmetanjas.Add(statusNadmetanja);
            StatusNadmetanja s = GetStatusNadmetanjaByID(statusNadmetanja.StatusNadmetanjaID);

            return new StatusNadmetanjaConfirmationDto
            {
                StatusNadmetanjaID = s.StatusNadmetanjaID
            };
        }

        public void DeleteStatusNadmetanja(Guid statusNadmetanjaID)
        {
            statusNadmetanjas.Remove(statusNadmetanjas.FirstOrDefault(s => s.StatusNadmetanjaID == statusNadmetanjaID));
        }

        public List<StatusNadmetanja> GetStatusiNadmetanja()
        {
            return (from s in statusNadmetanjas select s).ToList();
        }

        public StatusNadmetanja GetStatusNadmetanjaByID(Guid statusNadmetanjaID)
        {
            return statusNadmetanjas.FirstOrDefault(s => s.StatusNadmetanjaID == statusNadmetanjaID);
        }

        public StatusNadmetanjaConfirmationDto UpdateStatusNadmetanja(StatusNadmetanja statusNadmetanja)
        {
            StatusNadmetanja s = GetStatusNadmetanjaByID(statusNadmetanja.StatusNadmetanjaID);

            s.StatusNadmetanjaID = statusNadmetanja.StatusNadmetanjaID;
            s.NazivStatusaNadmetanja = statusNadmetanja.NazivStatusaNadmetanja;

            return new StatusNadmetanjaConfirmationDto
            {
                StatusNadmetanjaID = s.StatusNadmetanjaID
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
