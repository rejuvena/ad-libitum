using AdLibitum.Content.PortableStorages.Misc;
using Microsoft.Xna.Framework;
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
    public class PortableStorageHandleBeingInChestRangeEdit : Patch<ILContext.Manipulator>
    {
        public override MethodBase ModifiedMethod => typeof(Player).GetCachedMethod("HandleBeingInChestRange");

        protected override ILContext.Manipulator PatchMethod => (il) => {
            ILCursor c = new(il);

            if (!c.TryGotoNext(MoveType.After, instr => instr.MatchLdloc(0)))
                throw new Exception("Error applying patch \"PortableStorageHandleBeingInChestRangeEdit\": Unable to match ldloc instruction.");
            
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<bool, Player, bool>>((loc0, self) => {
                bool result = loc0;

                foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                {
                    Ref<TrackedProjectileReference> projRef = mps.GetTrackedProjRef(self);
                    int projIndex = projRef.Value.ProjectileLocalIndex;
                    
                    if (projIndex >= 0)
                    {
                        result = true;

                        if (!Main.projectile[projIndex].active || Main.projectile[projIndex].type != mps.ProjId)
                        {
                            Main.PlayInteractiveProjectileOpenCloseSound(Main.projectile[projIndex].type, open: false); self.chest = -1;
                            Recipe.FindRecipes();
                        }
                        else
                        {
                            int centerX = (int) ((self.position.X + self.width * 0.5) / 16.0);
                            int centerY = (int) ((self.position.Y + self.height * 0.5) / 16.0);
                            Vector2 vector2 = Main.projectile[projIndex].Hitbox.ClosestPointInRect(self.Center);
                            self.chestX = (int) vector2.X / 16;
                            self.chestY = (int) vector2.Y / 16;
                            if (centerX < self.chestX - Player.tileRangeX || centerX > self.chestX + Player.tileRangeX + 1 || centerY < self.chestY - Player.tileRangeY || centerY > self.chestY + Player.tileRangeY + 1)
                            {
                                if (self.chest != -1)
                                {
                                    Main.PlayInteractiveProjectileOpenCloseSound(Main.projectile[projIndex].type, open: false);
                                }
                                self.chest = -1;
                                Recipe.FindRecipes();
                            }
                        }
                    }
                }

                return result;
            });
        };
    }
}
