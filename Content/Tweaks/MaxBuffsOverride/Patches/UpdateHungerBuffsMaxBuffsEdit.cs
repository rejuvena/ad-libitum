﻿using JetBrains.Annotations;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Features.Utility;
using Terraria;

namespace AdLibitum.Content.Tweaks.MaxBuffsOverride.Patches
{
    [UsedImplicitly]
    public sealed class UpdateHungerBuffsMaxBuffsEdit : Patch<ILContext.Manipulator>
    {
        public override MethodInfo ModifiedMethod { get; } = typeof(Player).GetCachedMethod("UpdateHungerBuffs");

        protected override ILContext.Manipulator PatchMethod =>
            il =>
            {
                ILCursor c = new(il);

                if (c.TryGotoNext(MoveType.After, x => x.MatchLdcI4(22)))
                {
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Call, typeof(Player).GetCachedMethod("get_MaxBuffs"));
                }
                else
                {
                    Mod.Logger.Info(
                        "Skipping application of IL patch \"UpdateHungerBuffsMaxBuffsEdit\" as user already has a client with the patch applied."
                    );
                }
            };
    }
}