using UnityEngine;
using System.Collections;

namespace MirkoZambito
{

    [System.Serializable]
    public class SoundClip
    {
        [SerializeField] private AudioClip audioClip = null;
        public AudioClip AudioClip { get { return audioClip; } }
    }


    public class SoundManager : MonoBehaviour
    { 
        [Header("Audio Source References")]
        [SerializeField] private AudioSource soundSource = null;
        [SerializeField] private AudioSource musicSource = null;

        [Header("Audio Clips")]
        public SoundClip button = null;
        public SoundClip duplicatingBall = null;
        public SoundClip lockerCount = null;
        public SoundClip breakTheLocker = null;
        public SoundClip finishedOneStage = null;
        public SoundClip completedLevel = null;
        public SoundClip levelFailed = null;
        public SoundClip tick = null;


        void Start()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKey.SOUND_PPK))
                PlayerPrefs.SetInt(PlayerPrefsKey.SOUND_PPK, 1);
            if (!PlayerPrefs.HasKey(PlayerPrefsKey.MUSIC_PPK))
                PlayerPrefs.SetInt(PlayerPrefsKey.MUSIC_PPK, 1);

            soundSource.mute = IsSoundOff();
            musicSource.mute = IsMusicOff();
        }


        /// <summary>
        /// Determine whether the music is on
        /// </summary>
        /// <returns></returns>
        public bool IsSoundOff()
        {
            return (PlayerPrefs.GetInt(PlayerPrefsKey.SOUND_PPK, 1) == 0);
        }

        /// <summary>
        /// Determine whether the sound is on
        /// </summary>
        /// <returns></returns>
        public bool IsMusicOff()
        {
            return (PlayerPrefs.GetInt(PlayerPrefsKey.MUSIC_PPK, 1) == 0);
        }


        /// <summary>
        /// Play one audio clip
        /// </summary>
        /// <param name="clip"></param>
        public void PlayOneSound(SoundClip clip)
        {
            soundSource.PlayOneShot(clip.AudioClip);
        }

        /// <summary>
        /// Plays the given sound clip as music clip (automatically loop).
        /// The music will volume up from 0 to 1 with given volumeUpTime
        /// </summary>
        /// <param name="clip">Music.</param>
        /// <param name="loop">If set to <c>true</c> loop.</param>
        public void PlayMusic(SoundClip clip, float volumeUpTime)
        {
            if (!IsMusicOff()) //Music is on
            {
                musicSource.clip = clip.AudioClip;
                musicSource.loop = true;
                musicSource.Play();
                StartCoroutine(CRVolumeUp(volumeUpTime));
            }
        }


        /// <summary>
        /// Pauses the music.
        /// </summary>
        public void PauseMusic()
        {
            musicSource.Pause();
        }

        /// <summary>
        /// Resumes the music.
        /// </summary>
        public void ResumeMusic()
        {
            musicSource.UnPause();
        }

        /// <summary>
        /// Stop music.
        /// </summary>
        public void StopMusic(float volumeDownTime)
        {
            musicSource.Stop();
            StartCoroutine(CRVolumeDown(volumeDownTime));
        }


        /// <summary>
        /// Toggles the mute status.
        /// </summary>
        public void ToggleSound()
        {
            if (IsSoundOff())
            {
                //Turn the sound on
                PlayerPrefs.SetInt(PlayerPrefsKey.SOUND_PPK, 1);
                soundSource.mute = false;
            }
            else
            {
                //Turn the sound off
                PlayerPrefs.SetInt(PlayerPrefsKey.SOUND_PPK, 0);
                soundSource.mute = true;
            }
        }

        /// <summary>
        /// Toggles the mute status.
        /// </summary>
        public void ToggleMusic()
        {
            if (IsMusicOff())
            {
                //Turn the music on
                PlayerPrefs.SetInt(PlayerPrefsKey.MUSIC_PPK, 1);
                musicSource.mute = false;
            }
            else
            {
                //Turn the music off
                PlayerPrefs.SetInt(PlayerPrefsKey.MUSIC_PPK, 0);
                musicSource.mute = true;
            }
        }



        private IEnumerator CRVolumeUp(float time)
        {
            float t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                float factor = t / time;
                musicSource.volume = Mathf.Lerp(0, 0.3f, factor);
                yield return null;
            }
        }

        private IEnumerator CRVolumeDown(float time)
        {
            float t = 0;
            float currentVolume = musicSource.volume;
            while (t < time)
            {
                t += Time.deltaTime;
                float factor = t / time;
                musicSource.volume = Mathf.Lerp(currentVolume, 0, factor);
                yield return null;
            }
        }
    }
}
