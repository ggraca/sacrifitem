using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField]
	private GameObject disabledUI, selectedUI;
	[SerializeField]
	private GameObject player1, player2;
	private PlayerStatus[] playerStat = new PlayerStatus[2];
	private Inventory[] playerInv = new Inventory[2];
	private HealthBar[] playerHB = new HealthBar[2];
	private int currentPlayer = 0, opponent = 1;

	private IGameItem equiped, discarded;

	[SerializeField]
	private int startingInvSize = 5;
	
	[SerializeField]
	private  CentralItemManager cim;

	[SerializeField]
	private GameObject InventoryGUIChild;

	private GameObject[] disableSacrifices = new GameObject[2];

	private bool sacrificed = false;

	void Start () {
		playerStat[0] = player1.GetComponent<PlayerStatus>();
		playerStat[1] = player2.GetComponent<PlayerStatus>();
		playerInv[0] = player1.GetComponent<Inventory>();
		playerInv[1] = player2.GetComponent<Inventory>();
		playerHB[0] = GameObject.Find("HealthBarPlayer1").gameObject.GetComponent<HealthBar>();
		playerHB[1] = GameObject.Find("HealthBarPlayer2").gameObject.GetComponent<HealthBar>();
		disableSacrifices[0] = GameObject.Find("DisableSacrificeP1").gameObject;
		disableSacrifices[1] = GameObject.Find("DisableSacrificeP2").gameObject;
		disableSacrifices[0].SetActive(false);

		for(int i = 0; i < startingInvSize; i++) {
			playerInv[0].AddItemToInventory(cim.getRandomItem());
			playerInv[1].AddItemToInventory(cim.getRandomItem());	
		}

		// First Turn
		playerInv[0].AddItemToInventory(cim.getRandomItem());

		playerInv[0].SetGUI("BackgroundP1");
		playerInv[1].SetGUI("BackgroundP2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Sacrifice(IGameItem item) {
		if (equiped != null && equiped == item) return;
		discarded = item;
		SetGUI(item, "DiscardedSlot1");
	}

	public void Equip(IGameItem item) {
		if (discarded != null && discarded == item) return;
		equiped = item;
		SetGUI(item, "EquipedSlot");
	}

	public void EndTurn() {
		if (discarded == null || equiped == null) return;

		RemoveFromInv(discarded);
		RemoveFromInv(equiped);

		equiped.UseItem();

		// TODO: check for win condition

		ClearSlots();
		playerStat[currentPlayer].UpdatePlayerStatus();
		playerHB[currentPlayer].CurrentHealthPlayer = playerStat[currentPlayer].CurrentPlayerHealth;
		playerHB[opponent].CurrentHealthPlayer = playerStat[opponent].CurrentPlayerHealth;

		// Change Turn

		int temp = currentPlayer;
		currentPlayer = opponent;
		opponent = temp;
		sacrificed = false;
		ChangeTurnUI();
		
		// TODO: check for win condition

		// Give a new card to the player
		playerInv[currentPlayer].AddItemToInventory(cim.getRandomItem());
		if (playerStat[currentPlayer].CurrentPlayerHealth <= 10)
			playerInv[currentPlayer].AddItemToInventory(cim.getRandomItem());

		playerInv[currentPlayer].SetGUI("BackgroundP" + (currentPlayer + 1).ToString());
	}

	void ChangeTurnUI() {
		disabledUI.GetComponent<RectTransform>().anchoredPosition *= new Vector2(-1, 1);
		selectedUI.GetComponent<RectTransform>().anchoredPosition *= new Vector2(-1, 1);

		disableSacrifices[currentPlayer].SetActive(false);
		disableSacrifices[opponent].SetActive(true);
	}

	public PlayerStatus ReceivePlayerStat() {
		return playerStat[currentPlayer];
	}
	public PlayerStatus ReceiveOponentStat() {
		return playerStat[opponent];
	}

	public Inventory ReceivePlayerInv() {
		return playerInv[currentPlayer];
	}
	public Inventory ReceiveOponentInv() {
		return playerInv[opponent];
	}

	private void RemoveFromInv(IGameItem item) {
		playerInv[currentPlayer].RemoveItemFromInventory(item.GetItemBase());
		playerInv[currentPlayer].SetGUI("BackgroundP" + (currentPlayer + 1).ToString());
	}

	private void SetGUI(IGameItem gi, string parentName) {
		GameObject parent = GameObject.Find(parentName).gameObject;
		if (parent.transform.childCount > 2) 
            Destroy(parent.transform.GetChild(parent.transform.childCount - 1).gameObject);

		GameObject itemEntry = Instantiate(InventoryGUIChild, new Vector3(0, 0, 0), Quaternion.identity, parent.transform) as GameObject;
        itemEntry.transform.localPosition = new Vector3(0, 0, 0);
		itemEntry.GetComponent<Image>().sprite = gi.GetItemBase().ItemIcon;
		itemEntry.GetComponent<SlotEntry>().SetGameLogic(this.gameObject);
	}

	private void ClearSlots() {
		ClearSlot("EquipedSlot");
		ClearSlot("DiscardedSlot1");
		ClearSlot("DiscardedSlot2");
		equiped = null;
		discarded = null;
	}

	private void ClearSlot(string name) {
		GameObject parent = GameObject.Find(name).gameObject;
		if (parent.transform.childCount > 2) 
            Destroy(parent.transform.GetChild(parent.transform.childCount - 1).gameObject);
	}

	public void RemoveItem(string slotName) {
		print(slotName);
		ClearSlot(slotName);
		if (slotName == "EquipedSlot") equiped = null;
		if (slotName == "DiscardedSlot1") discarded = null;
	}

	public void Sacrifice() {
		if (sacrificed) return;

		playerStat[currentPlayer].TakeDamage(2);
		playerInv[currentPlayer].AddItemToInventory(cim.getRandomItem());
		playerInv[currentPlayer].SetGUI("BackgroundP" + (currentPlayer + 1).ToString());
		playerHB[currentPlayer].CurrentHealthPlayer = playerStat[currentPlayer].CurrentPlayerHealth;

		disableSacrifices[currentPlayer].SetActive(true);
		
		sacrificed = true;
	}
}
