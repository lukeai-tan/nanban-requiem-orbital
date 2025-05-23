extends CharacterBody2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var damage : float
var speed : float
var target : Enemy

func initialize(damage : float, speed : float, target : Enemy, pos : Vector2) -> void:
	damage = damage
	speed = speed
	target = target
	global_position = pos

func _physics_process(_delta: float) -> void:
	if not is_instance_valid(target):
		queue_free()
		return
		
	var direction = target.global_position - global_position
	velocity = direction.normalized() * speed
	move_and_slide()
	
	if global_position.distance_to(target.global_position) <= 10.0:
		if "get_hit" in target:
			target.get_hit(damage)
		queue_free()
