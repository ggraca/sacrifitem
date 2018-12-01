using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	[SerializeField]
	private GameObject player1, player2;

	[SerializeField]
	[Tooltip("Amount of starting items")]
	private int startingInvSize = 5;
	
	[SerializeField]
	private  CentralItemManager cim;
	void Start () {
		Inventory p1Inv = player1.GetComponent<Inventory>();
		Inventory p2Inv = player2.GetComponent<Inventory>();
		for(int i = 0; i < startingInvSize; i++) {
			p1Inv.AddItemToInventory(cim.getRandomItem());
			p2Inv.AddItemToInventory(cim.getRandomItem());	
		}

		p1Inv.SetGUI("BackgroundP1");
		p2Inv.SetGUI("BackgroundP2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
