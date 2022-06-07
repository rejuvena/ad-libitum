using AdLibitum.Configuration.Server;
using JetBrains.Annotations;
using System.Linq;
using System.Reflection;
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
            // Calculate default max amount of buffs + any additional buffs that another mod inserts through a detour.
            // To clarify, subtracting 22 and the max amount of buff slots added by a mod through the ExtraPlayerBuffSlots negates the NORMAL limit.
            // These allows us to safely accomodate any mods that use detours to add additional buff slots for things like accessories.
            return (int) (StandardServerConfig.Config.MaxBuffSlots + orig() - 22 - ModLoader.Mods.Max(x => x.ExtraPlayerBuffSlots));
        };
    }
}