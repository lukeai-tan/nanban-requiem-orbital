extends CharacterBody2D

var damage : float
var speed : float
var target : Node2D = null
var initialized = false

func initialize(damage : float, speed : float, target : Node2D, pos : Vector2) -> void:
	self.damage = damage
	self.speed = speed
	self.global_position = pos
	if target.has_signal("despawn") and target.has_method("get_hit") :
		self.target = target
		target.despawn.connect(enemy_despawn)
	self.initialized = true
	
func _physics_process(delta: float) -> void:
	if not initialized :
		return
	
	var direction = target.global_position - global_position
	rotation = direction.angle()
	velocity = direction.normalized() * speed
	global_position += velocity * delta
	
	if global_position.distance_to(target.global_position) < 3:
		hit_target()

func hit_target() :
	target.get_hit(damage)
	queue_free()

func enemy_despawn() -> void:
	queue_free()
