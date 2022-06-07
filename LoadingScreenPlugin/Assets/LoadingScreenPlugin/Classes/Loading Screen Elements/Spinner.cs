using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace LSP.LS.Elements
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField, Foldout("Animation Settings")] private float duration = 1f;
        [SerializeField, Foldout("Animation Settings")] private Ease easeType = Ease.Linear;

        private DG.Tweening.Core.TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> spinnerAnimation = null;
        private Vector3 endValue = new Vector3(0, 0, 360);
        private RotateMode rotateMode = RotateMode.LocalAxisAdd;
        private int loops = -1;

        public void StartSpinner()
        {
            spinnerAnimation = transform.DOLocalRotate(endValue, duration, rotateMode);
            spinnerAnimation.SetLoops(loops);
            spinnerAnimation.SetEase(easeType);
        }

        public void StopSpinner()
        {
            spinnerAnimation.Kill();
        }
    } 
}
