using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundPlayType
{
    Immediate = 1,
    Slow = 2,
    Fast = 3,
}



public class BGMManager : MonoBehaviour
{

    public List<AudioClip> BGMList = new List<AudioClip>();
    private AudioSource _audioSource;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = BGMList.Find(x => x.name.Equals("Sound_Bgm_Village"));
    }

    public void OnPlay(string bgmName, SoundPlayType type = SoundPlayType.Immediate)
    {
        StopCoroutine("ChangeSound");
        StartCoroutine(ChangeSound(bgmName, type));
    }


    private IEnumerator ChangeSound(string bgmName, SoundPlayType type = SoundPlayType.Immediate)
    {
        if (type == SoundPlayType.Immediate)
        {
            _audioSource.clip = BGMList.Find(x => x.name.Equals(bgmName));
            _audioSource.Play();
            yield break;
        }

        float value = (type == SoundPlayType.Slow) ? 0.02f : 0.035f;

        while(_audioSource.volume > 0f)
        {
            _audioSource.volume -= value;
            yield return null;
        }


        _audioSource.clip = BGMList.Find(x => x.name.Equals(bgmName));
        _audioSource.Play();
        while (_audioSource.volume < 1f)
        {
            _audioSource.volume += value;
            yield return null;
        }
    }
}
