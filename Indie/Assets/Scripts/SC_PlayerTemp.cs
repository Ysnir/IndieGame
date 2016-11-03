using UnityEngine;
using System.Collections;

public class SC_PlayerTemp : MonoBehaviour {

    public GameObject characterSprite;
	private float speed = 3.0f;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))       // pour bouger le personnage
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
            characterSprite.transform.rotation = new Quaternion();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
            characterSprite.transform.rotation = new Quaternion(0, 0, 90, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
            characterSprite.transform.rotation = new Quaternion(0, 0, 90, 90);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
            characterSprite.transform.rotation = new Quaternion(0, 0, 90, -90);
        }
    }
}
