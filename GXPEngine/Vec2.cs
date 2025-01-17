﻿using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	// TODO: Implement Length, Normalize, Normalized, SetXY methods (see Assignment 1)

	public float Length() {
        return Mathf.Sqrt(x * x + y * y);
    }

	// TODO: Implement subtract, scale operators

	public static Vec2 operator+ (Vec2 left, Vec2 right) {
		return new Vec2(left.x+right.x, left.y+right.y);
	}

	/*
	public static Vec2 operator(float _x,float _y)
	{
		return new Vec2(_x, _y);
	}
	*/

	public static Vec2 operator- (Vec2 left, Vec2 right) {
		return new Vec2(left.x - right.x, left.y - right.y);
	}
	
	public static Vec2 operator* (float multiplier, Vec2 vector) {
		return new Vec2(vector.x * multiplier, vector.y * multiplier);
    }

    public static Vec2 operator* (Vec2 vector, float multiplier)
    {
        return new Vec2(vector.x * multiplier, vector.y * multiplier);
    }

	public void SetXY(float _x, float _y) {
		x = _x;
		y = _y;
	}

	
	public void Normalize () {
		if (x == 0)
		{
			if (y != 0)
			{
                y /= Mathf.Abs(y);
            }
        }
		else if (y == 0)
		{
			x /= Mathf.Abs(x);
        }
		else
		{
            this.SetXY(x / Length(), y / Length());
        }
	}

	public Vec2 Normalized() {
        if (x == 0)
        {
            if (y != 0)
            {
				return new Vec2(0, y /= Mathf.Abs(y));
            }
			else
			{
				return new Vec2(0, 0);
			}
        }
        else if (y == 0)
        {
			return new Vec2(x / Mathf.Abs(x), 0);
        }
        else
        {
            return new Vec2(x / Length(), y / Length());
        }
    }

	

    public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
}

