
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct Entity
{
	public int id;

	public Entity(int id)
	{
		this.id = id;
	}
}

[Flags]
public enum EntityFlags
{
	kFlagPosition = 1<<0,
	kFlagMove = 1<<1,
	kFlagWorldBounds = 1<<2,
	kFlagBuoyancy = 1 << 3,
	kFlagGravity = 1<<4,
	kFlagForce = 1<<5,
	kFlagCollision = 1<<6,
}

public struct MoveComponent
{
	public Vector2 velocity;
	public Vector2 acceleration;
}

public struct ForceComponent
{
	public float massInverse;
	public Vector2 force;
}

public struct CollisionComponent
{
	public float radius;
	public float coeffOfRestitution;
}

public struct BuoyancyComponent
{
	public float volume;
}


public class Entities
{
	public List<Vector2> positions = new List<Vector2>();
	public List<EntityFlags> flags = new List<EntityFlags>();
	public List<BuoyancyComponent> buoyancyComponents = new List<BuoyancyComponent>();
	public List<ForceComponent> forceComponents = new List<ForceComponent>();
	public List<MoveComponent> moveComponents = new List<MoveComponent>();
	public List<CollisionComponent> collisionComponents = new List<CollisionComponent>();

	public void AddComponent(Entity entity, EntityFlags flag)
	{
		flags[entity.id] |= flag;
	}

	public Entity AddEntity(Vector2 position)
	{
		// We assume that all entities have at least position component
		positions.Add(position);
		flags.Add(EntityFlags.kFlagPosition);
		
		// reserve space for all other components
		buoyancyComponents.Add(new BuoyancyComponent());
		forceComponents.Add(new ForceComponent());
		moveComponents.Add(new MoveComponent());
		collisionComponents.Add(new CollisionComponent());
		
		return new Entity(positions.Count - 1);
	}
	
	public Entity AddEntity()
	{
		return AddEntity(Vector2.zero);
	}

	public void Init(int count = 10)
	{
		for (var i = 0; i < count; i++)
		{
			AddEntity(new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(0f, 0.1f)));
		}
	}

}
