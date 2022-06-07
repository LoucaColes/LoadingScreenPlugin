using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace LSP.LS.Elements
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField, Foldout("Components")] private Image loadingBarFillImage = null;
        [SerializeField, Foldout("Components")] private GameObject loadingBarText = null;

        [SerializeField, Foldout("Settings")] private bool showLoadingBarText = true;

        private void Awake()
        {
            ResetLoadingBarProgress();

            loadingBarText.SetActive(showLoadingBarText);
        }

        public void UpdateLoadingBar(float _progress)
        {
            loadingBarFillImage.fillAmount = _progress;
        }

        public void ResetLoadingBarProgress()
        {
            loadingBarFillImage.fillAmount = 0;
        }
    } 
}
