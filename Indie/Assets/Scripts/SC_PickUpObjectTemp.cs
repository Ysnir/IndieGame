using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // pour pouvoir utiliser des listes

public class SC_PickUpObjectTemp : MonoBehaviour {

    CapsuleCollider capsuleCollider;

    bool isHold = false;    // l'objet est-il tenu par quelqu'un ?
<<<<<<< HEAD
    //int potentialOwner;
    //int owner;
    List<int> potentialOwners = new List<int>();
=======
    int potentialOwner;
    int owner;
>>>>>>> origin/master

    // Use this for initialization
    void Start () {

        capsuleCollider = GetComponent<CapsuleCollider>();

	}
	
	// Update is called once per frame
	void Update ()      // Ne fonctionne pas, appelle les deux dans la même frame
    {
<<<<<<< HEAD
        for (int i = 0; i<potentialOwners.Count; i++)   // pour tout télément dans la liste potentialOwners
        {
            if (potentialOwners.Count > 0 && Input.GetButtonDown("ActionJ" + potentialOwners[i]) && !isHold)   // si le joueur appuie sur A alors qu'il ne tient pas l'objet
            {
                Grab(GameObject.FindWithTag("Player" + potentialOwners[i]).GetComponent<SC_Player>());
            }
            else if (Input.GetButtonDown("ActionJ" + potentialOwners[i]) && isHold)   // si le joueur appuie sur A alors qu'il tient l'objet
            {
                Release();
            }
=======
        Debug.Log(owner);

        if (potentialOwner != 0 && Input.GetButtonDown("ActionJ" + potentialOwner) && !isHold)   // si le joueur appuie sur A alors qu'il ne tient pas l'objet
        {
            Grab(GameObject.FindWithTag("Player" + potentialOwner).GetComponent<SC_Player>());
        }else if (owner != 0 && Input.GetButtonDown("ActionJ" + potentialOwner) && isHold)   // si le joueur appuie sur A alors qu'il tient l'objet
        {
            Release();
>>>>>>> origin/master
        }
    }

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if ((c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4")) && !isHold)   //  Si c'est l'un des joueurs qui est entré dans le trigger et que l'objet n'est pas possédé
        {
<<<<<<< HEAD
            //potentialOwner=c.GetComponent<SC_Player>().player;
            potentialOwners.Add(c.GetComponent<SC_Player>().player);        // On ajoute le joueur à la liste des gens qui peuvent prendre l'objet
=======
            potentialOwner=c.GetComponent<SC_Player>().player;
>>>>>>> origin/master
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose quitte le trigger
    {
<<<<<<< HEAD
        int leavingPlayer = c.GetComponent<SC_Player>().player;

        //if (c.GetComponent<SC_Player>().player == potentialOwner)
        if (potentialOwners.Contains(leavingPlayer))   // si ce qui vient de partir faisait partie des possesseurs potentiels
        {
            potentialOwners.Remove(leavingPlayer);
=======
        if (c.GetComponent<SC_Player>().player == potentialOwner)
        {
            potentialOwner = 0;
>>>>>>> origin/master
        }
    }

    void Grab(SC_Player c)
    {
        transform.SetParent(c.transform, true);
        capsuleCollider.enabled = false;
        isHold = true;
<<<<<<< HEAD
=======
        owner = potentialOwner;
>>>>>>> origin/master
    }

    void Release()
    {
<<<<<<< HEAD
        transform.SetParent(null);
        capsuleCollider.enabled = true;
        isHold = false;
=======
        Debug.Log("Lâche moi !");
        transform.SetParent(null);
        capsuleCollider.enabled = true;
        isHold = false;
        owner = 0;
>>>>>>> origin/master
    }
}
