using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class RegulationsState : AppState
    {
        private RegulationsData _back;

        public RegulationsState() : base()
        {
            _sceneName = "Regulations";
            _id = EAppStateId.Regulations;
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
                _back = _dataGO.GetComponent<RegulationsData>();
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

        public override void Deactivate() {
            if (_back != null)
            {
                _back.BackBtn.onClick.RemoveAllListeners();
            }
        }

        public override void Update() { }

        protected override IEnumerator Fade()
        {
            yield return null;
        }
    }
}
