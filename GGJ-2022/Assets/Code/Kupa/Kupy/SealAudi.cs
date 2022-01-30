using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealAudi : MonoBehaviour
{
    public List<AudioClip> _damageClips;
    public AudioClip _eatClip;

    AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Damaged()
    {
        AudioClip clip = null;

        if (_damageClips.Count > 0)
            clip = _damageClips[Mathf.RoundToInt(Random.Range(0, _damageClips.Count - 1))];

        if (clip != null)
            _audio.PlayOneShot(clip);
    }
}
