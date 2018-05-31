using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenPosition : MonoBehaviour {

    [SerializeField]
    private Vector2 amount;


    // Use this for initialization
    void Start () {
        RectTransform rt = GetComponent<RectTransform>();

        rt.position = new Vector2(Screen.width / amount[0], Screen.height / amount[1]);

    }
}
