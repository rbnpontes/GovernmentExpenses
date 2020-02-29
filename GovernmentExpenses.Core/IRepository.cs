using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GovernmentExpenses.Core
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        IEnumerable<T> Where(Func<T, bool> predicate);
        int Count { get; }
        void Update(T item);
        void Remove(T item);
        void Insert(T item);
        void Commit();
    }
}
