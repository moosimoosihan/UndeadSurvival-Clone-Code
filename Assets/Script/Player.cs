using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVec;
    public Scaner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scaner>();

        // bool 값을 넣으면 비활성화 된 애들도 초기화 됨!
        hands = GetComponentsInChildren<Hand>(true);
    }
    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    void OnMove(InputValue value)
    {
        if(!GameManager.instance.isLive)
            return;

        inputVec = value.Get<Vector2>();
    }
    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed",inputVec.magnitude);

        if(inputVec.x != 0){
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if(!GameManager.instance.isLive)
            return;
        
        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0){
            for(int index = 2; index < transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
