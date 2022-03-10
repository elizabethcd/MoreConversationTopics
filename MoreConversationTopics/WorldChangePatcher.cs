using System;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Events;

namespace MoreConversationTopics
{
    // Applies Harmony patches to WorldChangeEvents.cs to add conversation topics for various overnight world changes
    public class WorldChangePatcher
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
                Monitor.Log("Adding Harmony postfix to setUp() in WorldChangeEvent.cs", LogLevel.Trace);
                harmony.Patch(
                    original: AccessTools.Method(typeof(WorldChangeEvent), nameof(WorldChangeEvent.setUp)),
                    postfix: new HarmonyMethod(typeof(WorldChangePatcher), nameof(WorldChangePatcher.WorldChangeEvent_setUp_Postfix))
                );
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to add postfix to world change events with exception: {ex}", LogLevel.Error);
            }
        }

        // Method that is used to postfix
        private static void WorldChangeEvent_setUp_Postfix(WorldChangeEvent __instance)
        {
            try
            {
                switch ((int)__instance.whichEvent)
                {
                    // If the world change event in question is building the Joja greenhouse, add Joja greenhouse conversation topic
                    case 0:
                        try
                        {
                            Game1.player.activeDialogueEvents.Add("joja_Greenhouse", Config.JojaGreenhouseDuration);
                        }
                        catch (Exception ex)
                        {
                            Monitor.Log($"Failed to add Joja greenhouse conversation topic with exception: {ex}", LogLevel.Error);
                        }
                        break;
                    // If the world change event is the abandoned JojaMart being struck by lightning, add JojaMart lightning conversation topic
                    case 12:
                        try
                        {
                            Game1.player.activeDialogueEvents.Add("jojaMartStruckByLightning", Config.JojaLightningDuration);
                        }
                        catch (Exception ex)
                        {
                            Monitor.Log($"Failed to add abandonded JojaMart struck by lightning conversation topic with exception: {ex}", LogLevel.Error);
                        }
                        break;
                    // If the world change event is Willy's boat being repaired, add Willy boat repair conversation topic
                    case 13:
                        try
                        {
                            Game1.player.activeDialogueEvents.Add("willyBoatRepaired", Config.WillyBoatRepairDuration);
                        }
                        catch (Exception ex)
                        {
                            Monitor.Log($"Failed to add Willy's boat repaired conversation topic with exception: {ex}", LogLevel.Error);
                        }
                        break;
                    // If the world change event in question is Leo arriving in the valley, add Leo arrival conversation topic
                    case 14:
                        try
                        {
                            Game1.player.activeDialogueEvents.Add("leoValleyArrival", Config.LeoArrivalDuration);
                        }
                        catch (Exception ex)
                        {
                            Monitor.Log($"Failed to add Leo arrival to the valley conversation topic with exception: {ex}", LogLevel.Error);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to do world change event postfix with exception: {ex}", LogLevel.Error);
            }

        }
    }

}
