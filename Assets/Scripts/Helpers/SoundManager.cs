#if UNITY_EDITOR
#define EDIT_MODE
//#define SHOW_DEBUG
#endif

using UnityEngine;
using System.Collections.Generic;

public enum SoundType
{
	Replace,
	New,
	Only,
	Loop
}

public enum SoundID
{
	// Background musics
	MainMenu,
	MainGame,

	// Sound effects
	Start,
	Countdown,
	TapTrat,
	TapTrung,
	Perfect,
	CheGieu,
	VeDich,
    ReoHo,
    ButtonTap,

	CaChua1,
	CaChua2,
	CaChua3,

	VapTe1,
	VapTe2,
	VapTe3,

    // 100m hurdles
    NhayRao1,
    NhayRao2,
    NhayRao3,

    // 1000m
    Nitro_Start,
    Nitro_Loop,
    Nitro_End,

    // shooting
    Pull,
    Round_1,
    Round_2,
    Round_3,
    Shooting,
    Skeet_Break,
	Cheering,
	Lare,
	SkipIntro,
    Skeet_Throw,
	GunReload,
	StartList,
	PhaoHoa,

    Count
}

public class SoundManager : MonoBehaviour
{
	#region Singleton
	
	// Singleton
	private static SoundManager instance;
	
	// Instance
	public static SoundManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<SoundManager>();

				if (instance == null)
				{
					instance = new GameObject("Sound Manager").AddComponent<SoundManager>();
				}

				DontDestroyOnLoad(instance.gameObject);
			}
			
			return instance;
		}
	}
	
	private SoundManager() {}
	
	#endregion

	[Header("Background Musics")]

	/// <summary>
	/// The main menu.
	/// </summary>
	public AudioSource mainMenu;

	/// <summary>
	/// The main game.
	/// </summary>
	public AudioSource mainGame;

	[Header("Sound Effects")]

	public AudioSource cheGieu;
	public AudioSource countdown;
	public AudioSource perfect;
	public AudioSource start;
	public AudioSource tapTrat;
	public AudioSource tapTrung;
	public AudioSource reoHo;
	public AudioSource veDich;
	public AudioSource buttonTap;

	public AudioSource caChua1;
	public AudioSource caChua2;
	public AudioSource caChua3;

	public AudioSource vapTe1;
	public AudioSource vapTe2;
	public AudioSource vapTe3;

    public AudioSource nhayRao1;
    public AudioSource nhayRao2;
    public AudioSource nhayRao3;

    public AudioSource nitroStart;
    public AudioSource nitroLoop;
    public AudioSource nitroEnd;

    public AudioSource pull;
    public AudioSource round1;
    public AudioSource round2;
    public AudioSource round3;
    public AudioSource shooting;
    public AudioSource skeetBreak;
	public AudioSource cheering;
	public AudioSource lare;
	public AudioSource skipIntro;
    public AudioSource skeetThrow;
    public AudioSource gunReload;
	public AudioSource startList;
	public AudioSource phaoHoa;

    [Header("Volume")]

	/// <summary>
	/// The music volume.
	/// </summary>
	[Range(0,1)]
	public float musicVolume = 1f;
	
	/// <summary>
	/// The sound volume.
	/// </summary>
	[Range(0,1)]
	public float soundVolume = 1f;

	/// <summary>
	/// The lookup table for background musics.
	/// </summary>
	private Dictionary<SoundID, AudioSource> musicLookup;

	/// <summary>
	/// The lookup table for sound effects.
	/// </summary>
	private Dictionary<SoundID, AudioSource> soundLookup;

#if EDIT_MODE
	/// <summary>
	/// The lookup table for prefab background musics.
	/// </summary>
	private Dictionary<SoundID, AudioSource> prefabMusicLookup;
	
	/// <summary>
	/// The lookup table for prefab sound effects.
	/// </summary>
	private Dictionary<SoundID, AudioSource> prefabSoundLookup;
