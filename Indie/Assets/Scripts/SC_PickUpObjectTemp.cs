using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // pour pouvoir utiliser des listes

public class SC_PickUpObjectTemp : MonoBehaviour {

    CapsuleCollider capsuleCollider;

    bool isHold = false;    // l'objet est-il tenu par quelqu'un ?
    int potentialOwner;
    int owner;

    // Use this for initialization
    void Start () {

        capsuleCollider = GetComponent<CapsuleCollider>();

	}
	
	// Update is called once per frame
	void Update ()      // Ne fonctionne pas, appelle les deux dans la même frame
    {
        Debug.Log(owner);

        if (potentialOwner != 0 && Input.GetButtonDown("ActionJ" + potentialOwner) && !isHold)   // si le joueur appuie sur A alors qu'il ne tient pas l'objet
        {
            Grab(GameObject.FindWithTag("Player" + potentialOwner).GetComponent<SC_Player>());
        }else if (owner != 0 && Input.GetButtonDown("ActionJ" + potentialOwner) && isHold)   // si le joueur appuie sur A alors qu'il tient l'objet
        {
            Release();
        }
    }

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if ((c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4")) && !isHold)   //  Si c'est l'un des joueurs qui est entré dans le trigger et que l'objet n'est pas possédé
        {
            potentialOwner=c.GetComponent<SC_Player>().player;
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose quitte le trigger
    {
        if (c.GetComponent<SC_Player>().player == potentialOwner)
        {
            potentialOwner = 0;
        }
    }

    void Grab(SC_Player c)
    {
        transform.SetParent(c.transform, true);
        capsuleCollider.enabled = false;
        isHold = true;
        owner = potentialOwner;
    }

    void Release()
    {
        Debug.Log("Lâche moi !");
        transform.SetParent(null);
        capsuleCollider.enabled = true;
        isHold = false;
        owner = 0;
    }
}
