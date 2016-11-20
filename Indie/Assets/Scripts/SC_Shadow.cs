using UnityEngine;
using System.Collections;

public class SC_Shadow : MonoBehaviour {
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
            LightOn();
        }
    }

	public void LightOn() {
		gameObject.SetActive(false);
	}

	/*Methode gerant l'eclairage a l'aide de l'ouverture des portes
	*A appeler à chaque ouverture d'une porte
	*
	*TODO : Gerer le cas ou une piece est accessible depuis plusieurs portes
	*/
	public void DoorLight() {
		//On récupère le bounding de chaque joueur.
        var player1Bounds = GameObject.Find("Character1").transform.Find("WalkingSprite").GetComponent<Renderer>().bounds;
        var player2Bounds = GameObject.Find("Character2").transform.Find("WalkingSprite").GetComponent<Renderer>().bounds;
        var player3Bounds = GameObject.Find("Character3").transform.Find("WalkingSprite").GetComponent<Renderer>().bounds;
        var player4Bounds = GameObject.Find("Character4").transform.Find("WalkingSprite").GetComponent<Renderer>().bounds;

		//On vérifie s'il ne reste aucun joueur dans la pièce
        if(!player1Bounds.Intersects(GetComponent<Renderer>().bounds) && !player2Bounds.Intersects(GetComponent<Renderer>().bounds) && !player3Bounds.Intersects(GetComponent<Renderer>().bounds) && !player4Bounds.Intersects(GetComponent<Renderer>().bounds)) {
            gameObject.SetActive(true);    //On éteint la pièce
        }
	}
}
