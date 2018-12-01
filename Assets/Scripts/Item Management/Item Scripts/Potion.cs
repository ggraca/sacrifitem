using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour,IGameItem {

    [Tooltip("Basic data for the item.")]
    [SerializeField]
    private ItemBase itemBase;


    public void SetItemBase(ItemBase baseForItem) { itemBase = baseForItem; }
    public ItemBase GetItemBase() { return itemBase; }
    

    public void UseItem()
    {
        //Apply potion effect.
    }


    //TODO Speacialize according to secondary categories

}
