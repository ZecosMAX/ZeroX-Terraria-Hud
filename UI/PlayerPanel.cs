using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using ZeroXHUD.Utils;

namespace ZeroXHUD.UI
{
    internal class PlayerPanel : UIElement
    {
        Player player;
        Bar healthBar = new Bar();
        Bar manaBar = new Bar();

        UIHead playerHead = new UIHead();
        UIText playerName = new UIText("AAAAAA", 1f);

        UIText label1 = new UIText("AAAAAA", 1f);
        UIText label2 = new UIText("AAAAAA", 1f);
        UIText label3 = new UIText("AAAAAA", 1f);
        UIText label4 = new UIText("AAAAAA", 1f);
        UIText label5 = new UIText("AAAAAA", 1f);
        UIText label6 = new UIText("AAAAAA", 1f);
        UIText label7 = new UIText("AAAAAA", 1f);
        UIText label8 = new UIText("AAAAAA", 1f);


        public PlayerPanel(Player player)
        {
            this.player = player;

            this.MinHeight.Set(60, 0);
            this.MinWidth.Set(200, 0);
        }

        public void UpdateValues(Player player)
        {
            this.player = player;

            int life    = player.statLife;
            int maxLife = player.statLifeMax2;

            int mana = player.statMana;
            int maxMana = player.statManaMax2;

            int lifeRegen = player.lifeRegen;
            int manaRegen = player.manaRegen;

            float magicDamage = player.GetActualDamageModifier<MagicDamageClass>();
            float meleeDamage = player.GetActualDamageModifier<MeleeDamageClass>();
            float rangeDamage = player.GetActualDamageModifier<RangedDamageClass>();
            float throwDamage = player.GetActualDamageModifier<ThrowingDamageClass>();
            float summonDamage = player.GetActualDamageModifier<SummonDamageClass>();
            float genericDamage = player.GetActualDamageModifier<GenericDamageClass>();

            int def = player.statDefense;
            int dps = player.getDPS();

            float speed = player.moveSpeed;
            float ap = player.GetArmorPenetration<GenericDamageClass>();

            string name = player.name;

            healthBar.Value = (float)life / maxLife;
            healthBar.Text = $"HP: {life}/{maxLife} ({lifeRegen:+#;-#;0})";

            manaBar.Value = (float)mana / maxMana;
            manaBar.Text = $"MP: {mana}/{maxMana} ({manaRegen:+#;-#;0})";

            this.label1.SetText($"DEF: {def:0.00}", 0.8f, false);
            this.label2.SetText($"DPS: {dps:0}", 0.8f, false);

            this.label3.SetText($"Speed: {speed:0.00}", 0.8f, false);
            this.label4.SetText($"AP: {ap:0.00}", 0.8f, false);
            
            this.label5.SetText($"Magic: {magicDamage:0.00}", 0.8f, false);
            this.label6.SetText($"Melee: {meleeDamage:0.00}", 0.8f, false);

            this.label7.SetText($"Range: {rangeDamage:0.00}", 0.8f, false);
            this.label8.SetText($"Summn: {summonDamage:0.00}", 0.8f, false);

            playerName.SetText(name);
            playerHead.PlayerIndex = player.whoAmI;
        }

        public override void OnInitialize()
        {
            try
            {
                playerHead.Top.Set(0, 0);
                playerHead.Left.Set(12, 0);
                Append(playerHead);

                playerName.TextColor = new Color(210, 210, 210);
                playerName.SetText("", 1.0f, false);
                playerName.Top.Set(-5, 0);
                playerName.Left.Set(35, 0);
                Append(playerName);

                healthBar.BorderColor = Color.Black;
                healthBar.FillColor = new Color(127, 29, 29);
                healthBar.Width.Set(200, 0);
                healthBar.Height.Set(22, 0);
                healthBar.Top.Set(16, 0);
                healthBar.Left.Set(0, 0);
                Append(healthBar);

                #region | First row of labels |
                label1.TextColor = Color.White;
                label1.Top.Set(20, 0);
                label1.Left.Set(210, 0);
                Append(label1);           

                label3.TextColor = Color.White;
                label3.Top.Set(20, 0);
                label3.Left.Set(300, 0);
                Append(label3);

                label5.TextColor = Color.White;
                label5.Top.Set(20, 0);
                label5.Left.Set(390, 0);
                Append(label5);

                label7.TextColor = Color.White;
                label7.Top.Set(20, 0);
                label7.Left.Set(480, 0);
                Append(label7);
                #endregion

                #region | First row of labels |
                label2.TextColor = Color.White;
                label2.Top.Set(44, 0);
                label2.Left.Set(210, 0);
                Append(label2);

                label4.TextColor = Color.White;
                label4.Top.Set(44, 0);
                label4.Left.Set(300, 0);
                Append(label4);

                label6.TextColor = Color.White;
                label6.Top.Set(44, 0);
                label6.Left.Set(390, 0);
                Append(label6);

                label8.TextColor = Color.White;
                label8.Top.Set(44, 0);
                label8.Left.Set(480, 0);
                Append(label8);
                #endregion

                manaBar.BorderColor = Color.Black;
                manaBar.FillColor = new Color(30, 64, 175);
                manaBar.Width.Set(200, 0);
                manaBar.Height.Set(22, 0);
                manaBar.Top.Set(38, 0);
                manaBar.Left.Set(0, 0);
                Append(manaBar);
            }
            catch (Exception ex)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{ex}"), Color.White);
            }

        }
    }
}
