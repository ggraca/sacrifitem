using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField]
    private int player_id = 1;

    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("w")) {
            Attack();
        }
        if (Input.GetKeyDown("s"))
        {
            Idle();
      
        }
    }

   

    public void Idle() {
        animator.Play("Player" + player_id.ToString() + "_Idle");
    }

    public void Attack() {
        animator.Play("Player" + player_id.ToString() + "_Punch");
    }

    public void Spell() {
        animator.Play("Player" + player_id.ToString() + "_Vrt Strike");
    }

    public void Buff() {
        animator.Play("Player" + player_id.ToString() + "_Heal");
    }

    public void Guard() {
        animator.Play("Player" + player_id.ToString() + "_Guard");
    }

    public void Steal() {
        animator.Play("Player" + player_id.ToString() + "_Steal");
    }

    public void Dead() {
        animator.Play("Player" + player_id.ToString() + "_KO");
    }

    
}
