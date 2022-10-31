using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    internal int m_damage = 0;
    private void Awake()
    {
        StartCoroutine(destroyDelay());
        transform.rotation = Quaternion.LookRotation(transform.forward, Player.CurrentPlayer.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.instance.DealDamage(m_damage);
            Destroy(gameObject);
        }
    }

    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
