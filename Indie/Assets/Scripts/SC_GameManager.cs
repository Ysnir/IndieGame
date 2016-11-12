using UnityEngine;
using System.Collections;

public class SC_GameManager : MonoBehaviour {

    public GameObject[] characters; // un tableau qui contiendra tous les personnages
    public GameObject gmManager;    // le GMManager
    public GameObject startScreen; // l'écran qui s'affiche au début
    private bool gmIsChosen = false; // passe à true quand le MJ a été choisi

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (!gmIsChosen)    // si le MJ n'a pas encore été choisi
        {
            //Debug.Log("htrsd");

            if (Input.GetButtonDown("ActionJ1"))    // si le joueur 1 appuie sur A
            {
                characters[0].SetActive(false);  // on désactive le joueur 1
                gmManager.GetComponent<SC_GMManager>().enabled = true;  // on active le script du MJ
                gmManager.GetComponent<SC_GMManager>().player = 1;  // on associe la manette 1 au MJ
                gmIsChosen = true;  // on annonce que le MJ est choisi
                startScreen.SetActive(false);   // on fait disparaître l'écran de sélection de MJ
            }


            if (Input.GetButtonDown("ActionJ2"))    // si le joueur 2 appuie sur A
            {

                characters[1].SetActive(false); // on désactive le joueur 2
                gmManager.GetComponent<SC_GMManager>().enabled = true;  // on active le script du MJ
                gmManager.GetComponent<SC_GMManager>().player = 2;  // on associe la manette 2 au MJ
                gmIsChosen = true;  // on annonce que le MJ est choisi
                startScreen.SetActive(false);   // on fait disparaître l'écran de sélection de MJ
            }


            if (Input.GetButtonDown("ActionJ3"))    // si le joueur 3 appuie sur A
            {

                characters[2].SetActive(false);  // on désactive le joueur 3
                gmManager.GetComponent<SC_GMManager>().enabled = true;  // on active le script du MJ
                gmManager.GetComponent<SC_GMManager>().player = 3;  // on associe la manette 3 au MJ
                gmIsChosen = true;  // on annonce que le MJ est choisi
                startScreen.SetActive(false);   // on fait disparaître l'écran de sélection de MJ
            }


            if (Input.GetButtonDown("ActionJ4"))    // si le joueur 4 appuie sur A
            {

                characters[3].SetActive(false);  // on désactive le joueur 4
                gmManager.GetComponent<SC_GMManager>().enabled = true;  // on active le script du MJ
                gmManager.GetComponent<SC_GMManager>().player = 4;  // on associe la manette 4 au MJ
                gmIsChosen = true;  // on annonce que le MJ est choisi
                startScreen.SetActive(false);   // on fait disparaître l'écran de sélection de MJ
            }

        }

	}
}
