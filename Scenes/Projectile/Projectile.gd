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
	target.despawn.connect(enemy_despawn)

func _physics_process(delta: float) -> void:
	if not initialized :
		return
	
	var direction = target.global_position - global_position
	rotation = direction.angle()
	velocity = direction.normalized() * speed
	global_position += velocity * delta
	
	if global_position.distance_to(target.global_position) < 3:
		target.get_hit(damage)
		queue_free()

func enemy_despawn() -> void:
	queue_free()
