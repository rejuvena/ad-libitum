using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System.Linq;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.Audio;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStoragePlayInteractiveProjectileOpenCloseDetour : Patch<PortableStoragePlayInteractiveProjectileOpenCloseDetour.PlayInteractiveProjectileOpenCloseSound> {
        public delegate void PlayInteractiveProjectileOpenCloseSound(Orig orig, int projType, bool open);
        public delegate void Orig(int projType, bool open);

        public override MethodBase ModifiedMethod => typeof(Main).GetCachedMethod(nameof(PlayInteractiveProjectileOpenCloseSound));

        protected override PlayInteractiveProjectileOpenCloseSound PatchMethod => (orig, projType, open) => {
            ModdedPortableStorage ps = PortableStorageSystem.ModdedPortableStorages.FirstOrDefault(x => x.ProjId == projType);
            
            if (ps is not null)
                SoundEngine.PlaySound(open ? ps.OpenSound : ps.CloseSound);

            orig(projType, open);
        };
    }
}
