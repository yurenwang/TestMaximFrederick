using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frederick.Maxim.Data
{
    public interface IRepository<T>
    {
        int Count();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByCondition(Func<T, bool> p);
    }
}
