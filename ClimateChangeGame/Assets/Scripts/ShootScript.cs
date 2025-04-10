using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [SerializeField] private Transform _gun;
    [SerializeField] public GameObject _bullet;
    [SerializeField] public GameObject _altBullet;
    [SerializeField] public GameObject _player;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] public float _bulletSpeed;
    [SerializeField] public float _altBulletSpeed;
    public PlayerInput MPI;
    private InputAction shoot;
    private InputAction shootAlt;

    public bool CanShoot;
    public bool AltCanShoot;
    [SerializeField] private float _maxShotCooldown;
    [SerializeField] private float _shotCooldown;

    [SerializeField] private float _altMaxShotCooldown;
    [SerializeField] private float _altShotCooldown;

    [SerializeField] private int _maxShots;
    [SerializeField] private int _currentShots;
    [SerializeField] private float _maxReloadCooldown;
    [SerializeField] private float _reloadCooldown;

    [SerializeField] private int _altMaxShots;
    [SerializeField] private int _altCurrentShots;
    [SerializeField] private float _altMaxReloadCooldown;
    [SerializeField] private float _altReloadCooldown;

    [SerializeField] private bool _alt;

    [SerializeField] private PlayerGunAnimations _pGunAnims;

    // [SerializeField] private GameManager _gameManager;

    private bool shooting;
    private bool shootLock;
    public bool EndShooting;

    private Vector2 direction;
    public int CurrentShots { get => _currentShots; set => _currentShots = value; }

    //  [SerializeField] private TeleportFX _tFX;
    // Start is called before the first frame update

    void Start()
    {
        CanShoot = true;
        //MPI = GetComponent<PlayerInput>();

        //Grabs all the player's inputs
        shoot = MPI.currentActionMap.FindAction("Shoot");
        shootAlt = MPI.currentActionMap.FindAction("ShootAlt");
        MPI.currentActionMap.Enable();
        shoot.started += ShootStart;
        shoot.canceled += ShootCancel;
        shootAlt.started += ShootAltStart;
        shootAlt.canceled += ShootAltCancel;
    }

    public void OnDestroy()
    {
        //Remove control when OnDestroy activates
        if (shoot != null)
        {
            shoot.started -= ShootStart;
            shoot.canceled -= ShootCancel;
            shootAlt.started -= ShootAltStart;
            shootAlt.canceled -= ShootAltCancel;
        }
    }

    private void ShootStart(InputAction.CallbackContext obj)
    {
        shootLock = false;
        Invoke("PerformShoot", 0.15f);
        _pGunAnims.StartShoot();
    }

    public void PerformShoot()
    {
        if (!shootLock)
        {
            shooting = true;
            _alt = false;
        }
    }

    private void ShootCancel(InputAction.CallbackContext obj)
    {
        shootLock = true;
        shooting = false;
        _pGunAnims.EndShoot();
    }

    private void ShootAltStart(InputAction.CallbackContext obj)
    {
        shootLock = false;
        shooting = true;
        _alt = true;
    }
    private void ShootAltCancel(InputAction.CallbackContext obj)
    {
        shooting = false;
    }

    void ShootBullet()
    {
        if (!_alt)
        {
            GameObject BulletInstance = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(BulletInstance.transform.right * _bulletSpeed);
            _currentShots -= 1;
            //audioManager.BallThrow();
        }
    }

    void AltShootBullet()
    {
        if (_alt)
        {
            GameObject BulletInstance = Instantiate(_altBullet, _bulletSpawn.position, _bulletSpawn.rotation);
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(BulletInstance.transform.right * _altBulletSpeed);
            _altCurrentShots -= 1;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (shooting && EndShooting == false)
        {
            if (CanShoot && _currentShots > 0)
            {
                CanShoot = false;
                ShootBullet();

                print("Shot bullet");
            }
        }

        if (shooting && EndShooting == false)
        {
            if (AltCanShoot && _altCurrentShots > 0)
            {
                AltCanShoot = false;
                AltShootBullet();

                print("Shot bullet");
            }
        }


        if (CanShoot == false)
        {
            _shotCooldown -= Time.deltaTime;
            if (_shotCooldown <= 0)
            {
                CanShoot = true;
                _shotCooldown = _maxShotCooldown;
                // audioManager.BallRefill();
            }
        }

        if (AltCanShoot == false)
        {
            _altShotCooldown -= Time.deltaTime;
            if (_altShotCooldown <= 0)
            {
                AltCanShoot = true;
                _altShotCooldown = _altMaxShotCooldown;
                // audioManager.BallRefill();
            }
        }

        if (_currentShots < _maxShots)
        {
            _reloadCooldown -= Time.deltaTime;
            if (_reloadCooldown <= 0)
            {
                _reloadCooldown = _maxReloadCooldown;
                _currentShots += 1;

                //_gameManager.UpdateText();
            }
        }

        if (_altCurrentShots < _altMaxShots)
        {
            _altReloadCooldown -= Time.deltaTime;
            if (_altReloadCooldown <= 0)
            {
                _altReloadCooldown = _altMaxReloadCooldown;
                _altCurrentShots += 1;

                //_gameManager.UpdateText();
            }
        }



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)_gun.position;
        FaceMouse();
        if (_player != null)
        {
            if (mousePos.x < _player.transform.position.x)
            {
                _gun.transform.localScale = new Vector2(1, -1);
            }
            else if (mousePos.x > _player.transform.position.x)
            {
                _gun.transform.localScale = new Vector2(1, 1);
            }
        }
    }

    void FaceMouse()
    {
        _gun.transform.right = direction;
    }
}
