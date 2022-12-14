using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private Transform UI_Inventory;
    private Inventory inventory;
    private Rigidbody2D rigid;

    private Vector3 position;
    private Ray2D ray2D;
    private RaycastHit2D rayhit2D;

    private float speedStatus   = 3.75f;
    private int jumpStatus      = 40;
    private int dashStatus      = 1;

    private int jumpCount_Duration = 2;
    private int jumpCount = 2;
    private int dashCount = 1;

    private int itemcode = -1;

    void Start()
    {
        inventory = new Inventory();
        rigid = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
    }

    void LateUpdate()
    {
        Character_Jump();
    }

    void FixedUpdate()
    {
        Character_Move();
    }

    private void Character_Init()
    {
        gameManager = transform.Find("GameManager").GetComponent<GameManager>();
    }

    #region ĳ���� ����
    private void Character_Move()
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (rigid.velocity.x > speedStatus)
            rigid.velocity = new Vector2(speedStatus, rigid.velocity.y);
        else if(rigid.velocity.x < speedStatus*(-1))
            rigid.velocity = new Vector2(speedStatus*(-1), rigid.velocity.y);

        //jump Count
        if (rigid.velocity.y < 0)
        {
            ray2D    = new Ray2D(new Vector2(this.transform.position.x, this.transform.position.y - 1.1f), Vector2.down);
            rayhit2D = Physics2D.Raycast(ray2D.origin, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));

            if (jumpCount_Duration < jumpCount && rayhit2D.collider != null)
            {
                if (rayhit2D.distance < 0.5f)
                    jumpCount_Duration = jumpCount;
            }
        }
    }

    private void Character_Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount_Duration != 0)
        {
            rigid.AddForce(Vector2.up * jumpStatus - rigid.velocity, ForceMode2D.Impulse);
            jumpCount_Duration -= 1;
        }
    }
    #endregion

    #region ĳ���� ���� ����


    private void Inven_GetStatus()//������ ���� ��������(������ �Ծ��� �� �۵�)
    {
        List<int> statusList = new List<int>(3) { 0, 0, 0 };
        statusList = inventory.Inven_GetItemData(1001);

        Inven_StatusChange(statusList);
    }

    private void Inven_StatusChange(List<int> statuslist)//������ ���� ����
    {
        speedStatus += statuslist[0];
        jumpStatus  = statuslist[1] == 0 ? 1 : statuslist[1];
        dashStatus  = statuslist[2] == 0 ? 1 : statuslist[2];

    }
    #endregion

    #region �κ��丮 ����
    private void Inven_Open()
    {

    }


    #endregion
}
