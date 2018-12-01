using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour,IGameItem {

    [Tooltip("Basic data for the item.")]
    [SerializeField]
    private ItemBase itemBase;

    public void SetItemBase(ItemBase baseForItem) { itemBase = baseForItem; }
    public ItemBase GetItemBase() { return itemBase; }

    #region Not Implemented
   
    public void UseItem(){}
    #endregion

    //TODO Speacialize according to secondary categories
}
