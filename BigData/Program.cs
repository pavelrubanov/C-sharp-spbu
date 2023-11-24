using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace BigData
{
    internal class Program
    {
        private static void UpdateDb()
        {
            Parser parser = new Parser();
            parser.ReadInfo();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Actors.AddRange(parser.actors.Values);
                db.SaveChanges();
                db.Tags.AddRange(parser.tags.Values);
                db.SaveChanges();
                db.Movies.AddRange(parser.movies.Values);
                db.SaveChanges();
            }
        }
        public static void WriteFilmShort(Movie movie)
        {
            Console.WriteLine("\nНазвание RU: " + movie.NameRU);
            Console.WriteLine("Название US: " + movie.NameUS);
            Console.WriteLine("Рейтинг: " + movie.Rate);
        }
        public static void WriteFilmFull(Movie movie)
        {
            WriteFilmShort(movie);
            Console.WriteLine("Актеры:");
            foreach (var actor in movie.Actors)
            {
                Console.Write(actor.Name + ", ");
            }
            Console.WriteLine("\nТэги:");
            foreach (var tag in movie.Tags)
            {
                Console.Write(tag.Name + ", ");
            }
            Console.WriteLine();
        }
        public static void Main(string[] args)
        {
            using ApplicationContext db = new ApplicationContext();
            //Console.WriteLine(db.Movies.Count());Console.WriteLine(db.Actors.Count());Console.WriteLine(db.Tags.Count());
            while (true)
            {
                Console.WriteLine(
                    "\nКакую информацию вы хотите получить? \n" +
                    "1) О фильме \n" +
                    "2) Об актере-режиссере \n" +
                    "3) О конкретном тэге\n" +
                    "Введите 0, если хотите обновить базу данных");
                switch (Console.ReadLine())
                {
                    case "0":
                        {
                            UpdateDb();
                            break;
                        }
                    case "1":
                        {
                            Console.WriteLine("Введите название фильма");
                            string name = Console.ReadLine();
                            Movie? movie = db.Movies.Where(t => t.NameRU == name || t.NameUS == name).Include(m => m.Actors).Include(m => m.Tags).FirstOrDefault();
                            if (movie != null)
                            {
                                WriteFilmFull(movie);
                            }
                            else
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Введите имя актера");
                            string name = Console.ReadLine();
                            Actor? actor = db.Actors.Where(t => t.Name == name).Include(a => a.Movies).FirstOrDefault();
                            if (actor != null)
                            {
                                Console.WriteLine("Имя: " + actor.Name);
                                Console.WriteLine("Фильмы:");
                                foreach (var film in actor.Movies)
                                {
                                    WriteFilmShort(film);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Введите тэг");
                            string tagName = Console.ReadLine();
                            Tag? tag = db.Tags.Where(t => t.Name == tagName).Include(t => t.Movies).FirstOrDefault();
                            if (tag != null)
                            {
                                foreach (var film in tag.Movies)
                                {
                                    WriteFilmShort(film);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ничего не найдено");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неверный ввод");
                            break;
                        }
                }
            }
        }
    }
}