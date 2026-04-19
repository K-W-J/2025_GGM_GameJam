using GMS;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KWJ
{
    public class Portal : MonoBehaviour, IInteraction
    {
        [SerializeField] private LayerMask _isPlayer;

        [SerializeField] private string _shopMap;
        [SerializeField] private string _boxMap;

        [SerializeField] private bool _twoPortals;

        [SerializeField] private string _goBack;

        private ParticleSystem _particleSystem;
        private int Rnadom;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            Rnadom = Random.Range(1, 3);
        }

        private void Start()
        {

            if (Rnadom == 1)
                _particleSystem.startColor = Color.blue;

            if (Rnadom == 2)
                _particleSystem.startColor = Color.yellow;

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isPlayer == (_isPlayer | (1 << collision.gameObject.layer)))
            {
                
            }
        }

        private void ChangeScene()
        {
            if (_twoPortals && _shopMap != null && _boxMap != null)
            {

                if (Rnadom == 1)
                    SceneManager.LoadScene(_shopMap);

                if (Rnadom == 2)
                    SceneManager.LoadScene(_boxMap);
            }
            else
            {
                if(_goBack != null)
                    SceneManager.LoadScene(_goBack);

            }
        }

        public void Interaction()
        {
            ChangeScene();
        }
    }
}
