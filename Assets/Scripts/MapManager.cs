using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapManager : Subject<bool>
{
    public Image blackOverlay;
    public float fadeDuration = 1.0f;

    public static MapManager Instance;
    public static bool UpdatingRoom { get; private set; }

    [SerializeField]
    private GameObject _player;
    [SerializeField] 
    private Camera _mainCamera;
    [SerializeField]
    private List<Room> Rooms;

    private Room _activeRoom;
    private int _currentRoom = 0;
    private float _timer;
    // eseni gazarde tu ginda ro mal-male ar sheicvalos adgili
    private float _maxTime = 100f;
    // esec!
    private float _minTime = 100f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _activeRoom = Rooms[_currentRoom];
        StartNewTimer();
    }

    private void StartNewTimer()
    {
        _timer = Random.Range(_minTime, _maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        TickTimer();
    }

    private void TickTimer()
    {
        if (UpdatingRoom)
        {
            return; 
        }
        
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            UpdatingRoom = true;

            StartCoroutine(UpdateRoom());
            NotifyObservers(false);
            _currentRoom = Random.Range(0, Rooms.Count);
            // dim screen
            
            // stop updating and receiving user input 
            
            // prepare map for start
            // stop current map
            // brighten screen
            // continue the timer
        } 
    }

    private IEnumerator UpdateRoom()
    {
        yield return StartCoroutine(DimScreen());
        
        // go to next map
        var nextRoom = Rooms[_currentRoom];
        _player.transform.position = nextRoom._spawnLocation.position;
        var pos = nextRoom.transform.position;
        var cameraTransform = _mainCamera.transform;
        cameraTransform.position = new Vector3(pos.x, pos.y, cameraTransform.position.z);
        
        yield return StartCoroutine(UndimScreen());
    }

    private IEnumerator DimScreen()
    {
        
        float elapsedTime = 0f;
        Color startColor = blackOverlay.color;
        Color targetColor = new Color(0f, 0f, 0f, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackOverlay.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        blackOverlay.color = targetColor;
    }
    private IEnumerator UndimScreen()
    {
        yield return new WaitForSeconds(2f);
        float elapsedTime = 0f;
        Color startColor = new Color(0f, 0f, 0f, 1f);
        Color targetColor = new Color(0f, 0f, 0f, 0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackOverlay.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        NotifyObservers(true);
        _timer = Random.Range(_minTime, _maxTime);
        UpdatingRoom = false;
        blackOverlay.color = targetColor;
    }
}
