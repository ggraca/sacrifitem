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
            case ItemBase.SecondaryItemType.DamageOverTimePoison: DamageOverTimePoison();return;
        }
    }


    private void Damage()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            if(opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.TakeDamage(GetComponent<IGameItem>().GetItemBase().ItemValue + 1); return;}
        }

        if(opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.TakeDamage(GetComponent<IGameItem>().GetItemBase().ItemValue);}
    }

    private void DamageOverTimePoison()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            if(opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.PoisonPlayer(GetComponent<IGameItem>().GetItemBase().ItemValue + 1); return;}
        }

        if(opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.PoisonPlayer(GetComponent<IGameItem>().GetItemBase().ItemValue); return;}

    }

    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    #endregion

    //TODO Speacialize according to secondary categories
}
