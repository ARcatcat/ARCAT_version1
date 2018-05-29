using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class moveMouse : MonoBehaviour
{

    // PUBLIC
    public SimpleTouchController leftController;
   // public Animator Cat;

    //public SimpleTouchController rightController;
    public Transform headTrans;
    private float speedMovements = 0.1f;
    Quaternion q = Quaternion.identity;
    private bool isJumping = false;
    private int jishi_jump = 0;
    private Vector3 nowrotate = new Vector3(0, 0, 0);

    // PRIVATE
    private Rigidbody _rigidbody;
    private GameObject btnObj;

    [SerializeField]
    //bool continuousRightController = true;

    void Start()
    {
        /*btnObj = GameObject.Find("JumpButton");//"Button"为你的Button的名称  
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            if (isJumping == false)
            {
                //猫咪跳
                Cat.SetBool("idle_to_jump", true);
                Cat.SetBool("walk_to_jump", true);
                Cat.SetBool("jump_to_idle", true);
                Cat.SetBool("walk_to_eat", false);
                Cat.SetBool("walk_to_sound", false);
                isJumping = true;
                jishi_jump = 0;
                //Cat.SetBool("idle_to_jump", false);
            }
        });*/
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //rightController.TouchEvent += RightController_TouchEvent;
    }

    void Update()
    {
        //jump jishi
       /* if (isJumping)
        {
            jishi_jump++;
            if (jishi_jump == 19)
            {
                MeshCollider mc = transform.GetChild(0).GetComponent<MeshCollider>();
                mc.transform.Rotate(new Vector3(-52, 0, 0), 14);
                mc.transform.localPosition = new Vector3(0, 0.6f, 0.3f);
            }
            else if (jishi_jump == 25)
            {
                MeshCollider mc = transform.GetChild(0).GetComponent<MeshCollider>();
                mc.transform.Rotate(new Vector3(52, 0, 0), 10);
                mc.transform.localPosition = new Vector3(0, -0.006091211f, -1.35252e-18f);
            }
            else if (jishi_jump == 26)
            {
                Cat.SetBool("idle_to_jump", false);
                isJumping = false;
                jishi_jump = 0;
            }
        }*/
        //walk action
        if (leftController.GetTouchPosition != (new Vector2(0, 0)))
        {
           // Cat.SetBool("walk", true);
            //face to the position
            Vector3 rotateVector = new Vector3(leftController.GetTouchPosition.x * 10, 0, leftController.GetTouchPosition.y * 10);
            nowrotate = rotateVector;
            q = Quaternion.LookRotation(rotateVector);
            _rigidbody.transform.rotation = q;
        }
        else
        {
            // idel action
         //   Cat.SetBool("walk", false);
          //  Cat.SetBool("walk_to_idle", true);
        }

        // move
        Vector3 move_des = (transform.localPosition + ((new Vector3(0, 0, 1)) * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
            ((new Vector3(1, 0, 0)) * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements));
        if (move_des.x < -0.4f) move_des.x = -0.4f;
        else if (move_des.x > 0.35f) move_des.x = 0.35f;
        if (move_des.z < -0.4f) move_des.z = -0.4f;
        else if (move_des.z > 0.4f) move_des.z = 0.4f;
        _rigidbody.transform.localPosition = move_des;
        //Vector3.forward = (0,0,1)
        //Vector3.right = (1,0,0)
    }

}
