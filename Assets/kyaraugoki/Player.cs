using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 0.07f; // キャラのスピード
    public static int maxLife = 3;// 最大HP
    public static int life ; // HP
    int keyDown = 0;// 押しているキーの数
    bool speedDown = false;// 斜めの速度低下

    private bool damage = false;// ダメージを受けた後の無敵判定
    float damageTime = 0.2f; // ダメージを受けたときの無敵判定の秒数
    private float damageTimeCount = 0f;// ダメージを受けた後の無敵判定終了のためのカウント

    public static bool heartDelete = false; // ダメージを受けたときにいったんハートを削除する
    public GameObject HeartSetter;
    bool heartResetOn = false;
    // public Heart heartscript;
     bool e = false;
     public GameObject heartOn;
    public GameObject heartOff;
    public float xPos;
    Vector2 HeartPosition;
    Quaternion HeartRotation;
    // Heart heartscript; // ハートを呼び出すスクリプトを取得する

    // public GameObject heart;
    
    // Start is called before the first frame update
    void Start()
    {
        HeartPosition = HeartSetter.transform.position;
        HeartRotation = HeartSetter.transform.rotation;
        Application.targetFrameRate = 60; // 一秒間のコマ数（これないとビルドしたときに多分挙動がおかしくなる）
        life = maxLife; //HPの設定

        // // ハートの設定
        // for(int i = 0; i < maxLife; i++){
        //     Instantiate(heart, new Vector2(i * ))
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // キャラ移動
        Vector2 position = transform.position;
        if(Input.GetKey(KeyCode.A)){ // 左
            position.x -= speed;
            keyDown++;
        }
        else if(Input.GetKey(KeyCode.D) ){ // 右
            position.x += speed;
            keyDown++;
        }
        

        if(Input.GetKey(KeyCode.W)){ // 上　
            position.y += speed;
            keyDown++;
        }
        else if(Input.GetKey(KeyCode.S)){ // 下
            position.y -= speed;
            keyDown++;
        }

        // キーを離した時の判定（気にしなくていいやつ）
        if(Input.GetKeyUp(KeyCode.A))keyDown--;
        if(Input.GetKeyUp(KeyCode.D))keyDown--;
        if(Input.GetKeyUp(KeyCode.W))keyDown--;
        if(Input.GetKeyUp(KeyCode.S))keyDown--;

        // 斜め移動時の速度の制御
        if(keyDown >= 2 && !speedDown){
            speed /= 1.4f;
            speedDown = true;
        }
        else if(keyDown <= 1 && speedDown){
            speed *= 1.4f;
            speedDown = false;
        }

        transform.position = position;

        // ダメージを受けた後の無敵判定
        if(damage){
            damageTimeCount += Time.deltaTime;
            if(damageTimeCount >= damageTime){
                damage = false;
            }
        }

        // if(heartResetOn){
        //     heartReSet();
        //     heartResetOn = false;
        // }
    }

    void OnTriggerEnter2D(Collider2D other){
        // 敵とぶつかったときのダメージ判定
        if(other.gameObject.CompareTag("Enemy"))
        {
            life--;
            damage = true;
            Debug.Log(life);
            damageTimeCount = 0;
            // heartDelete = true;
            // new WaitForSeconds(1f);
            // Heart.reSet = true;
            // heartReSet();
            // heartResetOn = true;
        }

        // 回復アイテムのプログラムの呼び出し
        else if(other.gameObject.CompareTag("Heal")){
            // other.Item();
            if(maxLife > life){// 現在のHPであるlifeが最大HPを超えないようにする
                life = maxLife;
                Debug.Log("回復した");
                // heartDelete = true;
                // Heart.reSet = true;
            }
        }
    }

    // 他のスクリプトにmaxLifeを渡す
    public static int getMaxLife(){
        return maxLife;
    }

    // 他のスクリプトにheartDeleteを渡す
    public static bool getHeartDelete(){
        return heartDelete;
    }

    // 他のスクリプトにlifeを渡す
    public static int getLife(){
        return life;
    }

    // void heartReSet(){
    //     // Destroy(heartOn);
    //     // Destroy(heartOff);
    //     yield return new WaitForSeconds(1f);
    //     Debug.Log("resetOn");
    //         xPos = HeartPosition.x;
    //     // nowLife = Player.getLife();
    //     for(int i = 0; i < life; i++){
    //         Instantiate(heartOn, new Vector2(xPos, HeartPosition.y),HeartRotation);
    //         Debug.Log("heartOn");
    //         xPos += 1.0f;
    //     }
    //     for(int i = life; i < maxLife; i++){
    //         Instantiate(heartOff, new Vector2(xPos, HeartPosition.y),HeartRotation);
    //         Debug.Log("heartOff");
    //         xPos += 1.0f;
    //     }
    // }
}
