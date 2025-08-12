using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip collectSound;
    public AudioClip hitSound;
    public AudioClip doorOpenSound;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource soundEffectsSourcePlayer; //hit, jump, land, collect, win
    [Header("other")]
    [SerializeField] playerFollow playerFollow;
    [SerializeField] PlayerDuplication playerDuplication;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        musicSource = soundEffectsSourcePlayer = playerDuplication.spawnedPlayers[playerFollow.currenPlayerIndex].GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (soundEffectsSourcePlayer && clip)
        {
            soundEffectsSourcePlayer.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound Effects Source Player is not assigned or clip is null!");
        }
    }


}
