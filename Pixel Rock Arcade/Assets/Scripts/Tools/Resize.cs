using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

    [SerializeField]
    private Vector3 amount;

    [SerializeField]
    private Vector3 axis;




    // Use this for initialization
    void Start()
    {
        Vector2 screenSizes = new Vector2(Screen.width, Screen.height);

        Vector3 screen2World = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100f));
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 scale = Vector3.one;

        Vector3 scaleReference;
        if (rect == null)
            scaleReference = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100f));
        else
            scaleReference = screenSizes = new Vector2(Screen.width, Screen.height);

        for (int i = 0; i < 3; i++)
        {
            if (amount[i] != 1)
            {
                scale[i] = scaleReference[(int)axis[i]] / amount[i];

            }
        }

        if (rect == null)
            transform.localScale = scale;
        else
            rect.sizeDelta = scale;
    }

    
	
}
