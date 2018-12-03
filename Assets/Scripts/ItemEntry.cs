using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemEntry : MonoBehaviour {


    GameObject toolBox;

    [SerializeField]
    float xOffset = 0;
    [SerializeField]
    float yOffset = 30;

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
                GetComponent<IGameItem>().EquipItem();
            }
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                GetComponent<IGameItem>().SacrificeItem();
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

        ItemBase currentBase = GetComponent<IGameItem>().GetItemBase();

        toolBox.transform.Find("Toolbox").transform.Find("ItemName").gameObject.GetComponent<Text>().text = currentBase.Name;
        toolBox.transform.Find("Toolbox").transform.Find("ItemDescription").gameObject.GetComponent<Text>().text = "* " + currentBase.Description;
        toolBox.transform.Find("Toolbox").transform.Find("SecondDescription").gameObject.GetComponent<Text>().text = "** " +currentBase.SecondaryDescription;

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
