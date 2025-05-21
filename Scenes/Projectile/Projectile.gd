extends CharacterBody2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var damage : float
var speed : float
var target : Enemy

func _ready() -> void:
	var curr_position = global_position
	if (!target.is_instance_valid()) :
		queue_free()
	while (curr_position != target.global_position) :
		var direction = target.global_position - curr_position
		velocity = direction.normalized() * speed
	target.get_hit(damage)
	queue_free()
