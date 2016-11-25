using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SC_InteractBed : MonoBehaviour {

    public GameObject action;
    SC_Player playerScript;
    SC_Bed BedScript;
    Text actionText;
    Action currentAction;
    enum Action { SLEEP = 1};   // l'action sélectionnée par le joueur. 1 = dormir
    bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
    int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
    bool isAlive = true;    // le joueur est-il toujours en vie ?
    bool isHolding = false;    // le joueur est-il entrain de porter un objet?
    
    public int damageDealt;  // le nombre de points de vie perdus en cas de paralysie du sommeil
    public float timeToSleep;    // le temps à passer immobile en cas de sommeil
    public float timeSleepParalysis;    // le temps à passer immobile en cas de paralysie du sommeil

    // Use this for initialization
    void Start () {
        actionText = action.GetComponent<Text>();
        currentAction = Action.SLEEP;
        playerScript = GetComponent<SC_Player>();
    }

    public void setPlayer()
    {
        player++;
    }

    public void setAliveStatus(bool status)
    {
        isAlive = status;
    }

	public void setIsHolding(bool _isHolding) {
        isHolding = _isHolding;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("ActionJ" + player) && canInteract && isAlive && !isHolding)  // lorsqu'on appuie sur la touche action
        {
            switch (currentAction)      // un switch pour agir en fonction de l'action
            {
                case Action.SLEEP:
                    playerScript.setCanMove(false);
                    if (BedScript.GetComponent<SC_Bed>().trap)   // si le lit était piégé, on enlève de la vie
                    {
                        playerScript.ChangeHealth(-damageDealt);
                        StartCoroutine(Sleep(timeSleepParalysis));
                    }
                    else    // sinon, on soigne
                    {
                        playerScript.FullHeal();
                        StartCoroutine(Sleep(timeToSleep));
                    }
                    StartCoroutine(Sleep(timeToSleep));
                    break;

                default:
                    break;
            }

        }

        /*if (Input.GetButtonDown("ChangeActionRightJ" + player) && canInteract && isAlive)
        {
            if ((int)currentAction < Enum.GetNames(typeof(Action)).Length)  //Compare l'action courante avec le nombre total d'action possible
            {
                currentAction++;
            }
            else
            {
                currentAction = (Action)1;  // retour à la première action
            }
            NewAction();
        }
        if (Input.GetButtonDown("ChangeActionLeftJ" + player) && canInteract && isAlive)
        {
            if ((int)currentAction > 1)
            {
                currentAction--;
            }
            else
            {
                currentAction = (Action)2;  // retour à la dernière action
            }
            NewAction();
        }*/

    }

    void NewAction()
    {
        switch (currentAction)
        {
            case Action.SLEEP:
                actionText.text = "Dormir";
                break;

            default:
                break;
        }
    }

    public void CanInteract(SC_Bed script)
    {
        BedScript = script;
        canInteract = true;
        NewAction();
        action.SetActive(true);
    }

    public void CanNotInteract()
    {
        canInteract = false;
        action.SetActive(false);
    }

    IEnumerator Sleep(float waitTime)    // une coroutine qui attend waitTime secondes, puis lui rend ses paramètres standard (kinematic, isThrown = false, pas trigger)
    {
        yield return new WaitForSeconds(waitTime);
        playerScript.setCanMove(true);
    }
}
