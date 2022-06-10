using System.Linq;
using System.Reflection;
using AdLibitum.Configuration.Server;
using JetBrains.Annotations;
using TeaFramework.Features.Patching;
using TeaFramework.Features.Utility;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.MaxBuffsOverride.Patches
{
    [UsedImplicitly]
    public class MaxBuffsDetour : Patch<MaxBuffsDetour.MaxBuffs>
    {
        public delegate int MaxBuffs(Orig orig);

        public delegate int Orig();

        public override MethodInfo ModifiedMethod { get; } = typeof(Player).GetCachedProperty(nameof(MaxBuffs)).GetMethod;

        protected override MaxBuffs PatchMethod { get; } = orig => {
            if (!StandardServerConfig.Config.MaxBuffSlotsEnabled) return orig();

            // Objective: override the normal buff limit calculation to use our specialized calculations.
            // Justification:
            //   1. We cannot subtract (lower) the buff limit, so it cannot got below 22.
            //   2. tModLoader's calculation is based off of the largest Mod.ExtraPlayerBuffSlots value, and will promptly ignore ours if a mod increases it more.
            //   3. The buff limit is not configurable after mods are loaded, normally.

            // Reproduce the regular max buff count calculated by tModLoader.
            int normalMax = (int) (22 - ModLoader.Mods.Max(x => x.ExtraPlayerBuffSlots));

            // Get any additional slots from mods that also override the buff count, this is to support things like mods that want to add slots conditionally.
            int additionalSlots = orig() - normalMax;

            // Return the new limit, which is our config plus any additional mod buff slots not added through Mod.ExtraPlayerBuffSlots.
            return (int) (StandardServerConfig.Config.MaxBuffSlots + additionalSlots);
        };
    }
}