using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {
        public float movePower = 2f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 10f; //Set Gravity Scale in Rigidbody2D Component to 5
        

        public GameObject attackCollision;
        public GameObject Skill1;
        public GameObject Skill2;
        public GameObject Skill3;
        public GameObject Skill4;

        [SerializeField] Transform pos;
        [SerializeField] float cheakRadius;
        [SerializeField] LayerMask isLayer;
        private Rigidbody2D rb;
        public Animator anim;
        

        private int direction = 1;
        bool isJumping = false;
        bool isGround;
        private bool alive = true;
        private bool isDash = false;
        private bool isAttack_ing = false;
        


        public float CameraSpeed;

        public int attackCombo = 0;
        public int Ad = 20;
        public int Max_Hp = 100;
        public int Hp = 100;


        private bool isskill1 = false;
        private bool isskill2 = false;
        private bool isskill3 = false;
        private bool isskill4 = false;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            this.gameObject.layer = 31;
            Hp = Max_Hp;
        }

        private void Update()
        {
            Restart();
            if (alive)
            {

                OnAttack();
                
                Dash();

                
                Jump();
                Run();

                AttackSkill1();
                AttackSkill2();
                AttackSkill3();
                AttackSkill4();

                if (Input.GetKey("escape"))
                    Application.Quit();


            }

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
            /*
            if (other.gameObject.tag == "Enemy" && this.tag == "Player" && !isAttack_ing)
            {
                TakeDamage(Max_Hp * 0.02f);
                anim.SetTrigger("hurt");

                if (direction == 1)
                    rb.AddForce(new Vector2(-3f, 2f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(3f, 2f), ForceMode2D.Impulse);

                this.gameObject.tag = "hurtPlayer";
                Invoke("PlayerStatement", 1f);
            }

            if (other.gameObject.tag == "Boss" && this.tag == "Player" && !isAttack_ing)
            {
                TakeDamage(Max_Hp * 0.05f);
                anim.SetTrigger("hurt");

                if (direction == 1)
                    rb.AddForce(new Vector2(-3f, 2f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(3f, 2f), ForceMode2D.Impulse);

                this.gameObject.tag = "hurtPlayer";
                Invoke("PlayerStatement", 1f);
            }
            */
        }
    
        

        public void OnAttackCollision()
        {
            attackCollision.SetActive(true);
        }

        public void OnSkill1()
        {
            Skill1.SetActive(true);
        }
        public void OnSkill2()
        {
            Skill2.SetActive(true);
        }
        public void OnSkill3()
        {
            Skill3.SetActive(true);
        }
        public void OnSkill4()
        {
            Skill4.SetActive(true);
        }

        public void AttackSkill1()
        {
            if (Input.GetKeyDown(KeyCode.Q)  && !isskill1)
            {
                isAttack_ing = true;
                isskill1 = true;
                anim.SetTrigger("skill1");
                
                Invoke("Skill1_statement", 5f);
                Invoke("Attack_statement", 0.3f);
            }
        }

        public void AttackSkill2()
        {
            if (Input.GetKeyDown(KeyCode.W)  && !isskill2)
            {
                isAttack_ing = true;
                isskill2 = true;
                anim.SetTrigger("skill2");
               
                Invoke("Skill2_statement", 5f);
                Invoke("Attack_statement", 0.3f);
            }
        }

        public void AttackSkill3()
        {
            if (Input.GetKeyDown(KeyCode.E)  && !isskill3)
            {
                isAttack_ing = true;
                isskill3 = true;
                anim.SetTrigger("skill3");
                
                Invoke("Skill3_statement", 10f);
                Invoke("Attack_statement", 1f);
            }
        }

        public void AttackSkill4()
        {
            if (Input.GetKeyDown(KeyCode.R)  && !isskill4)
            {
                isAttack_ing = true;
                isskill4 = true;
                anim.SetTrigger("skill4");
                
                Invoke("Skill4_statement", 30f);
                Invoke("Attack_statement", 1.2f);
            }
        }

        void Run()
        {
            
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

                
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (AnimationInfo())
                    direction = -1;
                    moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("OnAttack"))
                    direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 1, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("OnAttack") && !anim.GetBool("isJump"))
                    transform.position += moveVelocity * movePower * Time.deltaTime * 0.3f;
                else
                    transform.position += moveVelocity * movePower * Time.deltaTime;
            
            
        }
       

        void Jump()
        {
            isGround = Physics2D.OverlapCircle(pos.position, cheakRadius, isLayer);
                if ((Input.GetKeyDown(KeyCode.C)) && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }


        
        
        void OnAttack()
        {
            if (Input.GetKey(KeyCode.X) && !isAttack_ing)
            {
                isAttack_ing = true;

                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("OnAttack")
                    && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
                {
                    attackCombo++;
                 
                    anim.SetFloat("Blend", attackCombo);
                    
                }
                else
                {
                    anim.SetFloat("Blend", 0);
                    attackCombo = 0;
                }

                if (anim.GetFloat("Blend") >= 3)
                {
                    attackCombo = 0;
                    Invoke("Attack_statement", 0.8f);
                    
                }
                    
                else
                    Invoke("Attack_statement", 0.4f);

                

                anim.SetTrigger("onattack");
            }
        }
        void Hurt()
        {
            
            anim.SetTrigger("hurt");
                
            if (direction == 1)
                rb.AddForce(new Vector2(-8f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(8f, 1f), ForceMode2D.Impulse);
            
        }

        void Dash()
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isDash)
            {
                isDash = true;
                anim.SetTrigger("dash");
                if (direction == 1)
                    rb.AddForce(new Vector2(7f, 7f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(-7f, 7f), ForceMode2D.Impulse);
                
                Invoke("Dash_statement", 7f);
            }
                

        }

        bool AnimationInfo()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("OnAttack") ||
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1") ||
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill2") ||
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill3") ||
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill4"))
                return true;
            else
                return false;


        }
        public void TakeDamage(float damage)
        {
            
            
            Hp -= (int)damage;
            GetComponentInChildren<HpBar>().HandleHp((float)Hp / (float)Max_Hp);
            

            if (Hp <= 0)
                Die();
            else
            {
                Hurt();
            }

        }

        private void PlayerStatement()
        {
            
            if (alive)
                this.gameObject.tag = "Player";
            else
                this.gameObject.tag = "hurtPlayer";
        }

        void Die()
        {
            
            
            anim.SetBool("isKickBoard", false);
            anim.SetTrigger("die");
            alive = false;
            PlayerStatement();


        }
        void Attack_statement()
        {
            isAttack_ing = false;
        }

        void Skill1_statement()
        {
            isskill1 = false;
        }
        void Skill2_statement()
        {
            isskill2 = false;
        }
        void Skill3_statement()
        {
            isskill3 = false;
        }
        void Skill4_statement()
        {
            isskill4 = false;
        }
        void Dash_statement()
        {
            isDash = false;
        }

        public void Compensation(int levelup_hp,  int levelup_ad)
        {
            Max_Hp += levelup_hp;
            Hp = Max_Hp;
            Ad += levelup_ad;
            anim.SetTrigger("isLevelup");
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {

                Hp = Max_Hp / 2;
                anim.SetTrigger("idle");
                alive = true;
                Invoke("PlayerStatement", 3f);
            }
        }
    }

}