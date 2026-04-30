using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class StartMenu : MonoBehaviour
{
    [SerializeField] Button hostBtn;
    [SerializeField] Button joinbtn;


    void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.singleton.StartHost();
        });
        joinbtn.onClick.AddListener(() =>
        {
            NetworkManager.singleton.StartClient(); 
        });
    }
    

}
