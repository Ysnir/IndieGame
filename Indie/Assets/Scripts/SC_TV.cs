using UnityEngine;
using System.Collections;

public class SC_TV : MonoBehaviour {

    public BoxCollider collisionBox;    // la boîte de collision (et non de détection) de l'objet
    private AudioSource noise;
    private bool isPlayingSound = false;
    private bool isThrown = false;
    private float pushDuration = 0.2f;
    private float throwDuration = 0.5f;

    public int damageWhenThrown;  // les dégâts infligés par l'objet lorsqu'il est lancé

	// Use this for initialization
	void Start () {
        noise = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) // Si quelque chose entre dans le trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui est entré dans le trigger et que le son n'est pas déjà en train de se faire jouer
        {
            On();
            c.gameObject.GetComponent<SC_InteractTV>().CanIntertact(this, isPlayingSound);

			if (isThrown)  // si l'objet est en train de se faire lancer
			{
				c.SendMessage("ChangeHealth", -damageWhenThrown);   // damageWhenThrownest envoyé en négatif, car ce sont des dégâts
			}
        }
    }

    void OnTriggerExit(Collider c) // Si quelque chose sort du trigger
    {
        if (c.CompareTag("Player1") || c.CompareTag("Player2") || c.CompareTag("Player3") || c.CompareTag("Player4"))   // Si c'est l'un des joueurs qui vient d'en sortir et que le son jouait déjà
        {
            Off();
            c.gameObject.GetComponent<SC_InteractTV>().CanNotIntertact();
        }
    }

    public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
    {
        noise.volume = Random.value;
    }

    public void Off()   // fonction pour éteindre la TV
    {
            if (isPlayingSound)    // si la TV est en train de faire du bruit
            {
                noise.Pause();
                isPlayingSound = false;
            }
    }

    public void On()  
    {
            if (!isPlayingSound) {
                noise.Play();
                isPlayingSound = true;
            }
    }

    public void Push(Vector2 direction)
    {
        GetComponent<Rigidbody>().isKinematic = false;  // tant que le rigidbody est en kinematic, il ne peut pas être poussé, donc on corrige ça pour l'instant
        GetComponent<Rigidbody>().AddForce(direction * 500, ForceMode.Acceleration);
        StartCoroutine(Wait(pushDuration));     // à la fin de la coroutine, le rigidbody repassera en kinematic
    }

    public void Throw(Vector2 direction)
    {
        GetComponent<Rigidbody>().isKinematic = false;  // tant que le rigidbody est en kinematic, il ne peut pas être poussé, donc on corrige ça pour l'instant
        GetComponent<Rigidbody>().AddForce(direction * 500, ForceMode.Acceleration);
        collisionBox.isTrigger = true;
        isThrown = true;
        StartCoroutine(Wait(throwDuration));     // à la fin de la coroutine, le rigidbody repassera en kinematic
    }

    public bool getIsPlayingSound()
    {
        return isPlayingSound;
    }

    IEnumerator Wait(float waitTime)    // une coroutine qui attend waitTime secondes, puis lui rend ses paramètres standard (kinematic, isThrown = false, pas trigger)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Rigidbody>().isKinematic = true;
        isThrown = false;
        collisionBox.isTrigger = false;
    }
}
