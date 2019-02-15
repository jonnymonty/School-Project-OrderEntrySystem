using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileMoverConsole
{
    public class Program
    {
        private static FileSystemWatcher giles;
        private static string targetDirectory = @"C:\EndLocation";
        private static string sourceDirectory = @"C:\StartLocation";
        static void Main(string[] args)
        {
            giles = new FileSystemWatcher(sourceDirectory);
            giles.EnableRaisingEvents = true;
            giles.Renamed += OnRename;
            giles.Created += OnCreate;
            giles.Changed += OnChange;
            Console.ReadLine();
        }

        public static void FileMove(FileSystemEventArgs f)
        {
            try
            {
                if (File.Exists(f.FullPath))
                {
                    Thread.Sleep(1000);

                    File.Move(f.FullPath, $"{targetDirectory}\\{f.Name}");
                    Console.WriteLine($"{f.FullPath} was moved to {targetDirectory}\\");
                    EventLog log = new EventLog();
                    log.Source = "Application";
                    log.WriteEntry($"{f.FullPath} was moved to {targetDirectory}\\", EventLogEntryType.Information);
                }
            }
            catch
            {
                Console.WriteLine($"{f.FullPath} was NOT moved to {targetDirectory}\\");

                EventLog log = new EventLog();
                log.Source = "Application";
                log.WriteEntry($"{f.FullPath} was NOT moved to {targetDirectory}\\", EventLogEntryType.Error);
            }
        }

        private static void OnRename(object sender, FileSystemEventArgs f)
        {

        }

        private static void OnCreate(object sender, FileSystemEventArgs f)
        {
            FileMove(f);
        }

        private static void OnChange(object sender, FileSystemEventArgs f)
        {
            FileMove(f);
        }
    }
}