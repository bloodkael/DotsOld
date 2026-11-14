using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullet;

    public PhysicMaterial bounceMaterial;

    public float shootDelay;

    public float bulletLifetime = 2f;

    public bool IsBulletRebound { get; set; }

    private float _shootTime = float.MinValue;

    public PlayerStats stats;

    private void Start()
    {
        var jsonString = PlayerPrefs.GetString("Stats");
        if (!jsonString.Equals(string.Empty, System.StringComparison.Ordinal))
        {
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }
        else
        {
            stats = new PlayerStats();
        }
    }


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
            var spawnPosition = transform.TransformPoint(new Vector3(0, 0.5f, .7f));
            var newBullet = Instantiate(bullet, spawnPosition, t.rotation);
            if (IsBulletRebound)
            {
                //CollisionAbility collisionAbility = newBullet.GetComponent<CollisionAbility>();
                //Rebound rebound = newBullet.GetComponent<Rebound>();
                //collisionAbility.collisionActions.Add(rebound);
                Collider collider = newBullet.GetComponent<Collider>();
                collider.material = bounceMaterial;
            }
            Destroy(newBullet, bulletLifetime);
            stats.shotsCount++;
        }
    }
}
