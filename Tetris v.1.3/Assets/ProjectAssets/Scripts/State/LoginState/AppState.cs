using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Tetris
{

    public abstract class AppState
    {

        protected string _sceneName;
        protected GameObject _dataGO;
        protected GameObject _ModelGO;
        protected GameObject moveMod;
        protected EAppStateId _id;

        public virtual void Activate(IStateData data, bool reset) {
            AsyncOperation op = SceneManager.LoadSceneAsync(_sceneName);
            AppRoot.Instance.StartCoroutine(LoadSceneData(op));
        }

        public abstract void Initialize();

        public abstract void Deactivate();

        public abstract void Update();

        protected virtual IEnumerator LoadSceneData(AsyncOperation op) {
            yield return op;
            _dataGO = GameObject.Find(_sceneName);
        }

        protected virtual IEnumerator DelRow()
        {
            yield return null;
        }

        protected virtual IEnumerator Fade()
        {
            yield return null;
        }

        public EAppStateId ID {
            get {
                return _id;
            }
        }

    }

}
