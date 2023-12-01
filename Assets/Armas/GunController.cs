using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float defaultFireRate = 1f;
    public float temporaryFireRate = 0.4f;
    public float temporaryFireRateDuration = 2f;
    public float cooldownTime = 3f; // Tiempo de espera antes de que se pueda volver a usar "E"
    public int manaCostPerActivation = 20; // Nuevo costo de maná por activación.

    private float fireRate;
    private float nextFireTime = 0.0f;
    private bool isTemporaryFireRateActive = false;
    private float nextEUsageTime = 0.0f;

    private PlayerHealthAndMana playerHealthAndMana;

    void Start()
    {
        fireRate = defaultFireRate;
        playerHealthAndMana = FindObjectOfType<PlayerHealthAndMana>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextEUsageTime && playerHealthAndMana.GetMana() >= manaCostPerActivation)
        {
            if (!isTemporaryFireRateActive)
            {
                StartCoroutine(ActivateTemporaryFireRate());
                playerHealthAndMana.UseMana(manaCostPerActivation);
            }
        }
    }

    void Shoot()
    {
        // Instancia la bala en el punto de origen del arma
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Aplica fuerza para disparar la bala
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletSpawnPoint.forward * 1000f);

        // Destruye la bala después de un tiempo (ajusta esto según tus necesidades)
        Destroy(bullet, 3.0f);
    }

    IEnumerator ActivateTemporaryFireRate()
    {
        isTemporaryFireRateActive = true;
        fireRate = temporaryFireRate;

        // Espera durante el tiempo especificado para desactivar el fireRate temporal
        yield return new WaitForSeconds(temporaryFireRateDuration);

        fireRate = defaultFireRate;
        isTemporaryFireRateActive = false;

        // Establece el tiempo de espera antes de que se pueda volver a usar "E"
        nextEUsageTime = Time.time + cooldownTime;
    }
}
