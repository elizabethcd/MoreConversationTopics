using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;

namespace MoreConversationTopics
{
    // Applies Harmony patches to Utility.cs to add a conversation topic for weddings.
    public class WeddingPatcher
    {
        private static IMonitor Monitor;
        private static ModConfig Config;

        // call this method from your Entry class
        public static void Initialize(IMonitor monitor, ModConfig config)
        {
            Monitor = monitor;
            Config = config;
        }

        // Method to apply harmony patch
        public static void Apply(Harmony harmony)
        {
            try
            {
                harmony.Patch(
                    original: AccessTools.Method(typeof(Utility), nameof(Utility.getWeddingEvent)),
                    postfix: new HarmonyMethod(typeof(WeddingPatcher), nameof(WeddingPatcher.Utility_getWeddingEvent_Postfix))
                );
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to add postfix wedding event with exception: {ex}", LogLevel.Error);
            }

            try
            {
                harmony.Patch(
                    original: AccessTools.Method(typeof(Utility), nameof(Utility.getPlayerWeddingEvent)),
                    postfix: new HarmonyMethod(typeof(WeddingPatcher), nameof(WeddingPatcher.Utility_getWeddingEvent_Postfix))
                );
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to add postfix player wedding event with exception: {ex}", LogLevel.Error);
            }
        }

        // Method that is used to postfix
        private static void Utility_getWeddingEvent_Postfix(Farmer farmer)
            {
                try
                {
                    farmer.activeDialogueEvents.Add("wedding", Config.WeddingDuration);
                }
                catch (Exception ex)
                {
                    Monitor.Log($"Failed to add wedding conversation topic with exception: {ex}", LogLevel.Error);
                }
            }
        }
}
