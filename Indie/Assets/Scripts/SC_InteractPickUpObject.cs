using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SC_InteractPickUpObject : MonoBehaviour {
    public GameObject action;
    SC_PickUpObjectTemp PickUpObjectScript;
    Text actionText;
    Action currentAction;  
    enum Action {GRAB_RELEASE=1, THROW=2};// l'action sélectionnée par le joueur. 1 = attraper ; 2 = lacher ; 3 = lancer
    bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
    int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
    bool isAlive = true;    // le joueur est-il toujours en vie ?
    bool isHolding = false;    // le joueur est-il entrain de porter un objet?

	// Use this for initialization
	void Start () {
        actionText = action.GetComponent<Text>();
        currentAction = Action.GRAB_RELEASE;
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
			SC_Player playerScript = GameObject.FindWithTag("Player"+player).GetComponent<SC_Player>();
            switch (currentAction)      // un switch pour agir en fonction de l'action
            {
                case Action.GRAB_RELEASE:
				    if (PickUpObjectScript.IsHeld)    // si l'objet est deja porte
                    {
                  		PickUpObjectScript.Release(playerScript);
						actionText.text = "Attraper";
					}
					else 
					{
						PickUpObjectScript.Grab(playerScript);
						actionText.text = "Lacher";
					}
                    break;

                case Action.THROW:
                    //PickUpObjectScript.Throw(new Vector2(TVScript.gameObject.transform.position.x - transform.position.x, TVScript.gameObject.transform.position.y - transform.position.y).normalized);
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

	void NewAction () {
        switch (currentAction)
        {
            case Action.GRAB_RELEASE:
                if (PickUpObjectScript.IsHeld)
                {
                	actionText.text = "Lacher";
                }
                else
                {
                    actionText.text = "Attraper";
                }
                break;

            case Action.THROW:
                actionText.text = "Lancer";
                break;

            default:
                break;
        }
    }

	public void CanIntertact(SC_PickUpObjectTemp script)
    {
        PickUpObjectScript = script;
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
