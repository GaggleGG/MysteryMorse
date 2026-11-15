using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : ScrollToPoint {
    //public GameObject background;
    [SerializeField]
    Vector3 menuStart;
    [SerializeField]
    Vector3 menuEnd;

    public GameObject paper;

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //Finds the paper object in the scene so it can call its function to start the game
        if (paper == null) {
            paper = GameObject.Find("Paper");
        }
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
