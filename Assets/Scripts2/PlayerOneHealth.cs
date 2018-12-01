using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneHealth : MonoBehaviour
{

	public Slider healthBarPlayer1;
	public Image fillPlayer1;
	public int currentHealthPlayer1;
	public int maxHealthPlayer1;
	public Text healthTextPlayer1;
	bool isDead;

	void Start ()
	{
		currentHealthPlayer1 = maxHealthPlayer1;
	}
	
	void Update ()
	{
		healthBarPlayer1.maxValue = maxHealthPlayer1;
		healthBarPlayer1.value = currentHealthPlayer1;
		healthTextPlayer1.text = "Player 1: " + currentHealthPlayer1.ToString() + " / " + maxHealthPlayer1.ToString();

		if (currentHealthPlayer1 <= 0)
		{
			if(isDead)
			{
				return;
			}

			Dead();
		}

		if (Input.GetKeyDown (KeyCode.A))
		{
			if(currentHealthPlayer1 - 1 >= 0)currentHealthPlayer1 -= 1;
		}

		if (Input.GetKeyDown (KeyCode.S))
		{
			if(currentHealthPlayer1 + 1 <= 20)currentHealthPlayer1 += 1;
		}

		playerOneHealthColor();
	}

	void playerOneHealthColor()
	{
	if (healthBarPlayer1.value <= 0)
		{
			fillPlayer1.color = Color.red;
		}
	if (healthBarPlayer1.value > 0)
		{
			fillPlayer1.color = Color.green;
		}
	}

	void Dead()
	{
		isDead = true;
	}
}