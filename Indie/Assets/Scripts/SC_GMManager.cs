using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_GMManager : MonoBehaviour {

    public GameObject[] target;
    public GameObject[] glowObject;
    private int actualTarget = 0;
    public int player;  // le numéro de joueur, pour pouvoir le donner aux scripts d'action

    // Use this for initialization
    void Start () {
        
        glowObject[actualTarget].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("ChangeActionRightJ" + player))   // Ajouter "Input.GetKeyDown(KeyCode.D) || " dans la condition pour jouer au clavier
        {
            glowObject[actualTarget].SetActive(false);      // on quitte l'objet actuel

            if (actualTarget < target.Length-1)
            {
                actualTarget++;
            }
            else
            {
                actualTarget = 0;
            }

            glowObject[actualTarget].SetActive(true);   // on rentre dans le nouvel objet
        }

        if (Input.GetButtonDown("ChangeActionLeftJ" + player))  // Ajouter "Input.GetKeyDown(KeyCode.Q) || " dans la condition pour jouer au clavier
        {
            glowObject[actualTarget].SetActive(false);      // on quitte l'objet actuel

            if (actualTarget > 0)
            {
                actualTarget--;
            }
            else
            {
                actualTarget = target.Length-1;
            }

            glowObject[actualTarget].SetActive(true);   // on rentre dans le nouve objet
        }

        if (Input.GetButtonDown("ActionJ" + player))
        {
            target[actualTarget].SendMessage("GMSpecificAction");
        }
    }
}
