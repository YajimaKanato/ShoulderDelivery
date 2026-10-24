using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class PlayerController : MonoBehaviour
    {
        PlayerActionBase[] _playerActionBases;
        Actions _actions;

        private void Awake()
        {
            _actions = new();
            _actions.Enable();

            _playerActionBases ??= GetComponents<PlayerActionBase>();
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.CacheAction(_actions);
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.OnUpdate();
        }

        private void FixedUpdate()
        {
            foreach (var playerActionBase in _playerActionBases)
                playerActionBase?.OnFixedUpdate();
        }
    }
}
