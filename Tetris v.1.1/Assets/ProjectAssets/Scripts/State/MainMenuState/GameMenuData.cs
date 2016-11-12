using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenuData : MonoBehaviour {

    [SerializeField]
    private Button _newGameBtn;
    [SerializeField]
    private Button _exit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Button NewGameBtn {
        get {
            return _newGameBtn;
            }
    }

    public Button ExitBtn
    {
        get {
            return _exit;
        }
    }
}
