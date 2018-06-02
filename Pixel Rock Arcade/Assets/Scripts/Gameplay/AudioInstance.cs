using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour {

    public static AudioInstance instance = null;

	void Awake () {
        instance = this;
	}
	
}
