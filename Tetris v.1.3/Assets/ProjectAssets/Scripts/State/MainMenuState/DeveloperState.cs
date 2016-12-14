using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class DeveloperState : AppState
    {
        private DeveloperData _back;

        public DeveloperState() : base()
        {
            _sceneName = "Developer";
            _id = EAppStateId.Developer;
        }

        protected override IEnumerator LoadSceneData(AsyncOperation op)
        {
            yield return base.LoadSceneData(op);
            if (_dataGO != null)
            {
                _back = _dataGO.GetComponent<DeveloperData>();
                if (_back != null)
                {
                    _back.BackBtn.onClick.AddListener(OnBackClicked);
                }
                else
                {
                    Debug.Log("_Back is null");
                }
            }
            else
            {
                Debug.Log("_dataGO is null");
            }
        }

        private void OnBackClicked()
        {
            Debug.Log("On Exit Clicked ");
            AppRoot.Instance.SetState(EAppStateId.InfoMenu);
        }

        public override void Initialize() { }

        public override void Deactivate()
        {
            if (_back != null)
            {
                _back.BackBtn.onClick.RemoveAllListeners();
            }
        }

        public override void Update() { }
    }
}
