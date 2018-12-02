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
            case ItemBase.SecondaryItemType.DebuffRemoval: RemoveDebuff();return;
            case ItemBase.SecondaryItemType.Heal: Heal();return;
        }
    }

    private void RemoveDebuff()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            //TODO find out
        }

        currentPlayerStatus.RandomlyRemoveDebuff(); return;


    }

    private void Heal()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            currentPlayerStatus.HealSelf(GetComponent<IGameItem>().GetItemBase().ItemValue + 1); return;
        }

        currentPlayerStatus.HealSelf(GetComponent<IGameItem>().GetItemBase().ItemValue); return;
    }



    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }

}
