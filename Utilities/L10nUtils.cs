using Terraria.Localization;

namespace AdLibitum.Utilities
{
    public static class L10nUtils
    {
        public static string GetModTextValue(string key) {
            return Language.GetTextValue("Mods.AdLibitum." + key);
        }

        public static string GetModTextValue(string key, params object[] substitutions) {
            return Language.GetTextValue("Mods.AdLibitum." + key, substitutions);
        }
    }
}
