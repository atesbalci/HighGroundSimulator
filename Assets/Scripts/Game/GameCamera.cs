using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class GameCamera : MonoBehaviour
    {
        private const float AnimationDuration = 3f;

        public Transform StartPos;

        private Vector3 _finalPos;
        private Quaternion _finalRotation;
        private List<Tweener> _tweeners;

        private void Awake()
        {
            _finalPos = transform.position;
            _finalRotation = transform.rotation;
            _tweeners = new List<Tweener>();
        }

        private void Start()
        {
            TakePosition();
        }

        public void TakePosition()
        {
            foreach (var tweener in _tweeners)
            {
                tweener.Kill();
            }
            _tweeners.Clear();

            transform.position = StartPos.position;
            transform.rotation = StartPos.rotation;
            _tweeners.Add(transform.DOMove(_finalPos, 3f).SetEase(Ease.OutExpo));
            _tweeners.Add(transform.DORotateQuaternion(_finalRotation, 3f).SetEase(Ease.OutExpo));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                TakePosition();
        }
    }
}
