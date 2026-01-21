using UnityEngine;

public enum SoundType
{
    CUTLASS_CLASH,
    CUTLASS_SPIN,
    KATANA_CLASH,
    CHARACTER_DEAD,
    AGENT_TELEPORT,
    TRANSACTION,
    SPECIAL_UPGRADE,
    VICTORY,
    GAME_OVER
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
