extends CharacterBody2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var damage : float
var speed : float = 100.0
var target : Enemy

func _ready() -> void:
	if target == null or not target.is_instance_valid() :
		queue_free()

func _physics_process(_delta: float) -> void:
	if target == null or not target.is_instance_valid() :
		queue_free()
		return

	var direction = target.global_position - global_position
	velocity = direction.normalized() * speed
	move_and_slide()
	
	if global_position.distance_to(target.global_position) <= 10.0:
		if "get_hit" in target:
			target.get_hit(damage)
		queue_free()
