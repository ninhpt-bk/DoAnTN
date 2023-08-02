using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource BG;
    [SerializeField] AudioClip bullet;
    [SerializeField] Button settingAudio;
    [SerializeField] Sprite[] hub;
    [SerializeField] Slider sldVolume;
    AudioSource bulletAS;
    float bgVolume = 1;
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    private static AudioController instance;
    void Awake()
    {
        instance = this;
    }
    public void PlaySound(int i = 2, float volume = 1f, bool isLoopback = false, bool repeat = false)
    {
        switch (i)
        {
           /* case 0:
                Play(run, ref runAS, volume, isLoopback);
                break;
            case 1:
                Play(hit, ref hitAS, volume, isLoopback);
                break;*/
            case 2:
                Play(bullet, ref bulletAS, volume*bgVolume, isLoopback, true);
                break;
            /*case 3:
                Play(item, ref itemAS, volume / 2, isLoopback);
                break;
            case 4:
                Play(end, ref endAS, volume, isLoopback);
                break;
            case 5:
                Play(eat, ref eatAS, volume / 2, isLoopback);
                break;
            case 6:
                Play(plane, ref planeAS, volume, isLoopback);
                break;
            case 7:
                Play(bom, ref bomAS, volume, isLoopback, true);
                break;*/
        }
    }

    void Play(AudioClip clip, ref AudioSource audioSource, float volume = 1f, bool isLoopback = false, bool repeat = false)
    {
        if (audioSource != null && audioSource.isPlaying && !repeat)
            return;
        audioSource = Instantiate(instance.prefab).GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    private void Start()
    {
        BG.volume = bgVolume;
        if(bgVolume > 0)
        {
            settingAudio.image.sprite = hub[0];
        }
        else
        {
            settingAudio.image.sprite = hub[1];
        }
        sldVolume.maxValue = 1;
        sldVolume.value = bgVolume;
    }
    public void UpdateVoLume()
    {
        bgVolume = sldVolume.value;
        if (bgVolume > 0)
        {
            settingAudio.image.sprite = hub[0];
        }
        else
        {
            settingAudio.image.sprite = hub[1];
        }
        BG.volume = bgVolume;
    }
    public void OnOfVoLume()
    {
        if (bgVolume > 0)
        {
            bgVolume = 0;
            settingAudio.image.sprite = hub[1];
        }
        else
        {
            bgVolume = 100;
            settingAudio.image.sprite = hub[0];
        }
        sldVolume.value = bgVolume;
        BG.volume = bgVolume;
    }
}
