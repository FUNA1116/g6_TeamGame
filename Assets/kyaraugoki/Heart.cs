using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    Player playerscript;
    Heart heartscript;
    bool delete = Player.getHeartDelete();
    public int number;
    public Sprite On;
    public Sprite Off;
    private Image image;
    bool statu = true;
    // Start is called before the first frame update
    void Start()
    {
        // spriteRenderer.sprite = On;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(number <= Player.life && !statu){
            image.sprite = On;
            statu = true;
        }

        if(number > Player.life && statu){
            image.sprite = Off;
            statu = false;
        }
    }

}
