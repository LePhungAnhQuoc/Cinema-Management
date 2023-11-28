using System.Collections.Generic;

namespace AnhQuoc_WPF_C1_B1
{
    interface IRepositoryBase<T>
    {
        int Length();

        List<T> Gets();
        T GetByIndex(int index);
        void Add(T entity);
    }
}
