using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Enemy[] _enemies;

    public static Room ActiveRoom;

    public GameObject DoorU;
    public GameObject DoorR;
    public GameObject DoorD;
    public GameObject DoorL;

    private bool _isCompleted = false;

    private void Awake()
    {
        if(DoorD != null)
            DoorD.SetActive(true);

        if(DoorU != null)
            DoorU.SetActive(true);

        if(DoorR != null)
            DoorR.SetActive(true);

        if(DoorL != null)
            DoorL.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            ActiveRoom = this;

            if (!_isCompleted)
            {
                SpawnEnemies();
            }
        }
    }

    private void SpawnEnemies()
    {
        var enemyClone = Instantiate(_enemies[Random.Range(0, _enemies.Length)]);
        enemyClone.transform.position = transform.position + new Vector3(3, 1, 3);

        _isCompleted = true;
    }
}