#endif

	/// <summary>
	/// True if enable to play background music.
	/// </summary>
	private bool isMusicEnabled = true;

	/// <summary>
	/// True if enable to play sound effect.
	/// </summary>
	private bool isSoundEnabled = true;

	// The current audio source to play music
	private AudioSource musicSource;

#if SHOW_DEBUG
	private static readonly float LabelWidth   = 70.0f;
	private static readonly float LabelHeight  = 50.0f;
	private static readonly float ButtonWidth  = 100.0f;
	private static readonly float ButtonHeight = 50.0f;

	private static readonly float Padding = 20.0f;
	private static readonly float HGap = 10.0f;
	private static readonly float VGap = 10.0f;

	void OnGUI()
	{
		float x = Padding;
		float y = Padding;

		// Music
		bool musicEnabled = GUI.Toggle(new Rect(x, y, ButtonWidth, ButtonHeight), isMusicEnabled, isMusicEnabled ? "Music On" : "Music Off");

		if (musicEnabled != isMusicEnabled)
		{
			SoundManager.Instance.MusicEnabled = musicEnabled;
		}

		x += ButtonWidth + HGap;
		
		// Sound
		bool soundEnabled = GUI.Toggle(new Rect(x, y, ButtonWidth, ButtonHeight), isSoundEnabled, isSoundEnabled ? "Sound On" : "Sound Off");
		
		if (soundEnabled != isSoundEnabled)
		{
			SoundManager.Instance.SoundEnabled = soundEnabled;
		}

		x = Padding;
		y += ButtonHeight + VGap;
		
		// Stop music
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Stop music"))
		{
			SoundManager.Instance.StopMusic();
		}
		
		x += ButtonWidth + HGap;

		// Stop sound
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Stop sounds"))
		{
			SoundManager.Instance.StopAllSounds();
		}

		x = Padding;
		y += ButtonHeight + VGap;
		
		// Main Game
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Main Game"))
		{
			SoundManager.Instance.PlayMusic(SoundID.MainGame);
		}
		
		x += ButtonWidth + HGap;
		
		// Main Menu
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Main Menu"))
		{
			SoundManager.Instance.PlayMusic(SoundID.MainMenu);
		}

		x = Padding;
		y += ButtonHeight + VGap;

		// Che gieu
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Che Gieu"))
		{
			SoundManager.Instance.PlaySound(SoundID.CheGieu);
		}
		
		y += ButtonHeight + VGap;
		
		// Reo ho
		if (GUI.Button(new Rect(x, y, ButtonWidth, ButtonHeight), "Reo Ho"))
		{
			SoundManager.Instance.PlaySound(SoundID.ReoHo, SoundType.New);
			SoundManager.Instance.PlaySound(SoundID.ReoHo, SoundType.New, 1.0f);
			SoundManager.Instance.PlaySound(SoundID.ReoHo, SoundType.New, 2.0f);
		}
		
		y += ButtonHeight + VGap;
	}
