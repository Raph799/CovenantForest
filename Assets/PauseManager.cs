using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject inventoryMenu; // Asigna el objeto del menú de inventario desde el Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // Pausa el tiempo
            inventoryMenu.SetActive(true); // Activa el menú de inventario al pausar el juego
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f; // Reanuda el tiempo
            inventoryMenu.SetActive(false); // Desactiva el menú de inventario al reanudar el juego
        }
    }
}
