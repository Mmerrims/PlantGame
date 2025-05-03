/*****************************************************************************
// File Name :         Oil Check.cs
// Author :            Amber C. Cardamone
// Creation Date :     April 29th, 2025
//
// Brief Description : Adds all the objects with the same tag together, and once all those gameobjects are gone, activates/disables an object.
*****************************************************************************/

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

    /// <summary>
    /// Finds every gameobject with the tag defined by the _oilTag string.
    /// </summary>
    private void Start()
    {
        _playerControls = FindObjectOfType<PlayerControls>();

        foreach (GameObject _oilTile in GameObject.FindGameObjectsWithTag(_oilTag))
        {

            _oil.Add(_oilTile);
        }
    }

    /// <summary>
    /// Checks the remaining oil count, if none are left, enables/disables an object.
    /// </summary>
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