#endif

	void Awake()
	{
		// Singleton
		if (instance == null)
		{
			instance = this;

			DontDestroyOnLoad(this.gameObject);
		}
		else if (instance != this)
		{
			Destroy(this.gameObject);
		}

		// Create loopkup table for background musics
		musicLookup = new Dictionary<SoundID, AudioSource>();
		
		// Create loopkup table for sound effects
		soundLookup = new Dictionary<SoundID, AudioSource>();

#if EDIT_MODE
		// Create loopkup table for prefab background musics
		prefabMusicLookup = new Dictionary<SoundID, AudioSource>();
		
		// Create loopkup table for prefab sound effects
		prefabSoundLookup = new Dictionary<SoundID, AudioSource>();
#endif

		// Add background musics
		AddMusic(SoundID.MainMenu, mainMenu);
		AddMusic(SoundID.MainGame, mainGame);

		// Add sound effects
		AddSound(SoundID.CheGieu, cheGieu);
		AddSound(SoundID.Countdown, countdown);
        AddSound(SoundID.Perfect, perfect);
        AddSound(SoundID.Start, start);
        AddSound(SoundID.TapTrat, tapTrat);
        AddSound(SoundID.TapTrung, tapTrung);
        AddSound(SoundID.ReoHo, reoHo);
        AddSound(SoundID.VeDich, veDich);
        AddSound(SoundID.ButtonTap, buttonTap);

		AddSound(SoundID.CaChua1, caChua1);
		AddSound(SoundID.CaChua2, caChua2);
		AddSound(SoundID.CaChua3, caChua3);

		AddSound(SoundID.VapTe1, vapTe1);
		AddSound(SoundID.VapTe2, vapTe2);
		AddSound(SoundID.VapTe3, vapTe3);

        AddSound(SoundID.NhayRao1, nhayRao1);
        AddSound(SoundID.NhayRao2, nhayRao2);
        AddSound(SoundID.NhayRao3, nhayRao3);

        AddSound(SoundID.Nitro_Start, nitroStart);
        AddSound(SoundID.Nitro_Loop, nitroLoop);
        AddSound(SoundID.Nitro_End, nitroEnd);

        AddSound(SoundID.Pull, pull);
        AddSound(SoundID.Round_1, round1);
        AddSound(SoundID.Round_2, round2);
        AddSound(SoundID.Round_3, round3);
        AddSound(SoundID.Shooting, shooting);
        AddSound(SoundID.Skeet_Break, skeetBreak);
		AddSound(SoundID.Cheering, cheering);
		AddSound(SoundID.Lare, lare);
		AddSound(SoundID.SkipIntro, skipIntro);
        AddSound(SoundID.Skeet_Throw, skeetThrow);
        AddSound(SoundID.GunReload, gunReload);
		AddSound(SoundID.StartList, startList);
		AddSound(SoundID.PhaoHoa, phaoHoa);
    }

