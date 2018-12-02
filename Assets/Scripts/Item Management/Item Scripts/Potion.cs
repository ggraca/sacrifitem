using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour,IGameItem {

    [Tooltip("Basic data for the item.")]
    [SerializeField]
    private ItemBase itemBase;



    public void SetItemBase(ItemBase baseForItem) { itemBase = baseForItem; }
    public ItemBase GetItemBase() { return itemBase; }
    

    public void EquipItem()
    {

    }

    public void SacrificeItem()
    {

    }


    public void UseItem()
    {
        switch(itemBase.SecondaryType)
        {
            case ItemBase.SecondaryItemType.DebuffRemoval: RemoveDebuff();return;
            case ItemBase.SecondaryItemType.Heal: Heal();return;
        }
    }

    private void RemoveDebuff()
    {

    }

    private void Heal()
    {

    }



    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    //TODO Speacialize according to secondary categories

}
