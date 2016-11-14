using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SC_GMManager : MonoBehaviour {
    
    public List<GameObject> target = new List<GameObject>();    // liste des objets avec lesquels le MJ peut interagir
    public List<GameObject> glowObject = new List<GameObject>();    // liste des glowObjects correspondant aux objets avec lesquels le MJ peut interagir

    private int actualTarget = 0;
    public int player;  // le numéro de joueur, pour pouvoir le donner aux scripts d'action
    bool isVisible = true;  // le MJ s'est-il rendu invisible ?
    bool hasBeenReleased = true;    // booléen utilisé pour "changer" la gâchette en bouton

    // Use this for initialization
    void Start () {
        
        glowObject[actualTarget].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("ChangeActionRightJ" + player))   // Ajouter "Input.GetKeyDown(KeyCode.D) || " dans la condition pour jouer au clavier
        {
            glowObject[actualTarget].SetActive(false);      // on quitte l'objet actuel

            if (actualTarget < target.Count-1)
            {
                actualTarget++;
            }
            else
            {
                actualTarget = 0;
            }
            if (isVisible)  // si le MJ est visible
            {
                glowObject[actualTarget].SetActive(true);   // on montre qu'on rentre dans le nouvel objet
            }
        }

        if (Input.GetButtonDown("ChangeActionLeftJ" + player))  // Ajouter "Input.GetKeyDown(KeyCode.Q) || " dans la condition pour jouer au clavier
        {
            glowObject[actualTarget].SetActive(false);      // on quitte l'objet actuel

            if (actualTarget > 0)
            {
                actualTarget--;
            }
            else
            {
                actualTarget = target.Count-1;
            }
            if (isVisible)  // si le MJ est visible
            {
                glowObject[actualTarget].SetActive(true);   // on montre qu'on rentre dans le nouvel objet
            }
        }

        if (Input.GetButtonDown("ActionJ" + player))
        {
            target[actualTarget].SendMessage("GMSpecificAction");
        }

        if (Input.GetAxisRaw("SprintJ" + player) != 0 && hasBeenReleased)  // quand le MJ appuie sur la gâchette
        {
            isVisible = !isVisible;   // on change son état
            glowObject[actualTarget].SetActive(isVisible);  // on l'applique à l'affichage
            hasBeenReleased = false;
        }

        if (Input.GetAxisRaw("SprintJ" + player) == 0 && !hasBeenReleased)  // quand la gâchette est relâchée
        {
            hasBeenReleased = true;
        }
    }

    public void RemoveObject (GameObject objectToRemove)    // lorsqu'il faut retirer un objet de la liste (en cas de destruction par exemple)
    {
        int indexOfObjectToRemove = target.IndexOf(objectToRemove);     // on récupère l'index de l'objet à retirer

        glowObject[actualTarget].SetActive(false);      // on montre qu'on quitte l'objet actuel

        if (actualTarget == target.Count - 1)   // si on avait pour target le dernier objet de la liste, on prend celui juste avant
        {
            actualTarget = target.Count - 2;
        }

        target.Remove(objectToRemove);  // on retire l'objet de la liste
        glowObject.RemoveAt(indexOfObjectToRemove); // on retire le glow de la liste

        if (isVisible)  // si le MJ est visible
        {
            glowObject[actualTarget].SetActive(true);   // on montre qu'on rentre dans le nouvel objet
        }

    }
}
