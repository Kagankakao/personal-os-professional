
using System;
using System.IO;

class Program
{
    static void Main() {        
        string[] tumDosyalar = Directory.GetFileSystemEntries(@"C:\");
        foreach (string dosya in tumDosyalar) {
            Console.WriteLine(File.GetAttributes(dosya) + " - " + dosya);
        }      
    }
}
