using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Tetris
{

    public class InfoMenuState : AppState
    {
        private InfoMenuData _about;
        private InfoMenuData _regulation;
        private InfoMenuData _back;

        public InfoMenuState() : base()
        {
            _sceneName = "InfoMenu";
            _id = EAppStateId.InfoMenu;    
        }

        public override void Initialize() { }

        public override void Deactivate() {

            if (_about != null)
            {
                _about.OnAboutClicked.onClick.RemoveAllListeners();
            }
            if (_regulation != null)
            {
                _regulation.OnRegulationClicked.onClick.RemoveAllListeners();
            }
            if (_back != null)
            {
                _back.OnBackClicked.onClick.RemoveAllListeners();
            }
        }

        public override void Update() { }

        protected override IEnumerator LoadSceneData(AsyncOperation op)
        {
            yield return base.LoadSceneData(op);
            if (_dataGO != null)
            {
                _about = _dataGO.GetComponent<InfoMenuData>();
                if (_about != null)
                {
                    _about.OnAboutClicked.onClick.AddListener(OnAboutClicked);
                }
                else
                {
                    Debug.Log("_about is null");
                }
                _regulation = _dataGO.GetComponent<InfoMenuData>();
                if (_regulation != null)
                {
                    _regulation.OnRegulationClicked.onClick.AddListener(OnRegulationsClicked);
                }
                else
                {
                    Debug.Log("_regulation is null");
                }
                 _back = _dataGO.GetComponent<InfoMenuData>();
                if (_back != null)
                {
                    _back.OnBackClicked.onClick.AddListener(OnBackClicked);
                }
                else
                {
                    Debug.Log("_back is null");
                }
            }
            else
            {
                Debug.Log("_dataGO is null");
            }
            
        }

        private void OnAboutClicked()
        {
            Debug.Log("On About Clicked ");
            AppRoot.Instance.SetState(EAppStateId.Developer);
        }

        private void OnRegulationsClicked()
        {
            Debug.Log("On regulation Clicked ");
            AppRoot.Instance.SetState(EAppStateId.Regulations);
        }

        private void OnBackClicked()
        {
            Debug.Log("On Back Clicked ");
            AppRoot.Instance.SetState(EAppStateId.MainMenu);
        }

        protected override IEnumerator Fade()
        {
            yield return null;
        }
    }
}
