using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    public partial class AppRoot : MonoBehaviour
    {

        private static AppRoot mInstance;

        public AppRoot()
        {
            mInstance = this;
        }

        public void Start()
        {
            InitStates();
        }

        public void Update()
        {
            UpdateStates();
        }

        public void OnGUI()
        {

        }

        public bool OnUiAction(GameObject pressedGo, object p)
        {
            return false;
        }

        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        #region Implementation

        #endregion
        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////
        #region Properties

        public static AppRoot Instance
        {
            get
            {
                return mInstance;
            }
        }

        #endregion
        ///////////////////////////////////////////////////////////////////////////
    }

    ///////////////////////////////////////////////////////////////////////////
    #region Types
    public enum EAppStateId
    {
        MainMenu = 0,
        GridGame,
        GameOver,
        InfoMenu,
        GameMenu,
        Regulations,
        Developer
    };
    #endregion
    ///////////////////////////////////////////////////////////////////////////

}