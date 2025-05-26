extends CharacterBody2D

class_name Samurai

var hp : float = 300
@export var movement_speed: float = 100.0
var last_position: Vector2

signal despawn()

### Added this
func _ready():
	position.y -= 25
	scale = Vector2(2, 2)
	var enemy_sprite = get_node("AnimatedSprite2D")
	if enemy_sprite:
		enemy_sprite.play("running")

func _process(delta):
	var path = get_parent()
	if path is PathFollow2D:
		path.progress += movement_speed * delta
###


func get_hit(damage : float) :
	if damage >= hp :
		despawn.emit()
		queue_free()
	else :
		hp = hp - damage
