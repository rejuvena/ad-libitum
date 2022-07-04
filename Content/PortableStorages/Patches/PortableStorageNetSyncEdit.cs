using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using MonoMod.Cil;
using System;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageNetSyncEdit : Patch<ILContext.Manipulator>
    {
        public override MethodBase ModifiedMethod => typeof(Main).GetCachedMethod("TrySyncingMyPlayer");

        protected override ILContext.Manipulator PatchMethod => il =>
        {
            ILCursor c = new(il);

            if (!c.TryGotoNext(instr => instr.MatchLdfld<Player>("voidLensChest")))
                throw new Exception("Error applying patch \"PortableStorageNetSyncEdit\": Unable to match ldfld instruction.");

            c.EmitDelegate(() =>
            {
                bool shouldSync = false;

                foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                {
                    TrackedProjectileReference localRef = mps.GetTrackedProjRef(Main.LocalPlayer).Value;
                    TrackedProjectileReference clientRef = mps.GetTrackedProjRef(Main.clientPlayer).Value;

                    if (localRef != clientRef)
                    {
                        shouldSync = true;
                        break;
                    }
                }

                if (shouldSync)
                    NetMessage.SendData(MessageID.SyncProjectileTrackers, number: Main.myPlayer);
            });
        };
    }
}
