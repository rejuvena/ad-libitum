using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeaFramework.Features.Patching;
using TeaFramework.Features.Utility;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageHandleBeingInChestRangeDetour : Patch<PortableStorageHandleBeingInChestRangeDetour.HandleBeingInChestRange> {
        public delegate void HandleBeingInChestRange(Orig orig, Player self);
        public delegate void Orig(Player self);

        public override MethodInfo ModifiedMethod => typeof(Player).GetCachedMethod("HandleBeingInChestRange");

        protected override HandleBeingInChestRange PatchMethod => (orig, self) =>
        {
            if (self.chest != -1)
            {
                foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                {
                    if (self.chest != mps.ChestId)
                        mps.GetTrackedProjRef(self).Clear();
                }
            }
            
            orig(self);
        };
    }
}
