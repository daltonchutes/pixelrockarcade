using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsScript : MonoBehaviour {

    public static GameObjectsScript instance;



    public Dictionary<char, GameObject> mNoteCrunchers = new Dictionary<char, GameObject>();
	
    void Awake () {
        instance = this;

        mNoteCrunchers['G'] = transform.Find("FretParent").GetChild(0).Find("noteCrushGreen").gameObject;
        mNoteCrunchers['R'] = transform.Find("FretParent").GetChild(0).Find("noteCrushRed").gameObject;
        mNoteCrunchers['Y'] = transform.Find("FretParent").GetChild(0).Find("noteCrushYellow").gameObject;

        Camera.main.transform.parent = transform;
    }

    

}
