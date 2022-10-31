using UnityEngine;

public class shootProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody m_projectile;
    [SerializeField] Transform m_muzzel;
    [SerializeField] float m_force;
    [SerializeField] int m_Damage;

    public void ShootPlayer()
    {
        var bullet = Instantiate(m_projectile, m_muzzel.position, m_muzzel.rotation);
        var targetPos = Player.instance.m_ExampleCharacterController.transform.position;
        var targetDirection = (targetPos - transform.position);
        targetDirection.y = 0;
        bullet.AddForce(targetDirection * m_force, ForceMode.Impulse);
        bullet.GetComponent<Projectile>().m_damage = m_Damage;
    }
}
