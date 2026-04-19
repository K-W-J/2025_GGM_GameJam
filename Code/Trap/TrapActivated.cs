using BIS.Core;
using BIS.Entities;
using BIS.Players;
using System.Collections;
using UnityEngine;

namespace KWJ
{
    public class TrapActivated : MonoBehaviour
    {
        [SerializeField] private LayerMask _isPlayer;
        [SerializeField] private int _dmaamge;
        [SerializeField] private GameEventChannelSO _hitChannelSO;

        private Player _player;

        private Animator _animator;
        private BoxCollider2D _collision;

        private int Trap = Animator.StringToHash("Trap");
        private bool _isStart = false;
        private bool _isStartz = false;

        public bool _isActive = false;

        private void Awake()
        {
            _collision = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isActive && !_isStartz && null != collision.gameObject.GetComponent<Player>())
            {
                StartCoroutine(StartTrap());
                _isStartz = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (null != collision.gameObject.GetComponent<Player>() && !_isStart)
            {
                _isStart = true;
                _player = collision.gameObject.GetComponent<Player>();
                StartCoroutine(StartTrapAnimation());
            }
        }

        private IEnumerator StartTrapAnimation()
        {
            _animator.SetBool(Trap, true);

            yield return new WaitForSeconds(1f);
            _isStart = false;

            _animator.SetBool(Trap, false);
        }

        private IEnumerator StartTrap()
        {
            HitEvent evt = new HitEvent();
            _hitChannelSO.RaiseEvent(evt);
            _player.GetCompo<EntityHealth>().ApplyDamage(_dmaamge);
            yield return new WaitForSeconds(1f);
            _isStartz = false;
        }
    }
}