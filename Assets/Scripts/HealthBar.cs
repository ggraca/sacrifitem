using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	[SerializeField]
	private Slider healthBarPlayer;
	[SerializeField]
	private Image fillPlayer;
	[SerializeField]
	private int currentHealthPlayer;
	[SerializeField]
	private int maxHealthPlayer;


	[SerializeField]
    private string concatStr = "";
    [SerializeField]

	private Text healthTextPlayer;
	private bool isDead;

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

    public Text HealthTextPlayer
    {
        get
        {
            return healthTextPlayer;
        }

        set
        {
            healthTextPlayer = value;
        }
    }

    public int MaxHealthPlayer
    {
        get
        {
            return maxHealthPlayer;
        }

        set
        {
            maxHealthPlayer = value;
        }
    }

    public int CurrentHealthPlayer
    {
        get
        {
            return currentHealthPlayer;
        }

        set
        {
            currentHealthPlayer = value;
        }
    }

    public Image FillPlayer
    {
        get
        {
            return fillPlayer;
        }

        set
        {
            fillPlayer = value;
        }
    }

    public Slider HealthBarPlayer
    {
        get
        {
            return healthBarPlayer;
        }

        set
        {
            healthBarPlayer = value;
        }
    }

    void Start ()
	{
		currentHealthPlayer = maxHealthPlayer;
	}
	
	void Update ()
    {
        FillHealthBar();
    }

    private void FillHealthBar()
    {
        healthBarPlayer.maxValue = maxHealthPlayer;
        healthBarPlayer.value = currentHealthPlayer;
        healthTextPlayer.text = concatStr + currentHealthPlayer.ToString() + " / " + maxHealthPlayer.ToString();

        if (currentHealthPlayer <= 0)
        {
            if (isDead)
            {
                return;
            }

            Dead();
        }

        playerOneHealthColor();
    }

    void playerOneHealthColor()
	{
	    if (healthBarPlayer.value <= 0)
		{
			fillPlayer.color = Color.red;
		}
	    if (healthBarPlayer.value > 0)
		{
			fillPlayer.color = Color.green;
		}
	}

	private void Dead()
	{
		isDead = true;
	}

    public void TakeDamage(int damage)
    {
        if(currentHealthPlayer - damage <= 0 )
        {
            Dead();return;
        }

        currentHealthPlayer -= damage;
    }

    public void HealSelf(int healAmount)
    {
        if(currentHealthPlayer + healAmount > maxHealthPlayer)
        {
            currentHealthPlayer = maxHealthPlayer;return;
        }
        currentHealthPlayer += healAmount;
    }

}