using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour {

    public static GameInput instance;

    [SerializeField]
    private float mNoteBeforeHitThreshold = .2f;

    [SerializeField]
    private float mNoteAfterHitThreshold = .3f;

    [HideInInspector]
    public GameTouchController mGameTouchController;

    private Dictionary<GameplayManager.GameplayState, List<GameObject>> mButtons = new Dictionary<GameplayManager.GameplayState, List<GameObject>>();


    private void Awake()
    {
        instance = this;

        mGameTouchController = GetComponent<GameTouchController>();
    }

    // Use this for initialization
    void Start () {
        

        mNoteBeforeHitThreshold *= 44100;
        mNoteAfterHitThreshold *= 44100;
    }

    public void Initialize()
    {
        GameObject greenButton = GameplayCanvasScript.instance.transform.Find("GreenButton").gameObject;
        GameObject redButton = GameplayCanvasScript.instance.transform.Find("RedButton").gameObject;
        GameObject yellowButton = GameplayCanvasScript.instance.transform.Find("YellowButton").gameObject;

        mButtons[GameplayManager.GameplayState.PLAYING] = new List<GameObject>();
        mButtons[GameplayManager.GameplayState.PAUSED] = new List<GameObject>();
        
        mButtons[GameplayManager.GameplayState.PLAYING].Add(greenButton);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(redButton);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(yellowButton);
        mButtons[GameplayManager.GameplayState.PLAYING].Add(GameplayCanvasScript.instance.transform.Find("PauseButton").gameObject);
        mButtons[GameplayManager.GameplayState.PAUSED].Add(GameplayCanvasScript.instance.transform.Find("PauseButton").gameObject); //temporary

        mGameTouchController.SetThreshold(mNoteAfterHitThreshold, mNoteAfterHitThreshold);
        mGameTouchController.Initialize(greenButton.GetComponent<RectTransform>(), redButton.GetComponent<RectTransform>(), yellowButton.GetComponent<RectTransform>());
    }
	


	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch[] mTouches = Input.touches;

            for (int i = 0; i < mTouches.Length; i++)
            {
                Touch touch = mTouches[i];

                foreach (GameObject button in mButtons[GameplayManager.instance.mGameplayState])
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        RectTransform buttonRect = button.GetComponent<RectTransform>();
                        if (RectTransformUtility.RectangleContainsScreenPoint(buttonRect, touch.position))
                        {
                            button.GetComponent<UIButton>().OnButtonTouch(touch, buttonRect);
                            continue; 
                        }
                    }
                }
            }
        }
        
        
	}

    
}
