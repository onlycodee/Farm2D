using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Animal : ResetableEntity, IReset
{
    [SerializeField] float timeToTakeCrop = 2.0f;
    Crop targetCrop = null;
    float takeCropTimer = 0f;
    bool hasTakenCrop = false;
    Vector3 spawnPosition;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetCrop != null && !hasTakenCrop)
        {
            if (transform.position.x > targetCrop.transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            if (Vector3.Distance(transform.position, targetCrop.transform.position) > .2f)
            {
                if (targetCrop.transform.parent == null)
                {
                    transform.DOMove(targetCrop.transform.position, UnityEngine.Random.Range(3.0f, 4.0f));
                } else
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    transform.DOMove(spawnPosition, 2.0f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                    hasTakenCrop = true;
                }
            } else
            {
                takeCropTimer++;
                if (takeCropTimer >= timeToTakeCrop)
                {
                    targetCrop.transform.SetParent(transform);
                    targetCrop.GetDirt().CropDone();
                    targetCrop.SetOnDirt(false);
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    transform.DOMove(spawnPosition, 2.0f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                    hasTakenCrop = true;
                }
            }
        } 
    }

    public void SetSpawnedPosition(Vector3 position)
    {
        spawnPosition = position;
    }

    public void SetTarget(Crop target)
    {
        targetCrop = target;
        
    }

    public void Die()
    {
        print("Die");
        if (hasTakenCrop)
        {
            targetCrop.transform.SetParent(null);
        }
        Destroy(gameObject);
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
