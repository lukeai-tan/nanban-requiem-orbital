extends Area2D

class_name AOE

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var AOEDamage : float

func initialize(damage : float, pos : Vector2) :
	AOEDamage = damage
	global_position = pos

func _ready() -> void:
	await get_tree().physics_frame
	var bodies = get_overlapping_bodies()
	for body in bodies:
		if body is Enemy: 
			body.get_hit(AOEDamage)
	queue_free()
			
