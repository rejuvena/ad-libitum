﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using TeaFramework.Features.ID;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Projectiles
{
    public class EtherealChest : AbstractPortableStorageProjectile
    {
        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 3;
        }

        public override void SetDefaults() {
            Projectile.width = 32;
            Projectile.height = 36;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }

        public override void AI() {
            int frameThreshold = 4;

            if (Main.player[Projectile.owner].chest == BankID.DefendersForge)
            {
                if (++Projectile.frameCounter % frameThreshold == 0)
                    Projectile.frame++;

                Projectile.frameCounter = Math.Min(Projectile.frameCounter, 9);
            }
            else
            {
                if (--Projectile.frameCounter % frameThreshold == 0)
                    Projectile.frame--;

                Projectile.frameCounter = Math.Max(Projectile.frameCounter, 0);
            }

            Projectile.frame = (int) MathHelper.Clamp(Projectile.frame, 0, 2);

            base.AI();

            if (Projectile.ai[0] == 0f)
            {
                if ((double) Projectile.velocity.Length() < 0.1)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.velocity.Y = 0f;
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 45f;
                    return;
                }
                Projectile.velocity *= 0.94f;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.direction = -1;
                }
                else
                {
                    Projectile.direction = 1;
                }
                Projectile.spriteDirection = Projectile.direction;
                return;
            }

            if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
                Projectile.direction = -1;
            else
                Projectile.direction = 1;
            Projectile.spriteDirection = Projectile.direction;

            Projectile.ai[1] += 1f;
            float num843 = 0.004f;

            if (Projectile.ai[1] > 0f)
            {
                Projectile.velocity.Y -= num843;
            }
            else
            {
                Projectile.velocity.Y += num843;
            }
            if (Projectile.ai[1] >= 90f)
            {
                Projectile.ai[1] *= -1f;
            }
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D portal = ModContent.Request<Texture2D>(Texture + "_Portal", AssetRequestMode.ImmediateLoad).Value;
            float rotation = Main.GlobalTimeWrappedHourly * 5;
            Vector2 origin = portal.Size() * 0.5f;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

                Main.EntitySpriteDraw(portal, Projectile.Center - Main.screenPosition + new Vector2(0, 6), null, Color.DarkMagenta, rotation, origin, 0.75f, SpriteEffects.None, 0);


            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}
