using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour {


    [SerializeField]
    private float mNoteBeforeHitThreshold = .2f;

    [SerializeField]
    private float mNoteAfterHitThreshold = .3f;

    

    Dictionary<GameplayManager.GameplayState, List<GameObject>> mButtons = new Dictionary<GameplayManager.GameplayState, List<GameObject>>();



    // Use this for initialization
    void Start () { 

        mNoteBeforeHitThreshold *= 44100;
        mNoteAfterHitThreshold *= 44100;
    }

    public void Initialize()
    {
        mButtons[GameplayManager.GameplayState.PLAYING] = new List<GameObject>();
        mButtons[GameplayManager.GameplayState.PLAYING].Add(GameplayCanvasScript.instance.transform.Find("GreenButton").gameObject);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(GameplayCanvasScript.instance.transform.Find("RedButton").gameObject);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(GameplayCanvasScript.instance.transform.Find("YellowButton").gameObject);
        mButtons[GameplayManager.GameplayState.PLAYING][0].GetComponent<NoteHit>().SetThreshold(mNoteBeforeHitThreshold, mNoteAfterHitThreshold);
        mButtons[GameplayManager.GameplayState.PLAYING][1].GetComponent<NoteHit>().SetThreshold(mNoteBeforeHitThreshold, mNoteAfterHitThreshold);
        mButtons[GameplayManager.GameplayState.PLAYING][2].GetComponent<NoteHit>().SetThreshold(mNoteBeforeHitThreshold, mNoteAfterHitThreshold);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(GameplayCanvasScript.instance.transform.Find("PauseButton").gameObject);
    }
	


	// Update is called once per frame
	void Update () {
        if (GameplayManager.instance.mGameplayState == GameplayManager.GameplayState.PLAYING)
        {
            if (Input.touchCount > 0)
            {
                print("touched");
                Touch[] mTouches = Input.touches;

                for (int i = 0; i < mTouches.Length; i++)
                {
                    Touch touch = mTouches[i];

                    foreach (GameObject button in mButtons[GameplayManager.instance.mGameplayState])
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            if (RectTransformUtility.RectangleContainsScreenPoint(button.GetComponent<RectTransform>(), touch.position))
                            {
                                button.GetComponent<UIButton>().OnButtonTouch();
                            }
                        }
                    }
                }
            }
        }
        
	}

    
}
