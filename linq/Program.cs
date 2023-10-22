using LinqTask;

BusinessLogic test = new BusinessLogic();
var b1 = test.GetUserByID(5);
var c2 = test.GetUsersBySubstring("Vik");
var t3 = test.GetAllUniqueNames();
var a4 = test.GetUsersBySurname("Vikhrau");
var g15 = test.GetMaxID();
var g6 = test.GetOrderedUsers();
var g9 = test.GetReversedUsers();
var q7 = test.GetDescendingOrderedUsers();
var m8 = test.GetUsersPage(2, 2);
Console.ReadKey();