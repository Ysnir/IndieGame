using UnityEngine;
using System.Collections;
using UnityEngine.UI;   // pour pouvoir utiliser le canvas associé au joueur

public class SC_Player : MonoBehaviour {

    public GameObject characterSprite;  // le sprite du personnage
    float speed;  // la vitesse de déplacement actuelle du joueur
    public float walkSpeed = 3.0f;  // la vitesse de déplacement en marchant du joueur
    public float runSpeed = 5.0f;  // la vitesse de déplacement en courant du joueur
    bool canMove = true;    // à true tant que le joueur peut se déplacer
    public float coefSpeedCoffee = 1.0f;   //coefficient multiplicateur pour la vitesse quand le PJ a pris du café

    private Rigidbody rb;   // le rigidbody associé au personnage
    public int player;  // le numéro de joueur, pour pouvoir le donner aux scripts d'action

    public int hpMax = 50; // les points de vie du joueur, mis à 50 de manière arbitraire
    int hp; // les points de vie actuels du joueur
    public Slider lifebar;  // le slider servant de jauge de vie
    private bool isAlive = true;    // booléen à true tant que le personnage est vivant
    private bool isHolding = false;
    private bool isSprinting = false; //booleen a true lorsque le personnage est entrain de courir

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < player; i++)    // permet de mettre la variable "player" à la bonne valeur dans tous les autres scripts d'action du joueur
        {
            gameObject.SendMessage("setPlayer");    // on l'appelle une fois si c'est le joueur 1, deux fois pour le joueur 2...
        }
        hp = hpMax;
        lifebar.maxValue = hpMax;   // on initialise la bare de vie
        lifebar.value = hp;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("SprintJ" + player) != 0)
        {
            if (isSprinting == false)
            {
                speed = runSpeed * coefSpeedCoffee;
                isSprinting = true;
            }
        }
        if (Input.GetAxisRaw("SprintJ" + player) == 0)
        {
            speed = walkSpeed * coefSpeedCoffee;
            isSprinting = false;
        }
    }

    void FixedUpdate()
    {
        if (isAlive && canMove)
        {
            float moveHorizontal = Input.GetAxis("HorizontalJ" + player);
            float moveVertical = Input.GetAxis("VerticalJ" + player);

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

            gameObject.transform.position += movement * speed * Time.deltaTime;
        }
    }

    public void ChangeHealth (int life)     // la fonction à appeler lorsque la vie du personnage change (avec en paramètre les points de vie à ajouter)
    {
        hp += life;
        if (hp > hpMax) // pour ne pas avoir plus de PVs que la vie maximum
        {
            hp = hpMax;
        }
        lifebar.value = hp;
        if (hp <= 0)
        {
            Death();
        }
    }

    public void FullHeal()
    {
        hp = hpMax;
        lifebar.value = hp;
    }

    public void Death()
    {
        hp = 0;
        isAlive = false;
        gameObject.SendMessage("setAliveStatus", isAlive);    // on dit à tous les autres scripts que le personnage est mort
    }

    public void Resurrection()
    {
        isAlive = true;
        gameObject.SendMessage("setAliveStatus", isAlive);    // on dit à tous les autres scripts que le personnage est de nouveau vivant
    }

    public void setCanMove (bool newCanMove)
    {
        canMove = newCanMove;
    }

    public void setCoefSpeedCoffee(float newCoefSpeedCoffee)
    {
        coefSpeedCoffee = newCoefSpeedCoffee;
    }

    public void notifyIsHolding(bool _isHolding) {
        isHolding = _isHolding;
        gameObject.SendMessage("setIsHolding", _isHolding);
    }

    public bool getIsHolding () {
        return isHolding;
    }
}
