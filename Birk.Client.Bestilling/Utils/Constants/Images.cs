namespace Birk.Client.Bestilling.Utils.Constants
{
    public static class Images
    {
        private static string GetPath(string basePath, bool? isDark)
        {
            string color = isDark == null ? string.Empty : isDark == true ? "Dark" : isDark == false ? "White" : "";
            return $"{basePath}{color}.svg";
        }

        public static string GetArrowPath(string fileName, bool? isDark = false)
        {
            return GetPath(Path.Combine("images", "UI", "Arrows", fileName), isDark);
        }

        public static string GetMainPath(string fileName, string extraPath = null, bool? isDark = false)
        {
            return extraPath == null
                ? GetPath(Path.Combine("images", "Main", fileName), isDark)
                : GetPath(Path.Combine("images", "Main", extraPath, fileName), isDark);
        }

        public static string GetEventCallbackWindowPath(string fileName, bool? isDark = false)
        {
            return GetPath(Path.Combine("images", "EventCallbackWindow", fileName), isDark);
        }

        public static string GetCommonPath(string fileName, bool? isDark = false)
        {
            return GetPath(Path.Combine("images", "Common", fileName), isDark);
        }
    }
}