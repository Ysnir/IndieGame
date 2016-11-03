using UnityEngine;
using System.Collections;
using UnityEngine.UI;   // pour pouvoir utiliser le canvas associé au joueur

public class SC_Player : MonoBehaviour {

    public GameObject characterSprite;  // le sprite du personnage
    public float speed = 3.0f;  // la vitesse de déplacement du joueur
    private Rigidbody rb;   // le rigidbody associé au personnage
    public int player;  // le numéro de joueur, pour pouvoir le donner aux scripts d'action

    public int hpMax = 50; // les points de vie du joueur, mis à 50 de manière arbitraire
    private int hp; // les points de vie actuels du joueur
    public Slider lifebar;  // le slider servant de jauge de vie
    private bool isAlive = true;    // booléen à true tant que le personnage est vivant

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
	


	}

    void FixedUpdate()
    {
        if (isAlive)
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
        lifebar.value = hp;
        if (hp <= 0)
        {

        }
    }

    public void Death()
    {
        isAlive = false;
        gameObject.SendMessage("setAliveStatus", isAlive);    // on dit à tous les autres scripts que le personnage est mort
    }

    public void Resurrection()
    {
        isAlive = true;
        gameObject.SendMessage("setAliveStatus", isAlive);    // on dit à tous les autres scripts que le personnage est de nouveau vivant
    }
}
