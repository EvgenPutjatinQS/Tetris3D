using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{

    public class GameOverState : AppState
    {
        private GameOverData _data;
        private GameOverData _exit;

        public GameOverState() : base()
        {
            _sceneName = "GameOver";
            _id = EAppStateId.GameOver;
        }

        protected override IEnumerator LoadSceneData(AsyncOperation op)
        {
            yield return base.LoadSceneData(op);
            if (_dataGO != null)
            {
                _data = _dataGO.GetComponent<GameOverData>();
                if (_data != null)
                {
                    _data.NewGameBtn.onClick.AddListener(OnNewGameClicked);
                }
                else
                {
                    Debug.Log("_data is null");
                }

                _exit = _dataGO.GetComponent<GameOverData>();
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
            AppRoot.Instance.SetState(EAppStateId.GridGame);
        }

        private void OnExitClicked()
        {
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