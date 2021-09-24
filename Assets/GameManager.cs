using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject _canvas;
    [SerializeField] Text _ammo_Text;
    [SerializeField] Text _health_Text;
    [SerializeField] Text _FPS;
    [SerializeField] GameObject MainCamera;
    public Text Ammo() => _ammo_Text;
    public Text Health() => _health_Text;
    public GameObject Camera() => MainCamera;
    public bool PC = true;
    void Update()
    {
        var fps = 1 / Time.deltaTime;
        _FPS.text = fps.ToString();
    }
}
