using StardewModdingAPI;
using System.Collections.Generic;

namespace MoreConversationTopics
{
    public class JojaEventAssetEditor : IAssetEditor
    {
        private IMonitor Monitor;
        private ModConfig Config;
        public void Initialize(IMonitor monitor, ModConfig config)
        {
            Monitor = monitor;
            Config = config;
        }

        /// <summary>Get whether this instance can edit the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanEdit<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Data/Events/Town"))
            {
                return true;
            }

            return false;
        }

        /// <summary>Edit a matched asset.</summary>
        /// <param name="asset">A helper which encapsulates metadata about an asset and enables changes to it.</param>
        public void Edit<T>(IAssetData asset)
        {
            // Only pull data from the events in town
            if (asset.AssetNameEquals("Data/Events/Town"))
            {
                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                foreach ((string eventID, string eventScript) in data)
                {
                    // Only edit the Joja completion ceremony event
                    if (eventID.StartsWith("502261"))
                    {
                        // Check if the event script is null, just in case
                        if (eventScript is null)
                        {
                            Monitor.Log("Cannot edit Joja completion ceremony event due to null script.", LogLevel.Error);
                            return;
                        }
                        // Split up the actions in the event script
                        string[] eventActions = eventScript.Split('/');
                        int lastIndex = eventActions.Length - 1;

                        // Check that there's enough commands in the event for it to be a valid event
                        if (lastIndex < 3)
                        {
                            Monitor.Log("Cannot edit Joja completion ceremony event due to script having too few commands to be a valid event.", LogLevel.Error);
                            return;
                        }

                        // Split the event script into starting/ending actions
                        string[] startingActions = eventActions[0..2];
                        string startingActionsCombined = string.Join("/", startingActions);
                        string[] allOtherActions = eventActions[3..lastIndex];
                        string allOtherActionsCombined = string.Join("/", allOtherActions);

                        // Build the conversation topic command
                        string addJojaCT = "/addConversationTopic joja_Complete " + Config.JojaCompletionDuration.ToString() + "/";

                        // Insert the conversation topic after the starting actions and before all the other actions
                        string newEventScript = startingActionsCombined + addJojaCT + allOtherActionsCombined;

                        // Put everything back together at the end
                        data[eventID] = newEventScript;
                    }
                }
            }
        }
    }
}