using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{

    public AudioSource _audioSource;
    public List<AudioClip> effectSoundList = new List<AudioClip> ();
    public bool isSheInside = false;


    public void OnPlayFootSound()
    {
        _audioSource.clip = isSheInside ? effectSoundList.Find(x => x.name.Equals("Sound_Eff_FootInside")) :
                                          effectSoundList.Find(x => x.name.Equals("Sound_Eff_FootOutside"));   
        _audioSource.Play();
    }
}
