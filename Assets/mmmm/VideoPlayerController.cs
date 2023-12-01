using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string Basics; // Nombre de la próxima escena a cargar después del video
    public GameObject Mensaje; // Referencia al objeto que muestra el mensaje de salto

    private bool canSkip = false;
    private bool hasStarted = false;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.started += OnVideoStarted;
        }
    }

    private void OnVideoStarted(VideoPlayer vp)
    {
        hasStarted = true;
        Mensaje.SetActive(false); // Muestra el mensaje de salto al comenzar el video
    }

    private void Update()
    {
        if (hasStarted && !canSkip && Input.anyKeyDown)
        {
            // Mostrar el mensaje de salto al apretar una tecla por primera vez
            canSkip = true;
            Mensaje.SetActive(true);
            Invoke("DisableSkip", 5f); // Deshabilitar la posibilidad de saltar después de 5 segundos sin tocar ninguna tecla
        }
        else if (canSkip && Input.anyKeyDown)
        {
            // Si se toca una tecla nuevamente después de haber mostrado el mensaje, cargar la próxima escena
            SceneManager.LoadScene(Basics);
        }
    }


    private void OnVideoFinished(VideoPlayer vp)
    {
        // Cargar la próxima escena después de que el video haya terminado
        SceneManager.LoadScene(Basics);
    }

    private void DisableSkip()
    {
        canSkip = false;
        Mensaje.SetActive(false); // Oculta el mensaje de salto después de 5 segundos sin tocar ninguna tecla
    }
    //Script robado de!... No recuerdo donde, pero se que usamos este para Trick Or Spook:]
}