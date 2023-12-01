using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    public int manaCostPerUse = 10; // Nuevo costo de maná por cada uso.

    private float nextFireTime = 0.0f;
    private PlayerHealthAndMana playerHealthAndMana;

    void Start()
    {
        playerHealthAndMana = FindObjectOfType<PlayerHealthAndMana>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > nextFireTime && playerHealthAndMana.GetMana() >= manaCostPerUse)
        {
            UseMagic();
            nextFireTime = Time.time + fireRate;
            playerHealthAndMana.UseMana(manaCostPerUse);
        }
    }

    void UseMagic()
    {
        // Instancia la bala en el punto de origen del arma
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Aplica fuerza para disparar la bala
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletSpawnPoint.forward * 1000f);

        // Destruye la bala después de un tiempo (ajusta esto según tus necesidades)
        Destroy(bullet, 3.0f);
    }
}
