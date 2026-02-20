using UnityEngine;

public class Rotator : MonoBehaviour {
    void Update() {
        // Hace que la moneda rote 45 grados en cada eje por segundo
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}