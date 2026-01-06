
using System;
using System.IO;

class Program
{
    static void Main() {        
        string path = "C:\\Klasor\\readMe.txt";
        FileInfo dirInfo = new FileInfo(path);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
            Console.WriteLine("Klasör oluşturuldu: " + path);
        }
        else
        {
            Console.WriteLine("Klasör yolu: " + path);
        }
        System.Console.WriteLine("Dosya adı:" + dirInfo.Attributes);
        Console.WriteLine("Dosya uzantısı:" + dirInfo.Extension);
        System.Console.WriteLine("tam adı:" + dirInfo.FullName);
        System.Console.WriteLine("oluşturulma tarihi:" + dirInfo.CreationTime);
        System.Console.WriteLine("son erişim tarihi:" + dirInfo.LastAccessTime);
        System.Console.WriteLine("Üst dizin:" + dirInfo.DirectoryName);
        System.Console.WriteLine("root:" + dirInfo.Directory.Root);

    }
}
