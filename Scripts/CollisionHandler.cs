using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    #region "Variables"
    public float levelLoadDelay = 1.0f;

    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    // Variables for cheating
    bool collisionDisable = false;
    bool cheating = false;

    MovementController movementController;
    [SerializeField] FuelData fuelData;

    public Image fuelBarImage;
    #endregion

    #region "Built-in Functions"
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        movementController = GetComponent<MovementController>();
        fuelData.currentFuel = fuelData.maxFuel;
    }
    private void Update() {
        //if (cheating){
        //    ListenToDebugKey();
        //} else {
        //    return;
        //}
        //UpdateFuel();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }
        else
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This is friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            AddFuel();
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region "Custom functions"
    // Cheating function
    void ListenToDebugKey(){
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisable = !collisionDisable; // toggle collision
        }
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
     void StartSuccessSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticle.Play();
        GetComponent<MovementController>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
        GetComponent<MovementController>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

   void AddFuel(){
        fuelData.currentFuel += fuelData.addFuel;
        Debug.Log("We can add Fuel");
   }
    #endregion

}
