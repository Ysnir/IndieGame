using UnityEngine;
using System.Collections;

public class SC_Coffee : MonoBehaviour {

	public bool trap = false;   // à true si l'objet est piégé

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
	{
		if (c.CompareTag ("Player1") || c.CompareTag ("Player2") || c.CompareTag ("Player3") || c.CompareTag ("Player4")) {   // Si c'est l'un des joueurs qui est entré dans le trigger et que le son n'est pas déjà en train de se faire jouer
			SC_Player playerScript = c.GetComponent<SC_Player>();
            if(!playerScript.IsHolding) 
            {
				c.gameObject.GetComponent<SC_InteractCoffee> ().CanInteract(this);
			}
		}
	}

	void OnTriggerExit(Collider c) // Si quelque chose sort du trigger
	{
		if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui vient d'en sortir et que le son jouait déjà
		{
			c.gameObject.GetComponent<SC_InteractCoffee>().CanNotInteract();
		}
	}

	public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
	{
		if (trap == false) //inverse si piégé ou non
		{
			trap = true;
		}
		else
		{
			trap = false;
		}

	}
}
