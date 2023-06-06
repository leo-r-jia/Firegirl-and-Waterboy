using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
	//[SerializeField] private Material material;
	Material material;
	private bool isDissolving = false;
	private bool isAppearing = false;
	private float dissolveSpeed = 1f;
	private float fade = 1f;

	void Start()
    {
		material = GetComponent<SpriteRenderer>().material;
	}

	//Update based on isAppearing or isDissolving boolean values
	void Update()
	{
		//Dissolve
		if (isDissolving)
		{
			fade -= Time.deltaTime * dissolveSpeed;

			if (fade <= 0f)
			{
				fade = 0f;
				isDissolving = false;
			}

			// Set the property
			material.SetFloat("_Fade", fade);
		}
		//Appear
        if (isAppearing)
        {
			fade += Time.deltaTime * dissolveSpeed;

			if (fade >= 1f)
			{
				fade = 1f;
				isAppearing = false;
			}

			// Set the property
			material.SetFloat("_Fade", fade);
		}
	}

	//Trigger sprite dissolve
	public void Dissolve(float dissolveSpeed, Color dissolveColor)
	{
		material.SetColor("_Color", dissolveColor);
		this.dissolveSpeed = dissolveSpeed;
		isDissolving = true;
	}

	//Trigger sprite reappear
	public void Appear(float dissolveSpeed, Color dissolveColor)
	{
		material.SetColor("_Color", dissolveColor);
		this.dissolveSpeed = dissolveSpeed;
		isAppearing = true;
	}

	//Return if sprite is dissolving
	public bool getIsDissolving()
    {
		return this.isDissolving;
    }
}