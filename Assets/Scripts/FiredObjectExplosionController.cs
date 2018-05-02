using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Some objects may have explosive effects. This is handled and dictated by the presence of this controller
/// Multiplies the colliders on the object to have it hit more objects
/// 
/// Written by: Arthur Wollocko
/// </summary>
public class FiredObjectExplosionController : MonoBehaviour {

    public ParticleSystem explosionParticles;                   // The particle effect that should play if hit
    public AudioClip explosionSound;                            // Sound that will play on hit
    public float explosionColliderMultiplier = 2f;                   // How much to expand the collider to potentially hit other objects!

    private SphereCollider[] colliders;

	// Use this for initialization
	void Start () {
        colliders = this.GetComponents<SphereCollider>();
	}

    /// <summary>
    /// Called when the fired object collides with a target
    /// </summary>
    public void Explode()
    {
        //Play the explosion sound if it exists
        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);

        //Play the particle system if it exists
        if (explosionParticles != null)
        {
            GameObject parts = GameObject.Instantiate(explosionParticles.gameObject);
            parts.transform.position = this.transform.position;
            parts.transform.SetParent(this.transform);
            parts.GetComponentInChildren<ParticleSystem>().Play();
        }

        //Disable the renderer for the game object if it 'explodes'. Iterate through children and disable renderers
        foreach (Transform child in transform)
        {
            //Only disable rendering if its not the particle system
            if(child.GetComponent<ParticleSystem>() == null)
                child.GetComponent<Renderer>().enabled = false;
        }


        //Go through the sphere colliders and make them larger!
        foreach (SphereCollider c in colliders)
        {
            c.radius = c.radius * explosionColliderMultiplier;
        }


        //Destroy the object after a set amount of time
        Invoke("DestroyFireObject", 1f);
    }

    void DestroyFireObject()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
