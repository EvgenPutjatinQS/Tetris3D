using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoMenuData : MonoBehaviour {

    [SerializeField]
    private Button _about;
    [SerializeField]
    private Button _regulation;
    [SerializeField]
    private Button _back;

    public Button OnAboutClicked
    { 
        get
        {
            return _about;
        }
    }
    public Button OnRegulationClicked
    {
        get
        {
            return _regulation;
        }
    }
    public Button OnBackClicked
    {
        get
        {
            return _back;
        }
    }
 
}
