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
            case ItemBase.SecondaryItemType.Damage: Damage();return;
            case ItemBase.SecondaryItemType.DamageOverTime: DamageOverTime();return;
        }
    }


    private void Damage()
    {

    }

    private void DamageOverTime()
    {

    }

    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    #endregion

    //TODO Speacialize according to secondary categories
}
