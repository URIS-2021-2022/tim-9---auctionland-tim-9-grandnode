using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;

namespace Parcela.Data
{
    public interface IKatastarskaOpstinaRepository
    {
        List<KatastarskaOpstina> GetKatastarskaOpstinas();
        KatastarskaOpstina GetKatastarskaOpstinaById(Guid KatastarskaOpstinaId);

    }
}
