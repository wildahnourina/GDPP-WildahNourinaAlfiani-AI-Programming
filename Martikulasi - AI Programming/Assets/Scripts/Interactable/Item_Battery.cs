using UnityEngine;

public class Item_Battery : Item
{
    public override void Pickup(PlayerCharacter player)
    {
        base.Pickup(player);

        player.Flashlight.RefillBatteryLevel();
    }
}
