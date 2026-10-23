# SHO - MKF Patch

Simple Hunting Overhaul's **Pick Up Carcass** on the [Modifier Key Framework](https://www.nexusmods.com/skyrimspecialedition/mods/193566)
key instead of Dynamic Activation Key. Dynamic Activation Key is no longer needed.

- Look at a dead animal: the prompt stays vanilla (`Search` / `Deer`); activating searches the body as usual.
- Hold the Modifier Key Framework key: the prompt becomes `Pick Up Carcass` / `Deer`; activating runs
  Simple Hunting Overhaul's own pick-up (same checks, animations and carcass items as with DAK).

## Requirements

- Simple Hunting Overhaul
- Modifier Key Framework **1.8.0 or newer**
- Dynamic Activation Key is **not** needed: the patch includes a copy of `Simple Hunting Overhaul.esp`
  without it (included with jayserpa's permission).

## Files

| File | What it does |
|---|---|
| `Simple Hunting Overhaul.esp` | Simple Hunting Overhaul 1.16's plugin with one change: `Dynamic Activation Key.esp` is no longer a master. Its pick-up perk `_PickUpPerk` was the only record using DAK (its global, in both entries' conditions); those two entries are switched off (`GetIsID Player == 0`). Every other record is byte for byte the original. Overwrites SHO's own plugin, so this mod must win over Simple Hunting Overhaul in MO2. |
| `SHO - MKF Patch.esp` | light plugin: overrides SHO's `_PickUpPerk` so its two DAK entries never apply; quest `SHOMKF_Quest` (start game enabled) with the player alias script |
| `SEQ/SHO - MKF Patch.seq` | starts the quest |
| `Scripts/SHOMKF_PlayerScript.pex` (+ source) | on the framework's event, calls SHO's own `PickUpCarcass()` |
| `SKSE/Plugins/ModifierKeyFramework/SHO - MKF Patch.json` | the rule: dead + `ActorTypeAnimal` (the same targets as SHO's perk) |
| `Interface/Translations/SHO - MKF Patch_ENGLISH.txt` | the `Pick Up Carcass` label |

Load after `Simple Hunting Overhaul.esp`.

## Rebuilding the plugin for a new SHO version

`tools/ShoNoDak` (.NET 8, Mutagen) makes the DAK-free copy from SHO's original plugin:

```
dotnet run --project tools/ShoNoDak -- "<SHO>/Simple Hunting Overhaul.esp" "mod/Simple Hunting Overhaul.esp"
```

It stops if anything else in the plugin still points at DAK.

## Not compatible

QuickLoot IE (and other QuickLoot-style mods): they take over dead bodies, so the modifier action never
reaches the framework.

Tested 2026-10-12 without Dynamic Activation Key: plain activate opens the deer's loot as usual; with the
key held, `Pick Up Carcass` / `Deer`, and activating gives a Deer Carcass.

## License

Two owners, two sets of terms (details in `LICENSE.md`):

- `Simple Hunting Overhaul.esp` is jayserpa's work, included with jayserpa's permission for this patch only.
  It is not under the GPL; all rights stay with jayserpa.
- Everything else (the patch plugin, the script, the rule and translation files, `tools/ShoNoDak`) is ours,
  GPL-3.0-or-later (`LICENSE-GPL.txt`).
