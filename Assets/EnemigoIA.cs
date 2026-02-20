using UnityEngine;

public class EnemigoIA : MonoBehaviour {
    public Transform bola; 
    public float velocidad = 3f;
    public float distanciaLimite = 5f;

    void Update() {
        float distancia = Vector3.Distance(transform.position, bola.position);
        if (distancia < distanciaLimite) {
            Debug.Log("Estado: CERCA - Persiguiendo");
            transform.position = Vector3.MoveTowards(transform.position, bola.position, velocidad * Time.deltaTime);
        } else {
            Debug.Log("Estado: LEJOS - Esperando");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("¡CONTACTO DETECTADO! La bola ha tocado al cubo.");
        }
    }
}