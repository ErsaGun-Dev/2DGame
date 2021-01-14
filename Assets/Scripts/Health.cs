using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public enum attackableObjectType
    {
        Player, 
        Plant,
        Enemy
    }

    [Header("Plant Component")]
    SpriteRenderer spriteRenderer;

    [Header("Plant Values")]
    public int health;
    public bool isDead = false;
    public attackableObjectType objectType;

    [Header("Particles System")]
    public ParticleSystem LeafParticle;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            if(objectType == attackableObjectType.Plant)
            {
                this.gameObject.transform.DOShakePosition(0.15f, new Vector3(0.4f,0,0), 10, 90);
                LeafParticle.Play();
                Tween colorTween = spriteRenderer.DOBlendableColor(new Color(231f, 76f, 60f,0.75f), 0.1f);
                colorTween.OnComplete(() => spriteRenderer.DOBlendableColor(Color.white, 0.05f));
            }
            isDeadControl();
        }
    }


    public void isDeadControl()
    {
        if(health <= 0)
        {
            health = 0;
            isDead = true;
            Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, 0.1f);
            colorTween.OnComplete(() => this.gameObject.SetActive(false));
           
        }
    }

}
