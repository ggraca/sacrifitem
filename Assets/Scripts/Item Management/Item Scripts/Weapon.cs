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
        GetGameLogic().Equip(this);
    }

    public void SacrificeItem()
    {
        GetGameLogic().Sacrifice(this);
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
            if(!opponentPlayerStatus.IsShieldBlocking()) 
            {
                if(!opponentPlayerStatus.DoesReflectionOccur())
                {
                    opponentPlayerStatus.TakeDamage(this.GetItemBase().ItemValue + 2); return;
                }
                else
                {
                    currentPlayerStatus.TakeDamage(this.GetItemBase().ItemValue + 2);
                    opponentPlayerStatus.HealSelf(2);
                    return;
                }
            }
        }

        if(!opponentPlayerStatus.IsShieldBlocking()) 
        {
            if(!opponentPlayerStatus.DoesReflectionOccur())
            {
                opponentPlayerStatus.TakeDamage(this.GetItemBase().ItemValue); return;
            }
            else
            {
                currentPlayerStatus.TakeDamage(this.GetItemBase().ItemValue );
                opponentPlayerStatus.HealSelf(2);
                return;
            }
        }
    }

    private void DamageOverTimePoison()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            if(!opponentPlayerStatus.IsShieldBlocking()) 
            {
                if(!opponentPlayerStatus.DoesReflectionOccur())
                {
                   opponentPlayerStatus.PoisonPlayer(this.GetItemBase().ItemValue + 1); return;
                }
                else
                {
                    currentPlayerStatus.PoisonPlayer(this.GetItemBase().ItemValue + 1);
                    opponentPlayerStatus.HealSelf(2);
                    return;
                }
            }
        }

        if(!opponentPlayerStatus.IsShieldBlocking()) 
        {
            if(!opponentPlayerStatus.DoesReflectionOccur())
            {   
                opponentPlayerStatus.PoisonPlayer(this.GetItemBase().ItemValue); return;
            }
            else
            {
                currentPlayerStatus.PoisonPlayer(this.GetItemBase().ItemValue);
                opponentPlayerStatus.HealSelf(2);
                return;
            }
        }

    }

    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    #endregion

    //TODO Speacialize according to secondary categories
}
