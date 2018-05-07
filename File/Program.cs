using System;
using System.Collections.Generic;
using System.IO;

namespace File
{
    
    public class Student
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
    public class MainClass
    {
        public static void Main(string[] args)
        {
            File file = new File("test1.txt");
            Student std = new Student();
            int x;
            do
            {
                Console.WriteLine("1- Create File\n2- Insert to file\n3- Update data in file\n" +
                                  "4- Delete data from file\n5- Search In file");

                Console.Write("Enter Your Choice : ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        file.Create();
                        break;
                    case 2:
                        Console.Write("How many Student will Enter: ");
                        int n = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < n; i++)
                        {
                            Console.Write("Enter Id: ");
                            std.Id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter First Name: ");
                            std.FName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            std.LName = Console.ReadLine();
                            file.Insert(std, typeof(Student));
                        }
                        Console.WriteLine("Inserted....");
                        break;
                    case 3:
                        Console.Write("Enter the Student Id for UPDATE: ");
                        std.Id = Convert.ToInt32(Console.ReadLine());
                        if(file.Update(std.Id.ToString(), std, typeof(Student)))
                            Console.WriteLine("Not found !!");
                        break;
                    case 4:
                        Console.Write("Enter the Student Id for DELETE: ");
                        std.Id = Convert.ToInt32(Console.ReadLine());
                        if(!file.Delete(std.Id.ToString()))
                            Console.WriteLine("Not found !!");
                        break;
                    case 5:
                        Console.Write("Enter the Student Id for delete: ");
                        std.Id = Convert.ToInt32(Console.ReadLine());
                        if(!file.Search(std.Id.ToString(), std, typeof(Student)))
                            Console.WriteLine("Not found !!");
                        break;
                    default:
                        Console.WriteLine("Error Choice !!");
                        Console.Write("Enter Your Choice : ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        break;
                }
                Console.Write("Enter 99 to exit or any number to go ahead : ");
                x = Convert.ToInt32(Console.ReadLine());
            } while (x != 99);
        }

    }
}
