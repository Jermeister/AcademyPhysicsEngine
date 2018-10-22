# Mini Physics Engine with Buoyancy

Sample C# project for an internal Unity lecture in 2018, aimed at junior/future engineers.

This is a super-simple C# "game" + "physics engine" that Mantas Puida made to show how one might do simple 2D physics engine from scratch.
https://bitbucket.org/imantasp/miniphysicsengine/src/default/

My goal is to add Bouyancy force to extend capabilities of this simple "physics engine".

Update: goal has been reached successfully, buoyancy is up and running.
Link that I've used to read more about Buoyancy force:
https://www.khanacademy.org/science/physics/fluids/buoyant-force-and-archimedes-principle/a/buoyant-force-and-archimedes-principle-article

```charp
				if (pos.y - radius < waterBounds.yMax)
				{
					// F = q*V*g
					forceComponent.force += -gravity * fluidDensity * (float)areaUnderWater;
					entities.forceComponents[i] = forceComponent;
				}
```

To calculate partial area of a circle that's under water (I used area instead of a circle since this is a 2D project) I've used this:
https://www.mathopenref.com/segmentareaht.html

```csharp 
	public double VolumeUnderWater(float volume, float radius, float posY, float waterBoundsYMax)
	{
		float height;
		if (posY + radius < waterBoundsYMax) // Return full volume (area) if water is fully submerged
			return volume;
		height = radius - (posY - waterBoundsYMax); // calculate circle's height under water
		double area = radius * radius * Math.Acos((radius - height) / radius) - (radius - height) * Math.Pow(2f * radius * height - height * height, 0.5f);
		return area;
	}
```

Still cannot figure out why entities dive so deep, even though force wants to move it upwards.