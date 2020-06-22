using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbteilungenData
{
    interface Repository<T>
    {
        List<T> findAll();
        T findOne(int id);
        int create(T entity); // returns last insert id
        void deleteOne(T entity);
    }
}
