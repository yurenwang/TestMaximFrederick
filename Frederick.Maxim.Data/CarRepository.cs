using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frederick.Maxim.Data
{
    public class CarRepository : IRepository<Car>
    {
        private readonly CarComparisonDBEntities _db;
        
        public CarRepository (CarComparisonDBEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.Cars.Count();
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Cars;
        }

        public IEnumerable<Car> GetByCondition(Func<Car, bool> p)
        {
            return _db.Cars.Where(p);
        }
    }
}
