﻿using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageHandleBeingInChestRangeDetour : Patch<PortableStorageHandleBeingInChestRangeDetour.HandleBeingInChestRange> {
        public delegate void HandleBeingInChestRange(Orig orig, Player self);
        public delegate void Orig(Player self);

        public override MethodBase ModifiedMethod => typeof(Player).GetCachedMethod(nameof(HandleBeingInChestRange));

            protected override HandleBeingInChestRange PatchMethod => (orig, self) =>
            {
                if (self.chest != -1)
                {
                    foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                    {
                        if (self.chest != mps.ChestId)
                            mps.GetTrackedProjRef(self).Value.Clear();
                    }
                }
            
                orig(self);
            };
    }
}
