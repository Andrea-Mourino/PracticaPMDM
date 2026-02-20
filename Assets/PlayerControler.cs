using UnityEngine;
 using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

        count = 0;
        if (winTextObject != null) winTextObject.SetActive(false);
        SetCountText();

    }


    void SetCountText() 
    {
        if (countText != null) 
        {
            countText.text = "Count: " + count.ToString();
        }
        
        if (count >= 3)
        {
            if (winTextObject != null) winTextObject.SetActive(true);
            
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null) Destroy(enemy);
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    
        rb.AddForce(movement * speed);
    }

    
    private void OnTriggerEnter(Collider other) 
    {
       
        if (other.gameObject.CompareTag("Moneda")) 
        {
           
            other.gameObject.SetActive(false);

            count++;
            SetCountText();
            

            Debug.Log("¡Moneda recogida correctamente! Llevas: " + count);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            if (winTextObject != null)
            {
                winTextObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }
            
           
            Destroy(gameObject);
        }
    }
}