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
            case ItemBase.SecondaryItemType.Shield: Shield();return;

        }
    }


    private void Shield()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        
        currentPlayerStatus.EnableShield();

        if(currentPlayerStatus.IsPowerUp)
        {
            currentPlayerStatus.ShieldBlockPos = 1.0f;
        }
    }
    private void PowerUp()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        
        currentPlayerStatus.EnablePowerUp();
    }

    private void ExtraSacrifice()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            //TODO figure out
        }

       if(!opponentPlayerStatus.IsShieldBlocking()) { opponentPlayerStatus.EnableDoubleSacrifice();}
    }

    private void ItemShuffle()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();
        var currentPlayerInventory = gl.ReceivePlayerInv();
        var opponentPlayerInventory = gl.ReceiveOponentInv();

        if(opponentPlayerStatus.IsShieldBlocking()) { return;}

        var firstCount = 0;
        var secondCount = 0;

        List<ItemBase> items = new List<ItemBase>();

        foreach(var x in currentPlayerInventory.ItemList)
        {
            for(int i =0; i < x.itemCount ; i++)
            {
                items.Add(x.item);
                firstCount++;
            }
        }

        currentPlayerInventory.ClearInventory();

        foreach(var x in opponentPlayerInventory.ItemList)
        {
            for(int i =0; i < x.itemCount ; i++)
            {
                items.Add(x.item);
                secondCount++;
            }
        }

        opponentPlayerInventory.ClearInventory();

        items = ShuffleList(items);

        if(currentPlayerStatus.IsPowerUp)
        {
            for(int i =0; i < (items.Count+1)/2 ; i++)
            {
                currentPlayerInventory.AddItemToInventory(items[i]);
            }
            for(int i =((items.Count+1)/2); i < items.Count ; i++)
            {
                opponentPlayerInventory.AddItemToInventory(items[i]);
            }

            return;
        }

        print(firstCount);
        print(secondCount);
        print(items.Count);
        for(int i =0; i < firstCount ; i++)
        {
            currentPlayerInventory.AddItemToInventory(items[i]);
        }
        for(int i = firstCount; i < items.Count ; i++)
        {
            print(opponentPlayerInventory.AddItemToInventory(items[i]));
        }
    }

    private void Silence()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();

        if(currentPlayerStatus.IsPowerUp)
        {
            if(!opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.RandomlyRemoveBuff();}
        }

        if(!opponentPlayerStatus.IsShieldBlocking()) {opponentPlayerStatus.RandomlyRemoveBuff();}
    }

    private void Steal()
    {
        var gl = GetGameLogic();
        var currentPlayerStatus = gl.ReceivePlayerStat();
        var opponentPlayerStatus = gl.ReceiveOponentStat();
        var currentPlayerInventory = gl.ReceivePlayerInv();
        var opponentPlayerInventory = gl.ReceiveOponentInv();

        if(opponentPlayerStatus.IsShieldBlocking()) { return;}

        if(currentPlayerStatus.IsPowerUp)
        {
            for(int i =0; i < GetComponent<IGameItem>().GetItemBase().ItemValue + 1 ; i ++)
            {
                if(opponentPlayerInventory.ItemList.Count == 0 ){return;}
                var rn = Random.Range(0,opponentPlayerInventory.ItemList.Count);
                var item = opponentPlayerInventory.ItemList[rn].item;

                currentPlayerInventory.AddItemToInventory(item);
                opponentPlayerInventory.RemoveItemFromInventory(item);
            }
            return;
        }

       for(int i =0; i < GetComponent<IGameItem>().GetItemBase().ItemValue ; i ++)
        {
            if(opponentPlayerInventory.ItemList.Count == 0 ){return;}
            var rn = Random.Range(0,opponentPlayerInventory.ItemList.Count);
            var item = opponentPlayerInventory.ItemList[rn].item;

            currentPlayerInventory.AddItemToInventory(item);
            opponentPlayerInventory.RemoveItemFromInventory(item);
        }
    }

    private GameLogic GetGameLogic()
    {
        return GameObject.FindObjectOfType<GameLogic>();
    }
    //TODO Speacialize according to secondary categories

    private List<ItemBase> ShuffleList(List<ItemBase> l)
    {
        for (int i = 0; i < l.Count; i++) 
        {
                var temp = l[i];
                int randomIndex = Random.Range(i, l.Count);
                l[i] = l[randomIndex];
                l[randomIndex] = temp;
        }

        return l;
    }
    
}
