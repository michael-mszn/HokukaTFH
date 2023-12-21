using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Attributes")]
    public string targetTag = "Enemy";
    public float rotationSpeed;
    public float fireRate;
    public float range;
    public string turretName;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public Transform rotatingPart;
    
    [Header("Handled by Control Flow")]
    public GameObject target;
    private List<GameObject> targetList = new List<GameObject>();
    private float fireCooldown;
    private bool hasDamageAmp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 0f;
        
        //Methods use resource intensive GetComponent<>
        //Checking for a target does not need to be done every frame which makes these functions more performant
        
            switch (turretName)
            {
                case "Crossbow Tower":
                    InvokeRepeating("UpdateTargetSingleHit", 0f, 0.25f);
                    break;
                case "Multihit Tower":
                    InvokeRepeating("UpdateTargetMultiHit", 0f, 0.25f);
                    break;
                case "Watch Tower":
                    InvokeRepeating("UpdateTargetSingleHit", 0f, 0.25f);
                    InvokeRepeating("SlowEnemies", 0f, 0.25f);
                    break;
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (turretName)
        {
            //Deliberately do not do any action if no target is found
            case "Crossbow Tower":
                if (target == null)
                {
                    return;
                }

                LockOntoTarget();
                if (fireCooldown <= 0f)
                {
                    FireProjectileAtTarget();
                    fireCooldown = 1 / fireRate;
                }
                break;
            
            case "Multihit Tower":
                if (targetList.Count == 0)
                {
                    return;
                }
                if (fireCooldown <= 0f)
                {
                    FireProjectileAtTargetMultiHit();
                    fireCooldown = 1 / fireRate;
                }
                break;
            
            case "Watch Tower":
                if (target == null)
                {
                    return;
                }

                LockOntoTarget(); 
                if (fireCooldown <= 0f)
                {
                    FireProjectileAtTarget();
                    fireCooldown = 1 / fireRate;
                }
                break;
        }

        fireCooldown -= Time.deltaTime;
    }
    
    //Prioritizes the closest target in the towers' radius
    private void UpdateTargetSingleHit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        //No enemy found is to be understood as "infinite distance"
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if ((distanceToEnemy < shortestDistance) && (enemy.GetComponent<Mob>().isMobDead() == false))
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy;
            }
        else
        {
            target = null;
        }
    }
    
    //Prioritizes the first three enemies that enter the shooting radius
    private void UpdateTargetMultiHit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if ((distanceToEnemy <= range) && !(targetList.Contains(enemy)) && (targetList.Count < 3) && (enemy.GetComponent<Mob>().isMobDead() == false))
            {
                targetList.Add(enemy);
            }
        }
        
        //Handles cases where a target was already killed by another turret
        if (targetList.Count != 0)
        {
            try
            {
                targetList.RemoveAll(target =>
                    (Vector3.Distance(transform.position, target.transform.position)) >= range);
            }
            catch (MissingReferenceException ex)
            {
                targetList.Clear();
            }
        }

    }

    private void LockOntoTarget()
    {
        //Get the direction vector to a target, then convert the vector's Quaternion rotation into euler rotation
        Vector3 directionToTarget = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget); 
        //Rotation is no longer "jumpy" (Interpolates from one rotation to another with defined speed)
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        //Rotate around y-axis 
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void FireProjectileAtTarget()
    {
        GameObject currentBullet = (GameObject) Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        TurretBullet bulletScript = currentBullet.GetComponent<TurretBullet>();

        if (bullet != null)
        {
            bulletScript.LockOn(target);
            if (hasDamageAmp == true)
            {
                bulletScript.bulletDamage *= 2;
            }
        }
    }
    
    private void FireProjectileAtTargetMultiHit()
    {
        foreach (GameObject target in targetList)
        {
            GameObject currentBullet = (GameObject) Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            TurretBullet bulletScript = currentBullet.GetComponent<TurretBullet>();
            
            if (bullet != null)
            {
                bulletScript.LockOn(target);
                if (hasDamageAmp == true)
                {
                    bulletScript.bulletDamage *= 2;
                }
            }
        }
    }

    //Slows all enemies in its radius
    private void SlowEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            Mob mob = enemy.GetComponent<Mob>();
            if (distanceToEnemy <= range && mob.getStatusCondition() != "frozen")
            {
                mob.Freeze();
            }
            else if (distanceToEnemy <= range && mob.getStatusCondition() == "frozen")
            {
               //do nothing
            }
            else
            {
                if (mob.getStatusCondition() == "frozen")
                {
                    mob.ThawOut();
                }
                
                else if (mob.getStatusCondition() == "thawing out")
                {
                    mob.setStatusCondition("none");
                }
                else
                {
                   //for future extensions
                }
            }
        }
    }

    public void SetHasDamageAmp(bool value)
    {
        hasDamageAmp = value;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
