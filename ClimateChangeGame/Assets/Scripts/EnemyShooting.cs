using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;


    private float timer;
    [SerializeField] private float shootDistance = 6;
    public bool IsTargetInRange { get; private set; } = false;
    public GameObject Player { get; private set; } = null;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, Player.transform.position);
        IsTargetInRange = distance < shootDistance;
        Debug.Log(distance);

        if(IsTargetInRange)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

        
    }

    void shoot()
    {
        GameObject bulletObj = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        EnemyBulletScript bulletScript = bulletObj.GetComponent<EnemyBulletScript>();
        bulletScript.shoot(Player);
    }
}
