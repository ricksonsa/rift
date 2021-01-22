using System;
using System.Collections.Generic;

namespace rift.data.FakeData
{
    public abstract class FakeData<T>
    {
        public abstract T Generate();
        public abstract IList<T> GenerateList(int size);
       
    }
}
