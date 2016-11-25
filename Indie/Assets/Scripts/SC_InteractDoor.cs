using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SC_InteractDoor : MonoBehaviour {

    public GameObject action;
    SC_Door DoorScript;
    Text actionText;
    Action currentAction;
    enum Action { OPEN_CLOSE = 1, BREAKDOWN = 2 };// l'action sélectionnée par le joueur. 1 = ouvrir/fermer ; 2 = défoncer
    bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
    int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
    bool isAlive = true;    // le joueur est-il toujours en vie ?
    bool isHolding = false;    // le joueur est-il entrain de porter un objet?

    // Use this for initialization
    void Start ()
    {
        actionText = action.GetComponent<Text>();
        currentAction = Action.OPEN_CLOSE;
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
                case Action.OPEN_CLOSE:
                    if (DoorScript.getIsOpen())    // si la porte est ouverte
                    {
                        DoorScript.MoveDoor();  // on ferme la porte
                        actionText.text = "Ouvrir";
                    }
                    else
                    {
                        DoorScript.MoveDoor();  // on ouvre la porte
                        actionText.text = "Fermer";
                    }
                    break;

                case Action.BREAKDOWN:
                    // on tente de défoncer la porte
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
            case Action.OPEN_CLOSE:
                if (DoorScript.getIsOpen())    // si la porte est ouverte
                {
                    actionText.text = "Fermer";
                }
                else
                {
                    actionText.text = "Ouvrir";
                }
                break;

            case Action.BREAKDOWN:
                actionText.text = "Défoncer";
                break;

            default:
                break;
        }
    }

    public void CanInteract(SC_Door script)
    {
        DoorScript = script;
        canInteract = true;
        NewAction();
        action.SetActive(true);
    }

    public void CanNotInteract()
    {
        canInteract = false;
        action.SetActive(false);
    }
}
