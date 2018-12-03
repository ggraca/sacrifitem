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
	private PlayerAnimation[] playerAnim = new PlayerAnimation[2];
	private int currentPlayer = 0, opponent = 1;

	private IGameItem equiped, discarded, discarded2;

	[SerializeField]
	private int startingInvSize = 5;
	
	[SerializeField]
	private  CentralItemManager cim;

	[SerializeField]
	private GameObject SlotGUIEntry, BuffGUIChild;

	private GameObject[] disableSacrifices = new GameObject[2];

	public Sprite[] icons;

	private bool sacrificed = false;

	[SerializeField]
	private GameObject GameOverCanvas;

	[SerializeField]
	private List<AudioClip> clips;

	void Start () {
		playerStat[0] = player1.GetComponent<PlayerStatus>();
		playerStat[1] = player2.GetComponent<PlayerStatus>();
		playerInv[0] = player1.GetComponent<Inventory>();
		playerInv[1] = player2.GetComponent<Inventory>();
		playerAnim[0] = player1.GetComponent<PlayerAnimation>();
		playerAnim[1] = player2.GetComponent<PlayerAnimation>();
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
		if (discarded == null) {
			discarded = item;
			SetGUI(item, "DiscardedSlot1");
		} else {
			if (discarded == item || playerStat[currentPlayer].IsSilenced) return;
			discarded2 = item;
			SetGUI(item, "DiscardedSlot2");
		}
	}

	public void Equip(IGameItem item) {
		if (discarded != null && discarded == item) return;
		if (discarded2 != null && discarded2 == item) return;
		equiped = item;
		SetGUI(item, "EquipedSlot");
	}

	public void EndTurn() {
		if (discarded == null || equiped == null) return;

		RemoveFromInv(equiped);
		RemoveFromInv(discarded);
		if (discarded2 != null) {
			RemoveFromInv(discarded2);
			playerStat[currentPlayer].EnablePowerUp();
		}

		equiped.UseItem();
		playerInv[currentPlayer].SetGUI("BackgroundP" + (currentPlayer + 1).ToString());
		playerInv[opponent].SetGUI("BackgroundP" + (opponent + 1).ToString());

		playerAnim[currentPlayer].SendMessage(equiped.GetItemBase().AnimationName);
		PlaySound(equiped.GetItemBase().name);

		// End of Attack Validation
		if (playerStat[0].IsDead || playerStat[1].IsDead) GameOver();

		ClearSlots();
		playerStat[currentPlayer].UpdatePlayerStatus();
		playerHB[currentPlayer].CurrentHealthPlayer = playerStat[currentPlayer].CurrentPlayerHealth;
		playerHB[opponent].CurrentHealthPlayer = playerStat[opponent].CurrentPlayerHealth;

		UpdateBuffsUI(0);
		UpdateBuffsUI(1);

		// End of Turn Validation
		if (playerStat[0].IsDead || playerStat[1].IsDead) GameOver();
		
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
	}

	private void SetGUI(IGameItem gi, string parentName) {
		GameObject parent = GameObject.Find(parentName).gameObject;
		if (parent.transform.childCount > 2) 
            Destroy(parent.transform.GetChild(parent.transform.childCount - 1).gameObject);

		GameObject itemEntry = Instantiate(SlotGUIEntry, new Vector3(0, 0, 0), Quaternion.identity, parent.transform) as GameObject;
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
		discarded2 = null;
	}

	private void ClearSlot(string name) {
		GameObject parent = GameObject.Find(name).gameObject;
		if (parent.transform.childCount > 2) 
            Destroy(parent.transform.GetChild(parent.transform.childCount - 1).gameObject);
	}

	public void RemoveItem(string slotName) {
		ClearSlot(slotName);
		if (slotName == "EquipedSlot") equiped = null;
		if (slotName == "DiscardedSlot1") discarded = null;
		if (slotName == "DiscardedSlot2") discarded2 = null;
	}

	public void Sacrifice() {
		if (sacrificed) return;

		playerStat[currentPlayer].TakeDamage(2);
		playerInv[currentPlayer].AddItemToInventory(cim.getRandomItem());
		playerInv[currentPlayer].SetGUI("BackgroundP" + (currentPlayer + 1).ToString());
		playerHB[currentPlayer].CurrentHealthPlayer = playerStat[currentPlayer].CurrentPlayerHealth;

		disableSacrifices[currentPlayer].SetActive(true);
		
		sacrificed = true;

		// End of Turn Validation
		if (playerStat[0].IsDead || playerStat[1].IsDead) GameOver();
	}

	private void UpdateBuffsUI(int p){
		GameObject buffEntry;

		GameObject buffs = GameObject.Find("BuffsP" + (p + 1).ToString()).gameObject;
		for(int i = 0; i < buffs.transform.childCount; i ++)
            Destroy(buffs.transform.GetChild(i).gameObject);

		float currentPos = 0;
		foreach(var pb in playerStat[p].PlayerBuffs) {
			Sprite icon;
			switch (pb) {
				case PlayerStatus.BuffTypes.Reflect:
					icon = icons[0];
					break;
				case PlayerStatus.BuffTypes.Shield:
					icon = icons[1];
					break;
				default:
					icon = icons[0];
					break;
			}

			buffEntry = Instantiate(BuffGUIChild,  new Vector3(0, 0, 0), Quaternion.identity, buffs.transform) as GameObject;
        	buffEntry.transform.localPosition = new Vector3(0, currentPos, 0);
			buffEntry.GetComponent<Image>().sprite = icon;

			currentPos -= 40;
		}

		foreach(var pb in playerStat[p].PlayerDebuffs) {
			Sprite icon;

			switch (pb) {
				case PlayerStatus.DebuffTypes.Poison:
					icon = icons[2];

					for(int i = 0 ; i < playerStat[p].PoisonValue; i ++) {
						buffEntry = Instantiate(BuffGUIChild,  new Vector3(0, 0, 0), Quaternion.identity, buffs.transform) as GameObject;
						buffEntry.transform.localPosition = new Vector3(0, currentPos, 0);
						buffEntry.GetComponent<Image>().sprite = icon;

						currentPos -= 40;
					}

					break;
				case PlayerStatus.DebuffTypes.Silence:
					icon = icons[3];
					buffEntry = Instantiate(BuffGUIChild,  new Vector3(0, 0, 0), Quaternion.identity, buffs.transform) as GameObject;
					buffEntry.transform.localPosition = new Vector3(0, currentPos, 0);
					buffEntry.GetComponent<Image>().sprite = icon;

					currentPos -= 40;
					break;
			}
		}
	}

	public void PlaySound(string itemName) {
        var clip = clips.Find(x => x.name.Equals(itemName.ToLower()));
        MSManager.PlaySound("SoundPlayer", clip);
    }

	private void GameOver() {
		GameOverCanvas.SetActive(true);
		
		int loser = (playerStat[0].IsDead) ? 1 : 2;
		GameObject.Find("WinnerP" + loser.ToString()).GetComponent<Text>().text = "";
		
		playerAnim[loser - 1].SendMessage("Dead");
	}
}
