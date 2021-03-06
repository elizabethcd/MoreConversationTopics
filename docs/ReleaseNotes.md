# Release Notes

## v1.1.1
- Adds ``islandResortUnlocked`` conversation topic
- Updates for SMAPI 3.14 (rewriting asset editors) thanks to atravita
- Better console command for checking current CTs thanks to sophie
- Moves repeatable conversation topics to be stored in a json in Content so that other mods can use this functionality as well
- New console commands to help with repeatable conversation topic debugging

## v.1.1.0
- Adds ``joja_Greenhouse``, ``joja_Complete``, ``jojaMartStruckByLightning``, ``willyBoatRepaired``, ``leoValleyArrival``, ``UFOLandedOnFarm``, ``meteoriteLandedOnFarm``, ``owlStatueLandedOnFarm``, ``railroadEarthquake``, ``witchSlimeHutVisit``, ``witchCoopVisit``, ``goldenWitchCoopVisit``, and ``fairyFarmVisit`` conversation topics
- Changes the console commands to use ``vl.mct`` as a prefix for uniqueness, and shortens them for ease of typing
- Changes how conversation topics are added to check for pre-existing conversation topics with the same name (for better compatibility)
- Adds GMCM support (thanks to atravita)
- Harmony patches now logged to trace for better debugging support
- Some refactoring to put helper functions into a separate class
- Adding GitHub releases

## v1.0.2
- Updates for game version 1.5.5 (not compatible with Stardew Valley 1.5.4 or earlier)
- Adds ``divorce``,``babyGirl``, and ``babyBoy`` conversation topics
- Fixes crucial bug where conversation topics aren't repeatable in SDV normally, now the ones added by this mod are

## v1.0.1
- Adds various luau conversation topics
- Fixes/improves multiplayer wedding events

## v1.0.0
- Initial release
- Includes ``wedding`` conversation topic
- Compatible with Stardew Valley 1.5.4
