using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEntry : MonoBehaviour {

	private GameObject gameLogic;
	private bool isMouseOver = false;
	
	void Update () {
		if(isMouseOver) {
            if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) {
				gameLogic.GetComponent<GameLogic>().RemoveItem(transform.parent.name);
            }
        }
	}

	public void HoverOnMouse()
    {
        isMouseOver = true;
    }


     public void HoverOffMouse()
    {
        isMouseOver = false;
    }

	public void SetGameLogic(GameObject gl) {
		gameLogic = gl;
	}
}
