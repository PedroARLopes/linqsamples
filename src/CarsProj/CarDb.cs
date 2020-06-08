using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace CarsProj
{
    class CarDb: DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
