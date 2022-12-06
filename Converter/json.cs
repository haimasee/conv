using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace converter6practa
{
    internal class json
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Открыть файл. ");
            string openPath = Console.ReadLine();
            Console.Clear();
            File.WriteAllText(openPath, Editor.Edit(MyConverter.ConvertToText(openPath)));
            Console.Clear();
            Console.WriteLine("Сохранить путь до файла.");
            string putDlyaSohranenia = Console.ReadLine();
            MyConverter.SaveFile(openPath, putDlyaSohranenia);
            Console.Clear();
            Console.WriteLine("Сохранено");

        }
    }
}