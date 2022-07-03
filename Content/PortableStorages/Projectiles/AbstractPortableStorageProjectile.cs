using AdLibitum.Content.PortableStorages.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using TeaFramework.Features.ID;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AdLibitum.Content.PortableStorages.Projectiles
{
    public abstract class AbstractPortableStorageProjectile : ModProjectile
    {
        public ModdedPortableStorage MyPortableStorage => PortableStorageSystem.ModdedPortableStorages.First(x => x.ProjId == Projectile.type);

        public override void PostDraw(Color lightColor) {
            int context = TryInteractingWithPortableStorage();
            
            if (context == 0)
                return;

            int num385 = (lightColor.R + lightColor.G + lightColor.B) / 3;
            if (num385 > 10)
            {
                Color selectionGlowColor = Colors.GetSelectionGlowColor(context == 2, num385);
                float num138 = (TextureAssets.Projectile[Projectile.type].Width() - Projectile.width) * 0.5f + Projectile.width * 0.5f;
                int num137 = 0;
                int num136 = 0;
                int num379 = TextureAssets.Projectile[Projectile.type].Height() / Main.projFrames[Projectile.type];
                int y27 = num379 * Projectile.frame;

                SpriteEffects spriteEffects = SpriteEffects.None;
                if (Projectile.spriteDirection == -1)
                    spriteEffects = SpriteEffects.FlipHorizontally;

                Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture + "_HoverOutline", AssetRequestMode.ImmediateLoad).Value,
                    new Vector2(Projectile.position.X - Main.screenPosition.X + num138 + num137,
                    Projectile.position.Y - Main.screenPosition.Y + (Projectile.height / 2) + Projectile.gfxOffY),
                    new Rectangle(0, y27, TextureAssets.Projectile[Projectile.type].Width(), num379 - 1), selectionGlowColor,
                    Projectile.rotation, new Vector2(num138, Projectile.height / 2 + num136), 1f, spriteEffects, 0);
            }
        }

        private int TryInteractingWithPortableStorage() {
            if (Main.gamePaused || Main.gameMenu)
                return 0;

            bool flag = !Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad;
            Player localPlayer = Main.LocalPlayer;
            Point point = Projectile.Center.ToTileCoordinates();
            Vector2 compareSpot = localPlayer.Center;
            Matrix matrix = Matrix.Invert(Main.GameViewMatrix.ZoomMatrix);
            Vector2 position = Main.ReverseGravitySupport(Main.MouseScreen);
            Vector2.Transform(Main.screenPosition, matrix);
            Vector2 v = Vector2.Transform(position, matrix) + Main.screenPosition;
            bool flag2 = Projectile.Hitbox.Contains(v.ToPoint());

            if (!((flag2 || Main.SmartInteractProj == Projectile.whoAmI) & !localPlayer.lastMouseInterface))
            {
                if (!flag)
                    return 1;
                return 0;
            }

            Main.HasInteractibleObjectThatIsNotATile = true;

            if (flag2)
            {
                localPlayer.noThrow = 2;
                localPlayer.cursorItemIconEnabled = true;
                localPlayer.cursorItemIconID = MyPortableStorage.CursorItem;
            }

            if (PlayerInput.UsingGamepad)
                localPlayer.GamepadEnableGrappleCooldown();

            if (Main.mouseRight && Main.mouseRightRelease && Player.BlockInteractionWithProjectiles == 0)
            {
                Main.mouseRightRelease = false;
                localPlayer.tileInteractAttempted = true;
                localPlayer.tileInteractionHappened = true;
                localPlayer.releaseUseTile = false;

                if (localPlayer.chest == MyPortableStorage.ChestId)
                {
                    localPlayer.chest = BankID.None;
                    Main.PlayInteractiveProjectileOpenCloseSound(Projectile.type, open: false);
                    Recipe.FindRecipes();
                }
                else
                {
                    localPlayer.chest = MyPortableStorage.ChestId;

                    for (int i = 0; i < 40; i++)
                    {
                        ItemSlot.SetGlow(i, -1f, chest: true);
                    }

                    MyPortableStorage.GetTrackedProjRef(localPlayer).Value.Set(Projectile);
                    localPlayer.chestX = point.X;
                    localPlayer.chestY = point.Y;
                    localPlayer.SetTalkNPC(-1);
                    Main.SetNPCShopIndex(0);
                    Main.playerInventory = true;
                    Main.PlayInteractiveProjectileOpenCloseSound(Projectile.type, open: true);
                    Recipe.FindRecipes();
                }
            }

            if (!Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad)
                return 0;

            if (!flag)
                return 2;

            return 0;
        }
    }
}
