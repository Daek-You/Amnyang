using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{

    public AudioSource _audioSource;
    public AudioClip[] walkSoundClips;
    public bool isSheInside = false;


    public void FootOutsideSound()
    {
        _audioSource.clip = isSheInside ? walkSoundClips[0] : walkSoundClips[1];
        _audioSource.volume = 0.55f;
        _audioSource.Play();
    }


}
