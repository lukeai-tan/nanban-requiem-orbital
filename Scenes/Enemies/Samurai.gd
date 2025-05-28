extends CharacterBody2D

class_name Samurai

const HealthBar = preload("res://Scenes/HealthBar/HealthBar.tscn")
var health_bar

var hp : float = 300
@export var movement_speed: float = 100.0
var last_position: Vector2

signal despawn()

func _ready():
	position.y -= 25
	scale = Vector2(2, 2)
	var enemy_sprite = get_node("AnimatedSprite2D")
	if enemy_sprite:
		enemy_sprite.play("running")
	
	## Health Bar
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp


func _process(delta):
	var path = get_parent()
	if path is PathFollow2D:
		path.progress += movement_speed * delta
		
func _get_progress() -> float:
	return get_parent().progress

func get_hit(damage : float) :
	if damage >= hp :
		despawn.emit()
		get_parent().queue_free()
	else :
		hp = hp - damage
		health_bar.value = hp
