using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    
    public AudioClip[] musicClips;
    public AudioClip[] winClips;
    public AudioClip[] loseClips;
    public AudioClip[] bonusClips;
    
    [Range(0,1)]
    public float musicVolume = 0.5f;
    
    [Range(0,1)]
    public float sfxVolume = 0.5f;

    private float lowPitch = 0.95f;
    private float highPitch = 1.05f;
    
    private List<string> m_musicNames = new List<string>();

    private void Start()
    {
        PlayRandomMusic();
    }

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f, bool loop = false)
    {
        if(m_musicNames.Contains(clip.name))
            return null;
        
        
        if (clip != null)
        {
            GameObject go = new GameObject("SoundFX" + clip.name);
            go.transform.position = position;
            
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            
            float randomPitch = Random.Range(lowPitch, highPitch);
            source.pitch = randomPitch;
            source.volume = volume;
            
            source.Play();
            source.loop = loop;
            m_musicNames.Add(clip.name);
            StartCoroutine(RemoveClipNameFromList(clip.name, clip.length));
            Destroy(go, clip.length);
            return source;
        }

        return null;
    }
    
    private IEnumerator RemoveClipNameFromList(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_musicNames.Remove(clipName);
    }

    public AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1f, bool loop = false)
    {
        if (clips is { Length: > 0 })
        {
            int randomIndex = Random.Range(0, clips.Length);
            
            if(clips[randomIndex] != null)
                return PlayClipAtPoint(clips[randomIndex], position, volume);
        }

        return null;
    }
    
    public void PlayRandomMusic()
    {
        PlayRandom(musicClips, Vector3.zero, musicVolume, true);
    }
    
    public void PlayRandomWin()
    {
        PlayRandom(winClips, Vector3.zero, sfxVolume);
    }
    
    public void PlayRandomLose()
    {
        PlayRandom(loseClips, Vector3.zero, sfxVolume);
    }
    
    public void PlayRandomBonus()
    {
        PlayRandom(bonusClips, Vector3.zero, sfxVolume);
    }
}
