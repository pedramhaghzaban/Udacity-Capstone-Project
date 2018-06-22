var impact : AudioClip;

function OnCollisionEnter (hit : Collision)

{
	if (hit.relativeVelocity.magnitude >= 5)
	{
		GetComponent.<AudioSource>().PlayOneShot (impact);
	}
}

function OnCollisionStay (hit : Collision)

{
	if (hit.relativeVelocity.magnitude >= 3.5)
	{
		GetComponent.<AudioSource>().PlayOneShot (impact);
	}
}