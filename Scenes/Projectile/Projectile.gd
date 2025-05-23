extends CharacterBody2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")

var damage : float
var speed : float
var target : Enemy
var initialized = false

func initialize(damage : float, speed : float, target : Enemy, pos : Vector2) -> void:
	self.damage = damage
	self.speed = speed
	self.target = target
	self.global_position = pos
	self.initialized = true

func _physics_process(delta: float) -> void:
	if not initialized :
		return
	
	if not is_instance_valid(target):
		queue_free()
		return
	
	var direction = target.global_position - global_position
	velocity = direction.normalized() * speed
	global_position += velocity * delta
	
	if global_position.distance_to(target.global_position) < 3:
		target.get_hit(damage)
		queue_free()
