using UnityEngine;
using System.Collections;

public class SC_Sofa : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GMSpecificAction()  // l'action que peut faire le GM sur cet objet
    {
        Debug.Log("Je suis un gros canapé blanc");
    }
}
