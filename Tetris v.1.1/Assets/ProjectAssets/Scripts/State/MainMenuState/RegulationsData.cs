using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    public class RegulationsData : MonoBehaviour
    {
        [SerializeField]
        private Button _back;

        public Button BackBtn
        {
            get {
                return _back;
            }
        }

    }

