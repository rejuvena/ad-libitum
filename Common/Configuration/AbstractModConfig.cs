using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AdLibitum.Common.Configuration
{
    public abstract class AbstractModConfig<T> : ModConfig
        where T : ModConfig
    {
        public static T Config => ModContent.GetInstance<T>();
    }
}