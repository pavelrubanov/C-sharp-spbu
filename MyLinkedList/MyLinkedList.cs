using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    public class MyLinkedList <T>: ICollection<T>, IEnumerable<T>, IComparable, ICloneable
    {
        public int Count { get; private set; }
        public MyItem<T>? First { get; private set; }
        public MyItem<T>? Last { get; private set; }

        public bool IsReadOnly => throw new NotImplementedException();

        public MyLinkedList(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Add(arr[i]);
        }
        public MyLinkedList() { }
        public T this[int index]
        {
            get
            {
                int i = 0;
                foreach (var value in this)
                {
                    if (i == index) return value;
                    i++;
                }
                throw new IndexOutOfRangeException();
            }

        }
        public bool Contains(T value)
        {
            for (MyItem<T> item = First; item.Next != null; item=item.Next)
            {
                if (item.Value!=null && item.Value.Equals(value))
                    return true;
            }
            return false;
        }
        public void AddFirst (T value)
        {
            MyItem<T> item = new MyItem<T>(value);
            AddFirst(item);
        }
        public void AddFirst(MyItem<T> item)
        {
            item.List = this;
            if (First == Last && First!=null)
            {
                First = item;
                First.Next = Last;
                Last.Previous = First;
            }
            else if (First == null)
            {
                First = item;
                Last = item;
            }
            else
            {
                MyItem<T> PreviousFirst = First;
                First = item;
                PreviousFirst.Previous = item;
                item.Next = PreviousFirst;
            }
            Count++;
        }
        public void RemoveFirst()
        {
            if (First != null)
            {
                First.List = null;
                First = First.Next;
                First.Previous = null;
                Count--;
            }
        }
        public void AddLast(T value)
        {
            MyItem<T> item = new MyItem<T>(value);
            AddLast(item);
        }
        public void AddLast(MyItem<T> item)
        {
            item.List = this;
            if (Last == First && Last != null)
            {
                Last = item;
                First.Next = Last;
                Last.Previous = First;
            }
            else if (Last == null)
            {
                Last = item;
                First = item;
            }
            else
            {
                MyItem<T> PreviousLast = Last;
                Last = item;
                PreviousLast.Next = item;
                item.Previous = PreviousLast;
            }
            Count++;
        }
        public void RemoveLast()
        {
            if (Last!=null)
            {
                Last.List = null;
                Last = Last.Previous;
                Last.Next = null;
                Count--;
            }
        }
        public MyItem<T> FindLast(T value)
        {
            for (MyItem<T>? item = First; item != null; item = item.Next)
                if ((item.Value!=null && item.Value.Equals(value))
                    || (item.Value==null && value==null)) 
                        return item;
            return null;
        }
        public void AddAfter (MyItem<T> item, T value)
        {
            if (item.List == this)
            {
                MyItem<T> new_item = new MyItem<T>(value);
                new_item.Previous = item;
                item.Next = new_item;
                new_item.List = this;
                if (Last != item)
                {
                    MyItem<T> old_next = item.Next;
                    new_item.Next = old_next;
                    old_next.Previous = new_item;
                }
                else
                    Last = new_item;
                Count++;
            }
        }
        public MyItem<T> Find (T value)
        {
            for (MyItem<T> item = First; item != null; item = item.Next)
                if (item.Value.Equals(value)) return item;
            return null;
        }
        public void AddBefore (MyItem<T> item, T value)
        {
            if (item.List==this)
            {
                MyItem<T> new_item = new MyItem<T>(value);
                new_item.Next = item;
                item.Previous = new_item;
                new_item.List = this;
                if (First != item)
                {
                    MyItem<T> old_previous = item.Previous;
                    old_previous.Next = new_item;
                    new_item.Previous = old_previous;
                }
                else
                    First = new_item;
                Count++;
            }
        }
        public bool Remove (MyItem<T>? item)
        {
            if (item!= null && item.List==this)
            {
                item.List = null;
                item.Next.Previous = item.Previous;
                item.Previous.Next = item.Next;
                item.List = null;
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove (T value)
        {
            if (Find(value) == null)
                return false;
            else
            {
                Remove(Find(value));
                return true;
            }
        }

        public object Clone()
        {
            MyLinkedList<T> ClonedList = new();
            foreach (var value in this)
            {
                ClonedList.Add(value);
            }
            return ClonedList;
        }

        public int CompareTo(object? obj)
        {
            if (obj==null || (obj.GetType() != this.GetType()))
            {
                throw new Exception();
            }
            MyLinkedList<T> obj1 = obj as MyLinkedList<T>;
            if (Count < obj1.Count) return -1;
            if (Count > obj1.Count) return 1;
            return 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (First == null) 
                yield break;
            MyItem<T>? item = First;
            do
            {
                yield return item.Value;
                item = item.Next;
            } while (item != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + Count)
            {
                throw new IndexOutOfRangeException();
            }
            foreach (var value in this)
            {
                array[arrayIndex] = value;
                arrayIndex++;
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            T[] array = new T[Count];
            CopyTo(array, 0);
            Array.Sort(array, comparer);
            MyItem<T>? item = First;
            for (int i=0; i<Count; i++)
            {
                item.Value = array[i];
                item = item.Next;
            }
        }
    }
}
