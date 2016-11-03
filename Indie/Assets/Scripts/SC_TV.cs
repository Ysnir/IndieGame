using UnityEngine;
using System.Collections;

public class SC_TV : MonoBehaviour {

    // variables pour la gestion de l'objet par rapport aux joueurs
    public AudioSource noise;
    public bool isPlayingSound = false;

    public SC_InteractTV playerScript;

	// Use this for initialization
	void Start () {
	    
        noise = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4") && !isPlayingSound)   // Si c'est l'un des joueurs qui est entré dans le trigger et que le son n'est pas déjà en train de se faire jouer
        {
            On();
            playerScript.CanIntertact(this, isPlayingSound);
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose sort du trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4") && isPlayingSound)   // Si c'est l'un des joueurs qui vient d'en sortir et que le son jouait déjà
        {
            Off();
            playerScript.CanNotIntertact();
        }
    }

    public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
    {
        noise.volume = Random.value;
    }

    public void On()
    {
        if (!isPlayingSound)    // si la TV n'est pas en train de faire du bruit
        {
            noise.Play();
            isPlayingSound = true;
        }
    }

    public void Off()   // fonction pour éteindre la TV
    {
        if (isPlayingSound)    // si la TV est en train de faire du bruit
        {
            noise.Pause();
            isPlayingSound = false;
        }
    }

    public void Push(Vector2 direction)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(direction*500, ForceMode.Acceleration);
        StartCoroutine(Wait(0.2f));
    }

    IEnumerator Wait(float waitTime)    // une coroutine qui attend waitTime secondes, puis re-met l'objet en kinematic
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
