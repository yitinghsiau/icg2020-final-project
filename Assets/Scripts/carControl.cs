using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControl : MonoBehaviour
{
    [SerializeField] StartUI m_startGameUI;
    [SerializeField] inGameUI m_inGameUI;
    [SerializeField] EndGameUI m_endGameUI;

    float m_horizontalInput;
    float m_verticalInput;
    float m_steeringangle;

    public Rigidbody car_body;
    public WheelCollider FR_R_Wheel, FR_L_Wheel;
    public WheelCollider RE_R_Wheel, RE_L_Wheel;
    public Transform FR_R_T, FR_L_T;
    public Transform RE_R_T, RE_L_T;
    public float maxSteerAngle = 15;
    public float motorForce = 50;

    public float m_stiffness;
    public WheelFrictionCurve friction_RE_L_forward;
    public WheelFrictionCurve friction_RE_L_side;
    public WheelFrictionCurve friction_RE_R_forward;
    public WheelFrictionCurve friction_RE_R_side;
    public WheelFrictionCurve friction_FR_L_forward;
    public WheelFrictionCurve friction_FR_L_side;
    public WheelFrictionCurve friction_FR_R_forward;
    public WheelFrictionCurve friction_FR_R_side;

    float acceleration = 1.6f;
    float deceleration = 2f;

    float angularAcceleration = 1.5f;

    bool counterStart_speed;
    bool counterStart_steer;
    float timer;

    public Vector3 acquiredBoxPos;
    public int acquiredBoxID = 0;

    public bool startLine_passed;
    public bool checkPoint_1_passed;
    public bool checkPoint_2_passed;
    public bool checkPoint_3_passed;


    [SerializeField] GameObject checkPoint_1;
    [SerializeField] GameObject checkPoint_2;
    [SerializeField] GameObject checkPoint_3;




    public void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {

            FR_L_Wheel.brakeTorque = 0;
            FR_R_Wheel.brakeTorque = 0;
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            FR_L_Wheel.brakeTorque = 0;
            FR_R_Wheel.brakeTorque = 0;
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow) && m_verticalInput > -0.01)
        {
            
            m_verticalInput = Mathf.Min(m_verticalInput + acceleration, 3f);
            
        }
        else if (Input.GetKey(KeyCode.DownArrow) && m_verticalInput < 0.01)
        {
            
            m_verticalInput = Mathf.Max((m_verticalInput - deceleration), -3f);

        }
        else
        {
            if (m_verticalInput > 0.01)
            {

                m_verticalInput = 0;
                FR_L_Wheel.brakeTorque = 50f;
                FR_R_Wheel.brakeTorque = 50f;
                RE_L_Wheel.brakeTorque = 50f;
                RE_R_Wheel.brakeTorque = 50f;

            }
            else if (m_verticalInput < -0.01)
            {

                m_verticalInput = 0;
                FR_L_Wheel.brakeTorque = 50f;
                FR_R_Wheel.brakeTorque = 50f;
                RE_L_Wheel.brakeTorque = 50f;
                RE_R_Wheel.brakeTorque = 50f;
            }


        }


        if (Input.GetKey(KeyCode.Space))
        {
            FR_L_Wheel.brakeTorque = 300f;
            FR_R_Wheel.brakeTorque = 300f;
            RE_L_Wheel.brakeTorque = 300f;
            RE_R_Wheel.brakeTorque = 300f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FR_L_Wheel.brakeTorque = 0;
            FR_R_Wheel.brakeTorque = 0;
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_stiffness = 0.001f;
            Drift(m_stiffness);
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_stiffness = 0.1f;
            Drift(m_stiffness);
        }



        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_horizontalInput = Mathf.Min(m_horizontalInput = m_horizontalInput + 0.5f * angularAcceleration, maxSteerAngle);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_horizontalInput = Mathf.Max(m_horizontalInput = m_horizontalInput - 0.5f * angularAcceleration, -maxSteerAngle);
            
        }
        else
        {
            if (m_horizontalInput > 0.01)
            {
                m_horizontalInput = m_horizontalInput - angularAcceleration;
            }
            else if (m_horizontalInput < -0.01)
            {
                m_horizontalInput = m_horizontalInput + angularAcceleration;
            }


        }


        if (Input.GetKey(KeyCode.R))
        {
            ManualReset();
        }


    }

    private void Steer()
    {
        if (counterStart_steer == false)
        {
            m_steeringangle = m_horizontalInput;
            FR_L_Wheel.steerAngle = m_steeringangle;
            FR_R_Wheel.steerAngle = m_steeringangle;
        }
        
    }

    private void ReverseSteer()
    {
        if(counterStart_steer == true)
        {
            m_steeringangle = -m_horizontalInput;
            FR_L_Wheel.steerAngle = m_steeringangle;
            FR_R_Wheel.steerAngle = m_steeringangle;
        }

    }

    private void Accelerate()
    {
        FR_L_Wheel.motorTorque = m_verticalInput * motorForce;
        FR_R_Wheel.motorTorque = m_verticalInput * motorForce;
        RE_L_Wheel.motorTorque = 0.3f * m_verticalInput * motorForce;
        RE_R_Wheel.motorTorque = 0.3f * m_verticalInput * motorForce;

    }

    private void SpeedUp()
    {
        if (counterStart_speed == true)
        {
            Vector3 localForce = new Vector3(0.007459f, 0f, 0.66602f);
            car_body.AddRelativeForce(localForce * 600f);

            frictionChange(0.8f, 0.001f);
        }

    }

    private void Drift(float m_stiffness)
    {

        friction_RE_L_forward = RE_L_Wheel.forwardFriction;
        friction_RE_L_side = RE_L_Wheel.sidewaysFriction;
        friction_RE_L_forward.stiffness = m_stiffness;
        friction_RE_L_side.stiffness = m_stiffness;
        RE_L_Wheel.forwardFriction = friction_RE_L_forward;
        RE_L_Wheel.sidewaysFriction = friction_RE_L_side;

        friction_RE_R_forward = RE_R_Wheel.forwardFriction;
        friction_RE_R_side = RE_R_Wheel.sidewaysFriction;
        friction_RE_R_forward.stiffness = m_stiffness;
        friction_RE_R_side.stiffness = m_stiffness;
        RE_R_Wheel.forwardFriction = friction_RE_R_forward;
        RE_R_Wheel.sidewaysFriction = friction_RE_R_side;
        
    }

    
    private void frictionChange(float m_stiffness_FR, float m_stiffness_RE)
    {
        
        friction_FR_L_forward = FR_L_Wheel.forwardFriction;
        friction_FR_L_side = FR_L_Wheel.sidewaysFriction;
        friction_FR_L_forward.stiffness = m_stiffness_FR;
        friction_FR_L_side.stiffness = m_stiffness_FR;
        FR_L_Wheel.forwardFriction = friction_FR_L_forward;
        FR_L_Wheel.sidewaysFriction = friction_FR_L_side;

        friction_FR_R_forward = FR_R_Wheel.forwardFriction;
        friction_FR_R_side = FR_R_Wheel.sidewaysFriction;
        friction_FR_R_forward.stiffness = m_stiffness_FR;
        friction_FR_R_side.stiffness = m_stiffness_FR;
        FR_R_Wheel.forwardFriction = friction_FR_R_forward;
        FR_R_Wheel.sidewaysFriction = friction_FR_R_side;
        

        friction_RE_L_forward = RE_L_Wheel.forwardFriction;
        friction_RE_L_side = RE_L_Wheel.sidewaysFriction;
        friction_RE_L_forward.stiffness = m_stiffness_RE;
        friction_RE_L_side.stiffness = m_stiffness_RE;
        RE_L_Wheel.forwardFriction = friction_RE_L_forward;
        RE_L_Wheel.sidewaysFriction = friction_RE_L_side;

        friction_RE_R_forward = RE_R_Wheel.forwardFriction;
        friction_RE_R_side = RE_R_Wheel.sidewaysFriction;
        friction_RE_R_forward.stiffness = m_stiffness_RE;
        friction_RE_R_side.stiffness = m_stiffness_RE;
        RE_R_Wheel.forwardFriction = friction_RE_R_forward;
        RE_R_Wheel.sidewaysFriction = friction_RE_R_side;

    }

    void forcedStop()
    {
        FR_L_Wheel.brakeTorque = 1000f;
        FR_R_Wheel.brakeTorque = 1000f;
        RE_L_Wheel.brakeTorque = 1000f;
        RE_R_Wheel.brakeTorque = 1000f;
    }
    void brakeRelease()
    {
        FR_L_Wheel.brakeTorque = 0;
        FR_R_Wheel.brakeTorque = 0;
        RE_L_Wheel.brakeTorque = 0;
        RE_R_Wheel.brakeTorque = 0;
    }

    
    void ManualReset()
    {


        if (checkPoint_1_passed == true)
        {           

            if (checkPoint_2_passed == true)
            {

                if (checkPoint_3_passed == true)
                {
                    this.transform.position = checkPoint_3.transform.position;
                    this.transform.rotation = Quaternion.Euler(0f, -475.744f, 0f);
                }
                else
                {
                    this.transform.position = checkPoint_2.transform.position;
                    this.transform.rotation = Quaternion.Euler(0f, -297.434f, 0f);
                }

            }
            else
            {
                this.transform.position = checkPoint_1.transform.position;
                this.transform.rotation = Quaternion.Euler(0f, -160.084f, 0f);
            }


        }
      
    }

    



    private void UpdateWheelPoses()
    {
        UpdateWheelPose(FR_L_Wheel, FR_L_T);
        UpdateWheelPose(FR_R_Wheel, FR_R_T);
        UpdateWheelPose(RE_L_Wheel, RE_L_T);
        UpdateWheelPose(RE_R_Wheel, RE_R_T);

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;


        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;

    }


    
    private void FixedUpdate()
    {
        if (counterStart_steer == true)
        {
            timer = timer + Time.deltaTime;

            if (timer > 3)
            {
                counterStart_steer = false;
                timer = 0;

            }
        }


        if (counterStart_speed == true)
        {
            timer = timer + Time.deltaTime;

            if (timer > 3)
            {
                counterStart_speed = false;
                timer = 0;

                frictionChange(0.1f, 0.1f);
            }
        }



        if (m_inGameUI.countDownCompleted == true && m_startGameUI.gameStarted == true)
        {           
            GetInput();                      
        }

        if (m_inGameUI.resetCheck == true)
        {
            
            forcedStop();
        }
        

        Steer();
        ReverseSteer();
        Accelerate();
        SpeedUp();
        UpdateWheelPoses();
        
        
    }


    void OnTriggerEnter(Collider other)
    {
        var accelerator = other.gameObject.GetComponent<accelerator>();
        var fly = other.gameObject.GetComponent<fly>();
        var convertedSteering = other.gameObject.GetComponent<convertedSteering>();

        



        if (accelerator != null)
        {         
            counterStart_speed = true;
            acquiredBoxPos = accelerator.transform.position;
            acquiredBoxID = accelerator.ID;

        }
        else if(fly != null)
        {
            car_body.AddForce(transform.up * motorForce * 150f);
            acquiredBoxPos = fly.transform.position;
            acquiredBoxID = fly.ID;
        }
        else if (convertedSteering != null)
        {
            counterStart_steer = true;
            acquiredBoxPos = convertedSteering.transform.position;
            acquiredBoxID = convertedSteering.ID;
        }   

    }

    void OnTriggerExit(Collider other)
    {
        var startLine = other.gameObject.GetComponent<startLine>();
        var checkPoint = other.gameObject.GetComponent<checkPoint>();

        if (startLine != null)
        {
            if (checkPoint_2_passed == true)
            {
                startLine_passed = true;
                checkPoint_1_passed = false;
                checkPoint_2_passed = false;
                checkPoint_3_passed = false;
            }
        }

        
        if (checkPoint != null)
        {
            if (checkPoint.checkPoint_ID == 1)
            {
                checkPoint_1_passed = true;
            }
            else if (checkPoint.checkPoint_ID == 2)
            {
                checkPoint_2_passed = true;
            }
            else if (checkPoint.checkPoint_ID == 3)
            {
                checkPoint_3_passed = true;
            }

        }
        


    }


}
