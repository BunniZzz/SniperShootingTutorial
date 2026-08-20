using UnityEngine;

public class EnemyHit : ShootableObject
{
    public EnemyRagdoll enemyRagdoll;
    public GameObject particlesPrefab;
    public float impactForce = 1000f;

    public override void OnHit(RaycastHit hit)
    {
        GameObject particles = Instantiate(particlesPrefab, hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), transform.root.parent);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startColor = Color.red;

        }
        enemyRagdoll.EnableRagdoll();
        GetComponent<Rigidbody>().AddForceAtPosition(hit.transform.forward * impactForce, hit.point, ForceMode.Force);
        Destroy(particles, 2f);


    }

}
