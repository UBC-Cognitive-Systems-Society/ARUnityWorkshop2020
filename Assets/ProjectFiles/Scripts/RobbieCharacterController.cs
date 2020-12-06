using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieCharacterController : MonoBehaviour
{

    protected Animator anim;
    [SerializeField] protected Camera cam;
    [SerializeField] protected private float waveTime;

    public bool allowMovement = true;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    protected IEnumerator TriggerWave()
    {
        anim.SetBool("Wave", true);
        yield return new WaitForSeconds(waveTime);
        anim.SetBool("Wave", false);
    }

    public void FadeOutAudio(AudioSource source, float t)
    {
        StartCoroutine(FadeOutSound(source, t));
    }

    static IEnumerator FadeOutSound(AudioSource source, float time)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / time;

            yield return null;
        }
        source.Stop();
        source.volume = startVolume;
    }
}
