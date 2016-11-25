using UnityEngine;
using System.Collections;

public class SC_Door : MonoBehaviour {

    public BoxCollider collisionBox;   // la box de collision de la porte
    private bool isOpen = false;
    public float damageWhenBrokenDown;  // les dégâts infligés si un joueur tente de défoncer la porte
    // public GameObject shadow; // à dé-commenter en cas d'utilisation des ombres

    Animator animator;

    // Use this for initialization
    void Start () {

        animator = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui est entré dans le trigger
        {
            SC_Player playerScript = c.GetComponent<SC_Player>();
            if(!playerScript.IsHolding) 
            {
                c.gameObject.GetComponent<SC_InteractDoor>().CanIntertact(this);
            }
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose sort du trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui vient d'en sortir
        {
            c.gameObject.GetComponent<SC_InteractTV>().CanNotIntertact();
        }
    }

    public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
    {

    }



    public void MoveDoor()  // la fonction pour ouvrir/fermer la porte
    {

        animator.SetTrigger("openClose");
        isOpen = !isOpen;
        if (isOpen)
        {
            collisionBox.isTrigger = true;  // pour pouvoir passer à travers
        }
        else
        {
            collisionBox.isTrigger = false;  // pour ne plus passer à travers
        }
    }

    public bool getIsOpen()
    {
        return isOpen;
    }

}
