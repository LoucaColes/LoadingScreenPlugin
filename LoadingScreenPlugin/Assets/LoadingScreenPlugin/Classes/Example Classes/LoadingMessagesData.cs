using UnityEngine;

namespace LSP.LS.Data
{
    [CreateAssetMenu(fileName = "Loading Messages Data", menuName = "ScriptableObjects/LoadingMessagesData")]
    public class LoadingMessagesData : ScriptableObject
    {
        [SerializeField] private string[] loadingMessages = new string[0];

        public string GetRandomMessage()
        {
            int index = Random.Range(0, loadingMessages.Length - 1);
            return loadingMessages[index];
        }
    }

}