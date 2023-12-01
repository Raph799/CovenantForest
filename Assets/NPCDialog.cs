using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialog : MonoBehaviour
{
    public string dialogText = "¡Hola! Soy un NPC."; // Texto que se mostrará
    public GameObject textUI;
    public TextMeshProUGUI dialogUI; // Referencia al objeto de TextMeshPro en la UI
    public float letterDelay = 0.05f; // Retardo entre cada letra
    public float displayDuration = 3f; // Duración que se muestra el texto antes de desactivarse

    private bool isPlayerInRange = false;

    void Update()
    {
        // Verifica si el jugador está cerca
        if (isPlayerInRange)
        {
            // Verifica si el jugador presiona la tecla "E"
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(ShowDialogLetterByLetter());
            }
        }
    }

    // Método para mostrar el cuadro de texto en la UI letra por letra
    IEnumerator ShowDialogLetterByLetter()
    {
        textUI.SetActive(true);
        dialogUI.text = "";

        // Recorre cada letra en el texto
        for (int i = 0; i < dialogText.Length; i++)
        {
            // Agrega la letra actual al texto
            dialogUI.text += dialogText[i];

            // Espera un breve tiempo antes de mostrar la siguiente letra
            yield return new WaitForSeconds(letterDelay);
        }

        // Espera durante la duración especificada antes de desactivar el texto
        yield return new WaitForSeconds(displayDuration);

        // Desactiva el cuadro de texto después de la espera
        textUI.SetActive(false);
    }

    // Cuando el jugador entra en el área del NPC
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Cuando el jugador sale del área del NPC
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Limpia el texto cuando el jugador se aleja
            dialogUI.text = "";
        }
    }
}
