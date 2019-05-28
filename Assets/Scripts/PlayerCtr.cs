using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerCtr : MonoBehaviour {
	
	public float maxSpeed = 6f;
	public float jumpForce = 1000f;
    public Transform groundCheck;
	 
    public string PressKey;
    public string StretchKey;
	int JumpIndex ;
    public int MaxJumpIndex;
	private Rigidbody2D _rigi;
	private Animator anim;

    public Image[] HealthImage;
    public Image EnergyImage;
    public float MaxEnergy;
    public  float Energy;
   public int HealthIndex;

    Vector3 resetpos;
    private PlayerState ps;
    private PolygonCollider2D[] PoColis;
	private bool isGrounded = false;
    public  float StartGravity;
     
    public GameObject Boost;//跳跃+落地特效
    // Use this for initialization
    void Start () {
        resetpos = transform.position;
        Energy = MaxEnergy;
        HealthIndex = HealthImage.Length;
        ps = GetComponent<PlayerState>();
         PoColis = GetComponents<PolygonCollider2D>();
        _rigi = GetComponent<Rigidbody2D>();
        StartGravity = _rigi.gravityScale;
        anim = GetComponentInChildren<Animator>();
        

    }
     
    IEnumerator ColorCtr()
    {
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.black, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.black, 0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 0.1f);
        ps.isHurt = false;
    }
    public void GetHurt()
    {
        if (!ps.isHurt)
        {
            ps.isHurt = true;

            HealthIndex--;
            HealthImage[HealthIndex].DOFade(0, 0.2f);
            StartCoroutine(ColorCtr());
            if (HealthIndex <= 0)
            {
            StartCoroutine(    PlayerReset());
            }
             
        }

    }
    public void HealthBack()
    {
        if (HealthIndex < HealthImage.Length)
        {
            
            HealthImage[HealthIndex].DOFade(1, 0.2f);
            HealthIndex++;
        }
    }
    public IEnumerator PlayerReset()
    {
        UIManager._instance.ShowImage.DOFade(1, 0.25f);
        yield return new WaitForSeconds(0.25f);
        transform.position = resetpos;
        
        for (int i = 0; i < HealthImage.Length; i++)
        {
            HealthBack();
        }
        yield return new WaitForSeconds(0.1f);
        UIManager._instance.ShowImage.DOFade(0, 0.25f);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="save")
        {
            
            resetpos = collision.gameObject.transform.position;
             
        }
        if (collision.gameObject.tag == "health")
        {
            if (HealthIndex < HealthImage.Length)
            {
                Destroy(collision.gameObject);
                HealthBack();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
		 
		if (collision2D.relativeVelocity.magnitude > 20)
        {
            GameObject temp = Instantiate(Boost, groundCheck.position, transform.rotation);
            Destroy(temp, 0.5f);
		}
	}
    void SlamsIntoGround()
    {
        if(!isGrounded&&Input.GetKeyDown(KeyCode.S)&&!anim.GetBool("isPress"))
        {
           
            ps.isSlim = true;
            AudioManager._instance.AudioPlay("吞噬");
            anim.SetTrigger("eat");
            _rigi.gravityScale = 40;
        }
        else if(isGrounded)
        {
            if (ps.isSlim)
            {
                Camera.main.transform.parent.DOShakePosition(0.25f,1f);
                ps.isSlim = false;
                _rigi.gravityScale = StartGravity;
            }
        }
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || JumpIndex < MaxJumpIndex-1))
        {
            _rigi.velocity = Vector2.zero;
            _rigi.AddForce(new Vector2(0, jumpForce));
            AudioManager._instance.AudioPlay("跳跃");
            JumpIndex++;
            if (JumpIndex == MaxJumpIndex-1 && !isGrounded)
            {
                anim.SetTrigger("doublejump");
               GameObject temp= Instantiate(Boost, groundCheck.position, transform.rotation);
                Destroy(temp, 0.5f);
            }
        }
    }
	void PressCtr()
    {
        if (Input.GetKeyDown(PressKey))
        {
            anim.SetBool("isPress", true);
        }
        if (Input.GetKeyUp(PressKey))
        {
            anim.SetBool("isPress", false);
        }
    }
    public void OnPressEnter()
    {
        for (int i = 0; i < PoColis.Length; i++)
        {
            PoColis[i].enabled = false;
        }
        PoColis[1].enabled = true;
    }
    public void OnPressExit()
    {
        for (int i = 0; i < PoColis.Length; i++)
        {
            PoColis[i].enabled = false;
        }
        PoColis[0].enabled = true;
    }
   
    
  public void StopPlayer()
    {
        if(ps.isSiphon)
        {
            ps.cantmove = true;
            _rigi.velocity = Vector2.zero;
            _rigi.gravityScale = 0;
        }
        else if(!ps.isSlim)
        {
            ps.cantmove = false;
            _rigi.gravityScale = StartGravity;
        }
    }
    // Update is called once per frame
    void Update () {
        StopPlayer();
        if (ps.cantmove)
            return;
        SlamsIntoGround();
        Jump();
        
        PressCtr();


    }
    public void RestoreEnergy(float e)
    {
        if(MaxEnergy-Energy>=e)
        {
            Energy += e;
        }
        else
        {
            Energy = MaxEnergy;
        }
    }

    void FixedUpdate()
    {
        anim.SetBool("isburn", ps.isBurn);
        EnergyImage.fillAmount = Energy / MaxEnergy;
        if (ps.cantmove)
            return;
        if (isGrounded)
            JumpIndex = 0;
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
        }
        anim.SetFloat("yspeed", _rigi.velocity.y);
        anim.SetBool("isGround", isGrounded);
        _rigi.velocity = new Vector2(h * maxSpeed, _rigi.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, (1 << LayerMask.NameToLayer("ground")) | (1 << LayerMask.NameToLayer("enemy")));
		if(h>0)
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
             else if(h<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
	}


	
	 

}
