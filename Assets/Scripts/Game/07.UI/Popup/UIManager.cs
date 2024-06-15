using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
   
    public GameObject dimmedPanel;

    private List<Popup> popupList = new List<Popup>();

    private Stack<Popup> popupStack = new Stack<Popup>();


    private void Start()
    {
        CreatePopup<MessagePopup>();
    }

    public void CreatePopup<T>() where T : Popup
    {
        Type t = typeof(T);

        var popup = popupList.Find(x => x.type.Equals(t));

        if (popup == null)
        {
            string path = $"{PathString.POPUP}/{typeof(T).Name}";

            var loadPopup = ResourceManager.Load<T>(AssetType.Prefab, path);

            popup = Instantiate(loadPopup, this.transform);

            popup.type = t;

            popupList.Add(popup);

            popup.gameObject.SetActive(false);

        }
    }

    public T OpenPopup<T>(UnityAction action) where T : Popup
    {
        Type t = typeof(T);

        var popup =  popupList.Find(x => x.type.Equals(t));


        if (popup == null)
        {
            string path = $"{PathString.POPUP}/{typeof(T).Name}";

           var loadPopup = ResourceManager.Load<T>(AssetType.Prefab, path);

           popup = Instantiate(loadPopup, this.transform);

           popup.type = t;
                     
           popupList.Add(popup);
        }


        popupStack.Push(popup);

        popup.Open(action);

        popup.transform.SetSiblingIndex(transform.childCount - 1);

        Dimmed();

        return (T)popup;        
    }





    public void ClosePopup(UnityAction action , Type type)
    {
        Type t = type;

        while (popupStack.Count > 0)
        {
           var p =  popupStack.Pop();

           p.Close(action);
           p.transform.SetSiblingIndex(0);

           if (p.type.Equals(t))
           {
                break;
           }
        }

        Dimmed();
    }






    public void Dimmed()
    {

        if (popupStack.Count > 0)
        {

            dimmedPanel.SetActive(true);

            dimmedPanel.transform.SetSiblingIndex(transform.childCount - 2);
     
        }
        else
        {
               
            dimmedPanel.SetActive(false);
        }
    }

    public void Dimmdlast()
    {
        dimmedPanel.transform.SetAsLastSibling();
    }

   
    
}
