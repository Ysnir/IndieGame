using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SC_InteractCoffee : MonoBehaviour {

	public GameObject action;
    public SC_GMManager gmManagerScript;
	SC_Coffee coffeeScript;
	Text actionText;
	Action currentAction;  
	enum Action {DRINK=1};   // l'action sélectionnée par le joueur. 1 = boire
	bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
	int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
	bool isAlive = true;    // le joueur est-il toujours en vie ?
	bool isHolding = false;    // le joueur est-il entrain de porter un objet?
    
	public float speedCoef;   //par combien l'action "Boire" multiplie la vitesse de déplacement
	public float buffsDuration;   //durée des effets
	public int restoredHP;   //nombre de PVs soignés en prenant un café
	public int lostHP; //nombre de PVs perdus si la cafetière est piégée

	// Use this for initialization
	void Start ()
	{
		actionText = action.GetComponent<Text> ();
		currentAction = Action.DRINK;
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
	void Update () 
	{
		if (Input.GetButtonDown("ActionJ"+player) && canInteract && isAlive && !isHolding)  // lorsqu'on appuie sur la touche action
		{
			if (coffeeScript.trap == true)
			{
				GetComponent<SC_Player> ().ChangeHealth (-lostHP);
                coffeeScript.gameObject.SetActive(false);
                gmManagerScript.RemoveObject(coffeeScript.gameObject);   // on retire la cafetière des objets du MJ
				CanNotIntertact ();
			}
			else
			{
				switch (currentAction)      // un switch pour agir en fonction de l'action
				{
				case Action.DRINK:
					GetComponent<SC_Player> ().setCoefSpeedCoffee (speedCoef);
					GetComponent<SC_Player> ().ChangeHealth (restoredHP);
					StopCoroutine ("TimerBuffs");
					StartCoroutine ("TimerBuffs");
					break;

				default:
					break;
				}
			}
		}

/*
 		if (Input.GetButtonDown("ChangeActionRightJ" + player) && canInteract && isAlive)
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
				currentAction = (Action)3;  // retour à la dernière action
			}
			NewAction();
		}
*/
	}

	IEnumerator TimerBuffs ()   //à la fin du timer, on réinitialise les coefficients
	{
		yield return new WaitForSecondsRealtime (buffsDuration);
		GetComponent<SC_Player> ().setCoefSpeedCoffee (1.0f);
	}

	void NewAction ()
	{
		switch (currentAction)
		{
		case Action.DRINK:
			actionText.text = "Boire";
			break;

		default:
			break;
		}
	}

	public void CanInteract (SC_Coffee script)
	{
		coffeeScript = script;
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
