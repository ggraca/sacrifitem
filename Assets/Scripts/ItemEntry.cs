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

	// Use this for initialization
	void Start ()
    {
		toolBox = GameObject.FindGameObjectWithTag("Toolbox").gameObject;
	}



    public void OpenToolbox()
    {
        toolBox.transform.GetChild(0).gameObject.SetActive(true);
        Vector3 cameraPos = Input.mousePosition;

        toolBox.transform.position = new Vector3(cameraPos.x + xOffset , cameraPos.y + yOffset, 0.0f);

        ItemBase currentBase = GetComponent<IGameItem>().GetItemBase();

        toolBox.transform.Find("Toolbox").transform.Find("ItemName").gameObject.GetComponent<Text>().text = currentBase.Name;
        toolBox.transform.Find("Toolbox").transform.Find("ItemDescription").gameObject.GetComponent<Text>().text = currentBase.Description;

    }

    public void CloseToolbox()
    {
        toolBox.transform.GetChild(0).gameObject.SetActive(false);
    }


}
