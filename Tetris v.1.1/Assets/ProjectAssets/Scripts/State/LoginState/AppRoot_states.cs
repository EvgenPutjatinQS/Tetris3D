using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public partial class AppRoot : MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////
        #region Variables

        private Dictionary<EAppStateId, AppState> mStates = new Dictionary<EAppStateId, AppState>();
        private AppState mCurState;

        #endregion
        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        #region Interface

        public AppState GetState(EAppStateId id)
        {
            AppState state;

            if (mStates.TryGetValue(id, out state))
            {
                return state;
            }

            return null;
        }

        public void SetState(EAppStateId id)
        {
            SetState(id, false, null);
        }

        public void SetState(EAppStateId id, bool resetState)
        {
            SetState(id, resetState, null);
        }

        public void SetState(EAppStateId id, IStateData data)
        {
            SetState(id, false, data);
        }

        public void SetState(EAppStateId id, bool resetState, IStateData data)
        {
            if (mCurState == null)
            {
                AppState newState;

                if (!mStates.ContainsKey(id))
                {
                    Debug.LogWarning("Error! Cannot find state: " + id);
                    newState = mStates[EAppStateId.MainMenu];
                }
                else
                {

                    newState = mStates[id];
                }

                mCurState = newState;
                mCurState.Activate(data, resetState);
                return;
            }
            Debug.Log(id + " " + mCurState.ID);
            if (mCurState.ID != id)
            {
                AppState newState;

                if (!mStates.ContainsKey(id))
                {
                    Debug.LogWarning("Error! Cannot find state: " + id);
                    newState = mStates[EAppStateId.MainMenu];
                    newState = mStates[EAppStateId.GameOver];
                    newState = mStates[EAppStateId.GridGame];
                    newState = mStates[EAppStateId.InfoMenu];
                    newState = mStates[EAppStateId.GameMenu];
                    newState = mStates[EAppStateId.Regulations];
                    newState = mStates[EAppStateId.Developer];
                }
                else
                {
                    newState = mStates[id];
                }

                if (newState != mCurState || resetState)
                {
                    mCurState.Deactivate();
                    mCurState = newState;
                    mCurState.Activate(data, resetState);
                }
                else
                {
                    Debug.Log("Error! Cannot set new state: newState = mCurState!");
                }
            }
            else
            {
                Debug.Log("Error trying to set the SAME state!");
            }
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        #region Implementation

        private void InitStates()
        {
            Debug.Log("AppRoot::InitStates called");

            mStates[EAppStateId.MainMenu] = new MainMenuState();
            mStates[EAppStateId.GridGame] = new GameGridState();
            mStates[EAppStateId.GameMenu] = new GameMenuState();
            mStates[EAppStateId.GameOver] = new GameOverState();
            mStates[EAppStateId.InfoMenu] = new InfoMenuState();
            mStates[EAppStateId.Regulations] = new RegulationsState();
            mStates[EAppStateId.Developer] = new DeveloperState();

            foreach (KeyValuePair<EAppStateId, AppState> statePair in mStates)
            {
                statePair.Value.Initialize();
                Debug.Log("Init+" + statePair.Value);
            }

            SetState(EAppStateId.MainMenu);
        }

        private void UpdateStates()
        {
            if (mCurState != null)
            {
                mCurState.Update();
            }
        }
        #endregion
        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        #region Properties

        public AppState State
        {
            get { return mCurState; }
        }

        public EAppStateId StateId
        {
            get { return mCurState.ID; }
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////
    }

}