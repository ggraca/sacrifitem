using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    /// <summary>
    /// Used for keeping track of item count in the inventory
    /// </summary>
    private class InventoryItemCount
    {
        public ItemBase item { get; set; }
        public int itemCount { get; set; }

        public InventoryItemCount(ItemBase newItem, int count = 1) { item = newItem; itemCount = count; }
        public void IncrementCount(int count = 1) { itemCount += count; }
        public void DecrementCount(int count = 1) { itemCount -= count; }
    }

    /// <summary>
    /// Item list in the inventory.
    /// </summary>
    private List<InventoryItemCount> itemList = new List<InventoryItemCount>();

    [Tooltip("Determines the size of the inventory, which shows how much in weight the inventory can hold.")]
    [SerializeField]
    private int InventorySize;

    /// <summary>
    /// Shows the cumitalive weight of the items in the inventory
    /// </summary>
    private int CurrentWeight = 0;

    //[Tooltip("This is a GameObject reference for the GUI parent object for visualizing the inventory for the player. If this is not null, it will be visually set for player.")]
    //[SerializeField]
    //private GameObject InventoryGUIParent;
    [Tooltip("This is a GameObject reference for the GUI child object for visualizing the inventory for the player.It will contain ItemBase data for the items.")]
    [SerializeField]
    private GameObject InventoryGUIChild;

    private List<InventoryItemCount> ItemList
    {
        get
        {
            return itemList;
        }

        set
        {
            itemList = value;
        }
    }


    /*private void Start()
    {
        foreach(var x in GameObject.FindObjectOfType<CentralItemManager>().ItemData)
        {
            AddItemToInventory(x.GetComponent<IGameItem>().GetItemBase());
        }

        SetGUI("Background");
    }*/


    /// <summary>
    /// Adds given item to inventory with a count option which determines 
    /// how many of the same item will be added to inventory. Replaced component amount 
    /// can be used for craftables which allows crafting system to replace the item weights of
    /// the crafting components so that the remaining inventory size can be calculated accordingly for
    /// craftable item.
    /// </summary>
    /// <param name="newItem"></param>
    /// <param name="count"></param>
    /// <param name="replacedComponentAmount"></param>
    /// <returns></returns>
    public bool AddItemToInventory(ItemBase newItem, int count = 1,int replacedComponentAmount = 0)
    {
        ///Check the Inventory Size and Return if not enough.
        if(((this.CurrentWeight-replacedComponentAmount) + (newItem.ItemWeight * count)) > this.InventorySize) { return false; }

        if (IsItemInInventory(newItem))
        {//If it is in the inventory
            IncrementItemCount(newItem,count); //Increment its count
        }
        else
        {
            this.itemList.Add(new InventoryItemCount(newItem,count)); //Add it to the inventory otherwise

        }

        //Increment the current weight
        this.CurrentWeight += (newItem.ItemWeight * count);
        return true;
    }

    /// <summary>
    /// Removes given amount of given item to inventory, count is 1 by default
    /// </summary>
    /// <param name="newItem"></param>
    /// <param name="count"></param>
    public bool RemoveItemFromInventory(ItemBase newItem, int count = 1)
    {
        var itemCount = GetItemCount(newItem);

        //There is not that much of this item.
        if (itemCount < count) { return false; }

        if (itemCount > count + 1)
        {
            DecrementItemCount(newItem,count);
        }
        else//Delete the item otherwise
        {
            DeleteItem(newItem);
        }

        //Decrement the current weight
        this.CurrentWeight -= (newItem.ItemWeight * count);
        return true;
    }


    /// <summary>
    /// Checks if there is any item that suits to given itembase or not.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsItemInInventory(ItemBase item)
    {
        foreach(var listItem in this.itemList)
        {
            if (listItem.item.Equals(item)) { return true; }
        }

        return false;
    }


    /// <summary>
    /// Returns the count of the requested item.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetItemCount(ItemBase item)
    {
        foreach (var listItem in this.itemList)
        {
            if (listItem.item.Equals(item)) { return listItem.itemCount; }
        }

        return 0;
    }


    /// <summary>
    /// Increment the count of the given item by the given count in the inventory
    /// </summary>
    /// <param name="item"></param>
    private void IncrementItemCount(ItemBase item, int count = 1)
    {
        for (int i = 0; i < this.itemList.Count; i++)
        {
            if (this.itemList[i].item.Equals(item)) { itemList[i].IncrementCount(count); return; }
        }
    }

    /// <summary>
    /// Decrements the count of the given item by the given count in the inventory
    /// </summary>
    /// <param name="item"></param>
    private void DecrementItemCount(ItemBase item, int count = 1)
    {
        for (int i = 0; i < this.itemList.Count; i++)
        {
            if (this.itemList[i].item.Equals(item)) { itemList[i].DecrementCount(count); return; }
        }
    }

    /// <summary>
    /// Deletes the item with given itembase from the inventory
    /// </summary>
    /// <param name="item"></param>
    private void DeleteItem(ItemBase item)
    {
        foreach (var listItem in this.itemList)
        {
            if (listItem.item.Equals(item)) { itemList.Remove(listItem); return; }
        }
    }
	

    /// <summary>
    /// Using the item list, it creates visual entries for inventory GUI,
    /// and each entry is attached the specialized item script to be able to
    /// keep track of the selected items easily later on.
    /// </summary>
    public void SetGUI(string parent = "Background")
    {
        var p = GameObject.Find(parent).gameObject;
        
        for(int i = 0; i < p.transform.childCount; i ++)
        {
            Destroy(p.transform.GetChild(i).gameObject);
        }

        float initialX = -250;
        float initialY = 100;
        int count = 0;

        foreach(var x in itemList)
        {
            for(int i = 0; i < x.itemCount; i ++)
            {
                var itemEntry = Instantiate(InventoryGUIChild,  new Vector3(initialX, initialY, 0), Quaternion.identity,p.transform) as GameObject;
                itemEntry.transform.localPosition = new Vector3(initialX, initialY, 0);


                initialX += 100;

                count++;
                if (count % 6 == 0)
                {
                    initialX = -250;
                    initialY -= 100;
                    count = 0;
                }

                ItemBase.SetObjectDetails(itemEntry,x.item);
               
                var gi = itemEntry.GetComponent<IGameItem>();

                gi.SetItemBase(x.item);

                itemEntry.GetComponent<Image>().sprite = gi.GetItemBase().ItemIcon;
            }
           
        }

    }
}
