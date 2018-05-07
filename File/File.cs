using System;
using System.Collections.Generic;
using System.IO;

namespace File
{
    public class File
    {
        public string FileName { get; set; }

        public File(string fileName)
        {
            FileName = fileName;
        }

        public bool Create()
        {
            if (!System.IO.File.Exists(FileName))
            {
                System.IO.File.Create(FileName);
                return true;
            }
            return false;
        }

        public void Insert(Object obj, Type typeObj)
        {
            if (System.IO.File.Exists(FileName))
            {
                System.IO.StreamWriter file = System.IO.File.AppendText(FileName);
                int count = typeObj.GetProperties().Length;
                if (count == 0)
                {
                    Console.WriteLine("You Don't have any Property !");
                    return;
                }
                int i = 0;

                foreach (System.Reflection.PropertyInfo p in typeObj.GetProperties())
                {
                    string propValue = obj.GetType().GetProperty(p.Name).GetValue(obj, null).ToString();
                    if (string.IsNullOrEmpty(propValue))
                    {
                        Console.WriteLine(p.Name + " is Null !!");
                        return;
                    }
                    if (i == count - 1)
                        file.Write(propValue + Environment.NewLine);
                    else
                        file.Write(propValue + '|');

                    i++;
                }
                file.Close();
            }

        }
        private bool CheckList(string[] rows, System.Collections.Generic.List<string> popName)
        {
            if (popName.Count != rows.Length)
            {
                Console.WriteLine("You have problem in your Property !");
                return false;
            }

            if (rows.Length == 0)
            {
                Console.WriteLine("File Is Empty !");
                return false;
            }

            if (popName.Count == 0)
            {
                Console.WriteLine("You Don't have any Property !");
                return false;
            }
            return true;
        }

        public void Read(Object obj, Type typeObj)
        {
            if (System.IO.File.Exists(FileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(FileName))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] rows = line.Split("|".ToCharArray());
                        int count = typeObj.GetProperties().Length;
                        List<string> popName = new List<string>(count);

                        foreach (System.Reflection.PropertyInfo p in typeObj.GetProperties())
                        {
                            popName.Add(p.Name);
                        }

                        if (!CheckList(rows, popName)) return;

                        for (int i = 0; i < rows.Length; i++)
                        {
                            Console.WriteLine(popName[i] + " : " + rows[i]);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public bool Search(string text, Object obj, Type typeObj)
        {
            if (System.IO.File.Exists(FileName))
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(FileName))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] rows = line.Split("|".ToCharArray());
                        int count = typeObj.GetProperties().Length;
                        List<string> popName = new List<string>(count);

                        foreach (System.Reflection.PropertyInfo p in typeObj.GetProperties())
                        {
                            popName.Add(p.Name);
                        }
                        if (!CheckList(rows, popName)) return false;
                        if (rows[0] == text)
                        {
                            for (int i = 0; i < rows.Length; i++)
                            {
                                Console.WriteLine(popName[i] + " : " + rows[i]);
                            }
                            file.Close();
                            return true;
                            //break;
                        }
                    }
                   

                }
            }
            return false;
        }

        public bool Delete(string text)
        {
            if (System.IO.File.Exists(FileName))
            {
                using (StreamReader file = System.IO.File.OpenText(FileName))
                {
                    string oldData;
                    string newData = "";
                    while ((oldData = file.ReadLine()) != null)
                    {
                        string[] rows = oldData.Split("|".ToCharArray());
                        if (rows[0] != text)
                        {
                            newData += oldData + Environment.NewLine;
                        }
                    }
                    file.Close();

                    if (!string.IsNullOrEmpty(newData))
                        System.IO.File.WriteAllText(FileName, newData);

                }
                return true;
            }
            return false;
        }
        private bool ConvertType(Object obj, System.Reflection.PropertyInfo prop, Type propertyType, string value)
        {

            System.TypeCode typeCode = System.Type.GetTypeCode(propertyType);
            try
            {

                switch (typeCode)
                {
                    case TypeCode.Int32:
                        prop.SetValue(obj, Convert.ToInt32(value), null);
                        break;
                    case TypeCode.Int64:
                        prop.SetValue(obj, Convert.ToInt64(value), null);
                        break;
                    case TypeCode.Double:
                        prop.SetValue(obj, Convert.ToDouble(value), null);
                        break;
                    case TypeCode.Decimal:
                        prop.SetValue(obj, Convert.ToDecimal(value), null);
                        break;
                    case TypeCode.String:
                        prop.SetValue(obj, value, null);
                        break;
                    default:
                        prop.SetValue(obj, value, null);
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set property value for our Foreign Key");
            }

        }
        public bool Update(string text, Object obj, Type typeObj)
        {
                
            if (System.IO.File.Exists(FileName))
            {

                string newFile = "";
                string temp = "";
                string[] file = System.IO.File.ReadAllLines(FileName);
                int count = typeObj.GetProperties().Length;
                if (count == 0)
                {
                    Console.WriteLine("You Don't have any Property !");
                    return false;
                }


                Console.WriteLine("\n...Updated Time....");
                foreach (string line in file)
                {
                    string[] rows = line.Split("|".ToCharArray());
                    if (rows[0] == text)
                    {

                        int i = 0;
                        string tempp = "";
                        foreach (System.Reflection.PropertyInfo p in typeObj.GetProperties())
                        {
                            string propValue = obj.GetType().GetProperty(p.Name).GetValue(obj, null).ToString();
                            Console.WriteLine(propValue);
                            if (string.IsNullOrEmpty(propValue))
                            {
                                Console.WriteLine(p.Name + " is Null !!");
                                return false;
                            }
                            if (i == count - 1)
                                tempp += propValue + '\n';
                            else
                                tempp += propValue + '|';

                            i++;
                        }
                        temp = line.Replace(line, tempp);
                        newFile += (temp);
                        continue;
                    }
                    newFile += (line + '\n');
                }
                if (newFile.Length != 0)
                {
                    System.IO.File.WriteAllText(FileName, newFile.ToString());
                    Console.WriteLine("\n...Updated Done....");
                    return true;
                }
            }
            return false;
        }
    }
}
