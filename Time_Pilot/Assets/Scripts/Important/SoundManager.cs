using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    AudioMixerGroup bgm, effect;

    public void Init(AudioMixerGroup bgm, AudioMixerGroup effect)
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; // bgm ������ ���� �ݺ� ���
        }

        this.bgm = bgm;
        this.effect = effect;
    }
    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ���� Dictionary ����
        _audioClips.Clear();
    }
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f, float volume = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm) // BGM ������� ���
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            audioSource.outputAudioMixerGroup = bgm;
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect ȿ���� ���
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.outputAudioMixerGroup = effect;
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }
    public void ChangePitch(float pitch)
    {
        AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
        audioSource.pitch = pitch;
    }
}

