using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;
    private static BackGrMusic instance;

    void Awake()
    {
        // Singleton pattern để đảm bảo chỉ có một instance của background music
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Lấy component AudioSource
        audioSource = GetComponent<AudioSource>();

        // Nếu chưa có AudioSource, thêm mới vào
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Thiết lập các thuộc tính cho AudioSource
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;       // Bật chế độ loop
        audioSource.playOnAwake = true; // Tự động phát khi game bắt đầu
        audioSource.volume = 0.2f;      // Âm lượng mặc định (0.0f đến 1.0f)

        // Bắt đầu phát nhạc
        PlayMusic();
    }

    void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Phương thức để điều chỉnh âm lượng (0.0f đến 1.0f)
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }
}