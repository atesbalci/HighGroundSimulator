using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Slash : MonoBehaviour
    {
        public Action<float> SlashEndAction;
        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value)
                    return;
                gameObject.SetActive(value);
                if (value)
                {
                    _distanceInside = -1f;
                    _prevPoint = null;
                }
                _active = value;
            }
        }

        private bool _active;
        private float _distanceInside;
        private Vector3? _prevPoint;

        private void Update()
        {
            if (Active)
            {
                if (_prevPoint != null && _distanceInside > -0.5f)
                {
                    _distanceInside += Vector3.Distance(_prevPoint.Value, transform.position);
                }
                _prevPoint = transform.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _prevPoint = other.ClosestPoint(transform.position);
            _distanceInside = 0f;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_prevPoint != null)
            {
                _distanceInside += Vector3.Distance(transform.position, _prevPoint.Value);
            }
            if (SlashEndAction != null)
            {
                SlashEndAction(_distanceInside);
            }
        }
    }
}
