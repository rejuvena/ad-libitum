using System.Reflection;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using TeaFramework.Features.Patching;
using TeaFramework.Features.Utility;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.MaxBuffsOverride.Patches
{
    public static class UpdateMaxBuffsEdit
    {
        public static ILContext.Manipulator MakeEdit(Mod mod, string className) {
            return il => {
                ILCursor c = new(il);

                if (c.TryGotoNext(MoveType.After, x => x.MatchLdcI4(22))) {
                    c.Emit(OpCodes.Pop);
                    c.Emit(OpCodes.Call, typeof(Player).GetCachedMethod("get_MaxBuffs"));
                }
                else {
                    mod.Logger.Info($"Skipping application of IL patch \"{className}\" as user already has a client with the patch applied.");
                }
            };
        }

        [UsedImplicitly]
        public sealed class UpdateHungerBuffsMaxBuffsEdit : Patch<ILContext.Manipulator>
        {
            public override MethodInfo ModifiedMethod { get; } = typeof(Player).GetCachedMethod("UpdateHungerBuffs");

            protected override ILContext.Manipulator PatchMethod => MakeEdit(Mod, "UpdateHungerBuffsMaxBuffsEdit");
        }

        [UsedImplicitly]
        public sealed class UpdateStarvingStateMaxBuffsEdit : Patch<ILContext.Manipulator>
        {
            public override MethodInfo ModifiedMethod { get; } = typeof(Player).GetCachedMethod("UpdateStarvingState");

            protected override ILContext.Manipulator PatchMethod => MakeEdit(Mod, "UpdateStarvingStateMaxBuffsEdit");
        }
    }
}