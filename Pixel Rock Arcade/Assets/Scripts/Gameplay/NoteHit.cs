using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour {

    [SerializeField]
    private RhythmManager mRhythmManager;

    [SerializeField]
    private char mLaneColor;

    private ParticleSystem mParticle;

    // Use this for initialization
    void Start () {
        mParticle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitNote()
    {
        if (mRhythmManager.mSpawnedNotes[mLaneColor].Count > 0)
        {
            Destroy(mRhythmManager.mSpawnedNotes[mLaneColor].Dequeue());
            mParticle.Emit(10);
        }
    }
}
