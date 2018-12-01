using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralItemManager : MonoBehaviour {


	private List<GameObject> itemData;

    #region Setters and Getters
    public List<GameObject> ItemData
    {
        get
        {
            return itemData;
        }

        set
        {
            itemData = value;
        }
    }
    #endregion

    // Use this for initialization
    void Awake ()
	{
        itemData = new List<GameObject>();
		LoadItems();
	}
	
	
	private void LoadItems()
	{
		Object[] items = Resources.LoadAll("Items",typeof(GameObject));

		foreach(GameObject x in items)
		{
			itemData.Add(x);
		}
	}

    public ItemBase getRandomItem() {
        int index = Random.Range(0, itemData.Count);
        return ItemData[index].GetComponent<IGameItem>().GetItemBase();
    }

}
