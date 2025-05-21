extends CharacterBody2D

class_name Samurai

var hp : float

func get_hit(damage : float) :
	if (damage >= hp) :
		queue_free()
	else :
		hp = hp - damage
