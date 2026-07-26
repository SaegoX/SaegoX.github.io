using Ans.Net10.Common;
using Practice.ConsoleApp.Models;
using System.Text;

namespace Practice.ConsoleApp
{

    public class Program
    {

        static readonly List<PrepModel> ListPreps = [];
        static readonly List<GroupModel> ListGroup = [];
        static readonly List<ExamModel> ListExam = [];



        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var source1 = GetSourceData("https://guap.ru/content/rasp/exam/current.xml");

            foreach (var element1 in source1.Subject)
            {
                AddNewPrep(element1);
                AddNewGroup(element1);
                AddExam(element1);
            }

            //OutPreps();
            //Console.ReadLine();
            //OutGroups();
            //Console.ReadLine();
            OutExam();

            Console.ReadLine();

        }




        /* methods */


        static void AddNewPrep(
          SubjectModel element1)
        {
            var count1 = element1.PrepIds?.Length ?? 0;
            if (count1 > 0)
            {
                for (int i1 = 0; i1 < count1; i1++)
                {
                    var prep1 = new PrepModel
                    {
                        Id = element1.PrepIds[i1],
                        Name = element1.PrepNames[i1],
                    };
                    if (!ListPreps.Any(x => x.Id == prep1.Id))
                        ListPreps.Add(prep1);
                }
            }
        }

        static void AddNewGroup(
         SubjectModel element1)
        {
            var count1 = element1.GroupIds?.Length ?? 0;
            if (count1 > 0)
            {
                for (int i1 = 0; i1 < count1; i1++)
                {
                    var group1 = new GroupModel
                    {
                        Id = element1.GroupIds[i1],
                        Name = element1.GroupNames[i1],
                    };
                    if (!ListGroup.Any(x => x.Id == group1.Id))
                        ListGroup.Add(group1);
                }
            }
        }


        static void AddExam(
          SubjectModel element1)
        {
            var group1 = new ExamModel
            {
                Id = element1.IDSubg,
                Date = element1.Day,
                Building = element1.Buildings,
                Groups = element1.GroupRaw,
                Less = element1.Less,
                Preps = element1.PrepRaw,
                Room = element1.Rooms,
                Disc = element1.Disc
            };
            ListExam.Add(group1);
        }


        //static void OutPreps()
        //{
        //    Console.WriteLine("Список преподавателей:");
        //    foreach (var prep1 in ListPreps.OrderBy(x => x.Name))
        //    {
        //        Console.WriteLine($"{prep1.Id}. {prep1.Name}");
        //    }
        //}


        //static void OutGroups()
        //{
        //    Console.WriteLine("Список групп:");
        //    foreach (var group1 in ListGroup.OrderBy(x => x.Name))
        //    {
        //        Console.WriteLine($"{group1.Id}. {group1.Name}");
        //    }
        //}


        static void OutExam()
        {
            Console.WriteLine("Экзамены");
            foreach (var exam1 in ListExam.OrderBy(x => x.Date))
            {
                Console.WriteLine($"Идентификатор: {exam1.Id}. Дата экзамена: {exam1.Date}. Смена {exam1.Less}. Аудитория: {exam1.Room}. Название:{exam1.Disc}.");
            }
        }



        /* functions */


        static RaspModel GetSourceData(
          string url)
        {
            var client1 = new HttpClient();
            string data1 = client1.GetStringAsync(url).Result;
            return SuppXml.GetObjectFromXml<RaspModel>(data1);
        }

    }

}