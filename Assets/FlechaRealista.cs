using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaRealista : MonoBehaviour
{
    private Rigidbody rb;
    private bool enAire = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Empezamos con cinemática para que no se caiga sola al aparecer
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Si la flecha fue disparada, hacemos que la punta mire hacia la dirección del movimiento
        if (enAire && rb.velocity.magnitude > 0.5f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void Disparar(float fuerza)
    {
        enAire = true;
        rb.isKinematic = false; // Activamos la física
        rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);
        
        // Desacoplamos la flecha del arco si estaba como hija
        transform.parent = null; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enAire)
        {
            enAire = false;
            rb.isKinematic = true; // Se detiene en seco
            rb.velocity = Vector3.zero;
            
            // Hacemos que la flecha sea "hija" de lo que golpeó (para que se quede clavada)
            transform.parent = collision.transform; 
        }
    }
}
