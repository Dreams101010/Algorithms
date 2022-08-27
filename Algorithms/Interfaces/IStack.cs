using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Interfaces
{
    internal interface IStack<T>
    {
        public void Push(T item);
        public T Pop();
        public T Peek();
        public bool IsEmpty();
    }
}
