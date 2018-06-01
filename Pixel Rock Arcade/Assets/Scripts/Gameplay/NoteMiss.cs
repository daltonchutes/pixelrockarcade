using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMiss : MonoBehaviour {

    [SerializeField]
    private RhythmManager mRhythmManager;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            print("MISS");
            mRhythmManager.mSpawnedNotes[other.GetComponent<NoteScript>().mColor].Dequeue();
            Destroy(other.gameObject);
        }
    }
}
