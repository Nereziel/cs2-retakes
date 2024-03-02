using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using CounterStrikeSharp.API.Modules.Utils;

namespace RetakesPlugin.Modules.Managers;

public static class AllocationManager
{
    public MemoryFunctionVoid<IntPtr, string, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr> GiveNamedItem2 = new(@"\x55\x48\x89\xE5\x41\x57\x41\x56\x41\x55\x41\x54\x53\x48\x83\xEC\x18\x48\x89\x7D\xC8\x48\x85\xF6\x74");
    
    public void PlayerGiveNamedItem(CCSPlayerController player, string item)
    {
        if (!player.PlayerPawn.IsValid) return;
        if (player.PlayerPawn.Value == null) return;
        if (!player.PlayerPawn.Value.IsValid) return;
        if (player.PlayerPawn.Value.ItemServices == null) return;

        GiveNamedItem2.Invoke(player.PlayerPawn.Value.ItemServices.Handle, item, 0, 0, 0, 0, 0, 0);
    }

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
            PlayerGiveNamedItem(player, CsItem.AK47);
            // PlayerGiveNamedItem(player, CsItem.Glock);
            PlayerGiveNamedItem(player, CsItem.Deagle);
        }

        if (player.Team == CsTeam.CounterTerrorist)
        {
            // @klippy
            if (player.PlayerName.Trim() == "klip")
            {
                PlayerGiveNamedItem(player, CsItem.M4A4);
            }
            else
            {
                PlayerGiveNamedItem(player, CsItem.M4A1S);
            }

            // PlayerGiveNamedItem(player, CsItem.USPS);
            PlayerGiveNamedItem(player, CsItem.Deagle);
        }

        PlayerGiveNamedItem(player, CsItem.Knife);
    }

    private static void AllocateGrenades(CCSPlayerController player)
    {
        switch (Helpers.Random.Next(4))
        {
            case 0:
                PlayerGiveNamedItem(player, CsItem.SmokeGrenade);
                break;
            case 1:
                PlayerGiveNamedItem(player, CsItem.Flashbang);
                break;
            case 2:
                PlayerGiveNamedItem(player, CsItem.HEGrenade);
                break;
            case 3:
                PlayerGiveNamedItem(player, player.Team == CsTeam.Terrorist ? CsItem.Molotov : CsItem.Incendiary);
                break;
        }
    }
}
