using UnityEngine;
using UnityEngine.AI;

public class EnemieAIScript : MonoBehaviour
{
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.

    public float shootingForce, shootingTimer;

    private GameObject player;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    }

    public void ShootingPlayer()
    {
        transform.LookAt(player.transform);
        timer += Time.deltaTime;
        if (timer <= 0.2f)
        {
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        else if (timer >= shootingTimer)
        {
            Shot();
            timer = 0;
        }
    }

    private void Shot()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = shootingForce * m_FireTransform.forward;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }
}
