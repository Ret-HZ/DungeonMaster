using System.Reflection;

namespace DungeonMaster
{
    internal static class Util
    {
        internal static string GetAppVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return version;
        }
    }
}
