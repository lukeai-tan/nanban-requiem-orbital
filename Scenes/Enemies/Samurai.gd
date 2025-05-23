extends CharacterBody2D

class_name Samurai

var hp : float = 100

signal despawn()

func get_hit(damage : float) :
	if damage >= hp :
		despawn.emit()
		queue_free()
	else :
		hp = hp - damage
