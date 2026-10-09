ScriptName SHOMKF_PlayerScript Extends ReferenceAlias
{SHO - MKF Patch: Simple Hunting Overhaul's "Pick Up Carcass" on the Modifier Key Framework key
instead of Dynamic Activation Key. The rule is in SKSE/Plugins/ModifierKeyFramework/SHO - MKF Patch.json.}

SHO_PickUpCarCass Property SHO Auto	; Simple Hunting Overhaul's own quest: its pick-up code runs unchanged

Event OnInit()
	RegisterForModEvent("SHOMKF_PickUpCarcass", "OnPickUpCarcass")
EndEvent

; Mod event registrations are not saved: register again on every load
Event OnPlayerLoadGame()
	RegisterForModEvent("SHOMKF_PickUpCarcass", "OnPickUpCarcass")
EndEvent

Event OnPickUpCarcass(String asEventName, String asRuleId, Float afIsAlternate, Form akTarget)
	; SHO picks up the carcass under the crosshair (as with its DAK perk): only if that's still the one activated
	If akTarget && Game.GetCurrentCrosshairRef() == akTarget As ObjectReference
		SHO.PickUpCarcass()
	EndIf
EndEvent
