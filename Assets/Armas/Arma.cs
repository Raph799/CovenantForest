using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour
{
    public PlayerMovement pm;
    public Animator animator;
    public float velocidadRotacion = 100.0f;
    public float duracionCorte = 0.2f;
    public Transform puntoCorte; // Punto desde el cual se realiza el corte.

    private bool cortando = false;
    
    Quaternion initialLocalRotation;

    private void Start()
    {
        initialLocalRotation = this.transform.localRotation;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cortando)
        {
            StartCoroutine(RealizarCorte());
        }

        // Rotar el arma gradualmente hacia adelante
        /*if (!cortando)
        {
            float rotacion = Input.GetAxis("Mouse X") * velocidadRotacion * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotacion);
        } */
    }

    IEnumerator RealizarCorte()
    {
        pm.estaAtacando = true;
        animator.Play("golpearEspada");
        transform.GetChild(0).gameObject.SetActive(true);
        cortando = true;

        // Guardar la rotación actual del arma
        Quaternion rotacionInicial = transform.rotation;

        // Rotar el arma hacia adelante (90 grados en el eje Z)
        Quaternion rotacionFinal = transform.rotation * Quaternion.Euler(0, 0, 90);
        float tiempoPasado = 0.0f;

        while (tiempoPasado < duracionCorte)
        {
            tiempoPasado += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempoPasado / duracionCorte);
            yield return null;
        }

        // Restablecer la rotación del arma
        transform.rotation = rotacionInicial;

        cortando = false;
        transform.GetChild(0).gameObject.SetActive(false);
        this.transform.localRotation = initialLocalRotation;
        pm.estaAtacando = false;
    }
}
