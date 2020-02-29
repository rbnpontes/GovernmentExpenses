using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GovernmentExpenses.Core
{
    public interface IRepository<T>
    {
        IList<T> All();
        IList<T> Where(Func<T, bool> predicate);
        void Update(T item);
        void Remove(T item);
        void Insert(T item);
        void Commit();
    }
}
