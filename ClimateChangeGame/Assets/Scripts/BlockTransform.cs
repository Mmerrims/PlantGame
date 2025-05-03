/*****************************************************************************
// File Name :         BlockTransform.cs
// Author :            Amber C. Cardamone
// Creation Date :     April 29th, 2025
//
// Brief Description : Changes the blocks based off of what objects hit them.
*****************************************************************************/

using UnityEngine;

public class BlockTransform : MonoBehaviour
{
    [SerializeField] private bool _oil;
    [SerializeField] private bool _ground;
    [SerializeField] private bool _grass;
    [SerializeField] private GameObject _grassObject;
    [SerializeField] private GameObject _oilObject;
    [SerializeField] private float _health;


    /// <summary>
    /// Checks if this gameobject collides with a bullet, if its grass, it takes damage until it transforms into another block, changing its sprite as well. If it collides with oil, it immediately changes to an oil block.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (_oil == true && collision.gameObject.CompareTag("Grass") || _ground == true && collision.gameObject.CompareTag("Grass"))
        {
            _health -= 1;
            if (_health == 3)
            {
                if (_oil)
                {
                    gameObject.GetComponent<Animator>().Play("Oil2");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Ground2");
                }
            }
            else if (_health == 2)
            {
                if (_oil)
                {
                    gameObject.GetComponent<Animator>().Play("Oil3");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Ground3");
                }
            }
            else if (_health == 1)
            {
                if (_oil)
                {
                    gameObject.GetComponent<Animator>().Play("Oil4");
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("Ground4");
                }
            }
            Destroy(collision.gameObject);
            if (_health <= 0)
            {
                Instantiate(_grassObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else if (_oil == true && collision.gameObject.CompareTag("BigGrass") || _ground == true && collision.gameObject.CompareTag("BigGrass"))
        {
            _health -= 5;
            if (_health <= 0)
            {
                Instantiate(_grassObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else if (_grass == true && collision.gameObject.CompareTag("OilBullet") || _ground == true && collision.gameObject.CompareTag("OilBullet"))
        {
            Instantiate(_oilObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
