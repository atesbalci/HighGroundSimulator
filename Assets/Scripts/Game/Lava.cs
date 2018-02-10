using System.Linq;
using UnityEngine;

namespace Game
{
    public class Lava : MonoBehaviour
    {
        [Header("Lava Parameters")]
        public int XDimension;
        public int YDimension;
        public float Gap;

        [Header("Wave Parameters")]
        public float Speed;
        public float WaveSize;
        public float PerlinMultiplier;
        public int PerlinAmount;

        private Vector3[] _verticesOriginal;
        private Vector3[] _verticesCur;
        private Mesh _lavaMesh;

        private Vector2[] _curLocs;
        private float[] _curDirs;

        private void Start()
        {
            _lavaMesh = GenerateMesh(XDimension, YDimension, Gap);
            GetComponent<MeshFilter>().sharedMesh = _lavaMesh;
            _verticesOriginal = _lavaMesh.vertices;
            _verticesCur = _verticesOriginal.ToArray();
            _curLocs = new Vector2[PerlinAmount];
            _curDirs = _curLocs.Select(x => Random.Range(0f, 2 * Mathf.PI)).ToArray();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
                return;
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            var h = WaveSize * PerlinAmount * 0.5f; //hopefully will give an average height
            Gizmos.DrawCube(transform.position + Vector3.up * (h / 2f),
                new Vector3(XDimension * Gap, h / 2f, YDimension * Gap));
        }

        private static Mesh GenerateMesh(int dimX, int dimY, float gap)
        {
            var mesh =  new Mesh();
            var vertices = new Vector3[dimX * dimY * 6];
            var triangles = new int[vertices.Length];
            var ind = 0;
            for (var y = 0; y < dimY; y++)
            {
                for (var x = 0; x < dimX; x++)
                {
                    var pos = new Vector3(x - dimX / 2f, 0, y - dimY / 2f);

                    //Triangle 1
                    vertices[ind] = pos * gap;
                    vertices[ind + 1] = (pos + Vector3.forward) * gap;
                    vertices[ind + 2] = (pos + Vector3.right) * gap;
                    triangles[ind] = ind;
                    triangles[ind + 1] = ind + 1;
                    triangles[ind + 2] = ind + 2;
                    ind += 3;

                    //Triangle 2
                    vertices[ind] = (pos + Vector3.forward) * gap;
                    vertices[ind + 1] = (pos + Vector3.right + Vector3.forward) * gap;
                    vertices[ind + 2] = (pos + Vector3.right) * gap;
                    triangles[ind] = ind;
                    triangles[ind + 1] = ind + 1;
                    triangles[ind + 2] = ind + 2;
                    ind += 3;
                }
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }

        private void Update()
        {
            //Shift the individual perlin waves
            for (var i = 0; i < _curLocs.Length; i++)
            {
                _curLocs[i] += new Vector2(Speed * Time.deltaTime * Mathf.Cos(_curDirs[i]),
                    Speed * Time.deltaTime * Mathf.Sin(_curDirs[i]));
            }

            //Updates the individual vertices
            for (var i = 0; i < _verticesOriginal.Length; i++)
            {
                var vert = _verticesOriginal[i];
                
                //Adds up the perlin waves
                var height = 0f;
                for (var j = 0; j < PerlinAmount; j++)
                {
                    height += Mathf.PerlinNoise((_curLocs[j].x + vert.x) * PerlinMultiplier, 
                             (_curLocs[j].y + vert.z) * PerlinMultiplier) * WaveSize;
                }

                //Applies the final height
                _verticesCur[i].y = height;
            }

            //Applies the updated vertices
            _lavaMesh.vertices = _verticesCur;
            _lavaMesh.RecalculateNormals();
        }
    }
}
