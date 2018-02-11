using UnityEngine;

namespace Game
{
    public class ObiWan : MonoBehaviour
    {
        public Slash Slash;
        public float MaxSlashDuration;
        
        private Plane _slashPlane;
        private float _slashTimer;

        private void Start()
        {
            _slashPlane = new Plane(Vector3.right, Slash.transform.position);
        }

        private void Update()
        {
            if (!Slash.Active)
            {
                Slash.Active = Input.GetKeyDown(KeyCode.Mouse0) && Mathf.Approximately(0f, _slashTimer);
            }
            Slash.Active = Slash.Active && Input.GetKey(KeyCode.Mouse0) && _slashTimer < MaxSlashDuration;
            if (Slash.Active)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;
                if(_slashPlane.Raycast(ray, out dist))
                {
                    Slash.transform.position = ray.GetPoint(dist);
                }
                _slashTimer += Time.deltaTime;
            }
            else if(_slashTimer > 0f && Input.GetKey(KeyCode.Mouse0))
            {
                _slashTimer -= Time.deltaTime;
            }
            else
            {
                _slashTimer = 0f;
            }
        }
    }
}
