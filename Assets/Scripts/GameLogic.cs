using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	[SerializeField]
	private GameObject disabledUI, selectedUI;
	[SerializeField]
	private GameObject player1, player2;
	private PlayerStatus[] playerStat = new PlayerStatus[2];
	private Inventory[] playerInv = new Inventory[2];
	private int currentPlayer = 0, opponent = 1;

	private IGameItem equiped, discarded;

	[SerializeField]
	private int startingInvSize = 5;
	
	[SerializeField]
	private  CentralItemManager cim;

	void Start () {
		playerStat[0] = player1.GetComponent<PlayerStatus>();
		playerStat[1] = player2.GetComponent<PlayerStatus>();
		playerInv[0] = player1.GetComponent<Inventory>();
		playerInv[1] = player2.GetComponent<Inventory>();

		for(int i = 0; i < startingInvSize; i++) {
			playerInv[0].AddItemToInventory(cim.getRandomItem());
			playerInv[1].AddItemToInventory(cim.getRandomItem());	
		}

		playerInv[0].SetGUI("BackgroundP1");
		playerInv[1].SetGUI("BackgroundP2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Sacrifice(IGameItem item) {
		discarded = item;
	}

	public void Equip(IGameItem item) {
		equiped = item;
	}

	public void EndTurn() {
		if (discarded == null || equiped == null) return;

		removeFromInv(discarded);
		removeFromInv(equiped);

		equiped.UseItem();

		// TODO: check for win condition

		ChangeTurnUI();
		int temp = currentPlayer;
		currentPlayer = opponent;
		opponent = temp;
		equiped = null;
		discarded = null;
		// TODO: check for win condition
	}

	void ChangeTurnUI() {
		disabledUI.GetComponent<RectTransform>().anchoredPosition *= new Vector2(-1, 1);
		selectedUI.GetComponent<RectTransform>().anchoredPosition *= new Vector2(-1, 1);
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

	private void removeFromInv(IGameItem item) {
		playerInv[currentPlayer].RemoveItemFromInventory(item.GetItemBase());
	}
}
