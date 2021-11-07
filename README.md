# MoreConversationTopics
A mod for Stardew Valley to add conversation topics that need code to add, so dialogue mods can reference them.

## Current Conversation Topics Added By This Mod
   * `wedding` (triggered when a player gets married)
   * `luauBest` (triggered when the luau soup gets the best result)
   * `luauShorts` (triggered when the Mayor's shorts are added to the luau soup)
   * `luauPoisoned` (triggered when a poisonous item is added to the luau soup)

These conversation topics have default lengths but are also configurable in the config file, with the following variables:
   * `WeddingDuration` (default of 7 days, controls length of `wedding` conversation topic)
   * `LuauDuration` (default of 7 days, controls length of all luau conversation topics)

## Some Examples of When It Might Be Advisable to Use Stats as Tokens Or Content Patcher Instead:

### Achievements

Most of the achievements rely on stats that are tracked in Stats as Tokens. For example, you could trigger dialogue based on amount of money earned with a “When” in Content Patcher. Or you could use the goodFriends token to trigger dialogue as the farmer is getting more popular. In terms of the fishing ones, there is the fishCaught stat, and also a specific mail item for the Master Angler achievement. 

### Weddings and Births

This mod adds a “wedding” conversation topic, but Stats as Tokens is also planning to track days married, so that you can add dialogue or other things based on anniversaries. This mod adds a “birth” conversation topic right when your children get born, but you may also want to use the upcoming Stats as Tokens children tokens to trigger dialogue/events/changes based on how old your children are. 

### Events That Happen Once or Things with Mail Flags

You can use Content Patcher to append “addConversationTopic <ID> [length]” at the end of an event or append “%item conversationTopic <key> <days> %%” at the end of a piece of mail. The only downside of this approach is that you may need to do some tricky Content Patcher tokens work to target all languages, and that you may or may not be compatible with any mods that also edit these events. Alternatively, you can use the “$1” command in dialogue to trigger some dialogue exactly once. Because of this, some conversation topics that could be added with events/mail flags have been added by this mod, but they are generally going to be lower priority. 
