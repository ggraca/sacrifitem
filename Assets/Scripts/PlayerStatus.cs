using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*That class contains the status of the player like is he being poisoned or does he have an active shield etc. */
public class PlayerStatus : MonoBehaviour 
{
	#region Member Variables Here
	//For example 
	private bool isPoisoned = false; // false to begin with
	private int poisonValue = 0;
	//TODO fill with other status properties for the player
	#endregion


	#region Setters and Getters

	//TODO fill with setters and getters for the member variables
	#endregion

	
	#region Status Change Methods
	//for example 
	public void PoisonPlayer(int poisonAmount)
	{	
		if(isPoisoned)
		{
			//TODO if the player has already been poisoned you may want to stack the effect
			//You can choose to do that by having a poison value or by having a stack amount, up to you, i put value for demo
			poisonAmount +=poisonAmount;
		}
		else
		{
			isPoisoned = true;
			poisonValue += poisonAmount;
		}
	}
	//Removal of the status should be there as well i think
	public void RemovePoison()
	{
		isPoisoned = false;
		poisonValue = 0;
	}

	//TODO fill with other status scripts 

	#endregion
}