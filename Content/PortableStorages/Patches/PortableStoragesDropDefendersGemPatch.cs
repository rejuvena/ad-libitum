using AdLibitum.Configuration.Server;
using AdLibitum.Content.PortableStorages.Items;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStoragesDropDefendersGemPatch : Patch<ILContext.Manipulator>
    {
        public override MethodBase ModifiedMethod => typeof(DD2Event).GetCachedMethod("DropMedals");

        protected override ILContext.Manipulator PatchMethod => (il) => {
            ILCursor c = new(il);

            if (!c.TryGotoNext(x => x.MatchCallvirt<NPC>("DropItemInstanced")))
                throw new Exception("Error applying patch \"PortableStoragesDropDefendersGemPatch\": Unable to match call instruction.");

            c.Emit(OpCodes.Ldloc_0);
            c.EmitDelegate<Action<int>>((i) => {
                // - Drop only after OOA t2
                // - Don't drop if the config is disabled
                if (!NPC.downedMechBossAny || !StandardServerConfig.Config.ItemToggles.PortableStorages)
                    return;

                NPC npc = Main.npc[i];
                npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<DefendersGem>());
            });
        };
    }
}
