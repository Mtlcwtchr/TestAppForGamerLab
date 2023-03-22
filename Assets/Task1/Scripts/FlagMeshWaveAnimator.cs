using System.Collections;
using UnityEngine;

namespace Task1.Scripts
{
    public class FlagMeshWaveAnimator : MonoBehaviour
    {
        [SerializeField] private FlagAnimationData animationData;
        [SerializeField] private FlagMeshGenerator meshGenerator;

        private Mesh _mesh;
        private Vector2Int _flagSize;

        private Vector3[] _baseVerts;

        private void Awake()
        {
            if (meshGenerator.IsGenerated)
            {
                FillMeshData();
            }
            else
            {
                meshGenerator.OnMeshGenerated += FillMeshData;
            }

            void FillMeshData()
            {
                _mesh = meshGenerator.SharedMesh;
                _baseVerts = _mesh.vertices;
                _flagSize = meshGenerator.FlagSize;
            }
        }

        private void Update()
        {
            StartCoroutine(ApplyVertsAnimation());
            StartCoroutine(ApplyUVsAnimation());
        }

        private IEnumerator ApplyVertsAnimation()
        {
            yield return null;
        
            var mesh = _mesh;
            if (!mesh)
            {
                yield break;
            }

            Vector2Int flagSize = _flagSize;
            Vector3[] verts = mesh.vertices;
            int i = 0;
            for (var x = 0; x < flagSize.x; ++x)
            {
                float xRel = (float)x / flagSize.x;
                float xOff = animationData.speed * Time.time;
                float pow = animationData.power * animationData.curve.Evaluate(xRel);
                float xFreq = x * animationData.freq;
                float sin = Mathf.Sin(xFreq + xOff + animationData.phaseShift);
                float zVal = pow * sin;
                Vector3 windPow = animationData.windDirection * animationData.windSpeed * Time.deltaTime;
                Vector3 windVal = windPow * sin;

                for(var y = 0; y < flagSize.y; ++y)
                {
                    Vector3 vert = verts[i];
                    vert.z = zVal;
                    vert += windVal;
                    verts[i] = vert;
                    ++i;
                }
            }

            for (var j = 0; j < verts.Length; ++j) {
                Vector3 vert = verts[j];
                Vector3 baseVertex = _baseVerts[j];
                vert.z = Mathf.Lerp(vert.z, baseVertex.z, animationData.damping * Time.deltaTime);
                verts[j] = vert;
            }

            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private IEnumerator ApplyUVsAnimation()
        {
            yield return null;
        
            var mesh = _mesh;
            if (!mesh)
            {
                yield break;
            }

            Vector2[] uvs = mesh.uv;
            for (int i = 0; i < uvs.Length; ++i)
            {
                Vector2 uv = uvs[i];
                float scrollPow = animationData.scrollSpeed * Time.deltaTime;
                uv += animationData.scrollDirection * scrollPow;
                uvs[i] = uv;
            }

            mesh.uv = uvs;
        }
    }
}
