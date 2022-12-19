using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TrailRenderer trailRenderer;
    private Transform UI_Inventory;
    private Rigidbody2D rigid;

    private Inventory inventory;
    private CharacterCamera characterCamera;

    private Vector3 position;
    private Ray2D ray2D;
    private RaycastHit2D rayhit2D;

    private float speedStatus   = 10f;
    private int jumpStatus      = 40;
    private float dashStatus      = 50f;

    private int speedStatusLimit = 20;

    private int jumpCountLimit = 5;
    private int jumpCountMax = 2;
    private int jumpCount = 2;

    private int dashCountLimit = 3;
    private int dashCountMax = 0;
    private int dashCount = 0;
    private int dashvelocity = 1;

    private bool isdash = false;
    private bool candash = true;

    private int itemcode = -1;

    void Start()
    {
        Character_Init();
    }

    void Update()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        Inven_Open();
        Character_Jump();
        Character_Dash();
        characterCamera.Camera_MoveCoroutine(this.transform);

        Debug.Log("���� Ȯ�� Speed :" + speedStatus + " ���� : " + jumpCount + " ��� : " + dashCount);
    }

    void FixedUpdate()
    {
        Character_Move();
    }

    private void Character_Init()
    {
        characterCamera         = new CharacterCamera();

        UI_Inventory            = GameObject.Find("Inventory").transform;
        UI_Inventory.gameObject.SetActive(false);

        inventory               = UI_Inventory.GetComponent<Inventory>();

        rigid                   = this.GetComponent<Rigidbody2D>();
         
        gameManager             = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    #region ĳ���� ����
    private void Character_Move()//ĳ���� �̵�
    {
        //Move Speed
        float horizontal = Input.GetAxisRaw("Horizontal");
        horizontal       = Character_CheckWall(horizontal);

        //=====================��ù���
        if(horizontal != 0)
            dashvelocity = (int)horizontal;
        //=====================��ù���^^
        
        rigid.AddForce(Vector2.right * horizontal * speedStatus, ForceMode2D.Impulse);

        Character_SpeedLimit(isdash);


        ray2D = new Ray2D(new Vector2(this.transform.position.x, this.transform.position.y - 1.1f), Vector2.down);
        rayhit2D = Physics2D.BoxCast(ray2D.origin, new Vector2(0.5f, 0.3f), 0, Vector3.down, 0.3f, LayerMask.GetMask("Ground"));

        if (rayhit2D.collider != null)
            dashCount = dashCountMax;

        //jump Count
        if (rigid.velocity.y < 0)
        {
            if (jumpCount < jumpCountMax && rayhit2D.collider != null)
            {
                if (rayhit2D.distance < 0.5f)
                    jumpCount = jumpCountMax;
            }
        }
    }

    private void Character_SpeedLimit(bool limit)//��� �ӵ� ���� Ǯ��
    {
        if (limit == false)
        {
            if (rigid.velocity.x > speedStatus)
                rigid.velocity = new Vector2(speedStatus, rigid.velocity.y);
            else if (rigid.velocity.x < speedStatus * (-1))
                rigid.velocity = new Vector2(speedStatus * (-1), rigid.velocity.y);
        }
        else
        {
            if (rigid.velocity.x > dashStatus)
                rigid.velocity = new Vector2(dashStatus, rigid.velocity.y);
            else if (rigid.velocity.x < dashStatus * (-1))
                rigid.velocity = new Vector2(dashStatus * (-1), rigid.velocity.y);
        }
    }

    private float Character_CheckWall(float movedirection)//ĳ���� ���� ������ �̵� ����
    {
        float tempdirection = movedirection;
        if (movedirection == 0)
            return movedirection;

        Vector3 tempPoistion        = this.gameObject.transform.position;
        RaycastHit2D tempRayhit2D   = Physics2D.BoxCast(tempPoistion, new Vector2(0.3f, 1.5f), 0f, new Vector2(movedirection, 0), 1f, LayerMask.GetMask("Ground"));

        if (movedirection == -1 && tempRayhit2D.collider != null)
            tempdirection = 0;
        else if (movedirection == 1 && tempRayhit2D.collider != null)
            tempdirection = 0;

        return tempdirection;
    }

    private void Character_Jump()//ĳ���� ����
    {
        if (Input.GetButtonDown("Jump") && jumpCount != 0)
        {
            rigid.AddForce(Vector2.up * jumpStatus - rigid.velocity, ForceMode2D.Impulse);
            jumpCount -= 1;
        }
    }

    private void Character_Dash()//ĳ���� �뽬
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (candash == true && dashCount > 0)
            {
                StartCoroutine(Character_DashCoroutine());
                dashCount -= 1;
            }
        }
    }

    private IEnumerator Character_DashCoroutine() //����ڷ�ƾ
    {
        isdash = true;
        candash = false;
        float originalGravity = rigid.gravityScale;
        rigid.gravityScale = 0f;
        rigid.velocity = new Vector2(dashStatus * dashvelocity, 0f);
        trailRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        isdash = false;
        rigid.gravityScale = originalGravity;
        trailRenderer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        candash = true;
    }
    #endregion

    #region ĳ���� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)//ĳ���� ������ �Ա�
    {
        if (collision.gameObject.layer == 7)//Layername = Item
        {
            ItemObject tempitem = collision.GetComponent<ItemObject>();
            Sprite imageindex   = null;
            int index           = 0;

            index       = tempitem.GetItemValue();
            imageindex  = tempitem.GetItemImage();
            tempitem.ItemObjectActive(false);

            Debug.Log("�����ۿ� ���ִ� �� : " + index);
            gameManager.Item_MaxCountCheck();
            gameManager.Item_ScoreCheck(index);


            Inven_GetStatus(index, imageindex);
        }
    }

    private void Inven_GetStatus(int itemvalue, Sprite imageindex)//������ ���� ��������(������ �Ծ��� �� �۵�)
    {
        List<int> statusList    = new List<int>(3) { 0, 0, 0 };

        statusList              = inventory.Inven_GetItemData(itemvalue, imageindex);

        Inven_StatusChange(statusList);
    }

    private void Inven_StatusChange(List<int> statuslist)//�κ��丮 �����ͷ� ���� ����
    {
        if(speedStatus < speedStatusLimit)
            speedStatus += statuslist[0];

        if(jumpCountMax < jumpCountLimit)
            jumpCountMax   += statuslist[1] == 0 ? 0 : statuslist[1];

        if(dashCountMax < dashCountLimit)
            dashCountMax += statuslist[2] == 0 ? 0 : statuslist[2];

    }
    #endregion

    #region �κ��丮 ����
    private void Inven_Open()//�κ��丮 ����
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UI_Inventory.gameObject.activeInHierarchy == true)
                UI_Inventory.gameObject.SetActive(false);
            else if (UI_Inventory.gameObject.activeInHierarchy == false)
                UI_Inventory.gameObject.SetActive(true);
        }
    }
    #endregion
}
