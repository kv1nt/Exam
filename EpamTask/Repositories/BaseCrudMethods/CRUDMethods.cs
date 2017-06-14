using System;
using System.Collections.Generic;

namespace EpamTask.Repositories.BaseCrudMethods
{
    public class CrudMethods<T> where T : class
    {
        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual bool Create(T item)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual bool Create(string item, int item2)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual bool Update(T item)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual void Update(int item1, string item2)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException("NotImplemented");
        }

        public virtual int FindIdEntity(int item)
        {
            throw new NotImplementedException("NotImplemented");
        }
    }
}