using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SacrificeEntry : MonoBehaviour {


    GameObject toolBox;

    [SerializeField]
    float xOffset = 0;
    [SerializeField]
    float yOffset = -150;

    private bool isMouseOver = false;


	// Use this for initialization
	void Start ()
    {
		toolBox = GameObject.FindGameObjectWithTag("Toolbox").gameObject;
	}

    void Update()
    {
        if(isMouseOver)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject.Find("GameLogic").GetComponent<GameLogic>().Sacrifice();
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

    public void OpenToolbox()
    {
        toolBox.transform.GetChild(0).gameObject.SetActive(true);
        Vector3 cameraPos = Input.mousePosition;

        toolBox.transform.position = new Vector3(transform.position.x + xOffset , transform.position.y + yOffset, 0.0f);

        toolBox.transform.Find("Toolbox").transform.Find("ItemName").gameObject.GetComponent<Text>().text = "Sacrifice";
        toolBox.transform.Find("Toolbox").transform.Find("ItemDescription").gameObject.GetComponent<Text>().text = "Sacrifice 2HP and gain a new item";
        toolBox.transform.Find("Toolbox").transform.Find("SecondDescription").gameObject.GetComponent<Text>().text = "If your HP is 10 or less, gain one extra item at the start of your turn";

    }

    public void CloseToolbox()
    {
        toolBox.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void HoverOn()
    {
        GetComponent<Image>().color = new Color(255,255,255,120);
    }

    public void HoverOff()
    {
        GetComponent<Image>().color = new Color(255,255,255,255);
    }
    public void UseItem()
    {
        GetComponent<IGameItem>().UseItem();
    }
}
