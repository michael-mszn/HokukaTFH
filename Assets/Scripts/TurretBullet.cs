using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float speed;
    public int bulletDamage;
    public AudioManager audioManager;
    private GameObject destination;
    private bool isMobDead;
    
    // Start is called before the first frame update
    void Start()
    {
        isMobDead = destination.GetComponent<Mob>().isMobDead();
        audioManager = AudioManager.instance;
        audioManager.PlayProjectileSound();
    }

    // Update is called once per frame
    void Update()
    {
        //If target dies while a bullet is directed at it, make the bullet disappear
        try
        {
            if (isMobDead == true)
            {
                Destroy(gameObject);
                return;
            }
        }
        catch (MissingReferenceException ex)
        {
            destination = null;
            return;
        }

        Vector3 direction = destination.transform.position - transform.position;
        float distanceInThisFrame = speed * Time.deltaTime;
        
        //If we hit something
        if (direction.magnitude <= distanceInThisFrame)
        {
            DamageTarget();
            return;
        }
        
        transform.Translate(direction.normalized * distanceInThisFrame, Space.World);
    }

    public void LockOn(GameObject target)
    {
        destination = target;
    }

    private void DamageTarget()
    {
        destination.GetComponent<Mob>().ReceiveDamage(bulletDamage);
        Destroy(gameObject);
    }
}
