using HarmonyLib;

namespace SingleUseSwampKey;

[HarmonyPatch(typeof(Door), nameof(Door.Interact))]
public static class SingleDoorPatch
{
    private static void Postfix(Door __instance, ref bool __result, Humanoid character)
    {

        var localPlayer = Player.m_localPlayer;
        
        if (__result)
        {
            if (character == localPlayer)
            {
                var keyItem = __instance.m_keyItem;
                if (keyItem != null)
                {
                    if (keyItem.m_itemData.m_shared.m_name == "$item_cryptkey")
                    {
                        var inventory = character.GetInventory();
                        if (inventory != null)
                        {
                            if (inventory.ContainsItemByName("$item_cryptkey"))
                            {
                                inventory.RemoveItem("$item_cryptkey", 1);
                            }
                        }
                    }  
                }
            }
        }
    }
} 