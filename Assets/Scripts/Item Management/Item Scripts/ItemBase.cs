using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Data", menuName = "Item/New Item Base", order = 1)]
public class ItemBase: ScriptableObject
{

    public enum MainItemType
    {
        Weapon,
        Potion,
        Spell
     }; //TODO May modified later on

    /*/public enum SecondaryItemType
    {
        None,
        Gun,
        Melee,
        Potion,
        Food,
        Drink,
        Structure,
        Ammo,
        Trap,
        Throwable
    }; //TODO May be modified later on*/

    // public enum ItemRarity
    // {
    //     Common,
    //     Uncommon,
    //     Rare,
    //     Unique,
    //     Legendary,
    //     WTF
    // };

    // [CreateAssetMenu(fileName = "Component Data", menuName = "Item/New Component Base", order = 2)]
    // public class CraftingComponentRequirement:ScriptableObject
    // {
    //     public ItemBase item;
    //     public int requiredCount;

    //     public CraftingComponentRequirement(ItemBase itemBase, int count) { item = itemBase; requiredCount = count; }
    // }


    public string Name;
    public string Description;
    public int ItemWeight;
    public int ItemValue;
    public string AnimationTrigger;

    public MainItemType MainType;
    // public SecondaryItemType SecondaryType;
    // public ItemRarity Rarity;
    public Sprite ItemIcon;

    /// <summary>
    /// Used for comparisons to be used in inventory and crafting system.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        var @base = obj as ItemBase;
        return @base != null &&
               base.Equals(obj) &&
               Name == @base.Name &&
               Description == @base.Description &&
               MainType == @base.MainType;// &&
               //SecondaryType == @base.SecondaryType&&
               //Rarity == @base.Rarity;
    }

    /// <summary>
    /// Given the item details, regarding game object will be attached the script type of
    /// detailed item type, and the content of the given item details will be copied to the new component.
    /// </summary>
    /// <param name="newEntry"></param>
    /// <param name="itemDetails"></param>
     public static void SetObjectDetails(GameObject newEntry, IGameItem itemDetails)
     {
         if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Weapon))
         {
             //Set the script details
             newEntry.AddComponent<Weapon>().SetItemBase(itemDetails.GetItemBase());
         }
         else if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Potion))
         {
             //Set the script details
             newEntry.AddComponent<Potion>().SetItemBase(itemDetails.GetItemBase());
         }
         else if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Spell))
         {
             //Set the script details
             newEntry.AddComponent<Spell>().SetItemBase(itemDetails.GetItemBase());
         }
     }
     
    /// <summary>
    /// Given the item details, regarding game object will be attached the script type of
    /// detailed item type, and the content of the given item details will be copied to the new component.
    /// </summary>
    /// <param name="newEntry"></param>
    /// <param name="itemDetails"></param>
     public static void SetObjectDetails(GameObject newEntry, ItemBase itemDetails)
     {
         if (itemDetails.MainType.Equals(ItemBase.MainItemType.Weapon))
         {
             //Set the script details
             newEntry.AddComponent<Weapon>().SetItemBase(itemDetails);

         }
         else if (itemDetails.MainType.Equals(ItemBase.MainItemType.Spell))
         {
             //Set the script details
             newEntry.AddComponent<Spell>().SetItemBase(itemDetails);
         }
         else if (itemDetails.MainType.Equals(ItemBase.MainItemType.Potion))
         {
             //Set the script details
             newEntry.AddComponent<Potion>().SetItemBase(itemDetails);
         }
     }

    /// <summary>
    /// Given the item details, regarding game object will be attached the script type of
    /// detailed item type, and the item base of the given item details will be copied to the new component.
    /// </summary>
    /// <param name="newEntry"></param>
    /// <param name="itemDetails"></param>
     public static void SetObjectItemBases(GameObject newEntry, IGameItem itemDetails)
     {
         if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Weapon))
         {
             //Set the script details
             newEntry.AddComponent<Weapon>().SetItemBase(itemDetails.GetItemBase());
         }
         else if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Potion))
         {
             //Set the script details
             newEntry.AddComponent<Potion>().SetItemBase(itemDetails.GetItemBase());
         }
         else if (itemDetails.GetItemBase().MainType.Equals(ItemBase.MainItemType.Spell))
         {
             //Set the script details
             newEntry.AddComponent<Spell>().SetItemBase(itemDetails.GetItemBase());
         }
     }
}

/// <summary>
/// Common interface for game items to ease the referencing between objects
/// </summary>
public interface IGameItem
{
    void SetItemBase(ItemBase baseForItem);
    ItemBase GetItemBase();
    void UseItem();
    
}