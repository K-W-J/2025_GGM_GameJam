using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using BIS.Utility;
using BIS.Manager;
using BIS.Entities;

namespace KWJ
{
    public class CreateRandomMap : MonoBehaviour
    {
        static private int _mapCount = 0;
        static private int _mapHardCount = 0;

        [SerializeField] private List<GameObject> _mapPrefab = new List<GameObject>();

        [SerializeField] private LayerMask _isPlayer;

        private int _maxMapCount = 10;
        private bool _isStart = false;

        private BoxCollider2D _collider;

        private Transform _referenceValue;
        private Transform _player;

        private GameObject _grid;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _grid = GameObject.Find("CreateMap");
        }

        private void Start()
        {
            _referenceValue = transform;
            _player = transform;
        }

        private void Update()
        {
            if (50 < Mathf.Abs(_player.position.y) - Mathf.Abs(transform.position.y) && _player != null)
            {
                Destroy(gameObject);

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isPlayer == (_isPlayer | (1 << collision.gameObject.layer)) && !_isStart)
            {
                _isStart = true;

                _mapCount++;

                _player = collision.gameObject.transform;

                int _createRandom = Random.Range(0, _mapPrefab.Count - 1);

                GameObject save = Instantiate(_mapPrefab[_mapCount == _maxMapCount ? _mapPrefab.Count - 1 : _createRandom], new Vector2(_referenceValue.position.x,
                                _referenceValue.position.y - 27),
                                _referenceValue.rotation);

                if (_mapCount == _maxMapCount)
                {
                    _mapHardCount++;
                    _mapCount = 0;
                }

                save.transform.parent = _grid.transform;

                _referenceValue = save.transform;

                EnemySpawn(save);

                _collider.enabled = false;
            }
        }

        private void EnemySpawn(GameObject go)
        {
            if (go.name == "PotoalPart(Clone)")
                return;
            if (go != null)
            {
                Transform[] _enemySpawners;
                _enemySpawners = Util.FindChild<Transform>(go, "EnemySpawn ", true).GetComponentsInChildren<Transform>();

                for (int i = 0; i < _enemySpawners.Length; ++i)
                {

                    int randNum = Random.Range(0, 100);

                    if (randNum > 90 - _mapHardCount * 10)
                    {
                        GameObject _enemySpawners2 = Managers.Enemy.RandomEnemySpawn(_enemySpawners[i].position, Quaternion.identity);
                        _enemySpawners2.GetComponentInChildren<EntityHealth>().currentHealth += _mapHardCount * 2;
                        print(_enemySpawners2.GetComponentInChildren<EntityHealth>().currentHealth);
                    }
                }
            }
        }
    }
}
