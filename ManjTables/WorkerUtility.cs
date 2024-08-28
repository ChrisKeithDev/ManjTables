using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using System.Globalization;

namespace ManjTables
{
    public class WorkerUtility
    {
        private readonly ILogger<WorkerUtility> _logger;
        private const int maxAttempts = 5;
        private const int delay = 5000;

        public WorkerUtility(ILogger<WorkerUtility> logger)
        {
            _logger = logger;
        }

        public static string GetControlLogPath()
        {
            return @"D:\Users\Local Admin\ManjApps\Database Apps\ManjDbSuite\Logs\Db Logs";
        }


        public static FileSystemWatcher SetupFileSystemWatcher(string path)
        {
            FileSystemWatcher fileSystemWatcher = new()
            {
                Path = path,
                Filter = "ControlLogFile.log",
                NotifyFilter = NotifyFilters.LastWrite
            };
            return fileSystemWatcher;
        }

        public string ReadDatabasePath(string filePath)
        {
            string controlLogContents = File.ReadAllText(filePath);
            _logger.LogInformation("{Now} Control log contents: {controlLogContents}", DateTime.Now, controlLogContents);
            return controlLogContents.Split("DBPath: ")[1].Trim();
        }

        public void LogDatabaseChange(string dbPath)
        {
            _logger.LogInformation("{Now} Database path changed: {dbPath}", DateTime.Now, dbPath);
            lock (new object())
            {
                File.WriteAllText(@"D:\Users\Local Admin\ManjApps\Database Apps\ManjDbSuite\Logs\Tables Logs\EventReceived.txt", $"Database changed!\nDB Path: {dbPath}");
            }
        }

        public void HandleEmptyChildInfoTable()
        {
            _logger.LogInformation("{Now} Table ChildInfos does not exist in the database or is empty.", DateTime.Now);
            File.AppendAllText(@"D:\Users\Local Admin\ManjApps\Database Apps\ManjDbSuite\Logs\Tables Logs\EventReceived.txt", "\nTable ChildInfos does not exist in the database or is empty.");
        }

        public void ProcessChildInfo(List<ChildInfo> childInfos)
        {
            if (childInfos.Count > 0)
            {
                var childInfo = childInfos[0];
                _logger.LogInformation("Processing ChildInfo for: {FirstName} {LastName}", childInfo.FirstName, childInfo.LastName);
            }
            _logger.LogInformation("Processed ChildInfo for {childInfos.Count} children", childInfos.Count);
        }

        public static void CopyDbFilesToNewFolder()
        {
            string lockFilePath = @"O:\Apps From Chris\Data\ManjTables Data\db.lock";
            string sourceFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            string destinationFolder = @"\\MANJDATTO\Office\Apps From Chris\Data\ManjTables Data";

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Data folder not found.");
                return;
            }

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            int attempts = 0;
            while (File.Exists(lockFilePath) && attempts < maxAttempts)
            {
                Console.WriteLine($"Database is locked. Waiting for {delay / 1000} seconds before retrying...");
                Thread.Sleep(delay);
                attempts++;
            }

            if (File.Exists(lockFilePath))
            {
                Console.WriteLine("Failed to copy database files after maximum attempts due to lock.");
                return;
            }

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destinationFolder, fileName);
                    File.Copy(file, destFile, true);
                    Console.WriteLine($"Copied {fileName} to {destinationFolder}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Failed to copy {file}: {ex.Message}");
                }
            }
        }

        public static string HandleCommonAbbreviations(string input)
        {
            Dictionary<string, string> commonAbbreviations = new()
            {
                {"ln", "Lane"},
                {"st", "Street"},
                {"rd", "Road"},
                {"hwy", "Highway"},
                {"ave", "Avenue"},
                {"av", "Avenue"},
                {"dr", "Drive"},
                {"ct", "Court"},
                {"pl", "Place"},
                {"cir", "Circle"},
                {"n", "North"},
                {"e", "East"},
                {"s", "South"},
                {"w", "West"}
            };

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].ToLower().TrimEnd('.');
                if (commonAbbreviations.ContainsKey(word))
                {
                    words[i] = commonAbbreviations[word];
                }
            }
            return string.Join(" ", words);
        }

        public static string RemoveExtraCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            // Remove periods and commas
            input = input.Replace(".", "").Replace(",", "");

            // Remove extra spaces
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            input = string.Join(" ", words);

            return input.Trim();
        }

        public static string ToTitleCaseAndTrim(string? input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(input?.Trim().ToLower() ?? "");
        }

        public static string ToUpperCaseAndTrim(string? input)
        {
            return input?.Trim().ToUpper() ?? "";
        }
    }
}
