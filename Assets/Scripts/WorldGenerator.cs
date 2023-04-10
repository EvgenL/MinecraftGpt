using UnityEngine;
using UnityEngine.Serialization;

public class WorldGenerator : MonoBehaviour {
    public int width = 50;
    public int height = 50;
    public float scale = 10.0f;
    public float heightScale = 10.0f;
    [FormerlySerializedAs("dirtBlockPrefab")] public GameObject bottomBlockPrefab;
    [FormerlySerializedAs("grassBlockPrefab")] public GameObject midBlockPrefab;
    [FormerlySerializedAs("stoneBlockPrefab")] public GameObject topBlockPrefab;

    private void Start() {
        GenerateWorld();
    }

    private void GenerateWorld() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                float heightValue = Mathf.PerlinNoise((float)x / width * scale, (float)z / height * scale) * heightScale;

                for (int y = 0; y < heightValue; y++) {
                    GameObject blockPrefab;

                    if (y == Mathf.FloorToInt(heightValue) - 1) {
                        blockPrefab = midBlockPrefab;
                    } else if (y < Mathf.FloorToInt(heightValue) - 1 && y >= Mathf.FloorToInt(heightValue) - 5) {
                        blockPrefab = bottomBlockPrefab;
                    } else {
                        blockPrefab = topBlockPrefab;
                    }

                    GameObject block = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }
}