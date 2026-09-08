using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullet;

    public float shootDelay;

    private float _shootTime = float.MinValue;
    public void Execute()
    {
        if (Time.time < _shootTime + shootDelay)
        {
            return;
        }
        _shootTime = Time.time;
        if (bullet != null)
        {
            var t = transform;
            var newBullet = Instantiate(bullet, t.position, t.rotation);
            Destroy(newBullet, 2f);
        }
    }
}
