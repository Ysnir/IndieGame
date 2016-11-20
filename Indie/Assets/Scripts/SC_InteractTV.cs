using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SC_InteractTV : MonoBehaviour {

    public GameObject action;
    SC_TV TVScript;
    Text actionText;
    Action currentAction;  
    enum Action {ON_OFF=1, PUSH=2, THROW=3};// l'action sélectionnée par le joueur. 1 = allumer/éteindre ; 2 = pousser ; 3 = lancer
    bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
    int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
    bool isAlive = true;    // le joueur est-il toujours en vie ?
    bool isHolding = false;    // le joueur est-il entrain de porter un objet?

	// Use this for initialization
	void Start () {
        actionText = action.GetComponent<Text>();
        currentAction = Action.ON_OFF;
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

        if (Input.GetButtonDown("ActionJ"+player) && canInteract && isAlive && !isHolding)  // lorsqu'on appuie sur la touche action
        {
            switch (currentAction)      // un switch pour agir en fonction de l'action
            {
                case Action.ON_OFF:
                    if (TVScript.getIsPlayingSound())    // si la TV est en train de faire du bruit
                    {
                        TVScript.Off();
                        actionText.text = "Allumer";
                    }
                    else
                    {
                        TVScript.On();
                        actionText.text = "Eteindre";
                    }
                    break;

                case Action.PUSH:
                    TVScript.Push(new Vector2(TVScript.gameObject.transform.position.x - transform.position.x, TVScript.gameObject.transform.position.y - transform.position.y).normalized);
                    break;

                case Action.THROW:
                    TVScript.Throw(new Vector2(TVScript.gameObject.transform.position.x - transform.position.x, TVScript.gameObject.transform.position.y - transform.position.y).normalized);
                    break;

                default:
                    break;
            }
            
        }

        if (Input.GetButtonDown("ChangeActionRightJ" + player) && canInteract && isAlive && !isHolding)
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
        if (Input.GetButtonDown("ChangeActionLeftJ" + player) && canInteract && isAlive && !isHolding)
        {
            if ((int)currentAction > 1)
            {
                currentAction--;
            }
            else
            {
                currentAction = (Action)3;  // retour à la dernière action
            }
            NewAction();
        }

    }

    void NewAction ()
    {
        switch (currentAction)
        {
            case Action.ON_OFF:
                if (TVScript.getIsPlayingSound())
                {
                    actionText.text = "Eteindre";
                }
                else
                {
                    actionText.text = "Allumer";
                }
                    break;

            case Action.PUSH:
                actionText.text = "Pousser";
                break;

            case Action.THROW:
                actionText.text = "Lancer";
                break;

            default:
                break;
        }
    }

    public void CanIntertact(SC_TV script)
    {
        TVScript = script;
        canInteract = true;
        NewAction();
        action.SetActive(true);
    }

    public void CanNotIntertact()
    {
        canInteract = false;
        action.SetActive(false);
    }
}
