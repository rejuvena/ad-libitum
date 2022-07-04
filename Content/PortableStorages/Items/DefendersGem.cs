using AdLibitum.Content.PortableStorages.Projectiles;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Items
{
    [UsedImplicitly]
    public class DefendersGem : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 24;
            Item.UseSound = SoundID.MaxMana;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 10);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<EtherealChest>();
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.White;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
            spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_GlowyOutline").Value, Item.Center - new Vector2(2, 1) - Main.screenPosition, null, Color.Magenta, rotation, TextureAssets.Item[Type].Size() * 0.5f, 1f, SpriteEffects.None, 0f);

            return true;
        }
    }
}
