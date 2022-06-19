using AdLibitum.Content.PortableStorages.Misc;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace AdLibitum.Content.PortableStorages.Patches
{
    public class PortableStorageHandleBeingInChestRangeEdit : Patch<ILContext.Manipulator>
    {
        public override MethodBase ModifiedMethod => typeof(Player).GetCachedMethod("HandleBeingInChestRange");

        protected override ILContext.Manipulator PatchMethod => (il) => {
            ILCursor c = new(il);

            if (!c.TryGotoNext(MoveType.After, instr => instr.MatchLdloc(0)))
                throw new Exception("Error applying patch \"PortableStorageHandleBeingInChestRangeEdit\": Unable to match stloc instruction.");
            
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<int, Player, int>>((loc0, self) => {
                int result = loc0;

                foreach (ModdedPortableStorage mps in PortableStorageSystem.ModdedPortableStorages)
                {
                    Ref<TrackedProjectileReference> projRef = mps.GetTrackedProjRef(self);
                    int projIndex = projRef.Value.ProjectileLocalIndex;
                    
                    if (projIndex >= 0)
                    {
                        result = 1;

                        if (!Main.projectile[projIndex].active || Main.projectile[projIndex].type != mps.ProjId)
                        {
                            SoundEngine.PlaySound(in SoundID.Item130);
                            self.chest = -1;
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
                                    SoundEngine.PlaySound(in SoundID.Item130);
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
