using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS_Lab1._2_No1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Частина 1

			// Дано рядок у форматі "Student1 - Group1; Student2 - Group2; ..."

			string studentsStr = "Дмитренко Олександр - ІП-84; Матвійчук Андрій - ІВ-83; Лесик Сергій - ІО-82; Ткаченко Ярослав - ІВ-83; Аверкова Анастасія - ІО-83; Соловйов Даніїл - ІО-83; Рахуба Вероніка - ІО-81; Кочерук Давид - ІВ-83; Лихацька Юлія - ІВ-82; Головенець Руслан - ІВ-83; Ющенко Андрій - ІО-82; Мінченко Володимир - ІП-83; Мартинюк Назар - ІО-82; Базова Лідія - ІВ-81; Снігурець Олег - ІВ-81; Роман Олександр - ІО-82; Дудка Максим - ІО-81; Кулініч Віталій - ІВ-81; Жуков Михайло - ІП-83; Грабко Михайло - ІВ-81; Іванов Володимир - ІО-81; Востриков Нікіта - ІО-82; Бондаренко Максим - ІВ-83; Скрипченко Володимир - ІВ-82; Кобук Назар - ІО-81; Дровнін Павло - ІВ-83; Тарасенко Юлія - ІО-82; Дрозд Світлана - ІВ-81; Фещенко Кирил - ІО-82; Крамар Віктор - ІО-83; Іванов Дмитро - ІВ-82";

			// Завдання 1
			// Заповніть словник, де:
			// - ключ – назва групи
			// - значення – відсортований масив студентів, які відносяться до відповідної групи

			var list = new List<KeyValuePair<string, string>>();
			var splString = studentsStr.Split(";");
			foreach (var item in splString)
			{
				var item2 = item.Split(" - ");
				var pair = new KeyValuePair<string, string>(item2[1].Trim(), item2[0].Trim());
				list.Add(pair);
			}
			var task1 = Task1(list);

			// Завдання 2
			// Заповніть словник, де:
			// - ключ – назва групи
			// - значення – словник, де:
			//   - ключ – студент, який відносяться до відповідної групи
			//   - значення – масив з оцінками студента (заповніть масив випадковими значеннями, використовуючи функцію `randomValue(maxValue: Int) -> Int`)

			var points = new List<int> { 12, 12, 12, 12, 12, 12, 12, 16 };
			var list2 = Task2(list, points);
			var list3 = Task3(list2);
			Task4(list2, points);
			Task5(list3);
		}

		static List<KeyValuePair<string, string>> Task1(List<KeyValuePair<string, string>> list)
		{
			var result = (
				from u in list
				group u by u.Key into g
				select new KeyValuePair<string, string>(g.Key, string.Join(", ", g.Select(kvp => kvp.Value)))
				).OrderBy(kvp => kvp.Key).ToList();
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Завдання 1");
			foreach (var kvp in result) Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
			Console.WriteLine("");
			return result;
		}

		static List<KeyValuePair<string, List<KeyValuePair<string, List<int>>>>> Task2(List<KeyValuePair<string, string>> list, List<int> points)
		{
			var result = new List<KeyValuePair<string, List<KeyValuePair<string, List<int>>>>>();
			var groups = (
				from u in list
				group u by u.Key into g
				select new { g.Key }
				).OrderBy(kvp => kvp.Key).ToList();
			foreach(var g in groups)
			{
				var students = list.Where(u => u.Key == g.Key).OrderBy(u => u.Value);
				var studentsInGroup = new List<KeyValuePair<string, List<int>>>();
				foreach (var s in students)
				{
					var studentPoints = new List<int>();
					foreach(var point in points)
					{
						studentPoints.Add(randomValue(point));
					}
					studentsInGroup.Add(new KeyValuePair<string, List<int>>(s.Value, studentPoints));
				}
				result.Add(new KeyValuePair<string, List<KeyValuePair<string, List<int>>>>(g.Key, studentsInGroup));
			}
			
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Завдання 2");
			foreach (var group in result)
			{
				Console.WriteLine("{0}", group.Key);
				foreach(var student in group.Value)
				{
					Console.WriteLine("{0} [{1}]", student.Key, string.Join(", ", student.Value));
				}
			}
			Console.WriteLine("");
			return result;
		}

		private static List<KeyValuePair<string, List<KeyValuePair<string, int>>>> Task3(List<KeyValuePair<string, List<KeyValuePair<string, List<int>>>>> list)
		{
			var result = new List<KeyValuePair<string, List<KeyValuePair<string, int>>>>();
			foreach(var group in list)
			{
				var studentsInGroup = new List<KeyValuePair<string, int>>();
				foreach(var student in group.Value)
				{
					int sum = 0;
					foreach (var point in student.Value)
					{
						sum += point;
					}
					studentsInGroup.Add(new KeyValuePair<string, int>(student.Key, sum));
				}
				result.Add(new KeyValuePair<string, List<KeyValuePair<string, int>>>(group.Key, studentsInGroup));
			}

			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Завдання 3");
			foreach (var group in result)
			{
				Console.WriteLine("{0}", group.Key);
				foreach (var student in group.Value)
				{
					Console.WriteLine("{0}: {1}", student.Key, student.Value);
				}
			}
			Console.WriteLine("");
			return result;
		}

		private static List<KeyValuePair<string, float>> Task4(List<KeyValuePair<string, List<KeyValuePair<string, List<int>>>>> list, List<int> points)
		{
			var result = new List<KeyValuePair<string, float>>();
			foreach (var group in list)
			{
				var studentsInGroup = new List<KeyValuePair<string, int>>();
				int sum = 0;
				float avg = 0;
				foreach (var student in group.Value)
				{
					foreach (var point in student.Value)
					{
						sum += point;
					}
				}
				avg = (float)sum / (group.Value.Count * points.Count);
				result.Add(new KeyValuePair<string, float>(group.Key, avg));
			}

			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Завдання 4");
			foreach (var group in result)
			{
				Console.WriteLine("{0} : {1}", group.Key, group.Value);
			}
			Console.WriteLine("");
			return result;
		}

		private static void Task5(List<KeyValuePair<string, List<KeyValuePair<string, int>>>> list)
		{
			var result = new List<KeyValuePair<string, List<string>>>();
			foreach (var group in list)
			{
				var studentsInGroup = new List<string>();
				foreach (var student in group.Value)
				{
					if (student.Value >= 60)
						studentsInGroup.Add(student.Key);
				}
				result.Add(new KeyValuePair<string, List<string>>(group.Key, studentsInGroup));
			}

			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Завдання 5");
			foreach (var group in result)
			{
				Console.WriteLine("{0}: {1}", group.Key, string.Join(", ", group.Value));
			}
			Console.WriteLine("");
		}

		static int randomValue(int maxValue){
			Random r = new Random();
			switch(r.Next(0, 6)) {
			case 1:
				return (int)Math.Ceiling((float)maxValue * 0.7);
			case 2:
				return (int)Math.Ceiling((float)maxValue * 0.9);
			case 3: case 4: case 5:
				return maxValue;
			default:
				return 0;
			}
		}
	}
}
