using System;
using UnityEngine;

namespace Player
{
    public class PlayerSizeHandler : MonoBehaviour
    {
        [SerializeField] private float minSize = 0.2f;
        [SerializeField] private float shrinkAmountSpeed = 0.5f;
        [SerializeField] private float startSize = 1f;

        private float _chargeTime;
        private float _currentSize;

        public float CurrentSize => _currentSize;

        public event Action<float> OnSizeChange;
        public event Action<float> OnChargeChange;
        public event Action OnSizeCompleted;

        public void Initialize()
        {
            _currentSize = startSize;
        }

        public void ResetCharging()
        {
            _chargeTime = 0f;
        }

        public void Shrink(float deltaTime)
        {
            float shrinkAmount = deltaTime * shrinkAmountSpeed;
            if (_currentSize > minSize)
            {
                _currentSize -= shrinkAmount;
                transform.localScale = Vector3.one * _currentSize;

                _chargeTime += shrinkAmount;

                OnSizeChange?.Invoke(_currentSize);
                OnChargeChange?.Invoke(_chargeTime);
            }
            else
            {
                OnSizeCompleted?.Invoke();
            }
        }
    }
}