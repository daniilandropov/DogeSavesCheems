using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class character : MonoBehaviour
    {
        [SerializeField]
        protected int health;
        [SerializeField]
        protected float speed;
        [SerializeField]
        public float jumpForce;

        protected bool isGrounded = false;

        public List<partOfHero> PartsOfHero;

        public List<partOfHero> Legs;

        new protected Rigidbody2D rigidbody;
        protected Animator animator;
        protected SpriteRenderer sprite;

        /// <summary>
        ///  Блокируем управление на момент удара
        /// </summary>
        protected bool _controlIsLocked = false;

        protected bool _inBlock = false;

        protected void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (_controlIsLocked)
                return;

            if (isGrounded) State = CharState.Idle;
        }

        protected CharState State
        {
            get { return (CharState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }

        protected void FixedUpdate()
        {
            CheckGround();
        }

        protected void CheckGround()
        {
            if (Legs.Where(x => x.IsGrounded).Count() > 0)
                isGrounded = true;
            else
                isGrounded = false;

            if (!isGrounded) State = CharState.Jump;
        }

        public void ReciveDamage(int damage)
        {
            health -= damage;
        }

        protected void Block()
        {
            State = CharState.Block;
            _inBlock = true;
        }

        protected IEnumerator Punch(CharState charState)
        {
            if (isGrounded)
            {
                State = charState;
                _controlIsLocked = true;
                yield return new WaitForSeconds(Helper.GetAnimLength(State.ToString(), this.gameObject));
                _controlIsLocked = false;
            }


            yield return null;
        }

        protected void PunchInTheAir()
        {
            State = CharState.InTheAirPunc;
        }

        /// <summary>
        ///  Какова продожительность текущей анимации
        /// </summary>
        /// <returns></returns>
        protected float CurrentStateLength()
        {
            AnimatorClipInfo[] stInfos = animator.GetCurrentAnimatorClipInfo(0);
            AnimationClip clip = stInfos[0].clip;
            var length = clip.length;

            return length;
        }

        public void Jump(float jf)
        {
            rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 0));
            rigidbody.AddForce(transform.up * jf, ForceMode2D.Impulse);
        }

        protected void Run(float axis)
        {
            if (isGrounded) State = CharState.Run;

            Vector3 direction = transform.right * axis;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            if (direction.x < 0.0F)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public enum CharState
    {
        Idle,
        Run,
        Jump,
        PunchFrontHand,
        PunchBackHand,
        InTheAirPunc,
        Block
    }
}
