using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;
    private AudioSource _audioSource;

    public bool IsEnable { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        IsEnable = _audioSource.volume == 1f;

        if (_musicClip != null)
        {
            _audioSource.clip = _musicClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    public void Enable()
    {
        IsEnable = true;
        _audioSource.volume = 1.0f;
    }

    public void Disable()
    {
        IsEnable = false;
        _audioSource.volume = 0f;
    }
}