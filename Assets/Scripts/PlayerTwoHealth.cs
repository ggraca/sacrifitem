using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoHealth : MonoBehaviour
{

	public Slider healthBarPlayer2;
	public Image fillPlayer2;
	public int currentHealthPlayer2;
	public int maxHealthPlayer2;
	public Text healthTextPlayer2;
	bool isDead;

	void Start ()
	{
		currentHealthPlayer2 = maxHealthPlayer2;
	}
	
	void Update ()
	{
		healthBarPlayer2.maxValue = maxHealthPlayer2;
		healthBarPlayer2.value = currentHealthPlayer2;
		healthTextPlayer2.text = "Player 2: " + currentHealthPlayer2.ToString() + " / " + maxHealthPlayer2.ToString();

		if (currentHealthPlayer2 <= 0)
		{
			if(isDead)
			{
				return;
			}

			Dead();
		}

		if (Input.GetKeyDown (KeyCode.F))
		{
			if(currentHealthPlayer2 - 1 >= 0)currentHealthPlayer2 -= 1;
		}

		if (Input.GetKeyDown (KeyCode.D))
		{
			if(currentHealthPlayer2 + 1 <= 20)currentHealthPlayer2 += 1;
		}

		playerTwoHealthColor();
	}

	void playerTwoHealthColor()
	{
	if (healthBarPlayer2.value <= 0)
		{
			fillPlayer2.color = Color.red;
		}
	if (healthBarPlayer2.value > 0)
		{
			fillPlayer2.color = Color.green;
		}
	}

	void Dead()
	{
		isDead = true;
	}
}