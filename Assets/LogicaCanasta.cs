using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Fundamental para el texto

public class LogicaCanasta : MonoBehaviour
{
    [Header("Configuración del Marcador")]
    public TextMeshProUGUI textoPuntos; // Arrastra el texto aquí en el Inspector

    [Header("Efectos Visuales")]
    public ParticleSystem efectoConfeti; // Arrastra el Particle System aquí

    private int contador = 0;
    private AudioSource sonidoCelebracion; // Variable interna para el audio
    private GameTimer cronometro; // Referencia al script del tiempo

    void Start()
    {
        // Buscamos el cronómetro en la escena automáticamente
        cronometro = FindObjectOfType<GameTimer>();

        // Al iniciar, buscamos el componente Audio Source que está en el mismo objeto
        sonidoCelebracion = GetComponent<AudioSource>();

        if (sonidoCelebracion == null)
        {
            Debug.LogWarning("¡Ojo! No encontré un Audio Source en " + gameObject.name + ". Recuerda añadir el componente.");
        }

        // Nos aseguramos de que el confeti no esté echando papelitos al empezar
        if (efectoConfeti != null)
        {
            efectoConfeti.Stop();
        }
    }

    // Este método se activa cuando el balón entra en el Box Collider (Is Trigger)
    private void OnTriggerEnter(Collider other)
    {
        // VERIFICACIÓN: Solo si el objeto es "Player" Y el juego sigue activo (tiempo > 0)
        if (other.CompareTag("Player") && (cronometro == null || cronometro.juegoActivo))
        {
            contador++; // Suma el punto
            ActualizarInterfaz();
            
            // 1. Reproducir el sonido de celebración
            if (sonidoCelebracion != null)
            {
                sonidoCelebracion.Play();
            }

            // 2. Activar la explosión de confeti
            if (efectoConfeti != null)
            {
                efectoConfeti.Play();
            }

            Debug.Log("¡Canasta anotada! Puntos: " + contador);
        }
        else if (cronometro != null && !cronometro.juegoActivo)
        {
            Debug.Log("Canasta intentada, pero el tiempo se ha agotado.");
        }
    }

    // Actualiza el texto en el Canvas
    void ActualizarInterfaz()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = contador.ToString();
        }
        else
        {
            Debug.LogWarning("No has asignado el objeto de texto al script LogicaCanasta en el Inspector.");
        }
    }
}