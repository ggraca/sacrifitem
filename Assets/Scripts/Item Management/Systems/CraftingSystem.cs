using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour {

    /// <summary>
    /// Reference to player's inventory to check the components and place the result of crafting operation
    /// </summary>
    private Inventory playerInventory;

    [Tooltip("Reference to the craftable item list GUI")]
    [SerializeField]
    private GameObject craftableList;

    [Tooltip("Entry prefab for craftable item list entries")]
    [SerializeField]
    private GameObject craftableEntry;
    

    private void Start()
    {
        //TODO Arrange inventory initialization according to script management that will be 
        //done later on
        //For the time being v
        playerInventory = GameObject.FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// Crafts an Consumable by checking the inventory for available space and required components
    /// If everything is ok, arranges the inventory by putting new item and removing used components
    /// </summary>
    /// <param name="itemToCraft"></param>
    /// <returns></returns>
    // public string CraftItem(IGameItem itemToCraft)
    // {

    //     int replacedComponentAmount = 0;

    //     foreach(var component in itemToCraft.GetRequiredComponents())
    //     {
    //         //Get the current amount exists in the inventory
    //         var currentAmount = playerInventory.GetItemCount(component.item);

    //         //If there isn't enough amount of the item
    //         if(currentAmount < component.requiredCount) { return "Not enough " + component.item.name; }

    //         replacedComponentAmount += component.item.ItemWeight;
    //     }

    //     ///Having all the necessary items
    //     //Add the requested item to the inventory
    //     if (playerInventory.AddItemToInventory(itemToCraft.GetItemBase(),1,replacedComponentAmount))
    //     {
    //         //Remove the used crafting components from the inventory
    //         foreach (var component in itemToCraft.GetRequiredComponents())
    //         {
    //             playerInventory.RemoveItemFromInventory(component.item, component.requiredCount);
    //         }

    //         return "Item Successfully Added to Your Inventory!";
    //     }

    //     return "You don't have enough inventory space to craft this item!";
    // }


    /// <summary>
    /// Loads the craftable item list with visual entries.
    /// </summary>
    public void LoadCraftableItemList()
    {
        //TODO must be filled for GUI loading. 
    }

    
}
