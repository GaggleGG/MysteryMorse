using System.Collections;
using UnityEngine;

public class MovePaper : MonoBehaviour {
    public Vector3 startPos;
    public Vector3 endPos;

    Transform paperModel;

    //Gets the transform of the object
    private void Awake() {
        paperModel = GetComponent<Transform>();
    }

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        //Sets the start pos and end pos based on the object's position
        startPos = new Vector3(paperModel.position.x, paperModel.position.y, paperModel.position.z);

        endPos = new Vector3(paperModel.position.x, paperModel.position.y, paperModel.position.z - 2);
    }

    //Update is called once per frame
    void Update() {
        
    }

    //Called by menu destruction
    public void StartPaperMove() {
        //Starts movement coroutine
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        //Interval
        var t = 0f;

        //Lerps the paper on the z axis
        while (t <= 1f) {
            t += 1.5f * Time.deltaTime;
            paperModel.position = Vector3.Lerp(startPos, endPos, t/1);
            yield return null;
        }
    }
}
