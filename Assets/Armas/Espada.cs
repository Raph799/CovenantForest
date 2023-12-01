using UnityEngine;

public class Espada : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si la espada colisiona con un enemigo u otro objeto
        if (other.CompareTag("Enemy"))
        {
            // Si colisiona con un enemigo, causa daño al enemigo (ajusta esto según tu sistema de salud)
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            MagoController mc = other.GetComponent<MagoController>();
            if (mc != null)
            {
                mc.RecibirDanio(damage);
            }

            Maguinho mmmago = other.GetComponent<Maguinho>();
            if (mmmago != null)
            {
                mmmago.RecibirDanio(damage);
            }

            Ogro ogro = other.gameObject.GetComponent<Ogro>();
            if (ogro != null)
            {
                ogro.RecibirDanio(damage);
            }

            Arbol arbol = other.gameObject.GetComponent<Arbol>();
            if (arbol != null)
            {
                arbol.TakeDamage(damage);
            }

            // No destruyas la espada, ya que quieres que siga causando daño
        }
        else
        {
            // Si colisiona con otro objeto, simplemente no hagas nada
        }
    }
}
