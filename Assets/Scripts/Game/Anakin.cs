using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Game
{
    public class Anakin : MonoBehaviour {
        public float JumpDuration = 1f;

        [Space(10)]
        public Transform HighPoint;
        public Transform Target;

        public Vector3 InitialPoint { get; private set; }
        public Quaternion InitialRotation { get; private set; }

        private TweenerCore<Vector3, Path, PathOptions> _curPath;

        private void Start()
        {
            InitialPoint = transform.position;
            InitialRotation = transform.rotation;
        }

        public void TryIt(float delay = 0f)
        {
            _curPath.Kill();
            _curPath = transform.DOPath(new[] {HighPoint.position, Target.position}, 1f, PathType.CatmullRom)
                .OnComplete(() => _curPath = null).OnKill(() => _curPath = null).SetDelay(delay);
        }

        private void Update()
        {
            if (_curPath != null)
            {
                var nextPoint = _curPath.PathGetPoint(Mathf.Min(_curPath.ElapsedPercentage() + 0.01f, 1f));
                transform.rotation = Quaternion.LookRotation(nextPoint - transform.position, Vector3.up);
                transform.Rotate(Vector3.right, 90f);
            }
        }
    }
}
