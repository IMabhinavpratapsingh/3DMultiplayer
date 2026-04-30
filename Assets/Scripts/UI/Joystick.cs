
using UnityEngine;
using UnityEngine.EventSystems;


public class Joystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    public static Joystick instance;
    [SerializeField] RectTransform border;
    [SerializeField] RectTransform Knob;

    public Vector2 output;
    float radius;
    
    void Start()
    {
        instance = this;
        radius = border.sizeDelta.x/2;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
          OnDrag(eventData);  
    }
    public void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
           
            border,
            data.position,
            data.pressEventCamera,
            out pos
        ); 
        
        pos = Vector2.ClampMagnitude(pos,radius);

        Knob.anchoredPosition = pos;
       
        output = pos / radius ;
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Knob.anchoredPosition = Vector2.zero;
        output = Vector2.zero;
    }

    public Vector2 Output()
    {
        return output;
    }
    
}
