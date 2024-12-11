using System;

namespace CODE_OF_STORY.Core.Items;

public class HealingPotion
{
    private int HealAmount;

    public HealingPotion(int healamount)
    {
        HealAmount = healamount;
    }

    public void Heal(Player player)
    {
        player.Heal(HealAmount);
    }
}
