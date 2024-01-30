using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayShooter : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private TextMeshProUGUI hitPointUI;
    [SerializeField] private GameObject bulletIndicator;

    void Start() {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                Damageable target = hitObject.GetComponent<Damageable>();
                if(target != null) {
                    target.Damage(20);
                    hitPointUI.text =  "Hit object at: " + hitObject.transform.position;
                }
                else {
                    StartCoroutine(SphereIndicator(hit.point));
                    hitPointUI.text = "";
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.Instantiate(bulletIndicator, pos, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
