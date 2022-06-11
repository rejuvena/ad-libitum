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

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageTileInteractionEdit : Patch<ILContext.Manipulator>
    {
        public override MethodInfo ModifiedMethod => typeof(Player).GetCachedMethod("TileInteractionsUse");

        protected override ILContext.Manipulator PatchMethod => il =>
        {
            ILCursor c = new(il);

            MethodInfo trackedProjRefClear = typeof(TrackedProjectileReference).GetCachedMethod("Clear");

            if (!c.TryGotoNext(instr => instr.Match(OpCodes.Call, trackedProjRefClear)))
                throw new Exception("Error applying patch \"PortableStorageTileInteractionEdit\": Unable to match call instruction.");

            c.EmitDelegate(PortableStorageSystem.ClearModPortableStorages);
        };
    }
}
