using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusBullet : EnemyBullet
{
    [SerializeField] private float frequency = 10f;//frekans
    [SerializeField] private float amplitude = 0.5f;//genlik

    private float time;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void FixedUpdate()
	{
        time += Time.fixedDeltaTime;

        Vector2 forwardMove = transform.up * speed;

        float waveOffset = Mathf.Cos(time * frequency) * amplitude;

        Vector2 waveMove = transform.right * waveOffset;

        rb.velocity = forwardMove + waveMove;
	}
}
