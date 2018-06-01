using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour {

    public float mVelocity;

    public Transform mParentTransform;

    public int mStartTime;

    public char mColor;



    public void Initialize(float velocity, int start_time, char color, Transform cruncher)
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        switch (color)
        {
            case 'G':
                mesh.material = Resources.Load("Materials/green") as Material;
                break;
            case 'R':
                mesh.material = Resources.Load("Materials/red") as Material;
                break;
            case 'Y':
                mesh.material = Resources.Load("Materials/yellow") as Material;
                break;
        }

        mVelocity = velocity;
        mStartTime = start_time;
        mColor = color;

        mParentTransform = cruncher;
        transform.position = cruncher.position;
        transform.Translate(cruncher.up * 2);
        transform.LookAt(mParentTransform);
        transform.parent = Camera.main.transform.parent.Find("FretParent");
    }
	

	void Update () {
        transform.LookAt(mParentTransform);
        transform.position = Vector3.MoveTowards(transform.position, mParentTransform.position - mParentTransform.up, mVelocity * Time.deltaTime);


    }
}
