using UnityEngine;
using TMPro; // Necesario para controlar el texto

public class GameTimer : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoRestante = 60f;
    public TextMeshProUGUI textoReloj;
    
    [Header("Estado del Juego")]
    public bool juegoActivo = true;

    void Update()
    {
        if (juegoActivo)
        {
            if (tiempoRestante > 0)
            {
                // Resta el tiempo que pasa entre cada frame
                tiempoRestante -= Time.deltaTime;
                MostrarTiempo(tiempoRestante);
            }
            else
            {
                // El tiempo llegó a cero
                tiempoRestante = 0;
                juegoActivo = false;
                FinalizarJuego();
            }
        }
    }

    void MostrarTiempo(float tiempoParaMostrar)
    {
        // Calculamos minutos y segundos
        float minutos = Mathf.FloorToInt(tiempoParaMostrar / 60);
        float segundos = Mathf.FloorToInt(tiempoParaMostrar % 60);

        // Actualiza el texto con formato 00:00
        textoReloj.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        
        // Opcional: Si quedan menos de 10 segundos, poner el texto en rojo
        if (tiempoParaMostrar < 10)
        {
            textoReloj.color = Color.red;
        }
    }

    void FinalizarJuego()
    {
        textoReloj.text = "¡FIN!";
        Debug.Log("Juego Terminado");
        
        // Aquí podrías agregar un sonido de chicharra de final de partido
    }
}