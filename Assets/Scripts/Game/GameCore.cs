using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class GameCore : MonoBehaviour
    {
        public ObiWan Player;
        public Anakin Enemy;

        private void Awake()
        {
            DOTween.Init();
        }
    }
}
