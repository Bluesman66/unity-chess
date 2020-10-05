using UnityEngine;

public class Board : MonoBehaviour
{
    DragAndDrop dad = new DragAndDrop();

    // Update is called once per frame
    void Update()
    {
        dad.Action();
    }
}

public class DragAndDrop
{
    State state;
    GameObject item;
    Vector2 offset;

    public DragAndDrop()
    {
        Drop();
    }

    enum State
    {
        none,         
        drag
    }

    public void Action()
    {
        Debug.Log(state);

        switch (state)
        {
            case State.none:
                if (IsMouseButtonPressed)                
                    PickUp();                
                break;
            case State.drag:
                if (IsMouseButtonPressed)                
                    Drag();                
                else                
                    Drop();                
                break;
            default:
                break;
        }
    }

    bool IsMouseButtonPressed => Input.GetMouseButton(0);

    Vector2 ClickPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    Transform GetItemAt(Vector2 position)
    {
        RaycastHit2D[] figures = Physics2D.RaycastAll(position, position, 0.5f);
        return figures.Length == 0 ? null : figures[0].transform;
    }

    void PickUp()
    {        
        Transform clickedItem = GetItemAt(ClickPosition);
        if (clickedItem == null)
            return;        
        state = State.drag;
        item = clickedItem.gameObject;
        offset = (Vector2)clickedItem.position - ClickPosition;
        Debug.Log(offset);
    }

    void Drag()
    {
        Debug.Log(offset);
        item.transform.position = ClickPosition + offset;
    }

    void Drop()
    {
        item = null;
        state = State.none;        
    }    
}
