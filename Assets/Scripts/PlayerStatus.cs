using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*That class contains the status of the player like is he being poisoned or does he have an active shield etc. */
public class PlayerStatus : MonoBehaviour 
{
	
	public enum BuffTypes{PowerUp,Shield};
	public enum DebuffTypes{Poison};
	#region Member Variables Here
	private bool isPoisoned = false; 
	private int poisonValue = 0;

	private int maxPlayerHealth;
	private int currentPlayerHealth;
	private bool isDead = false;
	private bool isPowerUp = false;
	private bool shouldDisablePowerUp = false;
	private bool isDoubleSacrificeReq = false;

	private List<DebuffTypes> playerDebuffs;
	private List<BuffTypes> playerBuffs;

	private bool isShielded = false;
	private float shieldBlockPos =0.3f;
	private float startingBlockPos = 0.3f;
	

	#endregion

	public PlayerStatus()
	{
		this.maxPlayerHealth = 20;
		this.currentPlayerHealth = 0;
		this.isPoisoned = false;
		this.poisonValue = 0;
		this.isDead = false;
		this.isPowerUp = false;
		playerDebuffs = new List<DebuffTypes>();
		this.isDoubleSacrificeReq = false;
		this.shouldDisablePowerUp = false;
		playerBuffs = new List<BuffTypes>();
		this.isShielded = false;
		this.shieldBlockPos = 0.3f;
		this.startingBlockPos = 0.3f;
	}

	public PlayerStatus(int maxHealth,float shieldBlock)
	{
		this.maxPlayerHealth = maxHealth;
		this.currentPlayerHealth = 0;
		this.isPoisoned = false;
		this.poisonValue = 0;
		this.isDead = false;
		this.isPowerUp = false;
		playerDebuffs = new List<DebuffTypes>();
		this.isDoubleSacrificeReq = false;
		this.shouldDisablePowerUp = false;
		playerBuffs = new List<BuffTypes>();
		this.isShielded = false;
		this.shieldBlockPos = shieldBlock;
		this.startingBlockPos = shieldBlock;
	}

    
    #region Setters and Getters

    public bool IsPoisoned
    {
        get
        {
            return isPoisoned;
        }

        set
        {
            isPoisoned = value;
        }
    }

    public int PoisonValue
    {
        get
        {
            return poisonValue;
        }

        set
        {
            poisonValue = value;
        }
    }

    public int MaxPlayerHealth
    {
        get
        {
            return maxPlayerHealth;
        }

        set
        {
            maxPlayerHealth = value;
        }
    }

    public int CurrentPlayerHealth
    {
        get
        {
            return currentPlayerHealth;
        }

        set
        {
            currentPlayerHealth = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public bool IsPowerUp
    {
        get
        {
            return isPowerUp;
        }

        set
        {
            isPowerUp = value;
        }
    }

    public List<DebuffTypes> PlayerDebuffs
    {
        get
        {
            return playerDebuffs;
        }

        set
        {
            playerDebuffs = value;
        }
    }

    public bool IsDoubleSacrificeReq
    {
        get
        {
            return isDoubleSacrificeReq;
        }

        set
        {
            isDoubleSacrificeReq = value;
        }
    }

    public float ShieldBlockPos
    {
        get
        {
            return shieldBlockPos;
        }

        set
        {
            shieldBlockPos = value;
        }
    }

    public bool IsShielded
    {
        get
        {
            return isShielded;
        }

        set
        {
            isShielded = value;
        }
    }
    #endregion


    #region Status Change Methods
    //for example 
    public void PoisonPlayer(int poisonAmount)
	{	
		if(isPoisoned)
		{
			poisonAmount +=poisonAmount;
		}
		else
		{
			playerDebuffs.Add(DebuffTypes.Poison);
			isPoisoned = true;
			poisonValue += poisonAmount;
		}
	}
	//Removal of the status should be there as well i think
	public void RemovePoison()
	{
		playerDebuffs.Remove(DebuffTypes.Poison);
		isPoisoned = false;
		poisonValue = 0;
	}

	public void TakeDamage(int damage)
    {
        if(currentPlayerHealth - damage <= 0 )
        {
            isDead = true;return;
        }

        currentPlayerHealth -= damage;
    }

    public void HealSelf(int healAmount)
    {
        if(currentPlayerHealth + healAmount > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;return;
        }
        currentPlayerHealth += healAmount;
    }

	public void EnablePowerUp()
	{
		isPowerUp = true;
	}

	public void DisablePowerUp()
	{
		isPowerUp = false;
		playerBuffs.Remove(BuffTypes.PowerUp);
	}

	public void EnableDoubleSacrifice()
	{
		isDoubleSacrificeReq = true;
	}

	public void DisableDoubleSacrifice()
	{
		isDoubleSacrificeReq = false;
	}

	public void EnableShield()
	{
		isShielded = true;
	}

	public bool IsShieldBlocking()
	{
		if(IsShielded && Random.value < ShieldBlockPos)
		{
			DisableShield();
			return true;
		}
		return false;
	}
	public void DisableShield()
	{
		isShielded = false;
		shieldBlockPos = startingBlockPos;
	}

	public void UpdatePlayerStatus()
	{
		if(isPoisoned)
		{
			TakeDamage(poisonValue);
		}

		if(IsPowerUp && shouldDisablePowerUp)
		{
			DisablePowerUp();
			shouldDisablePowerUp = false;
		}
		else if(IsPowerUp && ! shouldDisablePowerUp)
		{
			shouldDisablePowerUp = true;
		}
		else if(!IsPowerUp && shouldDisablePowerUp)
		{
			shouldDisablePowerUp = false;
		}

		if(IsDoubleSacrificeReq)
		{
			DisableDoubleSacrifice();
		}
	}

	private bool IsDebuffActive(DebuffTypes debuff)
	{
		foreach(var x in playerDebuffs){if(x.Equals(debuff)){return true;}}
		return false;
	}

	public void RandomlyRemoveDebuff()
	{
		var rn = Random.Range(0,playerDebuffs.Count);

		switch(playerDebuffs[rn])
		{
			case DebuffTypes.Poison: RemovePoison(); return;
		}

	}

	public void RandomlyRemoveBuff()
	{
		var rn = Random.Range(0,playerBuffs.Count);

		switch(playerBuffs[rn])
		{
			case BuffTypes.PowerUp: DisablePowerUp(); return;
			case BuffTypes.Shield : DisableShield();return;
		}

	}

	#endregion
}