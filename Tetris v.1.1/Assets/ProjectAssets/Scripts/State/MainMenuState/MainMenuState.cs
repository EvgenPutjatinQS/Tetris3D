using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class MainMenuState : AppState
    {
        private MainMenuData _data;
        private MainMenuData _option;

        public MainMenuState():base() {
            _sceneName = "MainMenu";
            _id = EAppStateId.MainMenu;
        }

        public override void Activate(IStateData data, bool reset) {
            SceneManager.LoadScene(_sceneName);
            AppRoot.Instance.StartCoroutine(LoadSceneData());
        }

        public override void Initialize() { }

        public override void Deactivate() {
            if (_data != null)
            {
                _data.NewGameBtn.onClick.RemoveAllListeners();
            }
            if (_option != null)
            {
                _option.OptionsBtn.onClick.RemoveAllListeners();
            }
        }

        public override void Update() { }

        protected override IEnumerator LoadSceneData()
        {
            yield return null;
            _dataGO = GameObject.Find(_sceneName);
            if (_dataGO != null)
            {
                _data = _dataGO.GetComponent<MainMenuData>();
                if (_data != null)
                {
                    _data.NewGameBtn.onClick.AddListener(OnNewGameClicked);
                }
                else
                {
                    Debug.Log("_data is null");
                }
                _option = _dataGO.GetComponent<MainMenuData>();
                if (_option != null)
                {
                    _option.OptionsBtn.onClick.AddListener(OnOptionsClicked);
                }
                else
                {
                    Debug.Log("_option is null");
                }
            }
            else
            {
                Debug.Log("_dataGO is null");
            } 
        }

        private void OnNewGameClicked()
        {
            Debug.Log("OnNewGameClicked ");
            AppRoot.Instance.SetState(EAppStateId.GameMenu);
        }

        private void OnOptionsClicked()
        {
            Debug.Log("On Optioins Clicked ");
            AppRoot.Instance.SetState(EAppStateId.InfoMenu);
        }

        protected override IEnumerator Fade()
        {
            yield return null;
        }
    }

}
