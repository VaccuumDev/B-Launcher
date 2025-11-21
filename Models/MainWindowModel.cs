
using System.Collections.Generic;
using Spamton;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace BW_Launcher.Models
{
  public class MainWindowModel
  {
    const string JsonURL = "http://192.168.1.8:8080/versions.json"; // TODO ПОДНЯТЬ СЕРВАК
    public static sbyte osId { get; } = GetOS();

    /*[BIG_SHOT]
    static BW_Launcher.Models.Version GetVersion()
    {
        return JSONProcesser.GetRemoteJsonAsync(JsonURL).Result;
    }*/

    public static List<BW_Launcher.Models.Version> versionsList = GetVersionsList();

    [BIG_SHOT]
    public static List<BW_Launcher.Models.Version> GetVersionsList()
    {
      List<BW_Launcher.Models.Version> output = JSONProcesser.GetRemoteJsonAsync(JsonURL).Result;

      ////for (byte i=0;i<versionsAmount;i++) тут надо шото нормальное делатии, а то не робит
      ////{
      ////    output[i] = GetVersion();
      ////}

      if (output == null)
      {
        Log.Error("Versions list is null");
        return new List<BW_Launcher.Models.Version>();
      }
      else
        Log.Debug($"versions count: {output.Count}");
      return output;
    }

    [BIG_SHOT]
    public static ObservableCollection<string> GetVersionsDisplayNames()
    {
      ObservableCollection<string> output = new ObservableCollection<string>();

      for (byte i = 0; i < versionsList.Count; i++)
      {
        output.Add(versionsList[i].name);
      }

      Log.Information($"Latest version is {output[0]}");

      return output;
      //return new ObservableCollection<string>() {"DEBUG"};
    }

    /*
        1 - Linux
        0 - Windows
    */
    [BIG_SHOT]
    public static sbyte GetOS()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Log.Information("Linux user detected!");
        return 1;
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        Log.Information("Microsoft Spyware user detected!");
        return 0;
      }
      else return -1;
    }
  }
}