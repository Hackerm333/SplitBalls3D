using UnityEngine;

namespace MirkoZambito
{
    public class ServicesManager : MonoBehaviour
    {
        public static ServicesManager Instance { private set; get; }

        [SerializeField] private SoundManager soundManager = null;

        public SoundManager SoundManager { get { return soundManager; } }

        void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

