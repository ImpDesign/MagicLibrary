using UnityEngine;
using System.Collections;
/// <summary>
/// Launch projectile
/// </summary>
public class LightScript : MonoBehaviour
{
    public Transform shotPrefab;
    public float shootingRate = 0.25f;
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

    public GameObject Attack(float timer, int direction)
    {
        shootCooldown = shootingRate;
        var shotTransform = Instantiate(shotPrefab) as Transform;
        shotTransform.position = transform.position;
        MoveProjectile move = shotTransform.gameObject.GetComponent<MoveProjectile>();
        move.timer = timer;
        if (move != null)
        {
            move.direction = new Vector2(direction, 0);
        }
        return shotTransform.gameObject;
    }
}
