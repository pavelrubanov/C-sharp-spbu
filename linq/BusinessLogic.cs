using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace LinqTask
{
    class BusinessLogic
    {
        private List<User> users = new List<User>();
        private List<Record> records = new List<Record>();
        public BusinessLogic()
        {

        }
        public List<User> GetUsersBySurname(String surname)
        {
            return (from user in users
                                where user.Surname == surname
                                select user).ToList();            
        }
        // Фильтрация коллекции пользователей по имени.В отличие от SQL-запросов, в LINQ сначала идёт ключевое слово from.Это сделано для сужения контекста автоподстановки в Visual Studio.После from идёт локальная переменная u(может быть любой, это как алиас в SQL-запросах), которой мы обозначаем текущий элемент в коллекции users в рамках данного запроса.Затем задаём ограничения на выборку в конструкции where.Здесь указывается любое булевое выражение.Каждый элемент коллекции будет добавлен в выборку, если для него будет удовлетворяться это условие.И в конце указываем при помощи select, какие именно данные нам нужны.Можно указать как весь элемент целиком, так и отдельные его поля.Результатом этого запроса будет отложенная операция. После всю эту выборку мы преобразуем в список при помощи не отложенной операции ToList().
        public User GetUserByID(int id)
        {
            return (from user in users
                    where user.ID == id
                    select user).Single();
        
        }
        //Получение одного конкретного пользователя по его id.В конце вызываем метод Single(), которые возвращает элемент, если он единственный.В противном случае, метод генерирует исключение, если найдено несколько элементов.
        public List<User> GetUsersBySubstring(String substring)
        {
            return (from user in users
                   where (user.Name.Contains(substring) || user.Surname.Contains(substring))
                   select user)
                   .ToList();
        }
        //Выборка пользователей по подстроке.Ищем эту подстроку в имени или в фамилии при помощи метода Contains().
        public List<String> GetAllUniqueNames()
        {
            return (from user in users
                   select user.Name)
                   .Distinct().ToList();
        }
        //Выбираем только уникальные имена при помощи метода Distinct().
        public List<User> GetAllAuthors()
        {
            return (from record in records
                    select record.Author)
                    .Distinct().ToList();
        }
        //Выбираем всех авторов, т.е.пользователей, у которых есть сообщения.Здесь демонстрируется объединение двух коллекций, аналогичное операции j
        //oin в SQL-запросе.Связывание коллекций идёт при помощи ссылки на объект пользователя, указанного в сущности Record.
        public Dictionary<int, User> GetUsersDictionary()
        {
            return users.ToDictionary(
                user => user.ID,
                user => user);
        }
        //Преобразуем коллекцию пользователей в словарь при помощи двух лямбда-выражений,
        //которые принимает метод ToDictionary().
        //Первое выражение указывает, какое поле сделать ключом,
        //второе - что выбрать в качестве значения
        //.В данном случае в качестве значения выбираем саму сущность пользователя.
        public int GetMaxID()
        {
            return (from user in users
                    select user.ID).Max();
        }
        //Выбираем максимальное значение поля ID сущности пользователя в коллекции.
        public List<User> GetOrderedUsers()
        {
            return (from user in users
                    .OrderBy(user => user.ID)
                    select user).ToList();
        }
        //Сортируем пользователей по их идентификатору при помощи конструкции orderby.
        public List<User> GetDescendingOrderedUsers()
        {
            return (from user in users
                    .OrderBy(user => user.ID)
                    select user).ToList();
        }
        //Обратная сортировка пользователей.После orderby добавлено ключевое слово descending.
        public List<User> GetReversedUsers()
        {
            return (from user in users
                    select user).Reverse().ToList();
        }
        //Здесь метод Reverse() возвращает итератор, который проходит по коллекции от конца в начало.
        //Результат работы данного метода идентичен предыдущему результату(обратная сортировка).
        public List<User> GetUsersPage(int pageSize, int pageIndex)
        {
            return (from user in users
                    select user).Skip(pageIndex - 1).Take(pageSize).ToList();
        }
    }
}
       /* И наконец, пример реализации пейджинга, т.е.постраничного вывода элементов
       коллекции.Метод Skip() пропускает указанное количество 
       элементов от начала, а Take() включает в выборку то количество элементов с 
       текущей позиции, которое максимально возможно на одной странице.*/
