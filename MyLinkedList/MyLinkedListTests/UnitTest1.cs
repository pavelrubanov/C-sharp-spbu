using NuGet.Frameworks;
using System.Collections;
using System.Xml.Linq;

namespace MyLinkedList.MyLinkedListTests
{
    
    public class Tests
    {

        [Test]
        public void AddShouldCreateNewElementsAndChangeCount()
        {
            var list = new MyLinkedList<int>();
            const int count = 10;
            for (int i = 0; i < count; i++)
                list.Add(i);
            int j = 0;
            foreach (int value in list)
            {
                Assert.That(j, Is.EqualTo(value));
                j++;
            }
            Assert.AreEqual(count, list.Count);
        }

        [Test]
        public void ClearShouldRemoveAllElementsAndChangeCount()
        {
            var list = new MyLinkedList<int>();
            const int count = 10;
            for (int i = 0; i < count; i++)
                list.Add(i);
            list.Clear();
            Assert.IsNull(list.First);
            Assert.IsNull(list.Last);
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Test]
        public void Contains()
        {
            var list = new MyLinkedList<string>();
            list.Add("1233");
            list.Add("testEl");
            list.Add("88");
            Assert.IsTrue(list.Contains("testEl"));
        }

        [Test]
        public void NotContains()
        {
            var list = new MyLinkedList<string>();
            list.Add("1233");
            list.Add("testEl");
            list.Add("88");
            Assert.IsFalse(list.Contains("A"));
        }

        [Test]
        public void CreateListFromArrayShouldContainsSameValues()
        {
            double[] array = { 0, 1, 2, 3, 4, 5 };
            var list = new MyLinkedList<double>(array);
            double v = 0;
            foreach (var value in list)
            {
                Assert.That(value, Is.EqualTo(v));
                v++;
            }
        }

        [Test]
        public void AddFirstValue()
        {
            string[] resultArray = { "0", "1", "2", "3" };
            var list = new MyLinkedList<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.AddFirst("0");
            int i = 0;
            foreach (var value in list)
            {
                Assert.That(value, Is.EqualTo(resultArray[i]));
                i++;
            }
        }

        [Test]
        public void RemoveFristFromEmptyListShouldDoNothing()
        {
            var list = new MyLinkedList<string>();
            list.RemoveFirst();
            Assert.Pass();
        }

        [Test]
        public void RemoveLastFromEmptyListShouldDoNothing()
        {
            var list = new MyLinkedList<string>();
            list.RemoveLast();
            Assert.Pass();
        }

        [Test]
        public void RemoveFristFromNotEmptyListShouldRemoveFirstElementAndChangeCount()
        {
            var list = new MyLinkedList<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.RemoveFirst();
            Assert.That("2", Is.EqualTo(list.First.Value));
            Assert.That(2, Is.EqualTo(list.Count));
        }

        [Test]
        public void RemoveLastFromNotEmptyListShouldRemoveFirstElementAndChangeCount()
        {
            var list = new MyLinkedList<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.RemoveLast();
            Assert.That(list.Last.Value, Is.EqualTo("2"));
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void CopyToSmallArrayShouldThrowExepction()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            var list = new MyLinkedList<int>(values);
            int[] resultArray = { -1, 0, 18, 19, 20 };
            Assert.Throws<IndexOutOfRangeException>(() => list.CopyTo(resultArray, 3));
        }

        [Test]
        public void CopyToArrayShouldCopy()
        {
            int[] values = { 1, 2, 3 };
            var list = new MyLinkedList<int>(values);
            int[] resultArray = { -1, 0, 18, 19, 20, 21 };
            list.CopyTo(resultArray, 2);
            int[] expectedResult = { -1, 0, 1, 2, 3, 21 };
            Assert.That(resultArray, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Sort()
        {
            int[] array = { 58, 1, 9, 3, 2, 4, -5 };
            var list = new MyLinkedList<int>(array);
            Array.Sort(array);
            var comparer = new IntComp();
            list.Sort(comparer);
            for (int i = 0; i<list.Count;i++)
            {
                Assert.That(list[i], Is.EqualTo(array[i]));
            }
        }

    }
    public class IntComp : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y) return 1;
            if (x < y) return -1;
            return 0;
        }
    }
}