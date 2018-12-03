using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*That class contains the status of the player like is he being poisoned or does he have an active shield etc. */
public class PlayerStatus : MonoBehaviour 
{
	
	public enum BuffTypes{PowerUp,Shield,Reflect};
	public enum DebuffTypes{Poison,Silence};
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

	private bool isSilenced = false;
	private bool isReflecting = false;
	private float reflectPos = 0.55f;
	

	#endregion

	public PlayerStatus()
	{
		this.maxPlayerHealth = 20;
		this.currentPlayerHealth = maxPlayerHealth;
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
		this.isSilenced = false;
		this.isReflecting = false;
		this.reflectPos = 0.55f;

	}

	public PlayerStatus(int maxHealth,float shieldBlock)
	{
		this.maxPlayerHealth = maxHealth;
		this.currentPlayerHealth = maxHealth;
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
		this.isSilenced = false;
		this.isReflecting = false;
		this.reflectPos = 0.55f;
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

    public List<BuffTypes> PlayerBuffs
    {
        get
        {
            return playerBuffs;
        }

        set
        {
            playerBuffs = value;
        }
    }

    public List<DebuffTypes> PlayerDebuffs1
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

    public bool IsSilenced
    {
        get
        {
            return isSilenced;
        }

        set
        {
            isSilenced = value;
        }
    }

    public bool IsReflecting
    {
        get
        {
            return isReflecting;
        }

        set
        {
            isReflecting = value;
        }
    }

    public float ReflectPos
    {
        get
        {
            return reflectPos;
        }

        set
        {
            reflectPos = value;
        }
    }

    public bool IsReflecting1
    {
        get
        {
            return isReflecting;
        }

        set
        {
            isReflecting = value;
        }
    }
    #endregion


    #region Status Change Methods
    //for example 
    public void PoisonPlayer(int poisonAmount)
	{	
		if(isPoisoned)
		{
			this.poisonValue +=poisonAmount;
		}
		else
		{
			if(!IsDebuffActive(DebuffTypes.Poison)){playerDebuffs.Add(DebuffTypes.Poison);}
			isPoisoned = true;
			poisonValue += poisonAmount;
		}
	}
	//Removal of the status should be there as well i think
	public void RemovePoison()
	{

		if(poisonValue > 1)
		{
			poisonValue -= 1;
		}
		else
		{
			playerDebuffs.Remove(DebuffTypes.Poison);
			isPoisoned = false;
			poisonValue = 0;
	
		}
	}

	public void TakeDamage(int damage)
    {
        if(currentPlayerHealth - damage <= 0 )
        {
			currentPlayerHealth -= damage;
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
	
		if(!IsBuffActive(BuffTypes.PowerUp)){playerBuffs.Add(BuffTypes.PowerUp);}
	}

	public void DisablePowerUp()
	{
		isPowerUp = false;
		playerBuffs.Remove(BuffTypes.PowerUp);
	}

	public void EnableSilence()
	{
		isSilenced = true;
		if(!IsDebuffActive(DebuffTypes.Silence)){playerDebuffs.Add(DebuffTypes.Silence);}
	}

	public void DisableSilence()
	{
		isSilenced = false;
		playerDebuffs.Remove(DebuffTypes.Silence);
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
		if(!IsBuffActive(BuffTypes.Shield)){playerBuffs.Add(BuffTypes.Shield);}
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
		playerBuffs.Remove(BuffTypes.Shield);
	}


	public void EnableReflect()
	{
		isReflecting = true;
		if(!IsBuffActive(BuffTypes.Reflect)){playerBuffs.Add(BuffTypes.Reflect);}
	}

	public void DisableReflect()
	{
		isReflecting =false;
		playerBuffs.Remove(BuffTypes.Reflect);
	}

	public bool DoesReflectionOccur()
	{
		if(IsReflecting && Random.value < reflectPos)
		{	
			DisableReflect();
			return true;	
		}

		return false;
	}

	public void UpdatePlayerStatus()
	{
		if(isPoisoned)
		{
			TakeDamage(poisonValue);
		}

		if(IsPowerUp)
		{
			DisablePowerUp();
		}

		if(IsDoubleSacrificeReq)
		{
			DisableDoubleSacrifice();
		}

		if(IsSilenced)
		{
			DisableSilence();
		}
	}

	private bool IsDebuffActive(DebuffTypes debuff)
	{
		foreach(var x in playerDebuffs){if(x.Equals(debuff)){return true;}}
		return false;
	}

	private bool IsBuffActive(BuffTypes buff)
	{
		foreach(var x in playerBuffs){if(x.Equals(buff)){return true;}}
		return false;
	}

	public void RandomlyRemoveDebuff()
	{

		if(playerDebuffs.Count == 0){return;}
		var rn = Random.Range(0,playerDebuffs.Count);

		switch(playerDebuffs[rn])
		{
			case DebuffTypes.Poison: RemovePoison(); return;
		}

	}

	public void RandomlyRemoveBuff()
	{
		if(playerBuffs.Count == 0){return;}

		var rn = Random.Range(0,playerBuffs.Count);
		switch(playerBuffs[rn])
		{
			case BuffTypes.PowerUp: DisablePowerUp(); return;
			case BuffTypes.Shield : DisableShield();return;
		}

	}

	#endregion
}