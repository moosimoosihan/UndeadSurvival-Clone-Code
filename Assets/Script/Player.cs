using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVec;
    public Scaner scanner;
    public Hand[] hands;

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

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void LateUpdate()
    {
        anim.SetFloat("Speed",inputVec.magnitude);

        if(inputVec.x != 0){
            spriter.flipX = inputVec.x < 0;
        }
    }
}
