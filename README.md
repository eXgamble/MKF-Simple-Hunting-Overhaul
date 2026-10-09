# SHO - MKF Patch

Simple Hunting Overhaul's **Pick Up Carcass** on the [Modifier Key Framework](https://www.nexusmods.com/skyrimspecialedition/mods/193566)
key instead of Dynamic Activation Key.

- Look at a dead animal: the prompt stays vanilla (`Search` / `Deer`); activating searches the body as usual.
- Hold the Modifier Key Framework key: the prompt becomes `Pick Up Carcass` / `Deer`; activating runs
  Simple Hunting Overhaul's own pick-up (same checks, animations and carcass items as with DAK).

## Requirements

- Simple Hunting Overhaul
- Modifier Key Framework **1.1.0 or newer** (the rule uses format 2: the vanilla `Search` label is kept)
- Simple Hunting Overhaul's plugin still lists `Dynamic Activation Key.esp` as a master, so DAK's plugin
  must stay installed; DAK's key no longer does anything for Simple Hunting Overhaul.

## Files

| File | What it does |
|---|---|
| `SHO - MKF Patch.esp` | light plugin: overrides SHO's `_PickUpPerk` so its two DAK entries never apply; quest `SHOMKF_Quest` (start game enabled) with the player alias script |
| `SEQ/SHO - MKF Patch.seq` | starts the quest |
| `Scripts/SHOMKF_PlayerScript.pex` (+ source) | on the framework's event, calls SHO's own `PickUpCarcass()` |
| `SKSE/Plugins/ModifierKeyFramework/SHO - MKF Patch.json` | the rule: dead + `ActorTypeAnimal` (the same targets as SHO's perk) |
| `Interface/Translations/SHO - MKF Patch_ENGLISH.txt` | the `Pick Up Carcass` label |

Load after `Simple Hunting Overhaul.esp`.

## Known issue

QuickLoot IE takes over activation of dead bodies, so the modifier action never reaches the framework.
Not compatible with QuickLoot IE for now.
