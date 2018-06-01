using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour {

    RhythmManager mRhythmManager;

    [SerializeField]
    private float mNoteBeforeHitThreshold = .2f;

    [SerializeField]
    private float mNoteAfterHitThreshold = .3f;

    [SerializeField]
    private GameObject mGreenButton;

    [SerializeField]
    private GameObject mRedButton;

    [SerializeField]
    private GameObject mYellowButton;


    Dictionary<char, GameObject> mButtons = new Dictionary<char, GameObject>();



    // Use this for initialization
    void Start () { 
        mRhythmManager = GetComponent<RhythmManager>();

        mButtons['G'] = mGreenButton;
        mButtons['R'] = mRedButton;
        mButtons['Y'] = mYellowButton;

        mNoteBeforeHitThreshold *= 44100;
        mNoteAfterHitThreshold *= 44100;
    }
	


	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch[] mTouches = Input.touches;

            for (int i = 0; i < mTouches.Length; i++)
            {
                Touch touch = mTouches[i];
                
                foreach(KeyValuePair<char, GameObject> button in mButtons)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (RectTransformUtility.RectangleContainsScreenPoint(button.Value.GetComponent<RectTransform>(), touch.position) && CheckIfNoteIsClose(button.Key))
                        {
                            mRhythmManager.GetNoteCrunchers()[button.Key].GetComponentInChildren<NoteHit>().HitNote();
                        }
                    }
                }
            }







        }
        
	}

    private bool CheckIfNoteIsClose(char lane)
    {
        if (mRhythmManager.mSpawnedNotes[lane].Count > 0)
        {
            int nextNoteTime = mRhythmManager.mSpawnedNotes[lane].Peek().GetComponent<NoteScript>().mStartTime;
            if (mRhythmManager.mKoreo.GetLatestSampleTime() + mNoteBeforeHitThreshold >= nextNoteTime && nextNoteTime >= mRhythmManager.mKoreo.GetLatestSampleTime() - mNoteAfterHitThreshold)
                return true;
        }
        return false;
    }
}
