using UnityEngine;
using System.Collections;

namespace Tetris
{

    public abstract class AppState
    {

        protected string _sceneName;
        protected GameObject _dataGO;
        protected GameObject _ModelGO;
        protected GameObject moveMod;
        protected EAppStateId _id;

        public abstract void Activate(IStateData data, bool reset);

        public abstract void Initialize();

        public abstract void Deactivate();

        public abstract void Update();

        protected abstract IEnumerator LoadSceneData();

        protected abstract IEnumerator Fade();

        public EAppStateId ID {
            get {
                return _id;
            }
        }

    }

}
