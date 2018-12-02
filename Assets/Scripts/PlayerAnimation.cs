using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("w")) {
            attack();
        }
        if (Input.GetKeyDown("s"))
        {
            idle();
      
        }
    }

    void idle() {
        animator.Play("Player_Idle");
    }

    void attack() {
        animator.Play("Player_Punch");
        animator.Play("Player_Hrz Strike");
    }

    void debuff() {
        animator.Play("Player_Vrt Strike");

    }
    void guard() {
        animator.Play("Player_Guard");
    }

    void dead() {
        animator.Play("Player_KO");
    }
    void buff() {
        animator.Play("Player_Heal");
    }

    
}
