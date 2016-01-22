using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface IMT940CustomerStatementRepository : IBaseRepository<MT940CustomerStatement, long>
    {
    }
}
