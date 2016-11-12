using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuData : MonoBehaviour {
    
    [SerializeField]
    private Button _gameBtn;
    [SerializeField]
    private Button _options;
 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Button NewGameBtn
    {
        get
        {
            return _gameBtn;
        }
    }

    public Button OptionsBtn
    {
        get
        {
            return _options;
        }
    }
}
