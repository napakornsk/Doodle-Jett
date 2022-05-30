using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody rbPlayer;
    AudioSource audioSource;

    [SerializeField] float thrustFroce = 0.0f;
    [SerializeField] float thrustRotation = 100.0f;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    [SerializeField] AudioClip thrustAudio;

    public bool isThrusting = false;

    private void Awake() {
        rbPlayer = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    public void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();    
        }
        else
        {
            StopThrust();
        }  
    }

    private bool StartThrust()
    {
        rbPlayer.AddRelativeForce(Vector3.up * thrustFroce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudio);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }

        return isThrusting = true;
    }

    private bool StopThrust()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();

        return isThrusting = false;
    }

    private void RotateLeft()
    {
        ApplyRotation(-thrustRotation);
        if (!rightThrusterParticle.isPlaying)
        {
            rightThrusterParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(thrustRotation);
        if (!leftThrusterParticle.isPlaying)
        {
            leftThrusterParticle.Play();
        }
    }

    private void StopRotating()
    {
        rightThrusterParticle.Stop();
        leftThrusterParticle.Stop();
    }

    void ApplyRotation(float rotationForce){
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
    }

}
