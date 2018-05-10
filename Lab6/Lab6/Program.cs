using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        interface IOyedTools
        {
            void GetOyed(Directory dirB);
        }
        interface IPerTools
        {
            void GetPer(Directory dirB);
        }
        interface IDirTools
        {
            bool Contains(File file);
        }
        private class File
        {

            private String filename;
            private int Data;
            public File()
            {
                Console.WriteLine("input filename");
                SetFileName();
                Console.WriteLine("input data");
                SetFileData();
            }//запилить конструктор по умолчанию
            public File(string name, int data)
            {
                SetFileName(name);
                SetFileData(data);
            }
            public string GetFileName()
            {
                return this.filename;
            }
            public int GetFileData()
            {
                return this.Data;
            }
           void SetFileName()
            {
                string newFileName = Console.ReadLine();
                this.filename = newFileName;
            }
            void SetFileData()
            {
                int newData = Convert.ToInt32(Console.ReadLine());
                this.Data = newData;
            }
            void SetFileName(string newName)
            {
                this.filename = newName;
            }
            void SetFileData(int newData)
            {
                this.Data = newData;
            }
        }
        class Directory:IOyedTools, IPerTools, IDirTools 
        {
             private  File[] Files;
            public int Length
            {
                get
                {
                    return Files.Length;
                }
                
            }
            public Directory()
            {
                Files = new File[3];
            }
            public Directory(string a = "def")
            {
                Files = new File[0];
            }
            public File[] GetFiles()
            {
                return Files;
            }
           public void AddNewFile()
           {
                File newFile = new File();
                int dirLength = Files.Length;
                Array.Resize(ref Files, dirLength + 1);
                Files[dirLength] = newFile;
            }
            public void AddFile(File file)
            {
                int dirLength = Files.Length;
                Array.Resize(ref Files, dirLength + 1);
                Files[dirLength] = file;
            }
           public bool Contains(File file)
            {
                for (int i = 0; i < Files.Length; i++)
                {

                    if (Files[i].GetFileName()==(file.GetFileName()))
                    {
                        return true;
                    }
                }
                return false;
            }
            public void  GetShiet()
            {
                Console.Write("{");
                for (int i = 0; i < Files.Length; i++)
                {
                    Console.Write(Files[i].GetFileName());
                    Console.Write(",");
                    Console.Write(Files[i].GetFileData());
                    Console.Write(";");
                }
                Console.WriteLine("}");
            }
            public void GetShiet(string a= "mod")
            {
                Console.Write("{");
                for (int i = 0; i < Files.Length; i++)
                {
                    Console.Write(Files[i].GetFileName());
                    Console.Write(";");
                }
                Console.WriteLine("}");
            }
            public void GetPer(Directory dirB)//при Б>А вылетает 
            {
                Directory Per = new Directory("def");
                for (int i = 0; i < dirB.Length; i++)
                {
                    if (Contains(dirB.GetFiles()[i]))
                    {
                        Per.AddFile(dirB.GetFiles()[i]);
                    }
                }
                    Per.GetShiet("mod");
                    Per = null;
            }
            public void GetOyed(Directory dirB)
            {
               Directory Oyed = new Directory("def");
                for (int i = 0; i < Files.Length; i++)
                {
                    Oyed.AddFile(Files[i]);
                }
                for(int i = 0; i < dirB.Length; i++)
                {
                    if (Contains(dirB.GetFiles()[i]))
                    {
                        Oyed.AddFile(dirB.GetFiles()[i]);
                    }
                    if (!Contains(dirB.GetFiles()[i]))
                    {
                        Oyed.AddFile(dirB.GetFiles()[i]);
                    }
                }
                Oyed.GetShiet("mod");
            }
            public void CleanShiet()
            {
                for (int i = 0; i <Files.Length;i++)
                {
                    if (Files[i].GetFileData() < 15)
                    {
                        for (int j = i; j != Files.Length-1;j++)
                        {
                            Files[j] = Files[j + 1];
                        }
                        Array.Resize(ref Files, Files.Length-1);
                        i--;
                    }
                }
            }
            
        }
        class DefaultDir : Directory
        {
            public DefaultDir()
            {
                GetFiles()[0] = new File("alpha", 1);
                GetFiles()[1] = new File("beta", 2);
                GetFiles()[2] = new File("gamma", 3);
                Console.WriteLine("Создана новая папка с файлами по умолчанию ");
            }
        }
        static void Main(string[] args)
        {
            Directory directoryA = null;
            Directory directoryB = null;
            DefaultDir mnozh = new DefaultDir();
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "adda":
                        if (directoryA != null)
                        {
                            directoryA.AddNewFile();
                        }
                        else
                        {
                            Console.WriteLine("Создана новая пустая папка А");
                            directoryA = new Directory("def");
                        }
                        break;
                    case "addb":
                        if (directoryB!= null)
                        {
                            directoryB.AddNewFile();
                        }
                        else
                        {
                            Console.WriteLine("Создана новая пустая папка Б");
                            directoryB = new Directory("def");
                        }
                        break;
                    case "showa":
                        if (directoryA!= null)
                        {
                            Console.WriteLine("множество А");
                            directoryA.GetShiet();
                        }
                        break;
                    case "showb":
                        if (directoryB!=null)
                        {
                            Console.WriteLine("множество Б");
                            directoryB.GetShiet();
                        }
                        break;
                    case "per":
                        Console.WriteLine("Пересечение");
                        if (directoryB.Length > directoryA.Length)
                        {
                            directoryB.GetPer(directoryA);
                        }
                        else
                        {
                            directoryA.GetPer(directoryB);
                        }
                        break;
                    case "oyed":
                        Console.WriteLine("Объединеие");
                        directoryA.GetOyed(directoryB);
                        break;
                    case "perm":
                        Console.WriteLine("Пересечение");
                        mnozh.GetPer(directoryA);
                        break;
                    case "oyedm":
                        Console.WriteLine("Объединеие");
                        mnozh.GetOyed(directoryA);
                        break;
                    case "showm":
                        Console.WriteLine("Множество ");
                        mnozh.GetShiet();
                        break;
                    case "cleana":
                        Console.WriteLine("Очистка");
                        directoryA.CleanShiet();
                        break;
                    default:
                        Console.WriteLine("wrong command\n");
                        break;
                }
            }
        }
    }
}