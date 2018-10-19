using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuoyancySystem : ISystemInterface 
{
    public void Start(World world)
    {
        var entities = world.entities;
		for (var i = 0; i < entities.flags.Count; i++)
		{
			if (entities.flags[i].HasFlag(EntityFlags.kFlagPosition))
			{
				entities.AddComponent(new Entity(i), EntityFlags.kFlagBuoyancy);
			}
		}
		Debug.Log("Buoyancy started");
        
    }

    public void Update(World world, float time = 0, float deltaTime = 0)
    {
        var entities = world.entities;
        var gravity = world.gravity;
		var fluidDensity = world.fluidDensity;
		var waterBounds = world.waterBounds;
	
		for (var i = 0; i < entities.flags.Count; i++)
		{

			if (entities.flags[i].HasFlag(EntityFlags.kFlagGravity) &&
                entities.flags[i].HasFlag(EntityFlags.kFlagForce) &&
				entities.flags[i].HasFlag(EntityFlags.kFlagBuoyancy))
            {
				var pos = entities.positions[i];
				var moveComponent = entities.moveComponents[i];
				var forceComponent = entities.forceComponents[i];
				var entityVolume = entities.buoyancyComponents[i].volume;
				var underWaterVolume = 0.0;

				var radius = 0f;
				if (pos.y - radius < waterBounds.yMax)
				{
					// F = q*V*g
					underWaterVolume = VolumeUnderWater(entityVolume, entities.collisionComponents[i].radius, pos.y, waterBounds.yMax);
					forceComponent.force += -gravity * fluidDensity * (float)underWaterVolume;
					entities.forceComponents[i] = forceComponent;
				}

            }
        }
    }

	public double VolumeUnderWater(float volume, float radius, float posY, float waterBoundsYMax)
	{
		float height;
		if (posY + radius < waterBoundsYMax)
			return volume;
		height = radius - (posY - waterBoundsYMax);
		// https://www.mathopenref.com/segmentareaht.html
		double area = radius * radius * Math.Acos((radius - height) / radius) - (radius - height) * Math.Pow(2f * radius * height - height * height, 0.5f);
		return area;
	}
}
