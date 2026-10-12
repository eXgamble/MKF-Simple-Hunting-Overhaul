// Builds SHO - MKF Patch's copy of Simple Hunting Overhaul.esp without Dynamic Activation Key:
// the pick-up perk's two entries lose the DAK global condition and are switched off
// (tab 0 = GetIsID Player == 0, as in SHO - MKF Patch.esp), so DAK is no longer a master.
// Usage: ShoNoDak <Simple Hunting Overhaul.esp> <output .esp>
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Binary.Parameters;

var dakMaster = ModKey.FromFileName("Dynamic Activation Key.esp");
var mod = SkyrimMod.CreateFromBinary(new ModPath(args[0]), SkyrimRelease.SkyrimSE);
var perk = mod.Perks.First(p => p.EditorID == "_PickUpPerk");
foreach (var eff in perk.Effects.OfType<APerkEntryPointEffect>())
    foreach (var tab in eff.Conditions.Where(t => t.RunOnTabIndex == 0))
    {
        tab.Conditions.RemoveAll(c => c.Data is GetGlobalValueConditionData g && g.Global.Link.FormKey.ModKey == dakMaster);
        foreach (var c in tab.Conditions.OfType<ConditionFloat>().Where(c => c.Data is GetIsIDConditionData))
            c.ComparisonValue = 0;
    }
var left = mod.EnumerateFormLinks().Count(l => l.FormKey.ModKey == dakMaster);
if (left > 0) throw new Exception($"{left} links to DAK left");
// SHO keeps a few records below 0x800 (its header is 1.71, which allows them): write them as they are
Console.WriteLine($"header {mod.ModHeader.Stats.Version}, low ids: {string.Join(", ", mod.EnumerateMajorRecords().Where(r => r.FormKey.ModKey == mod.ModKey && r.FormKey.ID < 0x800).Select(r => r.FormKey.ID.ToString("X6") + " " + r.EditorID))}");
mod.WriteToBinary(args[1], new BinaryWriteParameters { LowerRangeDisallowedHandler = new NoCheckIfLowerRangeDisallowed(),
    // keep SHO's own master order (Mutagen sorts alphabetically otherwise)
    MastersListOrdering = new MastersListOrderingByLoadOrder(mod.ModHeader.MasterReferences.Select(m => m.Master).Where(m => m != dakMaster).ToList()) });
var check = SkyrimMod.CreateFromBinary(new ModPath(args[1]), SkyrimRelease.SkyrimSE);
Console.WriteLine($"written {args[1]}\nmasters: {string.Join(", ", check.ModHeader.MasterReferences.Select(m => m.Master.FileName))}\nflags: {check.ModHeader.Flags}\nrecords: {mod.EnumerateMajorRecords().Count()} -> {check.EnumerateMajorRecords().Count()}");
