using JetBrains.Annotations;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.DataStructures;

namespace AdLibitum.Content.PortableStorages.Patches
{
    [UsedImplicitly]
    public class PortableStorageTileInteractionEdit : Patch<ILContext.Manipulator>
    {   
        public override MethodBase ModifiedMethod => typeof(Player).GetCachedMethod("TileInteractionsUse");

        protected override ILContext.Manipulator PatchMethod => il =>
        {
            ILCursor c = new(il);

            if (!c.TryGotoNext(instr => instr.MatchCall<TrackedProjectileReference>("Clear")))
                throw new Exception("Error applying patch \"PortableStorageTileInteractionEdit\": Unable to match call instruction.");
            
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Call, typeof(PortableStorageSystem).GetCachedMethod("ClearModPortableStorages"));
        };
    }
}
