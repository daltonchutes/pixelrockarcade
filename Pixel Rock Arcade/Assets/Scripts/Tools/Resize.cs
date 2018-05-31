using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

    [SerializeField]
    private Vector3 amount;

    [SerializeField]
    private Vector3 axis;
    



	// Use this for initialization
	void Start () {
        Vector2 screenSizes = new Vector2(Screen.width, Screen.height);

        Vector3 scale = Vector3.one;

        for (int i = 0; i < 3; i++)
        {
            if (amount[i] != 1)
            {
                scale[i] = screenSizes[(int)axis[i]] / amount[i];
            }
        }
        if (GetComponent<RectTransform>() != null)
            GetComponent<RectTransform>().sizeDelta = scale;
        else
            transform.localScale = scale;


        Vector3 screen2World = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100f));
        transform.localScale = new Vector3(screen2World.x / amount[0], 1, 1);

    }
	
}
