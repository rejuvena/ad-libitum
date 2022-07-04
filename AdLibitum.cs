using JetBrains.Annotations;
using TeaFramework;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AdLibitum
{
    [UsedImplicitly]
    public sealed class AdLibitum : TeaMod
    {
        public static AdLibitum Instance => ModContent.GetInstance<AdLibitum>();
        
        public static string GetTextValue(string key, params object[] objs) {
            return Language.GetTextValue("Mods.AdLibitum." + key, objs);
        }
    }
}