using UnityEngine;

public class NightclubLights : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject _lights;
    [SerializeField] float _delay;
    float _nextFlash = 0;

    private void Update()
    {
        // We want to flash the lights in rythm with the music in the nightclub
        // Get music data from the audio source
        float[] samples = new float[256];
        _audioSource.GetOutputData(samples, 0);

        // Calculate the average
        float sum = 0;
        for(int i = 0; i < 256;  i++)
            sum += samples[i];
        float average = sum / 256;

        // Activate or Deactivate the pulsing light if the music is above a given threshold
        // nextFlash and delay are used to determine the duration of a pulsation.
        if(Time.time >= _nextFlash)
        {
            if(average > 0.09)
            {
                _nextFlash = Time.time + _delay * Time.deltaTime;
                _lights.SetActive(true);
            }
            else
                _lights.SetActive(false);
        }
    }

}
