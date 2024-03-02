using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Utils;

namespace RetakesPlugin.Modules.Managers;

public static class AllocationManager
{


    public static void Allocate(CCSPlayerController player)
    {
        AllocateEquipment(player);
        AllocateWeapons(player);
        AllocateGrenades(player);
    }

    private static void AllocateEquipment(CCSPlayerController player)
    {
        player.GiveNamedItem(CsItem.KevlarHelmet);
        if (
            player.Team == CsTeam.CounterTerrorist
            && player.PlayerPawn.IsValid
            && player.PlayerPawn.Value != null
            && player.PlayerPawn.Value.IsValid
            && player.PlayerPawn.Value.ItemServices != null
        )
        {
            var itemServices = new CCSPlayer_ItemServices(player.PlayerPawn.Value.ItemServices.Handle);
            itemServices.HasDefuser = true;
        }
    }

    private static void AllocateWeapons(CCSPlayerController player)
    {
        if (player.Team == CsTeam.Terrorist)
        {
            RetakesPlugin.PlayerGiveNamedItem(player, "weapon_ak47");
            // RetakesPlugin.PlayerGiveNamedItem(player, CsItem.Glock);
            RetakesPlugin.PlayerGiveNamedItem(player, "weapon_deagle");
        }

        if (player.Team == CsTeam.CounterTerrorist)
        {
            // @klippy
            if (player.PlayerName.Trim() == "klip")
            {
                RetakesPlugin.PlayerGiveNamedItem(player, "weapon_m4a1");
            }
            else
            {
                RetakesPlugin.PlayerGiveNamedItem(player, "weapon_m4a1_silencer");
            }

            // RetakesPlugin.PlayerGiveNamedItem(player, CsItem.USPS);
            RetakesPlugin.PlayerGiveNamedItem(player, "weapon_deagle");
        }

        RetakesPlugin.PlayerGiveNamedItem(player, "weapon_knife");
    }

    private static void AllocateGrenades(CCSPlayerController player)
    {
        switch (Helpers.Random.Next(4))
        {
            case 0:
                player.GiveNamedItem(CsItem.SmokeGrenade);
                break;
            case 1:
                player.GiveNamedItem(CsItem.Flashbang);
                break;
            case 2:
                player.GiveNamedItem(CsItem.HEGrenade);
                break;
            case 3:
                player.GiveNamedItem(player.Team == CsTeam.Terrorist ? CsItem.Molotov : CsItem.Incendiary);
                break;
        }
    }
}
