using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _shortLifeTime;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OilBullet") || collision.gameObject.CompareTag("Oil") || collision.gameObject.layer == 11)
        {
            GoToDie();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GrassWall"))
        {
            GoToDie();
        }
    }

    private void GoToDie()
    {
        if (this.gameObject != null && this.gameObject.activeSelf)
        {
            StartCoroutine(QuickDie());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifeTime);

        _anim.Play("CellBlockDestroy");
        Invoke("DestroyWall", 0.1f);
    }

    IEnumerator QuickDie()
    {
        yield return new WaitForSeconds(_shortLifeTime);

        _anim.Play("CellBlockDestroy");
        Invoke("DestroyWall", 0.1f);
    }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
