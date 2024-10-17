using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }
        public static InputService InputService => Instance.inputService;
        
        [SerializeField] private InputService inputService;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
    }
}