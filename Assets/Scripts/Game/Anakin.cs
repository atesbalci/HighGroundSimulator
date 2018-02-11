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

        private TweenerCore<Vector3, Path, PathOptions> _curPath;
        private Vector3 _initialPoint;

        private void Start()
        {
            _initialPoint = transform.position;
            TryIt();
        }

        public void TryIt()
        {
            _curPath.Kill();
            _curPath = transform.DOPath(new[] {HighPoint.position, Target.position}, JumpDuration, PathType.CatmullRom)
                .OnComplete(() => _curPath = null).OnKill(() => _curPath = null);
        }

        private void Update()
        {
            if (_curPath != null)
            {
                var nextPoint = _curPath.PathGetPoint(Mathf.Min(_curPath.ElapsedPercentage() + 0.01f, 1f));
                transform.rotation = Quaternion.LookRotation(nextPoint - transform.position, Vector3.up);
                transform.Rotate(Vector3.right, 90f);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = _initialPoint;
                TryIt();
            }
        }
    }
}
