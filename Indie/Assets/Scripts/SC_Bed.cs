using UnityEngine;
using System.Collections;

public class SC_Bed : MonoBehaviour {
    
    public bool trap = false;  // le lit est-il piégé ?

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui est entré dans le trigger
        {
            SC_Player playerScript = c.GetComponent<SC_Player>();
            if(!playerScript.getIsHolding()) 
            {
                c.gameObject.GetComponent<SC_InteractBed>().CanIntertact(this);
            }
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose sort du trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui vient d'en sortir
        {
            c.gameObject.GetComponent<SC_InteractBed>().CanNotIntertact();
        }
    }

    public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
    {
        trap = !trap;   // piéger ou "dépiéger" le lit
    }
}
