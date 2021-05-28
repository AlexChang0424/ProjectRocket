using UnityEngine;
using UnityEngine.SceneManagement; 

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip goalAudio;
    [SerializeField] ParticleSystem goalParticle;
    [SerializeField] ParticleSystem crashParticle;
    AudioSource audioSource;
    bool isTransitioning = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning){ return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
           default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(goalAudio);
        goalParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }
    void StartCrashSequence()
    {  
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
