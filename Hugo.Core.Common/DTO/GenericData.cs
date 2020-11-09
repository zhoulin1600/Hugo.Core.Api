using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common
{
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData
    {
        public string Id { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1> : GenericData
    {
        public T1 t1 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2> : GenericData<T1>
    {
        public T2 t2 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3> : GenericData<T1, T2>
    {
        public T3 t3 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4> : GenericData<T1, T2, T3>
    {
        public T4 t4 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5> : GenericData<T1, T2, T3, T4>
    {
        public T5 t5 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5, T6> : GenericData<T1, T2, T3, T4, T5>
    {
        public T6 t6 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5, T6, T7> : GenericData<T1, T2, T3, T4, T5, T6>
    {
        public T7 t7 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5, T6, T7, T8> : GenericData<T1, T2, T3, T4, T5, T6, T7>
    {
        public T8 t8 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5, T6, T7, T8, T9> : GenericData<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public T9 t9 { get; set; }
    }
    /// <summary>
    /// 泛型传递数据
    /// </summary>
    public class GenericData<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : GenericData<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public T10 t10 { get; set; }
    }
}
