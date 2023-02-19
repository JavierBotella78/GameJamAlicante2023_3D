using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds : int
{
	chocharClip = 0,
	morirClip = 1,
	tocarMetaClip = 2,
};

public enum trapsSounds : int
{
    platano = 0,
    cañon = 1,
    balancin = 2,
};
public class SoundManager : MonoBehaviour
{
	// VARIABLES
	private AudioSource _audioSourceMusic;
	private AudioSource _audioSourceVFX;

	[SerializeField] private List<AudioClip> ChocarClips;
	[SerializeField] private List<AudioClip> MorirClips;
	[SerializeField] private List<AudioClip> TocatMetaClips;
	[SerializeField] private List<AudioClip> ItemClips;


	// FUNCIONES
	private void initComponents()
	{
		var audioSources = GetComponents<AudioSource>();
		_audioSourceMusic = audioSources[0];
		_audioSourceVFX = audioSources[1];
	}

	private void init()
	{
		_audioSourceMusic.Play();
	}

	private void Awake()
	{
		initComponents();
	}

	void Start()
	{
		init();
	}

	// Suena el sonido de victoria
	public void playVictorySound()
	{
		int rand = Random.Range(0, TocatMetaClips.Count - 1);
		_audioSourceVFX.PlayOneShot(TocatMetaClips[rand], 0.8f);
	}

	// Suena el sonido de derrota
	public void playLoseSound()
	{
		int rand = Random.Range(0, MorirClips.Count - 1);

		_audioSourceVFX.PlayOneShot(MorirClips[rand], 0.8f);
	}

	// Suena el sonido del jugador de poner una bomba
	public void playChocarSound()
	{
		int rand = Random.Range(0, ChocarClips.Count - 1);

		_audioSourceVFX.PlayOneShot(ChocarClips[rand], 0.56f);
	}

	public void playItemSound()
	{
		int rand = Random.Range(0, ItemClips.Count - 1);

		_audioSourceVFX.PlayOneShot(ItemClips[rand], 0.56f);
	}

	// Se cambia la musica del nivel
	/*public void playMusicV2()
    {
        float oldtime = _audioSourceMusic.time;
        _audioSourceMusic.clip = Clips[(int)Sounds.musicV2];
        _audioSourceMusic.time = oldtime;
        _audioSourceMusic.Play();
    }*/
}
