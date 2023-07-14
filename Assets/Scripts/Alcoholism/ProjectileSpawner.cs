using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    public static ProjectileSpawner projectileSpawnerInstance;

    [SerializeField]
    [Header("Projectile")]
    Projectile projectile;
    public ObjectPool<Projectile> projectilePool;
    public GameObject player;
    public float repeatProjectile;
    public float offset;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (projectileSpawnerInstance != null && projectileSpawnerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            projectileSpawnerInstance = this;
        }
    }
    private void Start()
    {
        projectilePool = new ObjectPool<Projectile>(CreateProjectile, GetProjectile, RemoveProjectile, DestroyProjectile, true, 3, 6);

        StartCoroutine(RocketsSpawner());
    }

    Projectile CreateProjectile()
    {
        return Instantiate(projectile);
    }

    void GetProjectile(Projectile projectile)
    {
        float xSpawn = 12f * (UnityEngine.Random.value < 0.5 ? 1 : -1);
        float ySpawn = Random.Range(player.transform.position.y - offset, player.transform.position.y + offset);

        Vector3 projectilePos = new Vector3(xSpawn, ySpawn, 0);
        projectile.transform.position = projectilePos;
        if(xSpawn < 0)
        {
            projectile.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            projectile.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        projectile.gameObject.SetActive(true);
    }
    void RemoveProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    void DestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    IEnumerator RocketsSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatProjectile);
            projectilePool.Get();
        }
    }
}
