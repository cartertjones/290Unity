using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayShooter : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private TextMeshProUGUI hitPointUI;
    [SerializeField] private GameObject bulletIndicator;

    private int damageAmt = 20;
    private float headshotMultiplier = 1.5f;

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
                Damageable damageable = FindDamageableRecursively(hitObject);
                hitPointUI.text =  "Hit object at: " + hitObject.transform.position + " " + hitObject.name;

                if(damageable != null) {
                    damageable.Damage(hitObject.name == "Head" ? (int)(damageAmt * headshotMultiplier) : damageAmt);
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

    public Damageable FindDamageableRecursively(GameObject obj)
    {
        Damageable damageable = obj.GetComponent<Damageable>();
        if (damageable != null)
        {
            // Found Damageable component on the current object
            return damageable;
        }
        else
        {
            // Check parent recursively
            Transform parentTransform = obj.transform.parent;
            while (parentTransform != null && damageable == null)
            {
                damageable = parentTransform.GetComponent<Damageable>();
                parentTransform = parentTransform.parent;
            }
            return damageable;
        }
    }
}
