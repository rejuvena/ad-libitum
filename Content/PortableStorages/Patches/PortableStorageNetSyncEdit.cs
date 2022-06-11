using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeaFramework.Features.Patching;
using TeaFramework.Features.Utility;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageNetSyncEdit : Patch<ILContext.Manipulator>
    {
        public override MethodInfo ModifiedMethod => typeof(Main).GetCachedMethod("TrySyncingMyPlayer");

        protected override ILContext.Manipulator PatchMethod => il =>
        {
            ILCursor c = new(il);

            FieldInfo fieldToMatch = typeof(Player).GetCachedField("voidLensChest");

            if (!c.TryGotoNext(instr => instr.Match(OpCodes.Ldfld, fieldToMatch)))
                throw new Exception(" ");

            c.EmitDelegate(() =>
            {
                bool shouldSync = false;

                foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                {
                    TrackedProjectileReference localRef = mps.GetTrackedProjRef(Main.LocalPlayer);
                    TrackedProjectileReference clientRef = mps.GetTrackedProjRef(Main.clientPlayer);

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
