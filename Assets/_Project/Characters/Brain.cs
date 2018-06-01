using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    #region Properties
    public Dna Dna { get; private set; }
    public float TimeAlive { get; private set; }
    #endregion

    private int DnaLength = 2;
    private GameObject eyes;
    private bool isAlive = true;
    private bool canSeeGround = true;

    #region Public Methods
    public void Init ()
    {
        // Initialize Dna.
        // 0 = move forward.
        // 1 = turn left.
        // 2 = turn right.
        Dna = new Dna(DnaLength, 3);
        TimeAlive = 0;
        isAlive = true;
    } 
    #endregion
	
	// Update is called once per frame
	void Update ()
    {
        if (!isAlive) return;
        CheckIfCanSeeGround();
        MoveBasedOnDna();
    }

    private void MoveBasedOnDna ()
    {
        float turn = 0;
        float move = 0;

        if (canSeeGround)
        {
            if (Dna.Genes[0] == 0)
            {
                move = 1;
            }
            else if (Dna.Genes[0] == 1)
            {
                turn = -90;
            }
            else if (Dna.Genes[0] == 2)
            {
                turn = 90;
            }
        }
        else
        {
            if (Dna.Genes[1] == 0)
            {
                move = 1;
            }
            else if (Dna.Genes[1] == 1)
            {
                turn = -90;
            }
            else if (Dna.Genes[1] == 2)
            {
                turn = 90;
            }
        }

        this.transform.Translate(0, 0, move * 0.1f);
        this.transform.Rotate(0, turn, 0);
    }

    private void CheckIfCanSeeGround ()
    {
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10f);
        canSeeGround = false;

        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
        {
            if (hit.collider.gameObject.tag == "platform")
            {
                canSeeGround = true;
            }
        }
        TimeAlive = PopulationManager.TimeElapsed;
    }

    private void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.tag == "dead")
        {
            isAlive = false;
        }
    }

}
