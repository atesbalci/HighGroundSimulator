using UnityEngine;

namespace Game
{
    public class ObiWan : MonoBehaviour
    {
        public Slash Slash;
        
        private Plane _slashPlane;

        private void Start()
        {
            _slashPlane = new Plane(Vector3.right, Slash.transform.position);
        }

        private void Update()
        {
            Slash.Active = Input.GetKey(KeyCode.Mouse0);
            if (Slash.Active)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;
                if(_slashPlane.Raycast(ray, out dist))
                {
                    Slash.transform.position = ray.GetPoint(dist);
                }
            }
        }
    }
}
