using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverData : MonoBehaviour {

    [SerializeField]
    private Button _newGameBtn;
    [SerializeField]
    private Button _exit;

    public Button NewGameBtn
    {
        get
        {
            return _newGameBtn;
        }
    }

    public Button ExitBtn
    {
        get
        {
            return _exit;
        }
    }
}
