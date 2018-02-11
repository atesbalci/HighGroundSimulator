using DG.Tweening;
using UnityEngine;
using Utility.Helpers;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public ObiWan Player;
        public Anakin Enemy;
        public FlowingText Text;

        [Space(10)]
        [SerializeField]
        private TextAsset _namesText;
        private string[] _names;

        private void Awake()
        {
            DOTween.Init();
        }

        private void Start()
        {
            _names = _namesText.text.Split('\n');
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Text.SetText("It's over " + _names[Random.Range(0, _names.Length)] +
                             ", I have the high ground!");
                Text.DoneAction = () =>
                {
                    Enemy.transform.position = Enemy.InitialPoint;
                    Enemy.transform.rotation = Enemy.InitialRotation;
                    Enemy.TryIt(Random.Range(2f, 4f));
                };
            }
        }
    }
}
