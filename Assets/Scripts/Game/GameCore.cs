using System.Collections;
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
            Player.Slash.SlashEndAction += dist =>
            {
                if (dist >= 0.5f)
                {
                    StartCoroutine("SlowTime");
                }
            };
            Begin();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Begin();
            }
        }

        private void Begin()
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

        private IEnumerator SlowTime()
        {
            Time.timeScale = 0.025f;
            yield return new WaitForSecondsRealtime(3f);
            Time.timeScale = 1f;
            Begin();
        }
    }
}
