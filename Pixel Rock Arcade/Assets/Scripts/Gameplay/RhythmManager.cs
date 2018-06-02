using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class RhythmManager : MonoBehaviour {

    public static RhythmManager instance;

    [SerializeField]
    private GameObject mNotePrefab;
    
    

    private Dictionary<char, Queue<NoteDataObject>> mNotesToSpawn = new Dictionary<char, Queue<NoteDataObject>>();

    public Dictionary<char, Queue<GameObject>> mSpawnedNotes = new Dictionary<char, Queue<GameObject>>();

    public Koreography mKoreo;

    int mNoteToHitTravelTime = 0;

	// Use this for initialization
	void Awake () {

        instance = this;
        

        mNotesToSpawn['R'] = new Queue<NoteDataObject>();
        mNotesToSpawn['G'] = new Queue<NoteDataObject>();
        mNotesToSpawn['Y'] = new Queue<NoteDataObject>();

        mSpawnedNotes['R'] = new Queue<GameObject>();
        mSpawnedNotes['G'] = new Queue<GameObject>();
        mSpawnedNotes['Y'] = new Queue<GameObject>();

        
    }

    public void Initialize()
    {
        mKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);

        mNoteToHitTravelTime = 2 * 1 * 44100;

        InitializeNotes();
        
        
    }
	
	// Update is called once per frame
	void Update () {
		foreach (Queue<NoteDataObject> lane in mNotesToSpawn.Values)
        {
            if (lane.Count > 0)
            {

                if (mKoreo.GetLatestSampleTime() + mNoteToHitTravelTime >= lane.Peek().GetStartTime())
                {
                    NoteDataObject spawnedNote = lane.Dequeue();
                    spawnNote(spawnedNote.GetColor(), 1, spawnedNote.GetStartTime());   //hard coded speed for now. Will later depend on difficulty
                }
            }
        }


        
	}


    private void spawnNote(char color, float velocity, int start_time)
    {
        GameObject newNote = Instantiate(mNotePrefab);
        
        NoteScript newScript = newNote.GetComponent<NoteScript>();
        
        newScript.Initialize(velocity, start_time, color, GameObjectsScript.instance.mNoteCrunchers[color].transform);
        mSpawnedNotes[color].Enqueue(newNote);
    }


    private void InitializeNotes()
    {
        List<KoreographyEvent> rawEvents = mKoreo.GetTrackByID("FurEliseRhythmEASY").GetAllEvents();
        foreach (KoreographyEvent kevent in rawEvents)
        {
            if (kevent.HasTextPayload())
            {

                char[] payload = kevent.GetTextValue().ToCharArray();

                NoteDataObject newNote = new NoteDataObject();

                if (payload[0].Equals('N'))
                {
                    //payload[1] == color
                    newNote.Initialize(payload[1], payload[0], kevent.StartSample);
                }


                mNotesToSpawn[payload[1]].Enqueue(newNote);
            }
        }
    }
}
