extends CharacterBody2D

class_name RocketSamurai

var hp : float = 300

signal despawn()

func get_hit(damage : float) :
	if damage >= hp :
		despawn.emit()
		queue_free()
	else :
		hp = hp - damage
