using UnityEngine;

public class BlockPlacer : MonoBehaviour {
    public GameObject blockPrefab;
    public float maxDistance = 4.0f;
    public float blockSize = 1.0f;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance)) {
                Vector3 spawnPosition = hit.point + hit.normal / 2.0f;
                spawnPosition.x = Mathf.Round(spawnPosition.x / blockSize) * blockSize;
                spawnPosition.y = Mathf.Round(spawnPosition.y / blockSize) * blockSize;
                spawnPosition.z = Mathf.Round(spawnPosition.z / blockSize) * blockSize;

                if (spawnPosition.y < 255.0f) {
                    Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}