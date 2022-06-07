using JetBrains.Annotations;
using TeaFramework;
using Terraria.Localization;

namespace AdLibitum
{
    [UsedImplicitly]
    public sealed class AdLibitum : TeaMod
    {
        public static string GetTextValue(string key, params object[] objs)
        {
            return Language.GetTextValue("Mods.AdLibitum." + key, objs);
        }
    }
}