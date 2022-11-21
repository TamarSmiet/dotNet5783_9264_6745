using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    
    public interface ICrud<T>
    {
        public int Add(T item);
        public void Update(T item);
        public void Delete(int id);
        public T Get(int id);
        public IEnumerable<T> GetAll();

    }
}
