using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net.Http;
using Spamton;
using DialogHostAvalonia;
using Avalonia.Controls;
using Downloader;

namespace BW_Launcher.Models
{
    public class VersionsInstaller
    {
        private static void DownloadFileCompleted(string targetPath, string targetDir)
        {
            bool unpackOnLinux = false; // * idk some kind of debug

            if (unpackOnLinux || MainWindowModel.osId != 1)
            {
                Log.Information("Unpacking...");
                ZipFile.ExtractToDirectory(targetPath, targetDir);
                Log.Information("Unpacked!");
                DialogHost.Show(new TextBlock
                {
                    Text = $"Версия {targetPath} установлена!",
                    FontSize = 18,
                    Foreground = Avalonia.Media.Brushes.Green
                });
            }
        }

        [BIG_SHOT]
        public static async Task<int> DownloadAsync(string url, string id)
        {
            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 8, // Number of file parts, default is 1
                ParallelDownload = true // Download parts in parallel (default is false)
            };
            string targetDir = (MainWindowModel.osId == 1) ? "~/.b-world/versions/" : @"C:\Program Files\B-World\versions";
            string localFileName = id + ((MainWindowModel.osId == 1) ? ".AppImage" : ".zip");
            try
            {
                if (targetDir.StartsWith("~"))
                {
                    string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    targetDir = Path.Combine(home, targetDir.TrimStart('~', '/', '\\'));
                }

                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);

                string targetPath = Path.Combine(targetDir, localFileName);
                Log.Information($"Target path is {targetPath}");
                Log.Information($"target directory is {targetDir}");

                using var http = new HttpClient();
                using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                resp.EnsureSuccessStatusCode();

                Log.Information($"Response content length: {resp.Content.Headers.ContentLength} bytes");

                var downloader = new DownloadService(downloadOpt);
                await downloader.DownloadFileTaskAsync(url, targetPath);
                DownloadFileCompleted(targetPath, targetDir);
                /*using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await resp.Content.CopyToAsync(fs).ContinueWith(
                    (copytask) => {
                      fs.Dispose(); 
                      Log.Information($"Saved raw file to {targetPath}"); 
                      DownloadFileCompleted(targetPath, targetDir);
                    });*/
                //fs.Dispose();
                return 0;
            }
            catch (UnauthorizedAccessException)
            {
                Log.Error("Access denied.");
                return 2;
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return 3;
            }
        }
    }
}
