using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Material[] lineMaterialglow = new Material[1];
    public Material[] lineMaterialreddot = new Material[1];
    public LineRenderer line;
    public HealthBar healthBar;
    public AudioSource runSound;
    public VariableJoystick left, right;
    [SerializeField] Animator animator;
    private Vector3 movement;
    [SerializeField] private float movementSpeed, bulletSpeed;
    //GameObject kamera;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;
    public int maxHeatlh = 100;
    public int currentHealth;
    float yGravity = -9.81f;
    private void Awake()
    {
        runSound.enabled = false;
        currentHealth = maxHeatlh;
        healthBar.SetMaxHealth(maxHeatlh);
    }
    void Start()
    {

    }
    void Move()
    {
        movement.x = left.Horizontal * movementSpeed;
        movement.y += yGravity;
        movement.z = left.Vertical * movementSpeed;
        gameObject.GetComponent<CharacterController>().Move(movement * Time.fixedDeltaTime);
        if(left.Horizontal>0.01f|| left.Horizontal < -0.01f|| left.Vertical > 0.01f || left.Vertical < -0.01f)
        {
            runSound.enabled = true;
            animator.SetBool("kosuyor", true);
            animator.speed = Mathf.Max(Mathf.Abs(left.Horizontal), Mathf.Abs(left.Vertical));
        }
        else
        {
            animator.speed = 1;
            runSound.enabled = false;
            animator.SetBool("kosuyor", false);
        }
    }
    void Look()
    {
        Vector3 direction;
        direction.x = transform.position.x + left.Horizontal * 10f;
        direction.z = transform.position.z + left.Vertical * 10f;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }
    void Update()
    {
        Move();
        Look();
    }
    public void Shooting()
    {
        animator.speed = 1;
        StartCoroutine(MuzzleFlash());
        animator.SetTrigger("ates");
        GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        firedBullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
    }
    IEnumerator MuzzleFlash()
    {
        line.materials = lineMaterialreddot;
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);
        line.materials = lineMaterialglow;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            yGravity = 0;
        }
        else
        {
            yGravity = -9.81f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Claw")
        {
            TakeDamage(1);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
    }
}
