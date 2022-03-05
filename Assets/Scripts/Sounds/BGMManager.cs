using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundPlayType
{
    Slow = 1,
    Fast = 2,
}

public class BGMManager : MonoBehaviour
{

    public AudioClip[] arrayClip;
    private AudioSource _audioSource;
    private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        foreach (var clip in arrayClip)
        {
            _clips.Add(clip.name, clip);
        }

        _audioSource.clip = _clips["Sound_Bgm_Village"];
        OnPlay(Idle.GetInstance(), SoundPlayType.Slow);
    }

    public void OnPlay(IState state, SoundPlayType type)
    {
        StartCoroutine(ChangeSound(state, type));
    }

    public void OnPlay(bool isSujiDied = true)
    {
        if (isSujiDied)
        {
            _audioSource.clip = _clips["Sound_Bgm_Die"];
            _audioSource.Play();
        }
    }

    private IEnumerator ChangeSound(IState state, SoundPlayType type)
    {

        float value = (type == SoundPlayType.Slow) ? 0.02f : 0.035f;

        while (_audioSource.volume > 0f)
        {
            _audioSource.volume -= value;
            yield return null;
        }

        /// 더 좋은 방법이 있을텐데... 생각이 안 났다.
        if (state == Idle.GetInstance())
            _audioSource.clip = _clips["Sound_Bgm_Village"];
        else if (state == FeelStrange.GetInstance())
            _audioSource.clip = _clips["Sound_Bgm_EnemyFeel"];
        else if (state == ChaseState.GetInstance())
            _audioSource.clip = _clips["Sound_Bgm_Chase"];


        _audioSource.Play();
        while (_audioSource.volume < 1f)
        {
            _audioSource.volume += value;
            yield return null;
        }
    }

}
