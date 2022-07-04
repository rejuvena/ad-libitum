using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System.Linq;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageGetContainerIndexDetour : Patch<PortableStorageGetContainerIndexDetour.TryGetContainerIndex>
    {
        public delegate bool TryGetContainerIndex(Orig orig, Projectile self, out int containerIndex);
        public delegate bool Orig(Projectile self, out int containerIndex);

        public override MethodBase ModifiedMethod => typeof(Projectile).GetCachedMethod("TryGetContainerIndex");

        protected override TryGetContainerIndex PatchMethod => (Orig orig, Projectile self, out int containerIndex) => {
            bool ret = orig(self, out containerIndex);

            ModdedPortableStorage mps = PortableStorageSystem.ModdedPortableStorages.FirstOrDefault(x => x.ProjId == self.type);
            if (mps != default)
            {
                containerIndex = mps.ChestId;
                ret = true;
            }

            return ret;
        };
    }
}
