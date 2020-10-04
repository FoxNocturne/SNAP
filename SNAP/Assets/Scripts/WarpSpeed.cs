using UnityEngine;
using System.Collections;

public class WarpSpeed : MonoBehaviour {
	public float WarpDistortion;
	public float Speed;
	ParticleSystem particles;
	ParticleSystemRenderer rend;
	bool isWarping;
    public float SizeTransition;
    public Transform Transition;
    public SpriteRenderer Mask;
    public ParticleSystemRenderer Mask2;


    void Awake()
	{
		particles = GetComponent<ParticleSystem>();
		rend = particles.GetComponent<ParticleSystemRenderer>();
	}

	void Update()
	{
		if(isWarping && !atWarpSpeed())
		{
			rend.velocityScale += WarpDistortion * (Time.deltaTime * Speed);
		}

		if(!isWarping && !atNormalSpeed())
		{
			rend.velocityScale -= WarpDistortion * (Time.deltaTime * Speed);
		}

        Transition.localScale = new Vector2(SizeTransition, SizeTransition);
        
	}

	public void Engage()
	{
		isWarping = true;
	}

	public void Disengage()
	{
		isWarping = false;
	}

	bool atWarpSpeed()
	{
		return rend.velocityScale < WarpDistortion;
	}

	bool atNormalSpeed()
	{
		return rend.velocityScale > 0;
	}
}
