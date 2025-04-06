using System.Collections;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _shortLifeTime;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OilBullet") || collision.gameObject.CompareTag("Oil"))
        {
            if (this.gameObject != null)
            {
                StartCoroutine(QuickDie());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GrassWall"))
        {
            if (this.gameObject != null)
            {
                StartCoroutine(QuickDie());
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }

    IEnumerator QuickDie()
    {
        yield return new WaitForSeconds(_shortLifeTime);

        Destroy(gameObject);
    }

}
