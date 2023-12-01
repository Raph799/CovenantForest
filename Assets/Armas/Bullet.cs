using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 10;

    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la bala colisiona con un enemigo u otro objeto
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si colisiona con un enemigo, causa daño al enemigo (ajusta esto según tu sistema de salud)
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            MagoController mc = collision.gameObject.GetComponent<MagoController>();
            if(mc != null)
            {
                mc.RecibirDanio(damage);
            }

            Maguinho mmmago = collision.gameObject.GetComponent<Maguinho>();
            if (mmmago != null)
            {
                mmmago.RecibirDanio(damage);
            }

            Ogro ogro = collision.gameObject.GetComponent<Ogro>();
            if (ogro != null)
            {
                ogro.RecibirDanio(damage);
            }

            Arbol arbol = collision.gameObject.GetComponent<Arbol>();
            if (arbol != null)
            {
                arbol.TakeDamage(damage);
            }

            // Destruye la bala
            Destroy(gameObject);
        }
        else
        {
            // Si colisiona con otro objeto, simplemente destruye la bala
            Destroy(gameObject);
        }
    }
}
