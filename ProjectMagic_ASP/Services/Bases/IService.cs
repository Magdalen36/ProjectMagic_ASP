using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Services.Bases
{
    public interface IService<T, U>
        where T : class
        where U : class

    {
        bool Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(U form);
        void Update(U form);
    }
}
