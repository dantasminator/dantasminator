using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controle3d : MonoBehaviour
{
    Vector3 strafemove;
    Vector3 fowardmove;

    public Transform camera3d;

    [SerializeField] float speed;
    public CharacterController controle;

    [SerializeField]  float inputstrafe;
    [SerializeField]  float inputfoward;

    Vector3 geralmove;

    public float speedturn;

    public Animator animador;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moves();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            rotates();
            animador.SetBool("andando", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animador.SetBool("correndo", true);
                speed = 13f;

            }
            else
            {
                animador.SetBool("correndo", false);
                speed = 7.5f;
            }
        }
        else
        {
            animador.SetBool("correndo", false);

            animador.SetBool("andando", false);

        }

    }
    void moves()
    {

        inputstrafe = Input.GetAxis("Horizontal");
        inputfoward = Input.GetAxis("Vertical");
        inputfoward = Mathf.Abs(inputfoward);
        inputstrafe = Mathf.Abs(inputstrafe);

        strafemove = (inputstrafe * speed * Time.deltaTime * transform.forward);
        fowardmove = (inputfoward * speed * Time.deltaTime * transform.forward);

        

        controle.Move(strafemove += fowardmove);
    }

    void rotates()
    {
        inputstrafe = Input.GetAxis("Horizontal");
        inputfoward = Input.GetAxis("Vertical");
        var fowardcamera = camera3d.forward;
        var strafecamera = camera3d.right;
        strafecamera.y = 0f;
        fowardcamera.y = 0f;

        Vector3 directionplayer = fowardcamera * inputfoward + strafecamera * inputstrafe;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionplayer), speedturn);
    }
}
