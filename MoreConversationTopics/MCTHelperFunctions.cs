using System;
using System.Linq;
using StardewModdingAPI;
using StardewValley;

namespace MoreConversationTopics
{
    public class MCTHelperFunctions
    {
        private static IMonitor Monitor;
        private static ModConfig Config;

        private static readonly string[] modRepeatableConversationTopics = new string[] {
                "wedding", "divorce", "babyBoy", "babyGirl", // repeatable social CTs
                "luauBest", "luauShorts", "luauPoisoned", // repeatable luau CTs
                "meteoriteLandedOnFarm", "owlStatueLandedOnFarm", // repeatable night event CTs
                "fairyFarmVisit", "witchSlimeHutVisit", "goldenWitchCoopVisit", "witchCoopVisit" // repeatable magical farm event CTs
            };

        // Initialize Monitor and Config for this class and all other classes
        public static void Initialize(IMonitor monitor, ModConfig config)
        {
            Monitor = monitor;
            Config = config;

            RepeatPatcher.Initialize(Monitor, Config);
            WeddingPatcher.Initialize(Monitor, Config);
            BirthPatcher.Initialize(Monitor, Config);
            DivorcePatcher.Initialize(Monitor, Config);
            LuauPatcher.Initialize(Monitor, Config);
            WorldChangePatcher.Initialize(Monitor, Config);
            NightEventPatcher.Initialize(Monitor, Config);
            JojaEventAssetEditor.Initialize(Monitor, Config);
            IslandPatcher.Initialize(Monitor, Config);
        }

        // Helper function to check if a string is on the list of repeatable CTs added by this mod
        public static bool IsRepeatableCTAddedByMod(string topic)
            => modRepeatableConversationTopics.Contains(topic);

        // Function to add conversation topics that may already exist to the current player
        public static void AddOrExtendCT(string conversationTopic, int duration)
            => AddOrExtendCT(Game1.player, conversationTopic, duration);

        // Function to add conversation topics that may already exist to any player
        public static void AddOrExtendCT(Farmer playerToAddTo, string conversationTopic, int duration)
        {
            // Check if the conversation topic has already been added
            if (playerToAddTo.activeDialogueEvents.ContainsKey(conversationTopic))
            {
                Monitor.Log($"{conversationTopic} because it's already there, extending duration", LogLevel.Trace);
            }

            // Adds or extends the CT.
            playerToAddTo.activeDialogueEvents[conversationTopic] = duration;
        }

        internal static bool TryAddCT(Farmer playerToAddTo, string conversationTopic, int duration)
        {
            if (playerToAddTo.activeDialogueEvents.ContainsKey(conversationTopic))
            {
                Monitor.Log($"{playerToAddTo.Name} has CT {conversationTopic} already, not adding", LogLevel.Info);
                return false;
            }
            playerToAddTo.activeDialogueEvents[conversationTopic] = duration;
            return true;
        }

        internal static bool TryAddCT(string conversationTopic, int duration)
            => TryAddCT(Game1.player, conversationTopic, duration);

        // Prints out all current conversation topics for console command
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "SMAPI command format")]
        public static void console_GetCurrentCTs(string command, string[] args)
        {
            if (!Context.IsWorldReady)
                return;

            try
            {
                Monitor.Log(string.Join(", ", Game1.player.activeDialogueEvents.Keys), LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to print conversation topics with exception: {ex}", LogLevel.Error);
            }
        }

        // Checks mail flags for console command
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "SMAPI command format")]
        public static void console_HasMailFlag(string command, string[] args)
        {
            if (!Context.IsWorldReady)
                return;

            try
            {
                if (Game1.player.mailReceived.Contains(args[0]))
                {
                    Monitor.Log($"Yes, you have this mail flag", LogLevel.Debug);
                }
                else
                {
                    Monitor.Log($"No, you don't have this mail flag", LogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                Monitor.Log($"Bad or missing argument with exception: {ex}", LogLevel.Error);
            }
        }

        // Add conversation topic for console command
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "SMAPI command format")]
        public static void console_AddConversationTopic(string command, string[] args)
        {
            if (!Context.IsWorldReady)
                return;

            // Try to get the duration from the input arguments, default to 1 if cannot be parsed
            if (!int.TryParse(args[0], out int duration))
            {
                duration = 1;
                Monitor.Log($"Couldn't parse duration as an integer, defaulting to 1 day", LogLevel.Warn);
            }

            // Add the conversation topic to the current player
            try
            {
                if (TryAddCT(args[0], duration))
                {
                    Monitor.Log($"Added conversation topic {args[0]} with duration {duration}", LogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                Monitor.Log($"Couldn't add conversation topic with exception: {ex}", LogLevel.Error);
            }
        }

        // Remove conversation topic for console command
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "SMAPI command format")]
        public static void console_RemoveConversationTopic(string command, string[] args)
        {
            if (!Context.IsWorldReady)
                return;

            try
            {
                Game1.player.activeDialogueEvents.Remove(args[0]);
                Monitor.Log("Removed conversation topic", LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Monitor.Log($"Bad or missing argument with exception: {ex}", LogLevel.Error);
            }
        }
    }
}
