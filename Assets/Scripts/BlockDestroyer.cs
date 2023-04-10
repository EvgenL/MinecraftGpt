using UnityEngine;

public class BlockDestroyer : MonoBehaviour {
    public float maxDistance = 4.0f;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, maxDistance)) {
                if (hit.transform.gameObject.CompareTag("Ground")) {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}