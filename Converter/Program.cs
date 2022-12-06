using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Serialization;

namespace converter6practa
{
    public class Editor
    {
        public static string Edit(string text) //Констрункция выбора
        {
            string[] lines = text.Replace(" ", "").Split("\n");
            int pos = 1;
            int ex = 0;
            while (ex != 1)
            {
                Console.Clear();
                Console.WriteLine("Нажмите F1, чтобы сохранить");
                int max = 0;
                foreach (string line in lines)
                {
                    Console.WriteLine("   " + line);
                    max += 1;
                }
                Console.SetCursorPosition(0, pos); //Создаем курсор
                Console.Write("=>");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        for (int i = 0; i < lines[pos - 1].Length + 3; i++)
                            Console.Write(" ");
                        Console.SetCursorPosition(3, pos);
                        lines[pos - 1] = Console.ReadLine();
                        break;
                    case ConsoleKey.DownArrow:
                        if (pos == max)
                            pos = 1;
                        else
                            pos++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos == 1)
                            pos = max;
                        else
                            pos--;
                        break;
                    case ConsoleKey.F1:
                        ex++;
                        break;
                }
            }
            return String.Join("\n", lines);
        }
    }
    public class Figura
    {
        public string name;
        public int width;
        public int height;
        public Figura()
        {

        }
        public Figura(string Name, int Height, int Width)
        {
            name = Name;
            height = Height;
            width = Width;
        }
    }
    public class MyConverter //Конвертер
    {
        private static List<Figura> ConvertToObject(string file) //Путь файла
        {
            List<Figura> figury = new List<Figura>();
            if (file.Contains(".xml")) //Содержание файла конвертируется в xml
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    figury = (List<Figura>)xml.Deserialize(fs);
                }
            }
            if (file.Contains(".json")) //Содержание файла конвертируется в json
            {
                figury = JsonConvert.DeserializeObject<List<Figura>>(File.ReadAllText(file));
            }

            if (file.Contains(".txt")) //проверяет, содержится ли элемент в коллекции
            {
                string[] linii = File.ReadAllLines(file); //Файл, открываемый для чтения.
                for (int i = 0; i < linii.GetLength(0); i = i + 3)
                {
                    Figura figura = new Figura();
                    if (i != linii.GetLength(0))
                    {
                        figura.name = linii[i];
                    }
                    else break;
                    if (i + 1 != linii.GetLength(0)) //Возвращает количество элементов в первом измерении объекта Array. 
                    {
                        figura.width = Convert.ToInt32(linii[i + 1]);

                    }
                    else break;
                    if (i + 2 != linii.GetLength(0))
                    {
                        figura.height = Convert.ToInt32(linii[i + 2]);
                    }
                    else break;
                    figury.Add(figura);
                }
            }
            return figury;
        }
        public static string ConvertToText(string file) //Преобразование
        {
            string text = "";
            List<Figura> figury = ConvertToObject(file);
            for (int i = 0; i < figury.Count(); i++)
            {
                text = text + figury[i].name + "\n";
                text = text + figury[i].height + "\n";
                text = text + figury[i].width + "\n";
            }
            return text;
        }
        public static void SaveFile(string oldFile, string newFile)
        {
            List<Figura> figury = ConvertToObject(oldFile);
            if (newFile.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(newFile, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, figury);
                }
            }
            if (newFile.Contains(".json"))
            { 
                File.WriteAllText(newFile, JsonConvert.SerializeObject(figury)); //pаписывает указанную строку в этот файл
            }
            if (newFile.Contains(".txt"))
            {
                File.WriteAllText(newFile, ConvertToText(newFile)); //pаписывает указанную строку в этот файл
            }
        }
    }
}

//n - перенос строки
//GetLength(0) - возвращает количество элементов в первом измерении объекта Array. Этот метод является операцией O(1).
//WriteAllText - Создает новый файл, записывает указанную строку в этот файл, используя заданную кодировку, и затем закрывает файл.
//Contains - проверяет, содержится ли элемент в коллекции, возвращает булевскую величину.