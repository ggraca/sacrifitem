using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public static void OpenInventoryTab()
	{
		var inventory = GameObject.FindGameObjectWithTag("Inventory").gameObject;

		inventory.SetActive(true);
	}
	public static void CloseInventoryTab()
	{
		var inventory = GameObject.FindGameObjectWithTag("Inventory").gameObject;

		inventory.SetActive(false);
	}

}
