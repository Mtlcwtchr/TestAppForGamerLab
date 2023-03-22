using System;
using System.Collections;
using UnityEngine;

namespace Task1.Scripts
{
    public class FlagMeshGenerator : MonoBehaviour
    {
        [SerializeField] private FlagGenerationData flagData;

        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;

        public event Action OnMeshGenerated;

        private Mesh _sharedMesh;
    
        private Vector2Int _flagSize;
    
        public Mesh SharedMesh => _sharedMesh;
        public Vector2Int FlagSize => _flagSize;

    
        public bool IsGenerated { get; private set; }

        private void Awake()
        {
            _flagSize = flagData.size;
            StartCoroutine(GenerateFlagMesh(mesh =>
            {
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();

                meshFilter.mesh = mesh;
                meshRenderer.material = flagData.mat;

                _sharedMesh = mesh;
                IsGenerated = true;
                OnMeshGenerated?.Invoke();
            }));
        }


        private IEnumerator GenerateFlagMesh(Action<Mesh> onMeshGenerated)
        {
            if (!flagData)
            {
                yield break;
            }

            yield return null;
            var (verts, uvs) = GenerateVertsAndUvs(_flagSize.x, _flagSize.y);
            var triangles = GenerateTriangles(_flagSize.x, _flagSize.y);

            Mesh mesh = new Mesh
            {
                vertices = verts,
                uv = uvs,
                triangles = triangles
            };
        
            onMeshGenerated?.Invoke(mesh);
        }

        private (Vector3[], Vector2[]) GenerateVertsAndUvs(int width, int height)
        {
            Vector3[] verts = new Vector3[width * height];
            Vector2[] uvs = new Vector2[width * height];

            int i = 0;
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    verts[i] = new Vector3(x, y, 0);
                    uvs[i] = new Vector2((float)x / width, (float)y / height);
                    ++i;
                }
            }

            return (verts, uvs);
        }

        private int[] GenerateTriangles(int width, int height)
        {
            int verts = width * height;
            int[] triangles = new int[verts * 6];

            int tris = 0;

            for (int i = 0; i < verts - height - 1; ++i)
            {
                triangles[tris + 0] = i + 0;
                triangles[tris + 1] = i + height;
                triangles[tris + 2] = i + 1;
                triangles[tris + 3] = i + 1;
                triangles[tris + 4] = i + height;
                triangles[tris + 5] = i + height + 1;

                tris += 6;
            }

            return triangles;
        }
    }
}