//	void LateUpdate()
//	{
//		foreach (AudioSource sound in soundLookup.Values)
//		{
//			// Check if sound enabled
//			if (sound.enabled)
//			{
//				// Check if sound done
//				if (!sound.isPlaying)
//				{
//					// Disable sound
//					sound.enabled = false;
//				}
//			}
//		}
//	}

	// Change music volume
	public float MusicVolume
	{
		get
		{
			return musicVolume;
		}

		set
		{
			musicVolume = Mathf.Clamp01(value);

#if EDIT_MODE
			foreach (SoundID soundID in musicLookup.Keys)
			{
				musicLookup[soundID].volume = prefabMusicLookup[soundID].volume * musicVolume;
			}
#else
			foreach (AudioSource audioSource in musicLookup.Values)
			{
				audioSource.volume = musicVolume;
			}
#endif
		}
	}

	// Change sound volume
	public float SoundVolume
	{
		get
		{
			return soundVolume;
		}
		
		set
		{
			soundVolume = Mathf.Clamp01(value);

#if EDIT_MODE
			foreach (SoundID soundID in soundLookup.Keys)
			{
				soundLookup[soundID].volume = prefabSoundLookup[soundID].volume * soundVolume;
			}
#else
			foreach (AudioSource audioSource in soundLookup.Values)
			{
				audioSource.volume = soundVolume;
			}
#endif
		}
	}

	// Enable/Disable background music
	public bool MusicEnabled
	{
		get
		{
			return isMusicEnabled;
		}

		set
		{
			if (isMusicEnabled != value)
			{
				isMusicEnabled = value;

				if (isMusicEnabled)
				{
#if EDIT_MODE
					foreach (SoundID soundID in musicLookup.Keys)
					{
						musicLookup[soundID].volume = prefabMusicLookup[soundID].volume * musicVolume;
					}
#else
					foreach (AudioSource audioSource in musicLookup.Values)
					{
						audioSource.volume = musicVolume;
					}
#endif
				}
				else
				{
					foreach (AudioSource audioSource in musicLookup.Values)
					{
						audioSource.volume = 0;
					}
				}
			}
		}
	}

	// Enable/Disable sound effect
	public bool SoundEnabled
	{
		get
		{
			return isSoundEnabled;
		}

		set
		{
			if (isSoundEnabled != value)
			{
				isSoundEnabled = value;

				if (!isSoundEnabled)
				{
					foreach (AudioSource audioSource in soundLookup.Values)
					{
						audioSource.enabled = false;
					}
				}
			}
		}
	}

	public bool PlaySound(SoundID soundID, SoundType type = SoundType.Replace, float delay = 0f)
	{
		if (!isSoundEnabled) return false;

		// Get audio source
		AudioSource audioSource = soundLookup[soundID];

		if (audioSource != null)
		{
#if EDIT_MODE
			audioSource.Copy(prefabSoundLookup[soundID]);
#endif

			// Set enabled
			audioSource.enabled = true;

			if (type == SoundType.Loop)
			{
				audioSource.loop = true;

				if (!audioSource.isPlaying)
				{
					if (delay > 0)
					{
						audioSource.PlayDelayed(delay);
					}
					else
					{
						audioSource.Play();                        
                    }
				}
			}
			else
			{
				audioSource.loop = false;

				if (type == SoundType.Replace)
				{
					if (delay > 0)
					{
						audioSource.PlayDelayed(delay);
					}
					else
					{
						audioSource.Play();
					}
				}
				else if (type == SoundType.New)
				{
					audioSource.PlayOneShot(audioSource.clip);
				}
				else if (type == SoundType.Only)
				{
					if (!audioSource.isPlaying)
					{
						if (delay > 0)
						{
							audioSource.PlayDelayed(delay);
						}
						else
						{
							audioSource.Play();
						}
					}
				}
			}

			return true;
		}

		return false;
	}

	public bool PlaySound(string soundName, SoundType type = SoundType.Replace, float delay = 0f)
	{
		for (int i = 0; i < (int)SoundID.Count; i++)
		{
			SoundID soundID = (SoundID)i;

			if (soundID.ToString() == soundName)
			{
				return PlaySound(soundID, type, delay);
			}
		}

		return false;
	}

	public bool PlayRandomSound(params SoundID[] soundIDs)
	{
		// Get random sound ID
		SoundID soundID = soundIDs[UnityEngine.Random.Range(0, soundIDs.Length)];

		return PlaySound(soundID);
	}

	public bool PlayMusic(SoundID soundID)
	{
		// Stop current music
		if (musicSource != null)
		{
			// Stop music
			musicSource.Stop();

			// Set disabled
			musicSource.enabled = false;
		}

		// Set music source
		musicSource = musicLookup[soundID];

		if (musicSource != null)
		{
#if EDIT_MODE
			musicSource.Copy(prefabMusicLookup[soundID]);
#endif

			// Set enabled
			musicSource.enabled = true;

			// Play music
			musicSource.Play();

			return true;
		}

		return false;
	}
	
	public void StopMusic()
	{
		if (musicSource != null)
		{
			// Stop music
			musicSource.Stop();
			
			// Set disabled
			musicSource.enabled = false;

			musicSource = null;
		}
	}

    public void StopSound(SoundID soundID)
    {
        AudioSource audioSource = soundLookup[soundID];

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.enabled = false;
        }
    }

    public bool IsSoundFinished(SoundID soundID)
    {
        AudioSource audioSource = soundLookup[soundID];

        if (audioSource != null)
        {
            return !audioSource.isPlaying;
        }

        return true;
    }

	public void StopAllSounds()
	{
		foreach (AudioSource audioSource in soundLookup.Values)
		{
			audioSource.Stop();
			audioSource.enabled = false;
		}
	}

	void AddMusic(SoundID soundID, AudioSource audioSource)
	{
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.Copy(audioSource);
		source.enabled = false;

		musicLookup.Add(soundID, source);

#if EDIT_MODE
		prefabMusicLookup.Add(soundID, audioSource);
#endif
	}

	void AddSound(SoundID soundID, AudioSource audioSource)
	{
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.Copy(audioSource);
		source.enabled = false;

		soundLookup.Add(soundID, source);

#if EDIT_MODE
		prefabSoundLookup.Add(soundID, audioSource);
#endif
	}
}
