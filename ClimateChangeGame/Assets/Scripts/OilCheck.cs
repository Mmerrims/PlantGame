using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OilCheck : MonoBehaviour
{
    [SerializeField] private List<GameObject> _oil;
    [SerializeField] private bool _activateWin;
    [SerializeField] private bool _disablePlayer;
    [SerializeField] private GameObject _blocker;
    [SerializeField] private string _oilTag;
    [SerializeField] private PlayerControls _playerControls;

    // Update is called once per frame
    private void Start()
    {
        _playerControls = FindObjectOfType<PlayerControls>();

        foreach (GameObject _oilTile in GameObject.FindGameObjectsWithTag(_oilTag))
        {

            _oil.Add(_oilTile);
        }
    }

    void Update()
    {
        


        _oil = _oil.Where(item => item != null).ToList();

        if (_oil.Count == 0)
        {
            print("Empty");
            if (!_activateWin)
            {
                _blocker.SetActive(false);
            }
            else
            {
                _blocker.SetActive(true);
            }

            if (_disablePlayer)
            {
                _playerControls.disableMovement();
            }
        }
    }
}
