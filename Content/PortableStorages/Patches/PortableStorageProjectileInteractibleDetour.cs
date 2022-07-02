using System.Linq;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Patches
{
    public class PortableStorageProjectileInteractibleDetour : Patch<PortableStorageProjectileInteractibleDetour.IsInteractable> {
        public delegate bool IsInteractable(Orig orig, Projectile self);
        public delegate bool Orig(Projectile self);

        public override MethodBase ModifiedMethod => typeof(Projectile).GetCachedMethod("IsInteractible");

        protected override IsInteractable PatchMethod => (orig, self) => {
            if (PortableStorageSystem.ModdedPortableStorages.Any(x => x.ProjId == self.type))
                return true;

            return orig(self);
        };
    }
}
