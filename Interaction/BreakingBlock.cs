using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace KWJ
{
    public class BreakingBlock : MonoBehaviour
    {
        [SerializeField] private int Health = 1;

        [SerializeField] private LayerMask _isBullet;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private ParticleSystem _particleSystem;

        private bool isStart = false;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isBullet == (_isBullet | (1 << collision.gameObject.layer)))
            {
                Health--;

                if (Health == 0 && !isStart)
                {
                    isStart = true;
                    _spriteRenderer.enabled = false;
                    _collider.enabled = false;
                    StartCoroutine(StartTrapAnimation());
                }
                else
                {
                    _particleSystem.Play();
                    gameObject.transform.DOShakePosition(0.5f, 0.05f, 10);
                }
            }
        }
     
        private IEnumerator StartTrapAnimation()
        {
            _particleSystem.Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}
