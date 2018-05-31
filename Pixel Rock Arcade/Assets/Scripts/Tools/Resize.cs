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
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        Vector2 screenSizes = new Vector2(width, height);

        Vector3 scale = Vector3.one;

        for (int i = 0; i < 3; i++)
        {
            if (amount[i] != 1)
            {
                scale[i] = screenSizes[(int)axis[i]] / amount[i];
            }
        }


        
        transform.localScale = scale;
        
	}
	
}
