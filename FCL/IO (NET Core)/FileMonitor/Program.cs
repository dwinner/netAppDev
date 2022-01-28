using System;
using System.IO;

FileSystemWatcher? watcher;

if (args is not { Length: 1 })
{
   Console.WriteLine("Enter the directory to watch markdown files: FileMonitor [directory]");
   return;
}

WatchFiles(args[0], "*.md");
Console.WriteLine("Press enter to stop watching");
Console.ReadLine();
UnWatchFiles();

void WatchFiles(string path, string filter)
{
   watcher = new FileSystemWatcher(path, filter)
   {
      IncludeSubdirectories = true
   };
   watcher.Created += OnFileChanged;
   watcher.Changed += OnFileChanged;
   watcher.Deleted += OnFileChanged;
   watcher.Renamed += OnFileRenamed;

   watcher.EnableRaisingEvents = true;
   Console.WriteLine("watching file changes...");
}

void UnWatchFiles()
{
   if (watcher == null)
   {
      throw new InvalidOperationException();
   }

   watcher.Created -= OnFileChanged;
   watcher.Changed -= OnFileChanged;
   watcher.Deleted -= OnFileChanged;
   watcher.Renamed -= OnFileRenamed;
   watcher.Dispose();
   watcher = null;
}

void OnFileRenamed(object sender, RenamedEventArgs e) =>
   Console.WriteLine($"file {e.OldName} {e.ChangeType} to {e.Name}");

void OnFileChanged(object sender, FileSystemEventArgs e) =>
   Console.WriteLine($"file {e.Name} {e.ChangeType}");