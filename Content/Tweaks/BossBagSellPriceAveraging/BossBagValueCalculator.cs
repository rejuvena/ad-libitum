using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.BossBagSellPriceAveraging
{
    public class BossBagValueCalculator
    {
        public struct BossBagInfo {
            public int Simulations;
            public List<int> RealValues;

            public BossBagInfo(int simulations) {
                Simulations = simulations;
                RealValues = new();
            }

            public float GetAverageValue() {
                float value = 0;

                foreach (int v in RealValues)
                    value += v;

                value /= (float)Simulations;

                return value;
            }
        }

        public const int DUMMY_ITEM_INDEX = 100;

        public BossBagInfo CurrentBagInfo;
        public Dictionary<int, int> AveragedValues = new();

        public int HandleQuickSpawnItem(int type, int stack) {
            Item item = new(type, stack);
            item.SetDefaults(type);
            Main.item[DUMMY_ITEM_INDEX] = item;

            int realValue = item.value * item.stack;
            CurrentBagInfo.RealValues.Add(realValue);

            return DUMMY_ITEM_INDEX;
        }

        public void CalculateAll() {
            for (int i = 0; i < ItemLoader.ItemCount - 1; i++)
            {
                // Item item = ContentSamples.ItemsByType[i]; // THIS CRASHES FOR SOME REASON. UGH I LOVE TML
                Item item = new(i);

                if (!ItemID.Sets.BossBag[i])
                    continue;

                Player dummy = new();
                CurrentBagInfo = new(BossBagAvgSystem.Simulations);
                bool success = false;

                for (int j = 0; j < BossBagAvgSystem.Simulations; j++)
                {
                    try
                    {
                        dummy.OpenBossBag(i);
                    }
                    catch (Exception e)
                    {
                        AdLibitum.Instance.Logger.Warn($"WARNING: Non-fatal error {e} ecountered while averaging treasure bag ID (ID: {item.type}, Name: {item.Name}). Skipping...");
                        break;
                    }

                    success = true;
                }

                if (success)
                    AveragedValues.Add(i, (int)CurrentBagInfo.GetAverageValue());
            }

            Main.item[DUMMY_ITEM_INDEX] = new Item();
        }
    }
}
