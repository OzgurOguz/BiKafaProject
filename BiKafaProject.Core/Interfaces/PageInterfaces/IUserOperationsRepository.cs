using BiKafaProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiKafaProject.Core.Interfaces
{
    public interface IUserOperationsRepository:ICrudRepository<UserModel>
    {
    }
}
