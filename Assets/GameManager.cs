using MobileFPS.PlayerHealth;
using MobileFPS.PlayerUI;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text            _ammo_Text;
    [SerializeField] Text            _health_Text;
    [SerializeField] Text            _FPS;
    [SerializeField] GameObject      MainCamera;
    [SerializeField] PlayerUIManager _playerUIManager;
    public           GameObject      _canvas;
    public           GameObject      _deadUI, _aliveUI;
    public           GameObject      Camera => MainCamera;
    public           Text            Ammo   => _ammo_Text;
    public           Text            Health => _health_Text;
    public           bool            PCmode; 

    void Awake()
    {
        _deadUI.SetActive( false );
        _aliveUI.SetActive( false );
        Application.targetFrameRate = 144;
    }
    void Update()
    {
        float fps = 1 / Time.deltaTime;
        _FPS.text = fps.ToString();
    }
}
