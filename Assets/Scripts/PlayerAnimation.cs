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
            attack();
        }
        if (Input.GetKeyDown("s"))
        {
            idle();
      
        }
    }

   

    void idle() {
        animator.Play("Player" + player_id.ToString() + "_Idle");
    }

    void attack() {
        animator.Play("Player" + player_id.ToString() + "_Punch");
        animator.Play("Player" + player_id.ToString() + "_Hrz Strike");
    }

    void spell() {
        animator.Play("Player" + player_id.ToString() + "_Vrt Strike");

    }
    void guard() {
        animator.Play("Player" + player_id.ToString() + "_Guard");
        
    }

    void dead() {
        animator.Play("Player" + player_id.ToString() + "_KO");
    }

    
}
