using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.Helpers
{
    [RequireComponent(typeof(Text))]
    public class FlowingText : MonoBehaviour
    {
        public float Speed;
        public Action DoneAction;

        private Text _text;
        private string _curText;
        private float _timer;

        private void Reset()
        {
            Speed = 10f;
        }

        private void Awake()
        {
            _text = GetComponent<Text>();
            SetText(_text.text);
        }

        private void Update()
        {
            if (_text.text.Length < _curText.Length)
            {
                _timer += Speed * Time.deltaTime;
                if (_timer > 1f)
                {
                    _timer -= 1f;
                    _text.text += _curText[_text.text.Length];
                }
                if (_text.text.Length >= _curText.Length && DoneAction != null)
                {
                    DoneAction();
                }
            }
        }

        public void SetText(string text)
        {
            _curText = text;
            _text.text = string.Empty;
        }
    }
}
