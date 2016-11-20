using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // pour pouvoir utiliser des listes

public class SC_PickUpObjectTemp : MonoBehaviour {

    CapsuleCollider capsuleCollider;

    public bool isHold = false;    // l'objet est-il tenu par quelqu'un ?
    //int potentialOwner;
    //int owner;
    List<int> potentialOwners = new List<int>();

    // Use this for initialization
    void Start () {

        capsuleCollider = GetComponent<CapsuleCollider>();

	}
	
	// Update is called once per frame
	void Update ()      // Ne fonctionne pas, appelle les deux dans la même frame
    {
        for (int i = 0; i<potentialOwners.Count; i++)   // pour tout télément dans la liste potentialOwners
        {
            SC_Player ownerScript = GameObject.FindWithTag("Player" + potentialOwners[i]).GetComponent<SC_Player>();
            if (potentialOwners.Count > 0 && Input.GetButtonDown("ActionJ" + potentialOwners[i]) && !isHold)   // si le joueur appuie sur A alors qu'il ne tient pas l'objet
            {
                Grab(ownerScript);
            }
            else if (Input.GetButtonDown("ActionJ" + potentialOwners[i]) && isHold)   // si le joueur appuie sur A alors qu'il tient l'objet
            {
                Release(ownerScript);
            }
        }
    }

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if ((c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4")) && !isHold)   //  Si c'est l'un des joueurs qui est entré dans le trigger et que l'objet n'est pas possédé
        {
            //potentialOwner=c.GetComponent<SC_Player>().player;
            potentialOwners.Add(c.GetComponent<SC_Player>().player);        // On ajoute le joueur à la liste des gens qui peuvent prendre l'objet
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose quitte le trigger
    {
        int leavingPlayer = c.GetComponent<SC_Player>().player;

        //if (c.GetComponent<SC_Player>().player == potentialOwner)
        if (potentialOwners.Contains(leavingPlayer))   // si ce qui vient de partir faisait partie des possesseurs potentiels
        {
            potentialOwners.Remove(leavingPlayer);
        }
    }

    public void Grab(SC_Player c)
    {
        transform.SetParent(c.transform, true);
        capsuleCollider.enabled = false;
        isHold = true;
        c.notifyIsHolding(true);
    }

    public void Release(SC_Player c)
    {
        transform.SetParent(null);
        capsuleCollider.enabled = true;
        isHold = false;
        c.notifyIsHolding(false);
    }
}
