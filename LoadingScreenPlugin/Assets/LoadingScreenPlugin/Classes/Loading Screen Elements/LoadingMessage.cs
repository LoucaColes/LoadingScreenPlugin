using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace LSP.LS.Elements
{
    public class LoadingMessage : MonoBehaviour
    {
        [SerializeField, Foldout("Components")] private TextMeshProUGUI messageText = null;

        public void SetLoadingMessage(string _message)
        {
            messageText.text = _message;
        }
    }

}