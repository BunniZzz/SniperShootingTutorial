using UnityEngine;

public class WallHit : ShootableObject
{
    public GameObject particlesPrefab;

    public override void OnHit(RaycastHit hit)
    {
        GameObject particles = Instantiate(particlesPrefab, hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), transform.root.parent);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();


        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startColor = meshRenderer.material.color;
        }
        Destroy(particles, 2f);
    }
}