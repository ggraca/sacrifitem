using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour,IGameItem {

    [Tooltip("Basic data for the item.")]
    [SerializeField]
    private ItemBase itemBase;

      public void SetItemBase(ItemBase baseForItem) { itemBase = baseForItem; }
    public ItemBase GetItemBase() { return itemBase; }

    public void EquipItem()
    {
        GetGameLogic().Equip(GetComponent<IGameItem>());
    }

    public void SacrificeItem()
    {
        GetGameLogic().Sacrifice(GetComponent<IGameItem>());
    }

    public void UseItem()
    {
        switch(itemBase.SecondaryType)
        {
            case ItemBase.SecondaryItemType.ItemShuffle: ItemShuffle();return;
            case ItemBase.SecondaryItemType.PowerUp: PowerUp();return;
            case ItemBase.SecondaryItemType.ExtraSacrifice: ExtraSacrifice();return;
            case ItemBase.SecondaryItemType.BuffRemoval: Silence();return;
            case ItemBase.SecondaryItemType.Steal: Steal();return;

        }
    }

    private void PowerUp()
    {

    }

    private void ExtraSacrifice()
    {

    }

    private void ItemShuffle()
    {

    }

    private void Silence()
    {

    }

    private void Steal()
    {

    }

    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    //TODO Speacialize according to secondary categories


}
