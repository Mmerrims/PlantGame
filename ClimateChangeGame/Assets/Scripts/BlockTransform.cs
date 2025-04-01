using UnityEngine;

public class BlockTransform : MonoBehaviour
{
    [SerializeField] private bool _oil;
    [SerializeField] private bool _ground;
    [SerializeField] private bool _grass;
    [SerializeField] private GameObject _grassObject;
    [SerializeField] private GameObject _oilObject;
    [SerializeField] private float _health;



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (_oil == true && collision.gameObject.CompareTag("Grass") || _ground == true && collision.gameObject.CompareTag("Grass"))
        {
            _health -= 1;
            if (_health <= 0)
            {
                Instantiate(_grassObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                Destroy(this.gameObject);
            }
        } 
        else if (_grass == true && collision.gameObject.CompareTag("Oil") || _ground == true && collision.gameObject.CompareTag("Oil"))
        {
            Instantiate(_oilObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
