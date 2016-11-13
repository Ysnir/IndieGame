using UnityEngine;
using System.Collections;

public class SC_Door : MonoBehaviour {

    private bool isOpen = false;
    public float damageWhenBrokenDown;  // les dégâts infligés si un joueur tente de défoncer la porte

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
            c.gameObject.GetComponent<SC_InteractDoor>().CanIntertact(this);
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

    public void MoveDoor()  // la fonction pou ouvrir/fermer la porte
    {
        animator.SetTrigger("openClose");
        isOpen = !isOpen;
    }

    public bool getIsOpen()
    {
        return isOpen;
    }

}
