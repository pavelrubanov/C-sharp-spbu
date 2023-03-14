using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    public class MyItem<T>
        {
            public T Value { get; set; }
            public MyLinkedList<T>? List { get; internal set; }
            public MyItem<T>? Next { get; internal set; }
            public MyItem<T>? Previous { get; internal set; }
            public MyItem (T value, MyLinkedList<T>? list, MyItem<T>? next, MyItem<T>? previous)
            {
                Value = value;
            }
            public MyItem(T value)
            {
                Value = value;
            }
        }
}
