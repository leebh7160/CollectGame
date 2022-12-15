using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    //=========================================�κ����� ����
    private Transform canvas;             //UI�� �ҼӵǾ� �ִ� �ֻ�� Canvas
    private Transform previousParent;     //�ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transform
    private RectTransform rect;               //UI�� ��ġ ��� ���� RectTransform
    private CanvasGroup canvasGroup;        //UI�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup
                                            //=========================================�κ����� ����^^

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }



    //https://www.youtube.com/watch?v=uTeZz4O12yU ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;//�巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����

        //���� �巡�� ���� UI�� ȭ���� �ֻ�ܿ� ��µ� �� �ֵ��� �ϱ�����
        transform.SetParent(canvas);        //�θ� ������Ʈ�� Canvas�� ����
        transform.SetAsLastSibling();       //���� �տ� ���̵��� ������ �ڽ����� ����

        //�巡�� ������ ������Ʈ �ϳ��� �ƴ� �ڽĵ��� ������ �� �� �������� �ֱ� ������ CanvasGroup���� ����
        //���İ��� 0.6���� �����ϰ�, ���� �浹ó���� ���� �ʵ��� �Ѵ�.
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //���� ��ũ������ ���콺 ��ġ�� Ui��ġ�� ����(UI�� ���콺�� �i�ƴٴϴ� ����)
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�巡�׸� �����ϸ� �θ� canvas�� �����Ǳ� ������
        //�巡�׸� ������ �� �θ� canvas�̸� ������ ������ �ƴ� ������ ����
        //����� �ߴٴ� ���̱� ������ �巡�� ������ �ҼӵǾ� �ִ� ������ �������� ������ �̵�
        if (transform.parent == canvas)
        {
            //�������� �ҼӵǾ� �־��� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }

        //���İ��� 1�� �����ϰ� ���� �浹ó��
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
