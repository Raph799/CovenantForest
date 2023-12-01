using UnityEngine;

public class SelfActivation : MonoBehaviour
{
    // Referencia al objeto que se debe destruir para activar otro GameObject.
    public GameObject objetoADestruir;

    // Referencia al GameObject que se activará.
    public GameObject objetoAActivar;

    private void Start()
    {
        // Verificar si se proporcionó el objeto a destruir y el objeto a activar.
        if (objetoADestruir == null || objetoAActivar == null)
        {
            Debug.LogError("Por favor, configure correctamente el objeto a destruir y el objeto a activar en el script.");
            return;
        }
    }

    private void Update()
    {
        // Verificar si el objeto a destruir ya no existe.
        if (objetoADestruir == null)
        {
            // Activar el otro GameObject.
            objetoAActivar.SetActive(true);

            // Desactivar este script para evitar que continúe verificando.
            enabled = false;
        }
    }
}
