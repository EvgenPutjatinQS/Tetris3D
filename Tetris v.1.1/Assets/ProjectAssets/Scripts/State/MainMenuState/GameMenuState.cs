using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class GameMenuState : AppState
    {

        private GameMenuData _data;
        private GameMenuData _exit;

        public GameMenuState() : base()
        {
            _sceneName = "GameMenu";
            _id = EAppStateId.GameMenu;
        }
        public override void Activate(IStateData data, bool reset)
        {
            Debug.Log("OnActivate");
            SceneManager.LoadScene(_sceneName);
            AppRoot.Instance.StartCoroutine(LoadSceneData());
            
        }

        protected override IEnumerator LoadSceneData()
        {
            yield return null;
            _dataGO = GameObject.Find(_sceneName);
            if (_dataGO != null)
            {
                _data = _dataGO.GetComponent<GameMenuData>();
                if (_data != null)
                {
                    _data.NewGameBtn.onClick.AddListener(OnNewGameClicked);
                }
                else
                {
                    Debug.Log("_data is null");
                }

                _exit = _dataGO.GetComponent<GameMenuData>();
                if (_exit != null)
                {
                    _exit.ExitBtn.onClick.AddListener(OnExitClicked);
                }
                else
                {
                    Debug.Log("_Exit is null");
                }
            }
            else
            {
                Debug.Log("_dataGO is null");
            }
        }

        private void OnNewGameClicked() {
            Debug.Log("OnNewGameClicked ");
            AppRoot.Instance.SetState(EAppStateId.GridGame);
        }

        private void OnExitClicked()
        {
            Debug.Log("On Exit Clicked ");
            Application.Quit();
        }

        public override void Initialize() { }

        public override void Deactivate() {
            if (_data != null)
            {
                _data.NewGameBtn.onClick.RemoveAllListeners();
            }
            if(_exit != null)
            {
                _exit.ExitBtn.onClick.RemoveAllListeners();
            }
        }

        public override void Update() { }

        protected override IEnumerator Fade()
        {
            yield return null;
        }
    }
}
