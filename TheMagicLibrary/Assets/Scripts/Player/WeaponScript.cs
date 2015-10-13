using UnityEngine;
using System.Collections;
/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
    public Transform shotPrefab;
    public float shootingRate = 1f;
    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(int direction)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;
            var shotTransform = Instantiate(shotPrefab) as Transform;
            shotTransform.position = transform.position;
            ShootBolt move = shotTransform.gameObject.GetComponent<ShootBolt>();
            if (move != null)
            {
                move.direction = direction;
            }
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
