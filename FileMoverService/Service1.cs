using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace FileMoverService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private static FileSystemWatcher giles;
        private static string targetDirectory = @"C:\EndLocation";
        private static string sourceDirectory = @"C:\StartLocation";

        protected override void OnStart(string[] args)
        {
            EventLog log = new EventLog();
            log.Source = "Application";
            log.WriteEntry($"The Service was started.", EventLogEntryType.Information);

            giles = new FileSystemWatcher(sourceDirectory);
            giles.EnableRaisingEvents = true;
            giles.Renamed += OnRename;
            giles.Created += OnCreate;
            giles.Changed += OnChange;
            Console.ReadLine();
        }

        protected override void OnStop()
        {
            EventLog log = new EventLog();
            log.Source = "Application";
            log.WriteEntry($"The Service was stopped.", EventLogEntryType.Information);
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
