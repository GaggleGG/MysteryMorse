using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : ScrollToPoint {
    //public GameObject background;
    private Vector3 menuStart;
    private Vector3 menuEnd;

    Transform menuPos;

    public GameObject paper;

    private void Awake() {
        menuPos = GetComponent<Transform>();
    }

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //Finds the paper object in the scene so it can call its function to start the game
        if (paper == null) {
            paper = GameObject.Find("Paper");
        }

        //Sets the start pos and end pos based on the object's position
        menuStart = new Vector3(menuPos.position.x, menuPos.position.y, menuPos.position.z);

        menuEnd = new Vector3(menuPos.position.x, menuPos.position.y + 4.4f, menuPos.position.z);
    }

    // Update is called once per frame
    void Update() {
        //If the slider value reaches 100, the game object gets destroyed
        if (slideValue >= 100) {
            Destroy(this.gameObject);
        }
    }

    public override void UpdateVisuals() {
        Vector3 position = Vector3.Lerp(menuStart, menuEnd, slideValue / targetValue);
        this.gameObject.transform.localPosition = position;
    }

    //When the menu is destroyed, calls the paper's move function
    private void OnDestroy() {
        paper.GetComponent<MovePaper>().StartPaperMove();
    }
}
