using System.Collections;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        StartCoroutine(Die());
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("OilBullet") || collision.gameObject.CompareTag("Oil"))
    //    {
    //        if (this.gameObject != null)
    //        {
    //            StartCoroutine(Die());
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("GrassWall"))
    //    {
    //        if (this.gameObject != null)
    //        {
    //            StartCoroutine(Die());
    //        }
    //    }
    //}

    IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
